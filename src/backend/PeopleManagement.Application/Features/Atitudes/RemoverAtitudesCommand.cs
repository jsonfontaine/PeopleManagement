using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Atitudes;

public sealed record RemoverAtitudesCommand(Guid LideradoId, DateOnly Data) : IStorageCommand<StorageUnit>;

