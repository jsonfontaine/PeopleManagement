using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Tooltips;

public sealed record SalvarTooltipCommand(TooltipPropriedadeRegistro Registro) : IStorageCommand<StorageUnit>;

