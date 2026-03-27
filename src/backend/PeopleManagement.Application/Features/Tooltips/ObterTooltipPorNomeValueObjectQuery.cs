using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Tooltips;

public sealed record ObterTooltipPorNomeValueObjectQuery(string Nome, string ValueObject) : IStorageCommand<TooltipPropriedadeRegistro?>;

