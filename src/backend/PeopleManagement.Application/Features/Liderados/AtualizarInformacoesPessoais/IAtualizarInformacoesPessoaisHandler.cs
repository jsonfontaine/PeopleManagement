namespace PeopleManagement.Application.Features.Liderados.AtualizarInformacoesPessoais;

/// <summary>
/// Contrato do handler de atualizacao dos dados pessoais.
/// </summary>
public interface IAtualizarInformacoesPessoaisHandler
{
    Task<AtualizarInformacoesPessoaisResponse> HandleAsync(AtualizarInformacoesPessoaisCommand command, CancellationToken cancellationToken);
}

