using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Metas;

public sealed class MetasRepository : IMetasRepository
{
    private readonly IStorageCommandBus _storageCommandBus;

    public MetasRepository(IStorageCommandBus storageCommandBus)
    {
        _storageCommandBus = storageCommandBus;
    }

    public Task<IReadOnlyCollection<MetasRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ListarMetasQuery(lideradoId), cancellationToken);

    public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new VerificarExistenciaLideradoMetasQuery(lideradoId), cancellationToken);

    public Task UpsertAsync(MetasRegistro registro, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new SalvarMetasCommand(registro), cancellationToken);

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new RemoverMetasCommand(lideradoId, data), cancellationToken);
}

