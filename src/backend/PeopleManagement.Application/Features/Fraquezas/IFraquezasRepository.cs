namespace PeopleManagement.Application.Features.Fraquezas;

public interface IFraquezasRepository
{
    Task<IReadOnlyCollection<FraquezasRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task UpsertAsync(FraquezasRegistro registro, CancellationToken cancellationToken);
    Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken);
}

