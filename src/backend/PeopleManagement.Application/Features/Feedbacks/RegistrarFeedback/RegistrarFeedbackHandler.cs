using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Domain;

namespace PeopleManagement.Application.Features.Feedbacks.RegistrarFeedback;

/// <summary>
/// Implementa o registro de feedback em historico tabular.
/// </summary>
public sealed class RegistrarFeedbackHandler : IRegistrarFeedbackHandler
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly IHistoricoAlteracaoRepository _historicoAlteracaoRepository;
    private readonly IUsuarioContexto _usuarioContexto;

    public RegistrarFeedbackHandler(
        IFeedbackRepository feedbackRepository,
        IHistoricoAlteracaoRepository historicoAlteracaoRepository,
        IUsuarioContexto usuarioContexto)
    {
        _feedbackRepository = feedbackRepository;
        _historicoAlteracaoRepository = historicoAlteracaoRepository;
        _usuarioContexto = usuarioContexto;
    }

    public async Task<RegistrarFeedbackResponse> HandleAsync(RegistrarFeedbackCommand command, CancellationToken cancellationToken)
    {
        if (command.Polaridade is not ("Positivo" or "Negativo"))
        {
            throw new DomainException("A polaridade do feedback deve ser Positivo ou Negativo.");
        }

        var registro = new FeedbackRegistro(command.LideradoId, command.Data, command.Conteudo.Trim(), command.Receptividade.Trim(), command.Polaridade);
        await _feedbackRepository.AdicionarAsync(registro, cancellationToken);

        var historico = new HistoricoAlteracaoRegistro(
            command.LideradoId,
            "Feedbacks",
            "Conteudo",
            null,
            command.Conteudo,
            DateTime.UtcNow,
            _usuarioContexto.UsuarioAtual);

        await _historicoAlteracaoRepository.RegistrarAsync(historico, cancellationToken);

        return new RegistrarFeedbackResponse(command.LideradoId, command.Data, command.Polaridade);
    }
}

