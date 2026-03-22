using Microsoft.AspNetCore.Mvc;
using PeopleManagement.Application.Features.Dashboard;

namespace PeopleManagement.Api.Controllers;

[ApiController]
[Route("api/dashboard")]
public sealed class DashboardController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ObterDashboard(
        [FromServices] DashboardService dashboardService,
        CancellationToken cancellationToken)
    {
        var response = await dashboardService.ObterAsync(cancellationToken);
        return Ok(response);
    }
}

