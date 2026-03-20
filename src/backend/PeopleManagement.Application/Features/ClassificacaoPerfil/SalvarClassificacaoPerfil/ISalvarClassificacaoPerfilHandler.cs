namespace PeopleManagement.Application.Features.ClassificacaoPerfil.SalvarClassificacaoPerfil;

/// <summary>
/// Contrato do caso de uso de classificacao de perfil.
/// </summary>
public interface ISalvarClassificacaoPerfilHandler
{
    Task<SalvarClassificacaoPerfilResponse> HandleAsync(SalvarClassificacaoPerfilCommand command, CancellationToken cancellationToken);
}

