using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Expectativas;

public sealed record VerificarExistenciaLideradoExpectativasQuery(Guid LideradoId) : IStorageCommand<bool>;

