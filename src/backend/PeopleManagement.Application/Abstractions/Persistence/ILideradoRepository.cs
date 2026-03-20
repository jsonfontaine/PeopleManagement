using PeopleManagement.Domain.Liderados;

namespace PeopleManagement.Application.Abstractions.Persistence;

/// <summary>
/// Porta de persistencia do agregado Liderado.
/// </summary>
public interface ILideradoRepository
{
    Task<bool> ExistePorNomeAsync(string nome, CancellationToken cancellationToken);

    Task AdicionarAsync(Liderado liderado, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Liderado>> ListarAsync(CancellationToken cancellationToken);

    Task<Liderado?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);
}

