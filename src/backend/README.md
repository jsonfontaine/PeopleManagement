# PeopleManagement Backend

Backend em .NET 8 organizado em camadas com slices por feature na aplicacao.

## Estrutura atual

- `PeopleManagement.Api`: controllers por feature, `Program.cs`, DI, Swagger e bootstrap do banco.
- `PeopleManagement.Application`: slices por feature com servicos, objetos de dominio da feature, repositorios e persistencia EF.
- `PeopleManagement.Infrastructure`: projeto de migrations do EF Core.
- `PeopleManagement.Tests`: testes automatizados por feature.

## Features ativas

- `Dashboard`
- `Liderados`
- `DISC`
- `Cultura`
- `Tooltips`

## Como executar

1. Restaurar pacotes: `dotnet restore PeopleManagement.slnx`
2. Rodar API: `dotnet run --project PeopleManagement.Api`
3. Abrir Swagger: `https://localhost:<porta>/swagger`

## Como testar

1. `dotnet test PeopleManagement.slnx`

