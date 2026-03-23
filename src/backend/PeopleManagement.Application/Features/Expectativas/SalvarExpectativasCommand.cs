using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Expectativas;

public sealed record SalvarExpectativasCommand(ExpectativasRegistro Registro) : IStorageCommand<StorageUnit>;

