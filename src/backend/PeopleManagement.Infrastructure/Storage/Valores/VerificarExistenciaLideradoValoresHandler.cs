using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Valores;
using PeopleManagement.Infrastructure.Persistence;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class VerificarExistenciaLideradoValoresHandler : IStorageCommandHandler<VerificarExistenciaLideradoValoresQuery, bool>
{
    private readonly PeopleManagementDbContext _dbContext;

    public VerificarExistenciaLideradoValoresHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> HandleAsync(VerificarExistenciaLideradoValoresQuery command, CancellationToken cancellationToken)
    {
        var lideradoIdStr = command.LideradoId.ToString().ToLowerInvariant();
        return _dbContext.Liderados.AsNoTracking().AnyAsync(x => x.Id.ToLower() == lideradoIdStr, cancellationToken);
    }
}

