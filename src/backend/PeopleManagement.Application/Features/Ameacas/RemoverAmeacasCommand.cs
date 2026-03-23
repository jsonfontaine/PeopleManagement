using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Ameacas;

public sealed record RemoverAmeacasCommand(Guid LideradoId, DateOnly Data) : IStorageCommand<StorageUnit>;

