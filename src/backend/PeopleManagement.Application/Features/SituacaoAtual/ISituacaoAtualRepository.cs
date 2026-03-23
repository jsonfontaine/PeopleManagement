namespace PeopleManagement.Application.Features.SituacaoAtual;

public interface ISituacaoAtualRepository
{
    Task<IReadOnlyCollection<SituacaoAtualRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task UpsertAsync(SituacaoAtualRegistro registro, CancellationToken cancellationToken);
    Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken);
}

