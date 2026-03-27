using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Tooltips;

public sealed record ListarTooltipsQuery : IStorageCommand<IReadOnlyCollection<TooltipPropriedadeRegistro>>;

