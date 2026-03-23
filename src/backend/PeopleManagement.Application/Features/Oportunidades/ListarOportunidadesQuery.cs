using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Oportunidades;

public sealed record ListarOportunidadesQuery(Guid LideradoId) : IStorageCommand<IReadOnlyCollection<OportunidadesRegistro>>;

