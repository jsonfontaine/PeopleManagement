using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Dicas;

public sealed record ObterDicasQuery : IStorageCommand<DicasRegistro?>;

