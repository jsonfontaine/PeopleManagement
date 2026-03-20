using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Abstractions.Persistence;

/// <summary>
/// Porta de leitura dos dados agregados do dashboard.
/// </summary>
public interface IDashboardRepository
{
    Task<IReadOnlyCollection<DashboardCardProjection>> ListarCardsAsync(CancellationToken cancellationToken);
}

