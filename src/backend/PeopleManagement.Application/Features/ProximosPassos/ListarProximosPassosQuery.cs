using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.ProximosPassos;

public sealed record ListarProximosPassosQuery(Guid LideradoId) : IStorageCommand<IReadOnlyCollection<ProximosPassosRegistro>>;

