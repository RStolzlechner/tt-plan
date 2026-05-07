#!/bin/bash
set -e

cd /opt/tt-plan/ops

set -a
source .env
set +a

RETENTION_DAYS_LOCAL=7
RETENTION_DAYS_REMOTE=30
BACKUP_DIR="/opt/tt-plan/backups"
TIMESTAMP=$(date +%Y%m%d_%H%M%S)
FILE="$BACKUP_DIR/db_$TIMESTAMP.sql"

echo "Creating backup..."

PGPASSWORD="$POSTGRES_PASSWORD" pg_dump \
  -h "$POSTGRES_HOST" \
  -p "$POSTGRES_PORT" \
  -U "$POSTGRES_USER" \
  "$POSTGRES_DB" > "$FILE"

echo "Uploading to object storage..."

mc cp "$FILE" "hetzner/$AWS_BUCKET/database-backups/"

  # 4. Clean
find $BACKUP_DIR -name "*.sql" -type f -mtime +$RETENTION_DAYS_LOCAL -delete
mc rm --recursive --force --older-than ${RETENTION_DAYS_REMOTE}d hetzner/$AWS_BUCKET/database-backups/

echo "Backup complete"