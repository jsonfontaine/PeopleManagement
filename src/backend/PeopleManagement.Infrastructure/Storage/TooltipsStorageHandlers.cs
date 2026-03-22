using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Tooltips;
using PeopleManagement.Infrastructure.Persistence;
using PeopleManagement.Infrastructure.Persistence.Entities;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class ObterTooltipHandler : IStorageCommandHandler<ObterTooltipQuery, TooltipResponse?>
{
    private readonly PeopleManagementDbContext _dbContext;

    public ObterTooltipHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TooltipResponse?> HandleAsync(ObterTooltipQuery command, CancellationToken cancellationToken)
    {
        var tooltip = await _dbContext.Tooltips
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ChaveCampo == command.ChaveCampo, cancellationToken);

        return tooltip is null ? null : new TooltipResponse(tooltip.ChaveCampo, tooltip.Texto);
    }
}

public sealed class SalvarTooltipHandler : IStorageCommandHandler<SalvarTooltipCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;

    public SalvarTooltipHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StorageUnit> HandleAsync(SalvarTooltipCommand command, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Tooltips
            .FirstOrDefaultAsync(x => x.ChaveCampo == command.ChaveCampo, cancellationToken);

        if (entity is null)
        {
            entity = new TooltipEntity { ChaveCampo = command.ChaveCampo };
            _dbContext.Tooltips.Add(entity);
        }

        entity.Texto = command.Texto.Trim();
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new StorageUnit();
    }
}

