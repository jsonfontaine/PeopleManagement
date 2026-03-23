using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Oportunidades;

public sealed record VerificarExistenciaLideradoOportunidadesQuery(Guid LideradoId) : IStorageCommand<bool>;

