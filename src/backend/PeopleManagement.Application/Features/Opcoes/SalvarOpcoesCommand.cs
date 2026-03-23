using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Opcoes;

public sealed record SalvarOpcoesCommand(OpcoesRegistro Registro) : IStorageCommand<StorageUnit>;

