using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Abstractions.Persistence;

/// <summary>
/// Porta de persistencia para registros de feedback.
/// </summary>
public interface IFeedbackRepository
{
    Task AdicionarAsync(FeedbackRegistro registro, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<FeedbackRegistro>> ListarPorLideradoAsync(Guid lideradoId, CancellationToken cancellationToken);
}

