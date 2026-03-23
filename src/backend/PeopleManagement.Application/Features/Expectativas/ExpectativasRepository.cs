using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Expectativas;

public sealed class ExpectativasRepository : IExpectativasRepository
{
    private readonly IStorageCommandBus _storageCommandBus;

    public ExpectativasRepository(IStorageCommandBus storageCommandBus)
    {
        _storageCommandBus = storageCommandBus;
    }

    public Task<IReadOnlyCollection<ExpectativasRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        return _storageCommandBus.ExecuteAsync(new ListarExpectativasQuery(lideradoId), cancellationToken);
    }

    public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        return _storageCommandBus.ExecuteAsync(new VerificarExistenciaLideradoExpectativasQuery(lideradoId), cancellationToken);
    }

    public Task UpsertAsync(ExpectativasRegistro registro, CancellationToken cancellationToken)
    {
        return _storageCommandBus.ExecuteAsync(new SalvarExpectativasCommand(registro), cancellationToken);
    }

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
    {
        return _storageCommandBus.ExecuteAsync(new RemoverExpectativasCommand(lideradoId, data), cancellationToken);
    }
}

