using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Fraquezas;

public sealed record RemoverFraquezasCommand(Guid LideradoId, DateOnly Data) : IStorageCommand<StorageUnit>;

