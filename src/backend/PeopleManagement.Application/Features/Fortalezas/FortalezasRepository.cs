using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Fortalezas;

public sealed class FortalezasRepository : IFortalezasRepository
{
    private readonly IStorageCommandBus _storageCommandBus;

    public FortalezasRepository(IStorageCommandBus storageCommandBus)
    {
        _storageCommandBus = storageCommandBus;
    }

    public Task<IReadOnlyCollection<FortalezasRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ListarFortalezasQuery(lideradoId), cancellationToken);

    public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new VerificarExistenciaLideradoFortalezasQuery(lideradoId), cancellationToken);

    public Task UpsertAsync(FortalezasRegistro registro, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new SalvarFortalezasCommand(registro), cancellationToken);

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new RemoverFortalezasCommand(lideradoId, data), cancellationToken);
}

