using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Fortalezas;

public sealed record SalvarFortalezasCommand(FortalezasRegistro Registro) : IStorageCommand<StorageUnit>;

