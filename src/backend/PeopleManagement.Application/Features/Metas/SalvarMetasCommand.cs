using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Metas;

public sealed record SalvarMetasCommand(MetasRegistro Registro) : IStorageCommand<StorageUnit>;

