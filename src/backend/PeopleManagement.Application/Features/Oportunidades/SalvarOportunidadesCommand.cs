using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Oportunidades;

public sealed record SalvarOportunidadesCommand(OportunidadesRegistro Registro) : IStorageCommand<StorageUnit>;

