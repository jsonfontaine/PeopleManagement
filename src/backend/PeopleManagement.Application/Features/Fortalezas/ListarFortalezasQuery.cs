using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Fortalezas;

public sealed record ListarFortalezasQuery(Guid LideradoId) : IStorageCommand<IReadOnlyCollection<FortalezasRegistro>>;

