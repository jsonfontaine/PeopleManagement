using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Abstractions.Persistence;

/// <summary>
/// Porta de persistencia para historico de alteracoes.
/// </summary>
public interface IHistoricoAlteracaoRepository
{
    Task RegistrarAsync(HistoricoAlteracaoRegistro registro, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<HistoricoAlteracaoRegistro>> ListarPorLideradoAsync(Guid lideradoId, CancellationToken cancellationToken);
}

