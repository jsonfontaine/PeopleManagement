using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using PeopleManagement.Application.Common;
using PeopleManagement.Application.Features.Atitudes;

namespace PeopleManagement.Api.Controllers;

[ApiController]
[Route("api/atitudes")]
public sealed class AtitudesController : ControllerBase
{
    [HttpGet("{lideradoId:guid}")]
    public async Task<IActionResult> Listar(
        Guid lideradoId,
        [FromServices] AtitudesService atitudesService,
        CancellationToken cancellationToken)
    {
        var registros = await atitudesService.ListarAsync(lideradoId, cancellationToken);
        return Ok(new
        {
            registros = registros.Select(r => new
            {
                lideradoId = r.LideradoId,
                valor = r.Valor,
                data = r.Data.ToString("yyyy-MM-dd")
            })
        });
    }

    [HttpPost]
    public async Task<IActionResult> Salvar(
        [FromBody] SalvarAtitudesRequest request,
        [FromServices] AtitudesService atitudesService,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!TryParseDate(request.Data, out var data))
            {
                return BadRequest(new { erro = "Nao consegui entender a data de Atitudes. Use dd/MM/aaaa (ex.: 27/11/2025) ou yyyy-MM-dd." });
            }

            await atitudesService.SalvarAsync(request.LideradoId, request.Valor, data, cancellationToken);
            return NoContent();
        }
        catch (RegraNegocioException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Remover(
        [FromBody] RemoverAtitudesRequest request,
        [FromServices] AtitudesService atitudesService,
        CancellationToken cancellationToken)
    {
        if (!TryParseDate(request.Data, out var data))
        {
            return BadRequest(new { erro = "Nao consegui entender a data para excluir Atitudes. Use dd/MM/aaaa (ex.: 27/11/2025) ou yyyy-MM-dd." });
        }

        await atitudesService.RemoverAsync(request.LideradoId, data, cancellationToken);
        return NoContent();
    }

    private static bool TryParseDate(string value, out DateOnly data)
    {
        var formats = new[] { "yyyy-MM-dd", "dd/MM/yyyy" };
        return DateOnly.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out data);
    }
}

