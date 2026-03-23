using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.ProximosPassos;

public sealed record RemoverProximosPassosCommand(Guid LideradoId, DateOnly Data) : IStorageCommand<StorageUnit>;

