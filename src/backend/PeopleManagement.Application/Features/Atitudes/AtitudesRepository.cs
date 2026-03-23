using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Atitudes;

public sealed class AtitudesRepository : IAtitudesRepository
{
    private readonly IStorageCommandBus _storageCommandBus;

    public AtitudesRepository(IStorageCommandBus storageCommandBus)
    {
        _storageCommandBus = storageCommandBus;
    }

    public Task<IReadOnlyCollection<AtitudesRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        return _storageCommandBus.ExecuteAsync(new ListarAtitudesQuery(lideradoId), cancellationToken);
    }

    public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        return _storageCommandBus.ExecuteAsync(new VerificarExistenciaLideradoAtitudesQuery(lideradoId), cancellationToken);
    }

    public Task UpsertAsync(AtitudesRegistro registro, CancellationToken cancellationToken)
    {
        return _storageCommandBus.ExecuteAsync(new SalvarAtitudesCommand(registro), cancellationToken);
    }

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
    {
        return _storageCommandBus.ExecuteAsync(new RemoverAtitudesCommand(lideradoId, data), cancellationToken);
    }
}

