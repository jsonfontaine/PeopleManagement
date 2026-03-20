namespace PeopleManagement.Application.Features.ClassificacaoPerfil.ObterClassificacaoPerfil;

/// <summary>
/// Contrato do caso de uso de consulta de classificacao de perfil.
/// </summary>
public interface IObterClassificacaoPerfilHandler
{
    Task<ObterClassificacaoPerfilResponse?> HandleAsync(ObterClassificacaoPerfilQuery query, CancellationToken cancellationToken);
}

