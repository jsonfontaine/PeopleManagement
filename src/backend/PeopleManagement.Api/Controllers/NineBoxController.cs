using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using PeopleManagement.Application.Common;
using PeopleManagement.Application.Features.NineBox;

namespace PeopleManagement.Api.Controllers;

[ApiController]
[Route("api/nine-box")]
public sealed class NineBoxController : ControllerBase
{
    [HttpGet("{lideradoId:guid}")]
    public async Task<IActionResult> Listar(
        Guid lideradoId,
        [FromServices] NineBoxService nineBoxService,
        CancellationToken cancellationToken)
    {
        var registros = await nineBoxService.ListarAsync(lideradoId, cancellationToken);
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
        [FromBody] SalvarNineBoxRequest request,
        [FromServices] NineBoxService nineBoxService,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!TryParseDate(request.Data, out var data))
                return BadRequest(new { erro = "Nao consegui entender a data de Nine Box. Use dd/MM/aaaa (ex.: 27/11/2025) ou yyyy-MM-dd." });

            await nineBoxService.SalvarAsync(request.LideradoId, request.Valor, data, cancellationToken);
            return NoContent();
        }
        catch (RegraNegocioException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Remover(
        [FromBody] RemoverNineBoxRequest request,
        [FromServices] NineBoxService nineBoxService,
        CancellationToken cancellationToken)
    {
        if (!TryParseDate(request.Data, out var data))
            return BadRequest(new { erro = "Nao consegui entender a data para excluir Nine Box. Use dd/MM/aaaa (ex.: 27/11/2025) ou yyyy-MM-dd." });

        await nineBoxService.RemoverAsync(request.LideradoId, data, cancellationToken);
        return NoContent();
    }

    private static bool TryParseDate(string value, out DateOnly data)
    {
        var formats = new[] { "yyyy-MM-dd", "dd/MM/yyyy" };
        return DateOnly.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out data);
    }
}

public sealed record SalvarNineBoxRequest(Guid LideradoId, string Valor, string Data);
public sealed record RemoverNineBoxRequest(Guid LideradoId, string Data);

