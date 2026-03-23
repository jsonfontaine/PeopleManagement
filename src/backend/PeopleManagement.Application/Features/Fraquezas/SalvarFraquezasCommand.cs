using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Fraquezas;

public sealed record SalvarFraquezasCommand(FraquezasRegistro Registro) : IStorageCommand<StorageUnit>;

