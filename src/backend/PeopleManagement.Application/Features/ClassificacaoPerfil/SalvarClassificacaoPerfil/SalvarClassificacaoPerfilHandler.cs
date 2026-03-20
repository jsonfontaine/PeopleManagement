using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Domain;

namespace PeopleManagement.Application.Features.ClassificacaoPerfil.SalvarClassificacaoPerfil;

/// <summary>
/// Atualiza a classificacao de perfil e registra historico de alteracoes.
/// </summary>
public sealed class SalvarClassificacaoPerfilHandler : ISalvarClassificacaoPerfilHandler
{
    private readonly IClassificacaoPerfilRepository _classificacaoPerfilRepository;
    private readonly IHistoricoAlteracaoRepository _historicoAlteracaoRepository;
    private readonly IUsuarioContexto _usuarioContexto;

    public SalvarClassificacaoPerfilHandler(
        IClassificacaoPerfilRepository classificacaoPerfilRepository,
        IHistoricoAlteracaoRepository historicoAlteracaoRepository,
        IUsuarioContexto usuarioContexto)
    {
        _classificacaoPerfilRepository = classificacaoPerfilRepository;
        _historicoAlteracaoRepository = historicoAlteracaoRepository;
        _usuarioContexto = usuarioContexto;
    }

    public async Task<SalvarClassificacaoPerfilResponse> HandleAsync(SalvarClassificacaoPerfilCommand command, CancellationToken cancellationToken)
    {
        var perfil = command.Perfil.Trim();
        var nineBox = command.NineBox.Trim();
        var disc = command.Disc?.Trim();

        if (string.IsNullOrWhiteSpace(perfil))
        {
            throw new DomainException("O perfil e obrigatorio.");
        }

        if (string.IsNullOrWhiteSpace(nineBox))
        {
            throw new DomainException("O nine box e obrigatorio.");
        }

        var atual = await _classificacaoPerfilRepository.ObterAsync(command.LideradoId, cancellationToken);
        await _classificacaoPerfilRepository.SalvarAsync(
            new ClassificacaoPerfilRegistro(command.LideradoId, perfil, nineBox, disc, DateTime.UtcNow),
            cancellationToken);

        if (!string.Equals(atual?.Perfil, perfil, StringComparison.Ordinal))
        {
            await _historicoAlteracaoRepository.RegistrarAsync(
                new HistoricoAlteracaoRegistro(
                    command.LideradoId,
                    "ClassificacaoPerfil",
                    "Perfil",
                    atual?.Perfil,
                    perfil,
                    DateTime.UtcNow,
                    _usuarioContexto.UsuarioAtual),
                cancellationToken);
        }

        if (!string.Equals(atual?.NineBox, nineBox, StringComparison.Ordinal))
        {
            await _historicoAlteracaoRepository.RegistrarAsync(
                new HistoricoAlteracaoRegistro(
                    command.LideradoId,
                    "ClassificacaoPerfil",
                    "NineBox",
                    atual?.NineBox,
                    nineBox,
                    DateTime.UtcNow,
                    _usuarioContexto.UsuarioAtual),
                cancellationToken);
        }

        if (!string.Equals(atual?.Disc, disc, StringComparison.Ordinal))
        {
            await _historicoAlteracaoRepository.RegistrarAsync(
                new HistoricoAlteracaoRegistro(
                    command.LideradoId,
                    "ClassificacaoPerfil",
                    "Disc",
                    atual?.Disc,
                    disc,
                    DateTime.UtcNow,
                    _usuarioContexto.UsuarioAtual),
                cancellationToken);
        }

        return new SalvarClassificacaoPerfilResponse(command.LideradoId, perfil, nineBox, disc);
    }
}

