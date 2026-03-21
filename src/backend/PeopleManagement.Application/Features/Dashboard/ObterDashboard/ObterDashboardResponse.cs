using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Features.Dashboard.ObterDashboard;

/// <summary>
/// Resposta do dashboard com os cards de resumo dos liderados.
/// </summary>
public sealed record ObterDashboardResponse(IReadOnlyCollection<DashboardCardProjection> Cards);

