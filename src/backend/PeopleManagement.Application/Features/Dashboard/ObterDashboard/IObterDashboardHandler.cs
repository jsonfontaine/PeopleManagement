namespace PeopleManagement.Application.Features.Dashboard.ObterDashboard;

/// <summary>
/// Contrato da query de dashboard.
/// </summary>
public interface IObterDashboardHandler
{
    Task<ObterDashboardResponse> HandleAsync(ObterDashboardQuery query, CancellationToken cancellationToken);
}

