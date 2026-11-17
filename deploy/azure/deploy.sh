#!/usr/bin/env bash
set -euo pipefail

: "${AZURE_RESOURCE_GROUP:?Set AZURE_RESOURCE_GROUP}"
: "${AZURE_LOCATION:?Set AZURE_LOCATION}"
: "${AZURE_REGISTRY:?Set AZURE_REGISTRY without .azurecr.io}"
: "${AZURE_APP_NAME:?Set AZURE_APP_NAME}"

az group create --name "$AZURE_RESOURCE_GROUP" --location "$AZURE_LOCATION"
az acr create --resource-group "$AZURE_RESOURCE_GROUP" --name "$AZURE_REGISTRY" --sku Basic
az acr login --name "$AZURE_REGISTRY"
docker build -t "$AZURE_REGISTRY.azurecr.io/se-lab-web:latest" ../..
docker push "$AZURE_REGISTRY.azurecr.io/se-lab-web:latest"
az appservice plan create --name "$AZURE_APP_NAME-plan" --resource-group "$AZURE_RESOURCE_GROUP" --is-linux --sku B1
az webapp create --resource-group "$AZURE_RESOURCE_GROUP" --plan "$AZURE_APP_NAME-plan" --name "$AZURE_APP_NAME" --deployment-container-image-name "$AZURE_REGISTRY.azurecr.io/se-lab-web:latest"
