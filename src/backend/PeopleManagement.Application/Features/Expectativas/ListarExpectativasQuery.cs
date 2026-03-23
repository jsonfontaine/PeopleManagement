using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Expectativas;

public sealed record ListarExpectativasQuery(Guid LideradoId) : IStorageCommand<IReadOnlyCollection<ExpectativasRegistro>>;

