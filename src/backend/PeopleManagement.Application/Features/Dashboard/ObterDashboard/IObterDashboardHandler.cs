namespace PeopleManagement.Application.Features.Dashboard.ObterDashboard;

/// <summary>
/// Contrato do caso de uso de obtenção do dashboard.
/// </summary>
public interface IObterDashboardHandler
{
    Task<ObterDashboardResponse> HandleAsync(ObterDashboardQuery query, CancellationToken cancellationToken);
}

