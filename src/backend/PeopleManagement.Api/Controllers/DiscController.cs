using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using PeopleManagement.Application.Features.Disc;
using PeopleManagement.Domain;

namespace PeopleManagement.Api.Controllers;

[ApiController]
[Route("api/disc")]
public sealed class DiscController : ControllerBase
{
    [HttpGet("{lideradoId:guid}")]
    public async Task<IActionResult> Listar(Guid lideradoId, [FromServices] IDiscService discService, CancellationToken cancellationToken)
    {
        var registros = await discService.ListarPorLideradoAsync(lideradoId, cancellationToken);
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
        [FromBody] SalvarDiscRequest request,
        [FromServices] IDiscService discService,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!TryParseDate(request.Data, out var data))
            {
                return BadRequest(new { erro = "Nao consegui entender a data do DISC. Use dd/MM/aaaa (ex.: 27/11/2025) ou yyyy-MM-dd." });
            }

            await discService.SalvarAsync(request.LideradoId, request.Disc, data, cancellationToken);
            return NoContent();
        }
        catch (DomainException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Remover(
        [FromBody] RemoverDiscRequest request,
        [FromServices] IDiscService discService,
        CancellationToken cancellationToken)
    {
        if (!TryParseDate(request.Data, out var data))
        {
            return BadRequest(new { erro = "Nao consegui entender a data para excluir o DISC. Use dd/MM/aaaa (ex.: 27/11/2025) ou yyyy-MM-dd." });
        }

        await discService.RemoverAsync(request.LideradoId, data, cancellationToken);
        return NoContent();
    }

    private static bool TryParseDate(string value, out DateOnly data)
    {
        var formats = new[] { "yyyy-MM-dd", "dd/MM/yyyy" };
        return DateOnly.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out data);
    }
}

public sealed record SalvarDiscRequest(Guid LideradoId, string Disc, string Data);

public sealed record RemoverDiscRequest(Guid LideradoId, string Data);


