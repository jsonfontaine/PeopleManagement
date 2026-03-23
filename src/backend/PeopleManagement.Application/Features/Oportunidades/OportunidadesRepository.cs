using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Oportunidades;

public sealed class OportunidadesRepository : IOportunidadesRepository
{
    private readonly IStorageCommandBus _storageCommandBus;

    public OportunidadesRepository(IStorageCommandBus storageCommandBus)
    {
        _storageCommandBus = storageCommandBus;
    }

    public Task<IReadOnlyCollection<OportunidadesRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ListarOportunidadesQuery(lideradoId), cancellationToken);

    public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new VerificarExistenciaLideradoOportunidadesQuery(lideradoId), cancellationToken);

    public Task UpsertAsync(OportunidadesRegistro registro, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new SalvarOportunidadesCommand(registro), cancellationToken);

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new RemoverOportunidadesCommand(lideradoId, data), cancellationToken);
}

