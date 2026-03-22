using Microsoft.AspNetCore.Mvc;
using PeopleManagement.Application.Features.Tooltips;

namespace PeopleManagement.Api.Controllers;

[ApiController]
[Route("api/tooltips")]
public sealed class TooltipsController : ControllerBase
{
    [HttpGet("{chaveCampo}")]
    public async Task<IActionResult> Obter(
        string chaveCampo,
        [FromServices] TooltipsService tooltipsService,
        CancellationToken cancellationToken)
    {
        var tooltip = await tooltipsService.ObterAsync(chaveCampo, cancellationToken);

        return tooltip is null ? NotFound() : Ok(tooltip);
    }

    [HttpPut("{chaveCampo}")]
    public async Task<IActionResult> Salvar(
        string chaveCampo,
        [FromBody] SalvarTooltipRequest request,
        [FromServices] TooltipsService tooltipsService,
        CancellationToken cancellationToken)
    {
        await tooltipsService.SalvarAsync(chaveCampo, request.Texto ?? string.Empty, cancellationToken);

        return NoContent();
    }
}

public sealed record SalvarTooltipRequest(string Texto);


