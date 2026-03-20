using PeopleManagement.Application.Features.Tooltips.ObterTooltip;
using PeopleManagement.Application.Features.Tooltips.SalvarTooltip;
using PeopleManagement.Domain;

namespace PeopleManagement.Api.Endpoints.Tooltips;

/// <summary>
/// Endpoints para consulta e edicao de tooltips.
/// </summary>
public static class TooltipsEndpoints
{
    public static IEndpointRouteBuilder MapTooltipsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/tooltips").WithTags("Tooltips");

        group.MapGet("/{chaveCampo}", async (
            string chaveCampo,
            IObterTooltipHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(new ObterTooltipQuery(chaveCampo), cancellationToken);
            return response is null ? Results.NotFound() : Results.Ok(response);
        })
        .WithName("ObterTooltip");

        group.MapPut("/{chaveCampo}", async (
            string chaveCampo,
            SalvarTooltipRequest request,
            ISalvarTooltipHandler handler,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var response = await handler.HandleAsync(new SalvarTooltipCommand(chaveCampo, request.Texto), cancellationToken);
                return Results.Ok(response);
            }
            catch (DomainException ex)
            {
                return Results.BadRequest(new { erro = ex.Message });
            }
        })
        .WithName("SalvarTooltip");

        return endpoints;
    }
}

