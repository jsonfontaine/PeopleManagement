using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.SituacaoAtual;

public sealed record VerificarExistenciaLideradoSituacaoAtualQuery(Guid LideradoId) : IStorageCommand<bool>;

