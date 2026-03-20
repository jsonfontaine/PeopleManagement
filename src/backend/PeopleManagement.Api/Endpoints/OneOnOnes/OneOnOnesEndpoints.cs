using PeopleManagement.Application.Features.OneOnOnes.ListarOneOnOnes;
using PeopleManagement.Application.Features.OneOnOnes.RegistrarOneOnOne;
using PeopleManagement.Domain;

namespace PeopleManagement.Api.Endpoints.OneOnOnes;

/// <summary>
/// Endpoints da secao 1:1.
/// </summary>
public static class OneOnOnesEndpoints
{
    public static IEndpointRouteBuilder MapOneOnOnesEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/liderados/{lideradoId:guid}/one-on-ones").WithTags("OneOnOnes");

        group.MapGet("/", async (
            Guid lideradoId,
            IListarOneOnOnesHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(new ListarOneOnOnesQuery(lideradoId), cancellationToken);
            return Results.Ok(response);
        })
        .WithName("ListarOneOnOnes");

        group.MapPost("/", async (
            Guid lideradoId,
            RegistrarOneOnOneRequest request,
            IRegistrarOneOnOneHandler handler,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var command = new RegistrarOneOnOneCommand(
                    lideradoId,
                    request.Data,
                    request.Resumo,
                    request.TarefasAcordadas,
                    request.ProximosAssuntos);

                var response = await handler.HandleAsync(command, cancellationToken);
                return Results.Created($"/api/liderados/{lideradoId}/one-on-ones", response);
            }
            catch (DomainException ex)
            {
                return Results.BadRequest(new { erro = ex.Message });
            }
        })
        .WithName("RegistrarOneOnOne");

        return endpoints;
    }
}

