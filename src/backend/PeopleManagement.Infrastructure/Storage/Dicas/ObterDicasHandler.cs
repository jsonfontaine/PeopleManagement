using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Dicas;
using PeopleManagement.Infrastructure.Persistence;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class ObterDicasHandler : IStorageCommandHandler<ObterDicasQuery, DicasRegistro?>
{
    private readonly PeopleManagementDbContext _dbContext;

    public ObterDicasHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DicasRegistro?> HandleAsync(ObterDicasQuery command, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Dicas
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        return entity is null ? null : new DicasRegistro(entity.ConteudoHtml);
    }
}

