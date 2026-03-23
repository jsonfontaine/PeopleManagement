using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.SituacaoAtual;

public sealed record ListarSituacaoAtualQuery(Guid LideradoId) : IStorageCommand<IReadOnlyCollection<SituacaoAtualRegistro>>;

