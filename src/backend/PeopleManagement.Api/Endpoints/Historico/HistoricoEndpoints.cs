using PeopleManagement.Application.Features.Historico.ListarHistoricoAlteracoes;

namespace PeopleManagement.Api.Endpoints.Historico;

/// <summary>
/// Endpoints para consulta de historico de alteracoes.
/// </summary>
public static class HistoricoEndpoints
{
    public static IEndpointRouteBuilder MapHistoricoEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/liderados/{lideradoId:guid}/historico").WithTags("Historico");

        group.MapGet("/", async (
            Guid lideradoId,
            IListarHistoricoAlteracoesHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(new ListarHistoricoAlteracoesQuery(lideradoId), cancellationToken);
            return Results.Ok(response);
        })
        .WithName("ListarHistoricoAlteracoes");

        return endpoints;
    }
}

