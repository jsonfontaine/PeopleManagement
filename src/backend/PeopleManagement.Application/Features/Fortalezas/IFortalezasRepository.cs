namespace PeopleManagement.Application.Features.Fortalezas;

public interface IFortalezasRepository
{
    Task<IReadOnlyCollection<FortalezasRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task UpsertAsync(FortalezasRegistro registro, CancellationToken cancellationToken);
    Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken);
}

