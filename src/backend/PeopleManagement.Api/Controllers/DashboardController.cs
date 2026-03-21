using Microsoft.AspNetCore.Mvc;
using PeopleManagement.Application.Features.Dashboard.ObterDashboard;

namespace PeopleManagement.Api.Controllers;

[ApiController]
[Route("api/dashboard")]
public sealed class DashboardController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ObterDashboard(
        [FromServices] IObterDashboardHandler handler,
        CancellationToken cancellationToken)
    {
        var response = await handler.HandleAsync(new ObterDashboardQuery(), cancellationToken);
        return Ok(response);
    }
}

