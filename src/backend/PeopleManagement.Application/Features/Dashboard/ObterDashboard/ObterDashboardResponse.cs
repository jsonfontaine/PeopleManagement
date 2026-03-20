using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Features.Dashboard.ObterDashboard;

/// <summary>
/// Resposta com os cards consolidados do dashboard.
/// </summary>
public sealed record ObterDashboardResponse(IReadOnlyCollection<DashboardCardProjection> Cards);

