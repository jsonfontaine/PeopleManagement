using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Abstractions.Persistence;

/// <summary>
/// Porta de persistencia para registros de 1:1.
/// </summary>
public interface IOneOnOneRepository
{
    Task AdicionarAsync(OneOnOneRegistro registro, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<OneOnOneRegistro>> ListarPorLideradoAsync(Guid lideradoId, CancellationToken cancellationToken);
}

