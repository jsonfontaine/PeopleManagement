using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Atitudes;
using PeopleManagement.Infrastructure.Persistence;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class VerificarExistenciaLideradoAtitudesHandler : IStorageCommandHandler<VerificarExistenciaLideradoAtitudesQuery, bool>
{
    private readonly PeopleManagementDbContext _dbContext;

    public VerificarExistenciaLideradoAtitudesHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> HandleAsync(VerificarExistenciaLideradoAtitudesQuery command, CancellationToken cancellationToken)
    {
        var lideradoIdStr = command.LideradoId.ToString().ToLowerInvariant();
        return _dbContext.Liderados.AsNoTracking().AnyAsync(x => x.Id.ToLower() == lideradoIdStr, cancellationToken);
    }
}

