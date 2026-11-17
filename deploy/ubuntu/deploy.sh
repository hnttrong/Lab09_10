#!/usr/bin/env bash
set -euo pipefail

IMAGE="${1:-YOUR_DOCKERHUB_USERNAME/se-lab-web:latest}"
docker pull "$IMAGE"
docker rm -f se-lab-web 2>/dev/null || true
docker run -d --name se-lab-web --restart unless-stopped -p 8080:8080 "$IMAGE"
docker ps --filter name=se-lab-web
