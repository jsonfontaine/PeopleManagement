using Microsoft.AspNetCore.Mvc;
using PeopleManagement.Application.Common;
using PeopleManagement.Application.Features.Dicas;

namespace PeopleManagement.Api.Controllers;

[ApiController]
[Route("api/dicas")]
public sealed class DicasController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Obter([FromServices] DicasService dicasService, CancellationToken cancellationToken)
    {
        var registro = await dicasService.ObterAsync(cancellationToken);
        return Ok(new { conteudoHtml = registro.ConteudoHtml });
    }

    [HttpPut]
    public async Task<IActionResult> Salvar(
        [FromBody] SalvarDicasRequest request,
        [FromServices] DicasService dicasService,
        CancellationToken cancellationToken)
    {
        try
        {
            await dicasService.SalvarAsync(request.ConteudoHtml, cancellationToken);
            return NoContent();
        }
        catch (RegraNegocioException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }
}

