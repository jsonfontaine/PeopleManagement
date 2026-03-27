using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Dicas;

public sealed record SalvarDicasCommand(string ConteudoHtml) : IStorageCommand<StorageUnit>;

