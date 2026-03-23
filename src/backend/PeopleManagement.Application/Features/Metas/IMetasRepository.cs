namespace PeopleManagement.Application.Features.Metas;

public interface IMetasRepository
{
    Task<IReadOnlyCollection<MetasRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task UpsertAsync(MetasRegistro registro, CancellationToken cancellationToken);
    Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken);
}

