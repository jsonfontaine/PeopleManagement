using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Abstractions.Persistence;

/// <summary>
/// Porta de persistencia para registros de habilidade.
/// </summary>
public interface IHabilidadeRepository
{
    Task AdicionarAsync(HabilidadeRegistro registro, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<HabilidadeRegistro>> ListarPorLideradoAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken);
}

