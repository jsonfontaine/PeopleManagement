# PeopleManagement Backend

Backend em .NET 8 com organizacao inicial em Vertical Slice, DDD tatico e principios de arquitetura hexagonal.

## Estrutura atual

- `PeopleManagement.Domain`: entidades e regras de dominio.
- `PeopleManagement.Application`: casos de uso por feature (`Features/...`).
- `PeopleManagement.Infrastructure`: adapters de infraestrutura (repositorio em memoria no MVP).
- `PeopleManagement.Api`: Minimal API com endpoints e Swagger.
- `PeopleManagement.Tests`: testes unitarios com xUnit, Moq e FluentAssertions.

## Primeira vertical slice implementada

Feature: `Liderados`

- `CriarLiderado` (POST `/api/liderados`)
- `ListarLiderados` (GET `/api/liderados`)
- `ObterLideradoPorId` (GET `/api/liderados/{id}`)

## Como executar

1. Restaurar pacotes: `dotnet restore PeopleManagement.slnx`
2. Rodar API: `dotnet run --project PeopleManagement.Api`
3. Abrir Swagger: `https://localhost:<porta>/swagger`

## Como testar

1. `dotnet test PeopleManagement.slnx`

