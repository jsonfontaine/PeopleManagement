using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Oportunidades;

public sealed record RemoverOportunidadesCommand(Guid LideradoId, DateOnly Data) : IStorageCommand<StorageUnit>;

