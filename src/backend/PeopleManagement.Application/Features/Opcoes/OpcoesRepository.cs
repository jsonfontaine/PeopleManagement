using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Opcoes;

public sealed class OpcoesRepository : IOpcoesRepository
{
    private readonly IStorageCommandBus _storageCommandBus;

    public OpcoesRepository(IStorageCommandBus storageCommandBus)
    {
        _storageCommandBus = storageCommandBus;
    }

    public Task<IReadOnlyCollection<OpcoesRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ListarOpcoesQuery(lideradoId), cancellationToken);

    public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new VerificarExistenciaLideradoOpcoesQuery(lideradoId), cancellationToken);

    public Task UpsertAsync(OpcoesRegistro registro, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new SalvarOpcoesCommand(registro), cancellationToken);

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new RemoverOpcoesCommand(lideradoId, data), cancellationToken);
}

