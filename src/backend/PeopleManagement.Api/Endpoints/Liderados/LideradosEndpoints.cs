using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Features.Liderados.AtualizarInformacoesPessoais;
using PeopleManagement.Application.Features.Liderados.CriarLiderado;
using PeopleManagement.Application.Features.Liderados.ListarLiderados;
using PeopleManagement.Application.Features.Liderados.ObterLideradoPorId;
using PeopleManagement.Application.Features.Liderados.ObterVisaoIndividual;
using PeopleManagement.Domain;

namespace PeopleManagement.Api.Endpoints.Liderados;

/// <summary>
/// Endpoints da feature de liderados.
/// </summary>
public static class LideradosEndpoints
{
    public static IEndpointRouteBuilder MapLideradosEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/liderados").WithTags("Liderados");

        group.MapPost("/", async (
            CriarLideradoRequest request,
            ICriarLideradoHandler handler,
            ILoggerFactory loggerFactory,
            CancellationToken cancellationToken) =>
        {
            var logger = loggerFactory.CreateLogger("LideradosEndpoints");

            try
            {
                var command = new CriarLideradoCommand(request.Nome);
                var created = await handler.HandleAsync(command, cancellationToken);

                logger.LogInformation("Endpoint de criacao executado. Id={LideradoId}", created.Id);

                return Results.Created($"/api/liderados/{created.Id}", created);
            }
            catch (DomainException ex)
            {
                logger.LogWarning(ex, "Falha de regra de dominio ao criar liderado.");
                return Results.BadRequest(new { erro = ex.Message });
            }
        })
        .WithName("CriarLiderado");

        group.MapGet("/", async (
            IListarLideradosHandler handler,
            CancellationToken cancellationToken) =>
        {
            var liderados = await handler.HandleAsync(new ListarLideradosQuery(), cancellationToken);
            return Results.Ok(liderados);
        })
        .WithName("ListarLiderados");

        group.MapGet("/{id:guid}", async (
            Guid id,
            IObterLideradoPorIdHandler handler,
            CancellationToken cancellationToken) =>
        {
            var liderado = await handler.HandleAsync(new ObterLideradoPorIdQuery(id), cancellationToken);
            return liderado is null ? Results.NotFound() : Results.Ok(liderado);
        })
        .WithName("ObterLideradoPorId");

        group.MapGet("/{id:guid}/visao-individual", async (
            Guid id,
            IObterVisaoIndividualHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(new ObterVisaoIndividualQuery(id), cancellationToken);
            return response is null ? Results.NotFound() : Results.Ok(response);
        })
        .WithName("ObterVisaoIndividual");

        group.MapPut("/{id:guid}/informacoes-pessoais", async (
            Guid id,
            AtualizarInformacoesPessoaisRequest request,
            IAtualizarInformacoesPessoaisHandler handler,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var informacoes = new InformacoesPessoais(
                    request.Nome,
                    request.DataNascimento,
                    request.EstadoCivil,
                    request.QuantidadeFilhos,
                    request.DataContratacao,
                    request.Cargo,
                    request.DataInicioCargo,
                    request.AspiracaoCarreira,
                    request.GostosPessoais,
                    request.RedFlags,
                    request.Bio);

                var response = await handler.HandleAsync(new AtualizarInformacoesPessoaisCommand(id, informacoes), cancellationToken);
                return Results.Ok(response);
            }
            catch (DomainException ex)
            {
                return Results.BadRequest(new { erro = ex.Message });
            }
        })
        .WithName("AtualizarInformacoesPessoais");

        return endpoints;
    }
}

