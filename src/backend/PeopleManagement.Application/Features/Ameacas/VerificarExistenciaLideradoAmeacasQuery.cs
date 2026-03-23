using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Ameacas;

public sealed record VerificarExistenciaLideradoAmeacasQuery(Guid LideradoId) : IStorageCommand<bool>;

