using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Tooltips;
using PeopleManagement.Infrastructure.Persistence;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class ObterTooltipPorNomeValueObjectHandler : IStorageCommandHandler<ObterTooltipPorNomeValueObjectQuery, TooltipPropriedadeRegistro?>
{
    private readonly PeopleManagementDbContext _dbContext;

    public ObterTooltipPorNomeValueObjectHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TooltipPropriedadeRegistro?> HandleAsync(ObterTooltipPorNomeValueObjectQuery command, CancellationToken cancellationToken)
    {
        var nome = command.Nome.Trim();
        var valueObject = command.ValueObject.Trim();

        return await _dbContext.Propriedades
            .AsNoTracking()
            .Where(x => x.Nome == nome && x.ValueObject == valueObject)
            .Select(x => new TooltipPropriedadeRegistro(x.Nome, x.ValueObject, x.Tooltip))
            .FirstOrDefaultAsync(cancellationToken);
    }
}

