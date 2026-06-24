#!/usr/bin/env bash
set -euo pipefail

# This script will configure your BTCPay Server developer environement to load the plugin during a debug session.

source plugin-env.sh

TARGET_PATH="$(dotnet build "src/$PROJECT/$PROJECT.csproj" -p:Configuration=Debug -getProperty:TargetPath)"

printf '{ "DEBUG_PLUGINS": "%s" }' "$TARGET_PATH" > "submodules/btcpayserver/BTCPayServer/appsettings.dev.json"

echo "The plugin will now start when debugging BTCPay Server"