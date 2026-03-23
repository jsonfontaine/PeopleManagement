using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.SituacaoAtual;

public sealed class SituacaoAtualRepository : ISituacaoAtualRepository
{
    private readonly IStorageCommandBus _storageCommandBus;

    public SituacaoAtualRepository(IStorageCommandBus storageCommandBus)
    {
        _storageCommandBus = storageCommandBus;
    }

    public Task<IReadOnlyCollection<SituacaoAtualRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ListarSituacaoAtualQuery(lideradoId), cancellationToken);

    public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new VerificarExistenciaLideradoSituacaoAtualQuery(lideradoId), cancellationToken);

    public Task UpsertAsync(SituacaoAtualRegistro registro, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new SalvarSituacaoAtualCommand(registro), cancellationToken);

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new RemoverSituacaoAtualCommand(lideradoId, data), cancellationToken);
}

