#!/bin/bash
set -e

echo "Deployment started..."

cd /opt/tt-plan/ops

echo "Loading environment..."
set -a
source .env
set +a


echo "Starting postgres..."
docker compose --env-file .env -f docker-compose.prod.yml up -d postgres

echo "Running migrations..."
.venv/bin/python migrate.py

echo "Starting application containers..."
docker compose --env-file .env -f docker-compose.prod.yml up -d --remove-orphans

echo "Cleaning up old images..."
docker image prune -f

echo "Successfully deployed!"