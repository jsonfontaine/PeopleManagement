using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Atitudes;
using PeopleManagement.Infrastructure.Persistence;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class RemoverAtitudesHandler : IStorageCommandHandler<RemoverAtitudesCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;

    public RemoverAtitudesHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StorageUnit> HandleAsync(RemoverAtitudesCommand command, CancellationToken cancellationToken)
    {
        var lideradoIdStr = command.LideradoId.ToString().ToLowerInvariant();
        var dataStr = command.Data.ToString("yyyy-MM-dd");

        var existente = await _dbContext.Atitudes
            .FirstOrDefaultAsync(
                x => x.IdLiderado.ToLower() == lideradoIdStr
                     && x.Data == dataStr,
                cancellationToken);

        if (existente is not null)
        {
            _dbContext.Atitudes.Remove(existente);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return new StorageUnit();
    }
}

