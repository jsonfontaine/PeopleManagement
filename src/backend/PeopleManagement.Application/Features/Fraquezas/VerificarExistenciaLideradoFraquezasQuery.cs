using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Fraquezas;

public sealed record VerificarExistenciaLideradoFraquezasQuery(Guid LideradoId) : IStorageCommand<bool>;

