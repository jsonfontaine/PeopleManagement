using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Fraquezas;

public sealed class FraquezasRepository : IFraquezasRepository
{
    private readonly IStorageCommandBus _storageCommandBus;

    public FraquezasRepository(IStorageCommandBus storageCommandBus)
    {
        _storageCommandBus = storageCommandBus;
    }

    public Task<IReadOnlyCollection<FraquezasRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ListarFraquezasQuery(lideradoId), cancellationToken);

    public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new VerificarExistenciaLideradoFraquezasQuery(lideradoId), cancellationToken);

    public Task UpsertAsync(FraquezasRegistro registro, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new SalvarFraquezasCommand(registro), cancellationToken);

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new RemoverFraquezasCommand(lideradoId, data), cancellationToken);
}

