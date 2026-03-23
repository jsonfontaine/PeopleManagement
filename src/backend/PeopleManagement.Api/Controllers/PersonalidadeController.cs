using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using PeopleManagement.Application.Common;
using PeopleManagement.Application.Features.Personalidade;

namespace PeopleManagement.Api.Controllers;

[ApiController]
[Route("api/personalidade")]
public sealed class PersonalidadeController : ControllerBase
{
    [HttpGet("{lideradoId:guid}")]
    public async Task<IActionResult> Listar(
        Guid lideradoId,
        [FromServices] PersonalidadeService personalidadeService,
        CancellationToken cancellationToken)
    {
        var registros = await personalidadeService.ListarAsync(lideradoId, cancellationToken);
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
        [FromBody] SalvarPersonalidadeRequest request,
        [FromServices] PersonalidadeService personalidadeService,
        CancellationToken cancellationToken)
    {
        try
        {
            if (!TryParseDate(request.Data, out var data))
                return BadRequest(new { erro = "Nao consegui entender a data de Personalidade. Use dd/MM/aaaa (ex.: 27/11/2025) ou yyyy-MM-dd." });

            await personalidadeService.SalvarAsync(request.LideradoId, request.Valor, data, cancellationToken);
            return NoContent();
        }
        catch (RegraNegocioException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Remover(
        [FromBody] RemoverPersonalidadeRequest request,
        [FromServices] PersonalidadeService personalidadeService,
        CancellationToken cancellationToken)
    {
        if (!TryParseDate(request.Data, out var data))
            return BadRequest(new { erro = "Nao consegui entender a data para excluir Personalidade. Use dd/MM/aaaa (ex.: 27/11/2025) ou yyyy-MM-dd." });

        await personalidadeService.RemoverAsync(request.LideradoId, data, cancellationToken);
        return NoContent();
    }

    private static bool TryParseDate(string value, out DateOnly data)
    {
        var formats = new[] { "yyyy-MM-dd", "dd/MM/yyyy" };
        return DateOnly.TryParseExact(value, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out data);
    }
}

public sealed record SalvarPersonalidadeRequest(Guid LideradoId, string Valor, string Data);
public sealed record RemoverPersonalidadeRequest(Guid LideradoId, string Data);

