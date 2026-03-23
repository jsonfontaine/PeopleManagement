using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Expectativas;
using PeopleManagement.Infrastructure.Persistence;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class ListarExpectativasHandler : IStorageCommandHandler<ListarExpectativasQuery, IReadOnlyCollection<ExpectativasRegistro>>
{
    private readonly PeopleManagementDbContext _dbContext;

    public ListarExpectativasHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<ExpectativasRegistro>> HandleAsync(ListarExpectativasQuery command, CancellationToken cancellationToken)
    {
        var lideradoIdStr = command.LideradoId.ToString().ToLowerInvariant();
        return await _dbContext.Expectativas
            .AsNoTracking()
            .Where(x => x.IdLiderado.ToLower() == lideradoIdStr)
            .OrderByDescending(x => x.Data)
            .Select(x => new ExpectativasRegistro(Guid.Parse(x.IdLiderado), DateOnly.Parse(x.Data), x.Valor))
            .ToArrayAsync(cancellationToken);
    }
}

