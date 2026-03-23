using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Valores;
using PeopleManagement.Infrastructure.Persistence;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class ListarValoresHandler : IStorageCommandHandler<ListarValoresQuery, IReadOnlyCollection<ValoresRegistro>>
{
    private readonly PeopleManagementDbContext _dbContext;

    public ListarValoresHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<ValoresRegistro>> HandleAsync(ListarValoresQuery command, CancellationToken cancellationToken)
    {
        var lideradoIdStr = command.LideradoId.ToString().ToLowerInvariant();
        return await _dbContext.Valores
            .AsNoTracking()
            .Where(x => x.IdLiderado.ToLower() == lideradoIdStr)
            .OrderByDescending(x => x.Data)
            .Select(x => new ValoresRegistro(Guid.Parse(x.IdLiderado), DateOnly.Parse(x.Data), x.Valor))
            .ToArrayAsync(cancellationToken);
    }
}

