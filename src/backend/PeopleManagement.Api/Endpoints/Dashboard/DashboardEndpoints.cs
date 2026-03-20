using PeopleManagement.Application.Features.Dashboard.ObterDashboard;

namespace PeopleManagement.Api.Endpoints.Dashboard;

/// <summary>
/// Endpoints de consulta do dashboard.
/// </summary>
public static class DashboardEndpoints
{
    public static IEndpointRouteBuilder MapDashboardEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/dashboard").WithTags("Dashboard");

        group.MapGet("/", async (
            IObterDashboardHandler handler,
            CancellationToken cancellationToken) =>
        {
            var response = await handler.HandleAsync(new ObterDashboardQuery(), cancellationToken);
            return Results.Ok(response);
        })
        .WithName("ObterDashboard");

        return endpoints;
    }
}

