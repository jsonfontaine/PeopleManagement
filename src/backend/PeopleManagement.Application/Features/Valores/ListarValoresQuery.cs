using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Valores;

public sealed record ListarValoresQuery(Guid LideradoId) : IStorageCommand<IReadOnlyCollection<ValoresRegistro>>;

