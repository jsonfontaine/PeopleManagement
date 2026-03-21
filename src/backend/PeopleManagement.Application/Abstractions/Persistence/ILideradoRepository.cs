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

    // DISC Value Object
    Task SalvarDiscAsync(Guid lideradoId, string valor, DateOnly data);
    Task<List<(string Valor, DateOnly Data)>> ListarDiscsAsync(Guid lideradoId);
    Task RemoverDiscAsync(Guid lideradoId, DateOnly data);

    // Update and Delete
    Task AtualizarAsync(Liderado liderado, CancellationToken cancellationToken);
    Task RemoverAsync(Guid id, CancellationToken cancellationToken);
}
