using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Valores;

public sealed class ValoresRepository : IValoresRepository
{
    private readonly IStorageCommandBus _storageCommandBus;

    public ValoresRepository(IStorageCommandBus storageCommandBus)
    {
        _storageCommandBus = storageCommandBus;
    }

    public Task<IReadOnlyCollection<ValoresRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        return _storageCommandBus.ExecuteAsync(new ListarValoresQuery(lideradoId), cancellationToken);
    }

    public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        return _storageCommandBus.ExecuteAsync(new VerificarExistenciaLideradoValoresQuery(lideradoId), cancellationToken);
    }

    public Task UpsertAsync(ValoresRegistro registro, CancellationToken cancellationToken)
    {
        return _storageCommandBus.ExecuteAsync(new SalvarValoresCommand(registro), cancellationToken);
    }

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
    {
        return _storageCommandBus.ExecuteAsync(new RemoverValoresCommand(lideradoId, data), cancellationToken);
    }
}

