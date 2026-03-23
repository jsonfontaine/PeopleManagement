namespace PeopleManagement.Application.Features.ProximosPassos;

public interface IProximosPassosRepository
{
    Task<IReadOnlyCollection<ProximosPassosRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task UpsertAsync(ProximosPassosRegistro registro, CancellationToken cancellationToken);
    Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken);
}

