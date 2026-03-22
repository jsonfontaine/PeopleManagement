using Microsoft.AspNetCore.Mvc;
using PeopleManagement.Application.Common;
using PeopleManagement.Application.Features.Liderados;

namespace PeopleManagement.Api.Controllers;

[ApiController]
[Route("api/liderados")]
public sealed class LideradosController : ControllerBase
{
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarLiderado(
        Guid id,
        [FromBody] CriarLideradoRequest request,
        [FromServices] LideradosService lideradosService,
        [FromServices] ILogger<LideradosController> logger,
        CancellationToken cancellationToken)
    {
        try
        {
            await lideradosService.AtualizarNomeAsync(id, request.Nome, cancellationToken);
            logger.LogInformation("Endpoint de atualizacao executado. Id={LideradoId}", id);
            return NoContent();
        }
        catch (RegraNegocioException ex)
        {
            logger.LogWarning(ex, "Falha de regra de negocio ao atualizar liderado.");
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> RemoverLiderado(
        Guid id,
        [FromServices] LideradosService lideradosService,
        [FromServices] ILogger<LideradosController> logger,
        CancellationToken cancellationToken)
    {
        try
        {
            await lideradosService.RemoverAsync(id, cancellationToken);
            logger.LogInformation("Endpoint de remocao executado. Id={LideradoId}", id);
            return NoContent();
        }
        catch (RegraNegocioException ex)
        {
            logger.LogWarning(ex, "Falha de regra de negocio ao remover liderado.");
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CriarLiderado(
        [FromBody] CriarLideradoRequest request,
        [FromServices] LideradosService lideradosService,
        [FromServices] ILogger<LideradosController> logger,
        CancellationToken cancellationToken)
    {
        try
        {
            var created = await lideradosService.CriarAsync(request.Nome, cancellationToken);
            logger.LogInformation("Endpoint de criacao executado. Id={LideradoId}", created.Id);
            return Created($"/api/liderados/{created.Id}", created);
        }
        catch (RegraNegocioException ex)
        {
            logger.LogWarning(ex, "Falha de regra de negocio ao criar liderado.");
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ListarLiderados(
        [FromServices] LideradosService lideradosService,
        CancellationToken cancellationToken)
    {
        var liderados = await lideradosService.ListarAsync(cancellationToken);
        return Ok(liderados);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterLideradoPorId(
        Guid id,
        [FromServices] LideradosService lideradosService,
        CancellationToken cancellationToken)
    {
        var liderado = await lideradosService.ObterPorIdAsync(id, cancellationToken);
        return liderado is null ? NotFound() : Ok(liderado);
    }

    [HttpGet("{id:guid}/visao-individual")]
    public async Task<IActionResult> ObterVisaoIndividual(
        Guid id,
        [FromServices] LideradosService lideradosService,
        CancellationToken cancellationToken)
    {
        var response = await lideradosService.ObterVisaoIndividualAsync(id, cancellationToken);
        return response is null ? NotFound() : Ok(response);
    }

    [HttpGet("{id:guid}/feedbacks")]
    public async Task<IActionResult> ListarFeedbacks(
        Guid id,
        [FromServices] LideradosService lideradosService,
        CancellationToken cancellationToken)
    {
        var registros = await lideradosService.ListarFeedbacksAsync(id, cancellationToken);
        return Ok(new { registros });
    }

    [HttpPost("{id:guid}/feedbacks")]
    public async Task<IActionResult> CriarFeedback(
        Guid id,
        [FromBody] CriarFeedbackRequest request,
        [FromServices] LideradosService lideradosService,
        CancellationToken cancellationToken)
    {
        await lideradosService.CriarFeedbackAsync(
            id,
            new CriarFeedbackInput(request.Data, request.Conteudo, request.Receptividade, request.Polaridade),
            cancellationToken);

        return Created($"/api/liderados/{id}/feedbacks", null);
    }

    [HttpGet("{id:guid}/one-on-ones")]
    public async Task<IActionResult> ListarOneOnOnes(
        Guid id,
        [FromServices] LideradosService lideradosService,
        CancellationToken cancellationToken)
    {
        var registros = await lideradosService.ListarOneOnOnesAsync(id, cancellationToken);
        return Ok(new { registros });
    }

    [HttpPost("{id:guid}/one-on-ones")]
    public async Task<IActionResult> CriarOneOnOne(
        Guid id,
        [FromBody] CriarOneOnOneRequest request,
        [FromServices] LideradosService lideradosService,
        CancellationToken cancellationToken)
    {
        await lideradosService.CriarOneOnOneAsync(
            id,
            new CriarOneOnOneInput(request.Data, request.Resumo, request.TarefasAcordadas, request.ProximosAssuntos),
            cancellationToken);

        return Created($"/api/liderados/{id}/one-on-ones", null);
    }

    [HttpPost("{id:guid}/cultura")]
    public async Task<IActionResult> SalvarCultura(
        Guid id,
        [FromBody] SalvarCulturaRequest request,
        [FromServices] LideradosService lideradosService,
        CancellationToken cancellationToken)
    {
        try
        {
            await lideradosService.SalvarCulturaAsync(
                id,
                new RadarCulturalResponse(
                    request.Data,
                    request.AprenderEMelhorarSempre,
                    request.AtitudeDeDono,
                    request.BuscarMelhoresResultadosParaClientes,
                    request.EspiritoDeEquipe,
                    request.Excelencia,
                    request.FazerAcontecer,
                    request.InovarParaInspirar),
                cancellationToken);

            return Created($"/api/liderados/{id}/cultura", null);
        }
        catch (RegraNegocioException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpGet("{id:guid}/cultura/radar")]
    public async Task<IActionResult> ObterRadarCultural(
        Guid id,
        [FromQuery] DateOnly data,
        [FromServices] LideradosService lideradosService,
        CancellationToken cancellationToken)
    {
        var radar = await lideradosService.ObterRadarCulturalAsync(id, data, cancellationToken);
        return radar is null ? NotFound() : Ok(new { radar });
    }

    [HttpPut("{id:guid}/informacoes-pessoais")]
    public async Task<IActionResult> AtualizarInformacoesPessoais(
        Guid id,
        [FromBody] AtualizarInformacoesPessoaisRequest request,
        [FromServices] LideradosService lideradosService,
        CancellationToken cancellationToken)
    {
        try
        {
            await lideradosService.AtualizarInformacoesPessoaisAsync(
                id,
                new AtualizarInformacoesPessoaisInput(
                    request.Nome,
                    request.DataNascimento,
                    request.EstadoCivil,
                    request.QuantidadeFilhos,
                    request.DataContratacao,
                    request.Cargo,
                    request.DataInicioCargo,
                    request.AspiracaoCarreira,
                    request.GostosPessoais,
                    request.RedFlags,
                    request.Bio),
                cancellationToken);

            return NoContent();
        }
        catch (RegraNegocioException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpPut("{id:guid}/classificacao-perfil")]
    public async Task<IActionResult> AtualizarClassificacaoPerfil(
        Guid id,
        [FromBody] AtualizarClassificacaoPerfilRequest request,
        [FromServices] LideradosService lideradosService,
        CancellationToken cancellationToken)
    {
        try
        {
            await lideradosService.AtualizarClassificacaoPerfilAsync(
                id,
                request.Perfil,
                request.NineBox,
                request.Data,
                cancellationToken);

            return NoContent();
        }
        catch (RegraNegocioException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }
}

public sealed record CriarLideradoRequest(string Nome);

public sealed record AtualizarInformacoesPessoaisRequest(
    string Nome,
    DateOnly? DataNascimento,
    string? EstadoCivil,
    int? QuantidadeFilhos,
    DateOnly? DataContratacao,
    string? Cargo,
    DateOnly? DataInicioCargo,
    string? AspiracaoCarreira,
    string? GostosPessoais,
    string? RedFlags,
    string? Bio);

public sealed record AtualizarClassificacaoPerfilRequest(string Perfil, string NineBox, DateOnly Data);

public sealed record CriarFeedbackRequest(DateOnly Data, string Conteudo, string Receptividade, string Polaridade);

public sealed record CriarOneOnOneRequest(DateOnly Data, string Resumo, string TarefasAcordadas, string ProximosAssuntos);

public sealed record SalvarCulturaRequest(
    DateOnly Data,
    int AprenderEMelhorarSempre,
    int AtitudeDeDono,
    int BuscarMelhoresResultadosParaClientes,
    int EspiritoDeEquipe,
    int Excelencia,
    int FazerAcontecer,
    int InovarParaInspirar);

