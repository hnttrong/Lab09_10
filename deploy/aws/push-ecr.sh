#!/usr/bin/env bash
set -euo pipefail

: "${AWS_ACCOUNT_ID:?Set AWS_ACCOUNT_ID}"
: "${AWS_REGION:?Set AWS_REGION}"

REPOSITORY="se-lab-web"
REGISTRY="$AWS_ACCOUNT_ID.dkr.ecr.$AWS_REGION.amazonaws.com"
aws ecr describe-repositories --repository-names "$REPOSITORY" >/dev/null 2>&1 || aws ecr create-repository --repository-name "$REPOSITORY"
aws ecr get-login-password --region "$AWS_REGION" | docker login --username AWS --password-stdin "$REGISTRY"
docker build -t "$REPOSITORY:latest" ../..
docker tag "$REPOSITORY:latest" "$REGISTRY/$REPOSITORY:latest"
docker push "$REGISTRY/$REPOSITORY:latest"
