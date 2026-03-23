using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Atitudes;

public sealed record VerificarExistenciaLideradoAtitudesQuery(Guid LideradoId) : IStorageCommand<bool>;

