using PeopleManagement.Application.Features.Cultura.ObterRadarCulturalPorData;
using PeopleManagement.Application.Features.Cultura.RegistrarAvaliacaoCultura;
using PeopleManagement.Domain;

namespace PeopleManagement.Api.Endpoints.Cultura;

/// <summary>
/// Endpoints da secao de cultura e radar cultural.
/// </summary>
public static class CulturaEndpoints
{
    public static IEndpointRouteBuilder MapCulturaEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/liderados/{lideradoId:guid}/cultura").WithTags("Cultura");

        group.MapPost("/", async (
            Guid lideradoId,
            RegistrarAvaliacaoCulturaRequest request,
            IRegistrarAvaliacaoCulturaHandler handler,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var command = new RegistrarAvaliacaoCulturaCommand(
                    lideradoId,
                    request.Data,
                    request.AprenderEMelhorarSempre,
                    request.AtitudeDeDono,
                    request.BuscarMelhoresResultadosParaClientes,
                    request.EspiritoDeEquipe,
                    request.Excelencia,
                    request.FazerAcontecer,
                    request.InovarParaInspirar);

                var response = await handler.HandleAsync(command, cancellationToken);
                return Results.Created($"/api/liderados/{lideradoId}/cultura", response);
            }
            catch (DomainException ex)
            {
                return Results.BadRequest(new { erro = ex.Message });
            }
        })
        .WithName("RegistrarAvaliacaoCultura");

        group.MapGet("/radar", async (
            Guid lideradoId,
            DateOnly data,
            IObterRadarCulturalPorDataHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(new ObterRadarCulturalPorDataQuery(lideradoId, data), cancellationToken);
            return response is null ? Results.NotFound() : Results.Ok(response);
        })
        .WithName("ObterRadarCulturalPorData");

        return endpoints;
    }
}

