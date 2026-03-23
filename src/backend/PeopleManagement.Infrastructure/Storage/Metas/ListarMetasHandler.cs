using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Metas;
using PeopleManagement.Infrastructure.Persistence;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class ListarMetasHandler : IStorageCommandHandler<ListarMetasQuery, IReadOnlyCollection<MetasRegistro>>
{
    private readonly PeopleManagementDbContext _dbContext;

    public ListarMetasHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<MetasRegistro>> HandleAsync(ListarMetasQuery command, CancellationToken cancellationToken)
    {
        var lideradoIdStr = command.LideradoId.ToString().ToLowerInvariant();
        return await _dbContext.Metas
            .AsNoTracking()
            .Where(x => x.IdLiderado.ToLower() == lideradoIdStr)
            .OrderByDescending(x => x.Data)
            .Select(x => new MetasRegistro(Guid.Parse(x.IdLiderado), DateOnly.Parse(x.Data), x.Valor))
            .ToArrayAsync(cancellationToken);
    }
}

