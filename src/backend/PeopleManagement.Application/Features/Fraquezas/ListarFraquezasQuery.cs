using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Fraquezas;

public sealed record ListarFraquezasQuery(Guid LideradoId) : IStorageCommand<IReadOnlyCollection<FraquezasRegistro>>;

