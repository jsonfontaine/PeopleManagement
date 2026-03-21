using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Abstractions.Persistence;

/// <summary>
/// Porta de persistencia para registros de conhecimento.
/// </summary>
public interface IConhecimentoRepository
{
    Task AdicionarAsync(ConhecimentoRegistro registro, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<ConhecimentoRegistro>> ListarPorLideradoAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken);
}

