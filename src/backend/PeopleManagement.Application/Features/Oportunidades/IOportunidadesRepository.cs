namespace PeopleManagement.Application.Features.Oportunidades;

public interface IOportunidadesRepository
{
    Task<IReadOnlyCollection<OportunidadesRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task UpsertAsync(OportunidadesRegistro registro, CancellationToken cancellationToken);
    Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken);
}

