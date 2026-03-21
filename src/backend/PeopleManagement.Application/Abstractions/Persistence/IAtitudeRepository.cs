using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Abstractions.Persistence;

/// <summary>
/// Porta de persistencia para registros de atitude.
/// </summary>
public interface IAtitudeRepository
{
    Task AdicionarAsync(AtitudeRegistro registro, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<AtitudeRegistro>> ListarPorLideradoAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken);
}

