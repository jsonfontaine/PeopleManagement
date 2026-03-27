using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Tooltips;
using PeopleManagement.Infrastructure.Persistence;
using PeopleManagement.Infrastructure.Persistence.Entities;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class SalvarTooltipHandler : IStorageCommandHandler<SalvarTooltipCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;

    public SalvarTooltipHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StorageUnit> HandleAsync(SalvarTooltipCommand command, CancellationToken cancellationToken)
    {
        var nome = command.Registro.Nome.Trim();
        var valueObject = command.Registro.ValueObject.Trim();

        var existente = await _dbContext.Propriedades
            .FirstOrDefaultAsync(x => x.Nome == nome && x.ValueObject == valueObject, cancellationToken);

        if (existente is null)
        {
            _dbContext.Propriedades.Add(new TooltipEntity
            {
                Nome = nome,
                ValueObject = valueObject,
                Tooltip = command.Registro.Tooltip
            });
        }
        else
        {
            existente.Tooltip = command.Registro.Tooltip;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new StorageUnit();
    }
}

