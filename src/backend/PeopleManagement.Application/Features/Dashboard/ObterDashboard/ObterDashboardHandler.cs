using PeopleManagement.Application.Abstractions.Persistence;

namespace PeopleManagement.Application.Features.Dashboard.ObterDashboard;

/// <summary>
/// Implementa a leitura do dashboard consolidado.
/// </summary>
public sealed class ObterDashboardHandler : IObterDashboardHandler
{
    private readonly IDashboardRepository _dashboardRepository;

    public ObterDashboardHandler(IDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;
    }

    public async Task<ObterDashboardResponse> HandleAsync(ObterDashboardQuery query, CancellationToken cancellationToken)
    {
        var cards = await _dashboardRepository.ListarCardsAsync(cancellationToken);
        return new ObterDashboardResponse(cards.OrderBy(x => x.Nome).ToArray());
    }
}

