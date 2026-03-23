using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Opcoes;

public sealed record ListarOpcoesQuery(Guid LideradoId) : IStorageCommand<IReadOnlyCollection<OpcoesRegistro>>;

