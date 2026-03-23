using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Expectativas;
using PeopleManagement.Infrastructure.Persistence;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class RemoverExpectativasHandler : IStorageCommandHandler<RemoverExpectativasCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;

    public RemoverExpectativasHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StorageUnit> HandleAsync(RemoverExpectativasCommand command, CancellationToken cancellationToken)
    {
        var lideradoIdStr = command.LideradoId.ToString().ToLowerInvariant();
        var dataStr = command.Data.ToString("yyyy-MM-dd");

        var existente = await _dbContext.Expectativas
            .FirstOrDefaultAsync(
                x => x.IdLiderado.ToLower() == lideradoIdStr
                     && x.Data == dataStr,
                cancellationToken);

        if (existente is not null)
        {
            _dbContext.Expectativas.Remove(existente);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return new StorageUnit();
    }
}

