namespace PeopleManagement.Application.Features.Expectativas;

public interface IExpectativasRepository
{
    Task<IReadOnlyCollection<ExpectativasRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task UpsertAsync(ExpectativasRegistro registro, CancellationToken cancellationToken);
    Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken);
}

