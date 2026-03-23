using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Ameacas;

public sealed record ListarAmeacasQuery(Guid LideradoId) : IStorageCommand<IReadOnlyCollection<AmeacasRegistro>>;

