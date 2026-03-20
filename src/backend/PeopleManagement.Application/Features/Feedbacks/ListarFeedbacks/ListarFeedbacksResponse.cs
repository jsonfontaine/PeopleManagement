using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Features.Feedbacks.ListarFeedbacks;

/// <summary>
/// Resposta da listagem de feedbacks.
/// </summary>
public sealed record ListarFeedbacksResponse(IReadOnlyCollection<FeedbackRegistro> Registros);

