using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Valores;

public sealed record SalvarValoresCommand(ValoresRegistro Registro) : IStorageCommand<StorageUnit>;

