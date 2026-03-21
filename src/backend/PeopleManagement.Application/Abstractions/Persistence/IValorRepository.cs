using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Abstractions.Persistence;

/// <summary>
/// Porta de persistencia para registros de valor.
/// </summary>
public interface IValorRepository
{
    Task AdicionarAsync(ValorRegistro registro, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<ValorRegistro>> ListarPorLideradoAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken);
}

