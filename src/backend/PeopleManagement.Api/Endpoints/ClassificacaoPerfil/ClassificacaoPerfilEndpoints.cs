using PeopleManagement.Application.Features.ClassificacaoPerfil.ObterClassificacaoPerfil;
using PeopleManagement.Application.Features.ClassificacaoPerfil.SalvarClassificacaoPerfil;
using PeopleManagement.Domain;

namespace PeopleManagement.Api.Endpoints.ClassificacaoPerfil;

/// <summary>
/// Endpoints da secao de classificacao de perfil.
/// </summary>
public static class ClassificacaoPerfilEndpoints
{
    public static IEndpointRouteBuilder MapClassificacaoPerfilEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/liderados/{lideradoId:guid}/classificacao-perfil").WithTags("ClassificacaoPerfil");

        group.MapGet("/", async (
            Guid lideradoId,
            IObterClassificacaoPerfilHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(new ObterClassificacaoPerfilQuery(lideradoId), cancellationToken);
            return response is null ? Results.NotFound() : Results.Ok(response);
        })
        .WithName("ObterClassificacaoPerfil");

        group.MapPut("/", async (
            Guid lideradoId,
            SalvarClassificacaoPerfilRequest request,
            ISalvarClassificacaoPerfilHandler handler,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var command = new SalvarClassificacaoPerfilCommand(lideradoId, request.Perfil, request.NineBox, request.Disc);
                var response = await handler.HandleAsync(command, cancellationToken);
                return Results.Ok(response);
            }
            catch (DomainException ex)
            {
                return Results.BadRequest(new { erro = ex.Message });
            }
        })
        .WithName("SalvarClassificacaoPerfil");

        return endpoints;
    }
}

