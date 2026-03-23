using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Valores;

public sealed record VerificarExistenciaLideradoValoresQuery(Guid LideradoId) : IStorageCommand<bool>;

