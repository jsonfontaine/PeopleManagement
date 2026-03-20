namespace PeopleManagement.Application.Features.Feedbacks.ListarFeedbacks;

/// <summary>
/// Contrato da query de listagem de feedbacks.
/// </summary>
public interface IListarFeedbacksHandler
{
    Task<ListarFeedbacksResponse> HandleAsync(ListarFeedbacksQuery query, CancellationToken cancellationToken);
}

