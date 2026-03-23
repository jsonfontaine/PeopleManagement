using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Opcoes;

public sealed record VerificarExistenciaLideradoOpcoesQuery(Guid LideradoId) : IStorageCommand<bool>;

