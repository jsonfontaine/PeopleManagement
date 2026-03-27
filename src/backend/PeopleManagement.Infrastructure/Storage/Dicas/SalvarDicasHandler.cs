using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Dicas;
using PeopleManagement.Infrastructure.Persistence;
using PeopleManagement.Infrastructure.Persistence.Entities;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class SalvarDicasHandler : IStorageCommandHandler<SalvarDicasCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;

    public SalvarDicasHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StorageUnit> HandleAsync(SalvarDicasCommand command, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Dicas
            .FirstOrDefaultAsync(x => x.Id == 1, cancellationToken);

        if (entity is null)
        {
            _dbContext.Dicas.Add(new DicaEntity
            {
                Id = 1,
                ConteudoHtml = command.ConteudoHtml
            });
        }
        else
        {
            entity.ConteudoHtml = command.ConteudoHtml;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new StorageUnit();
    }
}

