using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Atitudes;
using PeopleManagement.Infrastructure.Persistence;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class ListarAtitudesHandler : IStorageCommandHandler<ListarAtitudesQuery, IReadOnlyCollection<AtitudesRegistro>>
{
    private readonly PeopleManagementDbContext _dbContext;

    public ListarAtitudesHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<AtitudesRegistro>> HandleAsync(ListarAtitudesQuery command, CancellationToken cancellationToken)
    {
        var lideradoIdStr = command.LideradoId.ToString().ToLowerInvariant();
        return await _dbContext.Atitudes
            .AsNoTracking()
            .Where(x => x.IdLiderado.ToLower() == lideradoIdStr)
            .OrderByDescending(x => x.Data)
            .Select(x => new AtitudesRegistro(Guid.Parse(x.IdLiderado), DateOnly.Parse(x.Data), x.Valor))
            .ToArrayAsync(cancellationToken);
    }
}

