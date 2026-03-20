using PeopleManagement.Application.Abstractions.Persistence;

namespace PeopleManagement.Application.Features.Feedbacks.ListarFeedbacks;

/// <summary>
/// Implementa a consulta de feedbacks por liderado.
/// </summary>
public sealed class ListarFeedbacksHandler : IListarFeedbacksHandler
{
    private readonly IFeedbackRepository _feedbackRepository;

    public ListarFeedbacksHandler(IFeedbackRepository feedbackRepository)
    {
        _feedbackRepository = feedbackRepository;
    }

    public async Task<ListarFeedbacksResponse> HandleAsync(ListarFeedbacksQuery query, CancellationToken cancellationToken)
    {
        var registros = await _feedbackRepository.ListarPorLideradoAsync(query.LideradoId, cancellationToken);
        return new ListarFeedbacksResponse(registros.OrderByDescending(x => x.Data).ToArray());
    }
}

