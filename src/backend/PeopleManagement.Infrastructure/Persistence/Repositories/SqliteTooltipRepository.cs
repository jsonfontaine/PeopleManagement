using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Infrastructure.Persistence.Entities;

namespace PeopleManagement.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repositorio persistente de tooltips.
/// </summary>
public sealed class SqliteTooltipRepository : ITooltipRepository
{
    private readonly PeopleManagementDbContext _dbContext;

    public SqliteTooltipRepository(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TooltipRegistro?> ObterPorChaveAsync(string chaveCampo, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Tooltips.AsNoTracking().FirstOrDefaultAsync(x => x.ChaveCampo == chaveCampo, cancellationToken);
        return entity is null ? null : new TooltipRegistro(entity.ChaveCampo, entity.Texto);
    }

    public async Task SalvarAsync(TooltipRegistro registro, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Tooltips.FirstOrDefaultAsync(x => x.ChaveCampo == registro.ChaveCampo, cancellationToken);
        if (entity is null)
        {
            _dbContext.Tooltips.Add(new TooltipEntity
            {
                ChaveCampo = registro.ChaveCampo,
                Texto = registro.Texto
            });
        }
        else
        {
            entity.Texto = registro.Texto;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

