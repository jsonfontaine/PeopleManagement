using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Fortalezas;

public sealed record VerificarExistenciaLideradoFortalezasQuery(Guid LideradoId) : IStorageCommand<bool>;

