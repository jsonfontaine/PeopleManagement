#!/bin/sh
set -e

DB_FILE="/app/App_Data/peoplemanagement.db"
SEED_FILE="/app/seed/peoplemanagement.db"

# Se o banco ainda não existe no volume, inicializa a partir do seed
if [ ! -f "$DB_FILE" ]; then
    echo "[entrypoint] Banco de dados não encontrado. Inicializando a partir do seed..."
    mkdir -p /app/App_Data
    cp "$SEED_FILE" "$DB_FILE"
    echo "[entrypoint] Banco inicializado com sucesso."
else
    echo "[entrypoint] Banco de dados existente encontrado. Usando dados do volume."
fi

exec dotnet PeopleManagement.Api.dll

