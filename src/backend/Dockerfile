# ── Build stage ────────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Restore dependencies first (layer-cache friendly)
COPY PeopleManagement.Api/PeopleManagement.Api.csproj                   PeopleManagement.Api/
COPY PeopleManagement.Application/PeopleManagement.Application.csproj   PeopleManagement.Application/
COPY PeopleManagement.Infrastructure/PeopleManagement.Infrastructure.csproj PeopleManagement.Infrastructure/

RUN dotnet restore PeopleManagement.Api/PeopleManagement.Api.csproj

# Copy the rest and publish
COPY . .
RUN dotnet publish PeopleManagement.Api/PeopleManagement.Api.csproj \
      -c Release -o /app/publish --no-restore

# ── Runtime stage ───────────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Volume mountpoint para dados persistentes (criado vazio; entrypoint faz o seed)
RUN mkdir -p App_Data

COPY --from=build /app/publish .

# Seed: banco com dados produtivos – copiado para o volume apenas na 1ª execução
RUN mkdir -p /app/seed
COPY --from=build /src/PeopleManagement.Api/App_Data/peoplemanagement.db /app/seed/peoplemanagement.db

# Entrypoint script que inicializa o volume na 1ª execução
COPY entrypoint.sh /app/entrypoint.sh
RUN chmod +x /app/entrypoint.sh

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ConnectionStrings__DefaultConnection="Data Source=App_Data/peoplemanagement.db"

EXPOSE 8080

ENTRYPOINT ["/app/entrypoint.sh"]
