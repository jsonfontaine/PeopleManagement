using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using PeopleManagement.Application.Common;
using PeopleManagement.Application.Features.PropHistorica;

namespace PeopleManagement.Api.Controllers;

[ApiController]
[Route("api/liderados/{lideradoId:guid}/propriedades")]
public sealed class PropHistoricaController : ControllerBase
{
    private static readonly HashSet<string> TiposPermitidos = new(StringComparer.OrdinalIgnoreCase)
    {
        "conhecimentos", "habilidades", "atitudes", "valores", "expectativas",
        "metas", "situacaoAtual", "opcoes", "proximosPassos",
        "fortalezas", "oportunidades", "fraquezas", "ameacas"
    };

    [HttpGet("{tipo}")]
    public async Task<IActionResult> Listar(
        Guid lideradoId,
        string tipo,
        [FromServices] PropHistoricaService service,
        CancellationToken cancellationToken)
    {
        if (!TiposPermitidos.Contains(tipo))
            return BadRequest(new { erro = $"Tipo '{tipo}' nao e valido." });

        var registros = await service.ListarAsync(lideradoId, tipo, cancellationToken);
        return Ok(new
        {
            registros = registros.Select(r => new
            {
                lideradoId = r.LideradoId,
                tipo = r.Tipo,
                data = r.Data.ToString("yyyy-MM-dd"),
                valor = r.Valor
            })
        });
    }

    [HttpPost("{tipo}")]
    public async Task<IActionResult> Salvar(
        Guid lideradoId,
        string tipo,
        [FromBody] SalvarPropHistoricaRequest request,
        [FromServices] PropHistoricaService service,
        CancellationToken cancellationToken)
    {
        if (!TiposPermitidos.Contains(tipo))
            return BadRequest(new { erro = $"Tipo '{tipo}' nao e valido." });

        try
        {
            if (!TryParseDate(request.Data, out var data))
                return BadRequest(new { erro = "Data invalida. Use o formato yyyy-MM-dd ou dd/MM/yyyy." });

            await service.SalvarAsync(lideradoId, tipo, request.Valor, data, cancellationToken);
            return NoContent();
        }
        catch (RegraNegocioException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpDelete("{tipo}/{data}")]
    public async Task<IActionResult> Remover(
        Guid lideradoId,
        string tipo,
        string data,
        [FromServices] PropHistoricaService service,
        CancellationToken cancellationToken)
    {
        if (!TiposPermitidos.Contains(tipo))
            return BadRequest(new { erro = $"Tipo '{tipo}' nao e valido." });

        if (!TryParseDate(data, out var parsedData))
            return BadRequest(new { erro = "Data invalida. Use o formato yyyy-MM-dd." });

        await service.RemoverAsync(lideradoId, tipo, parsedData, cancellationToken);
        return NoContent();
    }

    private static bool TryParseDate(string value, out DateOnly data)
    {
        var formats = new[] { "yyyy-MM-dd", "dd/MM/yyyy" };
        return DateOnly.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out data);
    }
}

public sealed record SalvarPropHistoricaRequest(string Valor, string Data);

