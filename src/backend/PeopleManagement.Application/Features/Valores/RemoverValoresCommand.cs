using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Valores;

public sealed record RemoverValoresCommand(Guid LideradoId, DateOnly Data) : IStorageCommand<StorageUnit>;

