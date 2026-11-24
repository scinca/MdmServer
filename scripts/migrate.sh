#!/usr/bin/env bash

set -e

NAME=$1

PROJECT=${2:-"Migrations"}
STARTUP_PROJECT=${3:-"Api"}

if [ -z "$NAME" ]; then
    echo "Error: Migration name is required"
    echo "Usage: ./migrate.sh <migration-name> [PROJECT] [STARTUP_PROJECT]"
    exit 1
fi

echo "Adding ef migration '$NAME' ..."
dotnet ef migrations add "$NAME" --project "$PROJECT" --startup-project "$STARTUP_PROJECT"
