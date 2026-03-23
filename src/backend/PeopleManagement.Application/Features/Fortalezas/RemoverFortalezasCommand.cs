using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Fortalezas;

public sealed record RemoverFortalezasCommand(Guid LideradoId, DateOnly Data) : IStorageCommand<StorageUnit>;

