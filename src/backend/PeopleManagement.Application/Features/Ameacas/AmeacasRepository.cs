using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Ameacas;

public sealed class AmeacasRepository : IAmeacasRepository
{
    private readonly IStorageCommandBus _storageCommandBus;

    public AmeacasRepository(IStorageCommandBus storageCommandBus)
    {
        _storageCommandBus = storageCommandBus;
    }

    public Task<IReadOnlyCollection<AmeacasRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ListarAmeacasQuery(lideradoId), cancellationToken);

    public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new VerificarExistenciaLideradoAmeacasQuery(lideradoId), cancellationToken);

    public Task UpsertAsync(AmeacasRegistro registro, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new SalvarAmeacasCommand(registro), cancellationToken);

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new RemoverAmeacasCommand(lideradoId, data), cancellationToken);
}

