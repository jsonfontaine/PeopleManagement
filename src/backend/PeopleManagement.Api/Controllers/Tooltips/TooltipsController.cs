using Microsoft.AspNetCore.Mvc;
using PeopleManagement.Application.Common;
using PeopleManagement.Application.Features.Tooltips;

namespace PeopleManagement.Api.Controllers;

[ApiController]
[Route("api/tooltips")]
public sealed class TooltipsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ListarOuObter(
        [FromQuery] string? nome,
        [FromQuery] string? valueObject,
        [FromServices] TooltipsService tooltipsService,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(nome) && !string.IsNullOrWhiteSpace(valueObject))
        {
            var registro = await tooltipsService.ObterAsync(nome, valueObject, cancellationToken);
            if (registro is null)
            {
                return NotFound();
            }

            return Ok(new
            {
                nome = registro.Nome,
                valueObject = registro.ValueObject,
                tooltip = registro.Tooltip
            });
        }

        var registros = await tooltipsService.ListarAsync(cancellationToken);
        return Ok(new
        {
            registros = registros.Select(r => new
            {
                nome = r.Nome,
                valueObject = r.ValueObject,
                tooltip = r.Tooltip
            })
        });
    }

    [HttpPut]
    public async Task<IActionResult> Salvar(
        [FromBody] SalvarTooltipRequest request,
        [FromServices] TooltipsService tooltipsService,
        CancellationToken cancellationToken)
    {
        try
        {
            await tooltipsService.SalvarAsync(request.Nome, request.ValueObject, request.Tooltip, cancellationToken);
            return NoContent();
        }
        catch (RegraNegocioException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }
}

