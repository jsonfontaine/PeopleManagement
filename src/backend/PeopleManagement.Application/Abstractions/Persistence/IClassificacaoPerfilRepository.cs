using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Abstractions.Persistence;

/// <summary>
/// Porta de persistencia para classificacao de perfil.
/// </summary>
public interface IClassificacaoPerfilRepository
{
    Task<ClassificacaoPerfilRegistro?> ObterAsync(Guid lideradoId, CancellationToken cancellationToken);

    Task SalvarAsync(ClassificacaoPerfilRegistro registro, CancellationToken cancellationToken);
}

