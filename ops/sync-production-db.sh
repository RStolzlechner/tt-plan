#!/usr/bin/env bash
set -euo pipefail

# ========= CONFIG =========
REMOTE_SSH_HOST="test.tauerntec.at"
REMOTE_SSH_USER="tauerntec"

REMOTE_DB_HOST="127.0.0.1"
REMOTE_DB_PORT="5433"
REMOTE_DB_NAME="tt_test"
REMOTE_DB_USER="tauerntec"

LOCAL_DB_HOST="127.0.0.1"
LOCAL_DB_PORT="5453"
LOCAL_DB_NAME="tt_test"
LOCAL_DB_USER="tauerntec"
LOCAL_DB_PASSWORD="my-super-secure-pw"

DUMP_FILE="/tmp/tt_plan_prod_dump.sql"
# ==========================

echo "==> Dumping remote database..."
ssh "${REMOTE_SSH_USER}@${REMOTE_SSH_HOST}" \
  "PGPASSWORD=\$POSTGRES_PASSWORD pg_dump \
    -h ${REMOTE_DB_HOST} \
    -p ${REMOTE_DB_PORT} \
    -U ${REMOTE_DB_USER} \
    -d ${REMOTE_DB_NAME} \
    --clean \
    --if-exists \
    --no-owner \
    --no-privileges" > "${DUMP_FILE}"

echo "==> Terminating local connections..."
export PGPASSWORD="${LOCAL_DB_PASSWORD}"

psql -h "${LOCAL_DB_HOST}" -p "${LOCAL_DB_PORT}" -U "${LOCAL_DB_USER}" -d postgres <<SQL
SELECT pg_terminate_backend(pid)
FROM pg_stat_activity
WHERE datname = '${LOCAL_DB_NAME}'
  AND pid <> pg_backend_pid();
SQL

echo "==> Recreating local database..."
psql -h "${LOCAL_DB_HOST}" -p "${LOCAL_DB_PORT}" -U "${LOCAL_DB_USER}" -d postgres \
  -c "DROP DATABASE IF EXISTS ${LOCAL_DB_NAME};"

psql -h "${LOCAL_DB_HOST}" -p "${LOCAL_DB_PORT}" -U "${LOCAL_DB_USER}" -d postgres \
  -c "CREATE DATABASE ${LOCAL_DB_NAME};"

echo "==> Importing dump into local database..."
psql -h "${LOCAL_DB_HOST}" -p "${LOCAL_DB_PORT}" -U "${LOCAL_DB_USER}" -d "${LOCAL_DB_NAME}" \
  -f "${DUMP_FILE}"

echo "==> Cleanup..."
rm -f "${DUMP_FILE}"

echo "Done."