using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Metas;

public sealed record RemoverMetasCommand(Guid LideradoId, DateOnly Data) : IStorageCommand<StorageUnit>;

