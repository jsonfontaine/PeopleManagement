using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.ProximosPassos;

public sealed class ProximosPassosRepository : IProximosPassosRepository
{
    private readonly IStorageCommandBus _storageCommandBus;

    public ProximosPassosRepository(IStorageCommandBus storageCommandBus)
    {
        _storageCommandBus = storageCommandBus;
    }

    public Task<IReadOnlyCollection<ProximosPassosRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ListarProximosPassosQuery(lideradoId), cancellationToken);

    public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new VerificarExistenciaLideradoProximosPassosQuery(lideradoId), cancellationToken);

    public Task UpsertAsync(ProximosPassosRegistro registro, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new SalvarProximosPassosCommand(registro), cancellationToken);

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new RemoverProximosPassosCommand(lideradoId, data), cancellationToken);
}

