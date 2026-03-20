using PeopleManagement.Application.Features.Feedbacks.ListarFeedbacks;
using PeopleManagement.Application.Features.Feedbacks.RegistrarFeedback;
using PeopleManagement.Domain;

namespace PeopleManagement.Api.Endpoints.Feedbacks;

/// <summary>
/// Endpoints da secao de feedbacks.
/// </summary>
public static class FeedbacksEndpoints
{
    public static IEndpointRouteBuilder MapFeedbacksEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/liderados/{lideradoId:guid}/feedbacks").WithTags("Feedbacks");

        group.MapGet("/", async (
            Guid lideradoId,
            IListarFeedbacksHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(new ListarFeedbacksQuery(lideradoId), cancellationToken);
            return Results.Ok(response);
        })
        .WithName("ListarFeedbacks");

        group.MapPost("/", async (
            Guid lideradoId,
            RegistrarFeedbackRequest request,
            IRegistrarFeedbackHandler handler,
            CancellationToken cancellationToken) =>
        {
            try
            {
                var command = new RegistrarFeedbackCommand(
                    lideradoId,
                    request.Data,
                    request.Conteudo,
                    request.Receptividade,
                    request.Polaridade);

                var response = await handler.HandleAsync(command, cancellationToken);
                return Results.Created($"/api/liderados/{lideradoId}/feedbacks", response);
            }
            catch (DomainException ex)
            {
                return Results.BadRequest(new { erro = ex.Message });
            }
        })
        .WithName("RegistrarFeedback");

        return endpoints;
    }
}

