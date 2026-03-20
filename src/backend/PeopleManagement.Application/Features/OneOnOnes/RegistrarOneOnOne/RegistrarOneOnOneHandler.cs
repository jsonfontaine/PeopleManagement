using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Domain;

namespace PeopleManagement.Application.Features.OneOnOnes.RegistrarOneOnOne;

/// <summary>
/// Implementa o registro de historico de encontros 1:1.
/// </summary>
public sealed class RegistrarOneOnOneHandler : IRegistrarOneOnOneHandler
{
    private readonly IOneOnOneRepository _oneOnOneRepository;
    private readonly IHistoricoAlteracaoRepository _historicoAlteracaoRepository;
    private readonly IUsuarioContexto _usuarioContexto;

    public RegistrarOneOnOneHandler(
        IOneOnOneRepository oneOnOneRepository,
        IHistoricoAlteracaoRepository historicoAlteracaoRepository,
        IUsuarioContexto usuarioContexto)
    {
        _oneOnOneRepository = oneOnOneRepository;
        _historicoAlteracaoRepository = historicoAlteracaoRepository;
        _usuarioContexto = usuarioContexto;
    }

    public async Task<RegistrarOneOnOneResponse> HandleAsync(RegistrarOneOnOneCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.Resumo))
        {
            throw new DomainException("O resumo do 1:1 e obrigatorio.");
        }

        var registro = new OneOnOneRegistro(
            command.LideradoId,
            command.Data,
            command.Resumo.Trim(),
            command.TarefasAcordadas.Trim(),
            command.ProximosAssuntos.Trim());

        await _oneOnOneRepository.AdicionarAsync(registro, cancellationToken);

        var historico = new HistoricoAlteracaoRegistro(
            command.LideradoId,
            "1:1",
            "Resumo",
            null,
            command.Resumo,
            DateTime.UtcNow,
            _usuarioContexto.UsuarioAtual);

        await _historicoAlteracaoRepository.RegistrarAsync(historico, cancellationToken);

        return new RegistrarOneOnOneResponse(command.LideradoId, command.Data);
    }
}

