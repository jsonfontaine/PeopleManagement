using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Valores;
using PeopleManagement.Infrastructure.Persistence;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class RemoverValoresHandler : IStorageCommandHandler<RemoverValoresCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;

    public RemoverValoresHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StorageUnit> HandleAsync(RemoverValoresCommand command, CancellationToken cancellationToken)
    {
        var lideradoIdStr = command.LideradoId.ToString().ToLowerInvariant();
        var dataStr = command.Data.ToString("yyyy-MM-dd");

        var existente = await _dbContext.Valores
            .FirstOrDefaultAsync(
                x => x.IdLiderado.ToLower() == lideradoIdStr
                     && x.Data == dataStr,
                cancellationToken);

        if (existente is not null)
        {
            _dbContext.Valores.Remove(existente);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return new StorageUnit();
    }
}

