using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Metas;

public sealed record ListarMetasQuery(Guid LideradoId) : IStorageCommand<IReadOnlyCollection<MetasRegistro>>;

