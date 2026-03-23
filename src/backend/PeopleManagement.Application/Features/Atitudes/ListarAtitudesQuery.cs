using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Atitudes;

public sealed record ListarAtitudesQuery(Guid LideradoId) : IStorageCommand<IReadOnlyCollection<AtitudesRegistro>>;

