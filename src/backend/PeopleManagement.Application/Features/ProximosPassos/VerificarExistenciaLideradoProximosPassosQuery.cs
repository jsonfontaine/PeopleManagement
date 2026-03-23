using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.ProximosPassos;

public sealed record VerificarExistenciaLideradoProximosPassosQuery(Guid LideradoId) : IStorageCommand<bool>;

