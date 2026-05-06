import os
import time
from pathlib import Path
import psycopg

MIGRATIONS_DIR = Path(__file__).parent / "migrations"

# =========================
# ENV CONFIG
# =========================

DB_HOST = os.getenv("POSTGRES_HOST", "localhost")
DB_PORT = os.getenv("POSTGRES_PORT", "5453")
DB_NAME = os.getenv("POSTGRES_DB", "tt_plan")
DB_USER = os.getenv("POSTGRES_USER", "tauerntec")
DB_PASSWORD = os.getenv("POSTGRES_PASSWORD", "")

MAX_RETRIES = 10
RETRY_DELAY = 2


# =========================
# CONNECTION
# =========================

def wait_for_db():
    print(f"Waiting for database...")
    for i in range(MAX_RETRIES):
        try:
            with psycopg.connect(
                host=DB_HOST,
                port=DB_PORT,
                dbname=DB_NAME,
                user=DB_USER,
                password=DB_PASSWORD,
            ) as conn:
                print("Database is ready.")
                return
        except Exception as e:
            print(f"DB not ready yet ({i+1}/{MAX_RETRIES})...")
            time.sleep(RETRY_DELAY)

    raise Exception("Database not reachable after retries")


def get_connection():
    return psycopg.connect(
        host=DB_HOST,
        port=DB_PORT,
        dbname=DB_NAME,
        user=DB_USER,
        password=DB_PASSWORD,
        autocommit=False,
    )


# =========================
# MIGRATION LOGIC
# =========================

def ensure_migrations_table(conn):
    with conn.cursor() as cur:
        cur.execute("""
            CREATE TABLE IF NOT EXISTS schema_migrations (
                version TEXT PRIMARY KEY,
                applied_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
            );
        """)
    conn.commit()


def get_applied_versions(conn):
    with conn.cursor() as cur:
        cur.execute("SELECT version FROM schema_migrations;")
        return {row[0] for row in cur.fetchall()}


def apply_migration(conn, migration_file: Path):
    print(f"Applying migration: {migration_file.name}")

    sql = migration_file.read_text(encoding="utf-8")

    try:
        with conn.cursor() as cur:
            cur.execute(sql)
            cur.execute(
                "INSERT INTO schema_migrations (version) VALUES (%s);",
                (migration_file.name,)
            )
        conn.commit()
        print(f"Applied: {migration_file.name}")

    except Exception as e:
        conn.rollback()
        print(f"Failed migration: {migration_file.name}")
        raise e


def run_migrations():
    migration_files = sorted(MIGRATIONS_DIR.glob("*.sql"))

    if not migration_files:
        print("No migration files found.")
        return

    with get_connection() as conn:
        ensure_migrations_table(conn)
        applied_versions = get_applied_versions(conn)

        for migration_file in migration_files:
            if migration_file.name in applied_versions:
                print(f"Skipping: {migration_file.name}")
                continue

            apply_migration(conn, migration_file)

    print("All migrations complete.")


# =========================
# MAIN
# =========================

def main():
    print("Starting migration process...")

    wait_for_db()
    run_migrations()

    print("Migration process finished.")


if __name__ == "__main__":
    main()