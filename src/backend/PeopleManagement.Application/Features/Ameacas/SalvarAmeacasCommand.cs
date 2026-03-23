using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Ameacas;

public sealed record SalvarAmeacasCommand(AmeacasRegistro Registro) : IStorageCommand<StorageUnit>;

