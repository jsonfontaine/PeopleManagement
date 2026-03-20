using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Domain;

namespace PeopleManagement.Application.Features.Liderados.AtualizarInformacoesPessoais;

/// <summary>
/// Atualiza informacoes pessoais e registra historico de alteracoes.
/// </summary>
public sealed class AtualizarInformacoesPessoaisHandler : IAtualizarInformacoesPessoaisHandler
{
    private readonly IInformacoesPessoaisRepository _informacoesPessoaisRepository;
    private readonly IHistoricoAlteracaoRepository _historicoAlteracaoRepository;
    private readonly IUsuarioContexto _usuarioContexto;

    public AtualizarInformacoesPessoaisHandler(
        IInformacoesPessoaisRepository informacoesPessoaisRepository,
        IHistoricoAlteracaoRepository historicoAlteracaoRepository,
        IUsuarioContexto usuarioContexto)
    {
        _informacoesPessoaisRepository = informacoesPessoaisRepository;
        _historicoAlteracaoRepository = historicoAlteracaoRepository;
        _usuarioContexto = usuarioContexto;
    }

    public async Task<AtualizarInformacoesPessoaisResponse> HandleAsync(AtualizarInformacoesPessoaisCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.Informacoes.Nome))
        {
            throw new DomainException("O nome do liderado e obrigatorio.");
        }

        var atual = await _informacoesPessoaisRepository.ObterAsync(command.LideradoId, cancellationToken);
        await _informacoesPessoaisRepository.SalvarAsync(command.LideradoId, command.Informacoes, cancellationToken);

        var historico = new HistoricoAlteracaoRegistro(
            command.LideradoId,
            "InformacoesPessoais",
            "Nome",
            atual?.Nome,
            command.Informacoes.Nome,
            DateTime.UtcNow,
            _usuarioContexto.UsuarioAtual);

        await _historicoAlteracaoRepository.RegistrarAsync(historico, cancellationToken);

        return new AtualizarInformacoesPessoaisResponse(command.LideradoId, command.Informacoes.Nome);
    }
}

