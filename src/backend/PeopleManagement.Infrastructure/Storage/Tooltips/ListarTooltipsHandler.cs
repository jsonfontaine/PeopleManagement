using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Tooltips;
using PeopleManagement.Infrastructure.Persistence;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class ListarTooltipsHandler : IStorageCommandHandler<ListarTooltipsQuery, IReadOnlyCollection<TooltipPropriedadeRegistro>>
{
    private readonly PeopleManagementDbContext _dbContext;

    public ListarTooltipsHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<TooltipPropriedadeRegistro>> HandleAsync(ListarTooltipsQuery command, CancellationToken cancellationToken)
    {
        return await _dbContext.Propriedades
            .AsNoTracking()
            .OrderBy(x => x.Nome)
            .ThenBy(x => x.ValueObject)
            .Select(x => new TooltipPropriedadeRegistro(x.Nome, x.ValueObject, x.Tooltip))
            .ToArrayAsync(cancellationToken);
    }
}

