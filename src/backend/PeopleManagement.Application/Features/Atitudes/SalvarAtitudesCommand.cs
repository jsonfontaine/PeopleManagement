using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Atitudes;

public sealed record SalvarAtitudesCommand(AtitudesRegistro Registro) : IStorageCommand<StorageUnit>;

