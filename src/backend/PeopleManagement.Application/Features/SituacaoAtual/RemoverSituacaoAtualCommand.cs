using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.SituacaoAtual;

public sealed record RemoverSituacaoAtualCommand(Guid LideradoId, DateOnly Data) : IStorageCommand<StorageUnit>;

