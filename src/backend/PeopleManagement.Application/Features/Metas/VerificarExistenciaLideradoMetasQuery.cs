using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Metas;

public sealed record VerificarExistenciaLideradoMetasQuery(Guid LideradoId) : IStorageCommand<bool>;

