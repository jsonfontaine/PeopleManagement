using Microsoft.AspNetCore.Mvc;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Application.Features.Liderados.AtualizarInformacoesPessoais;
using PeopleManagement.Application.Features.Liderados.CriarLiderado;
using PeopleManagement.Application.Features.Liderados.ListarLiderados;
using PeopleManagement.Application.Features.Liderados.ObterLideradoPorId;
using PeopleManagement.Application.Features.Liderados.ObterVisaoIndividual;
using PeopleManagement.Domain;

namespace PeopleManagement.Api.Controllers;

[ApiController]

[Route("api/liderados")]
public class LideradosController : ControllerBase
{

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarLiderado(
        Guid id,
        [FromBody] CriarLideradoRequest request, // Reuse for simplicity; ideally, create a dedicated Update DTO
        [FromServices] PeopleManagement.Application.Features.Liderados.AtualizarLiderado.IAtualizarLideradoHandler handler,
        [FromServices] ILogger<LideradosController> logger,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new PeopleManagement.Application.Features.Liderados.AtualizarLiderado.AtualizarLideradoCommand(id, request.Nome);
            await handler.HandleAsync(command, cancellationToken);
            logger.LogInformation("Endpoint de atualização executado. Id={LideradoId}", id);
            return NoContent();
        }
        catch (DomainException ex)
        {
            logger.LogWarning(ex, "Falha de regra de dominio ao atualizar liderado.");
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> RemoverLiderado(
        Guid id,
        [FromServices] PeopleManagement.Application.Features.Liderados.RemoverLiderado.IRemoverLideradoHandler handler,
        [FromServices] ILogger<LideradosController> logger,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new PeopleManagement.Application.Features.Liderados.RemoverLiderado.RemoverLideradoCommand(id);
            await handler.HandleAsync(command, cancellationToken);
            logger.LogInformation("Endpoint de remoção executado. Id={LideradoId}", id);
            return NoContent();
        }
        catch (DomainException ex)
        {
            logger.LogWarning(ex, "Falha de regra de dominio ao remover liderado.");
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CriarLiderado(
        [FromBody] CriarLideradoRequest request,
        [FromServices] ICriarLideradoHandler handler,
        [FromServices] ILogger<LideradosController> logger,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new CriarLideradoCommand(request.Nome);
            var created = await handler.HandleAsync(command, cancellationToken);
            logger.LogInformation("Endpoint de criacao executado. Id={LideradoId}", created.Id);
            return Created($"/api/liderados/{created.Id}", created);
        }
        catch (DomainException ex)
        {
            logger.LogWarning(ex, "Falha de regra de dominio ao criar liderado.");
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ListarLiderados(
        [FromServices] IListarLideradosHandler handler,
        CancellationToken cancellationToken)
    {
        var liderados = await handler.HandleAsync(new ListarLideradosQuery(), cancellationToken);
        return Ok(liderados);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterLideradoPorId(
        Guid id,
        [FromServices] IObterLideradoPorIdHandler handler,
        CancellationToken cancellationToken)
    {
        var liderado = await handler.HandleAsync(new ObterLideradoPorIdQuery(id), cancellationToken);
        return liderado is null ? NotFound() : Ok(liderado);
    }

    [HttpGet("{id:guid}/visao-individual")]
    public async Task<IActionResult> ObterVisaoIndividual(
        Guid id,
        [FromServices] ILideradoRepository lideradoRepository,
        [FromServices] IInformacoesPessoaisRepository informacoesPessoaisRepository,
        [FromServices] IFeedbackRepository feedbackRepository,
        [FromServices] IOneOnOneRepository oneOnOneRepository,
        CancellationToken cancellationToken)
    {
        var liderado = await lideradoRepository.ObterPorIdAsync(id, cancellationToken);
        if (liderado is null)
        {
            return NotFound();
        }

        var informacoes = await informacoesPessoaisRepository.ObterAsync(id, cancellationToken)
            ?? new InformacoesPessoais(
                liderado.Nome,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null);

        var feedbacks = await feedbackRepository.ListarPorLideradoAsync(id, cancellationToken);
        var oneOnOnes = await oneOnOneRepository.ListarPorLideradoAsync(id, cancellationToken);

        var projection = new VisaoIndividualProjection(
            id,
            informacoes,
            null,
            null,
            feedbacks.Count,
            oneOnOnes.Count,
            Array.Empty<DateOnly>());

        return Ok(new ObterVisaoIndividualResponse(projection));
    }

    [HttpGet("{id:guid}/feedbacks")]
    public async Task<IActionResult> ListarFeedbacks(
        Guid id,
        [FromServices] IFeedbackRepository feedbackRepository,
        CancellationToken cancellationToken)
    {
        var registros = await feedbackRepository.ListarPorLideradoAsync(id, cancellationToken);
        return Ok(new { registros });
    }

    [HttpPost("{id:guid}/feedbacks")]
    public async Task<IActionResult> CriarFeedback(
        Guid id,
        [FromBody] CriarFeedbackRequest request,
        [FromServices] IFeedbackRepository feedbackRepository,
        CancellationToken cancellationToken)
    {
        await feedbackRepository.AdicionarAsync(
            new FeedbackRegistro(
                id,
                request.Data,
                request.Conteudo,
                request.Receptividade,
                request.Polaridade),
            cancellationToken);

        return Created($"/api/liderados/{id}/feedbacks", null);
    }

    [HttpGet("{id:guid}/one-on-ones")]
    public async Task<IActionResult> ListarOneOnOnes(
        Guid id,
        [FromServices] IOneOnOneRepository oneOnOneRepository,
        CancellationToken cancellationToken)
    {
        var registros = await oneOnOneRepository.ListarPorLideradoAsync(id, cancellationToken);
        return Ok(new { registros });
    }

    [HttpPost("{id:guid}/one-on-ones")]
    public async Task<IActionResult> CriarOneOnOne(
        Guid id,
        [FromBody] CriarOneOnOneRequest request,
        [FromServices] IOneOnOneRepository oneOnOneRepository,
        CancellationToken cancellationToken)
    {
        await oneOnOneRepository.AdicionarAsync(
            new OneOnOneRegistro(
                id,
                request.Data,
                request.Resumo,
                request.TarefasAcordadas,
                request.ProximosAssuntos),
            cancellationToken);

        return Created($"/api/liderados/{id}/one-on-ones", null);
    }

    [HttpPut("{id:guid}/informacoes-pessoais")]
    public async Task<IActionResult> AtualizarInformacoesPessoais(
        Guid id,
        [FromBody] AtualizarInformacoesPessoaisRequest request
        // Handler removido por limpeza de features
        , CancellationToken cancellationToken)
    {
        return NotFound("Endpoint removido por limpeza de features.");
    }

    [HttpGet("{id:guid}/disc")]
    public async Task<IActionResult> ListarDiscs(
        Guid id,
        [FromServices] ILideradoRepository repo)
    {
        var discs = await repo.ListarDiscsAsync(id);
        return Ok(discs.Select(d => new { valor = d.Valor, data = d.Data }));
    }

    [HttpPost("{id:guid}/disc")]
    public async Task<IActionResult> SalvarDisc(
        Guid id,
        [FromBody] DiscRequest request,
        [FromServices] ILideradoRepository repo)
    {
        await repo.SalvarDiscAsync(id, request.Valor, request.Data);
        return Ok();
    }

    [HttpDelete("{id:guid}/disc")]
    public async Task<IActionResult> RemoverDisc(
        Guid id,
        [FromBody] DiscRequest request,
        [FromServices] ILideradoRepository repo)
    {
        await repo.RemoverDiscAsync(id, request.Data);
        return Ok();
    }
}

public record DiscRequest(string Valor, DateOnly Data);

public sealed record CriarFeedbackRequest(DateOnly Data, string Conteudo, string Receptividade, string Polaridade);

public sealed record CriarOneOnOneRequest(DateOnly Data, string Resumo, string TarefasAcordadas, string ProximosAssuntos);

