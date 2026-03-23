using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.ProximosPassos;

public sealed record SalvarProximosPassosCommand(ProximosPassosRegistro Registro) : IStorageCommand<StorageUnit>;

