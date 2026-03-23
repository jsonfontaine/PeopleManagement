using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.NineBox;
using PeopleManagement.Infrastructure.Persistence;
using PeopleManagement.Infrastructure.Persistence.Entities;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class ListarNineBoxHandler
    : IStorageCommandHandler<ListarNineBoxQuery, IReadOnlyCollection<NineBoxRegistro>>
{
    private readonly PeopleManagementDbContext _dbContext;
    public ListarNineBoxHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;

    public async Task<IReadOnlyCollection<NineBoxRegistro>> HandleAsync(
        ListarNineBoxQuery command, CancellationToken cancellationToken)
    {
        var idStr = command.LideradoId.ToString().ToLowerInvariant();
        return await _dbContext.NineBoxes
            .AsNoTracking()
            .Where(x => x.IdLiderado.ToLower() == idStr)
            .OrderByDescending(x => x.Data)
            .Select(x => new NineBoxRegistro(Guid.Parse(x.IdLiderado), DateOnly.Parse(x.Data), x.Valor))
            .ToArrayAsync(cancellationToken);
    }
}

public sealed class VerificarExistenciaLideradoNineBoxHandler
    : IStorageCommandHandler<VerificarExistenciaLideradoNineBoxQuery, bool>
{
    private readonly PeopleManagementDbContext _dbContext;
    public VerificarExistenciaLideradoNineBoxHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;

    public Task<bool> HandleAsync(VerificarExistenciaLideradoNineBoxQuery command, CancellationToken cancellationToken)
    {
        var idStr = command.LideradoId.ToString().ToLowerInvariant();
        return _dbContext.Liderados.AsNoTracking().AnyAsync(x => x.Id.ToLower() == idStr, cancellationToken);
    }
}

public sealed class SalvarNineBoxHandler
    : IStorageCommandHandler<SalvarNineBoxCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;
    public SalvarNineBoxHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;

    public async Task<StorageUnit> HandleAsync(SalvarNineBoxCommand command, CancellationToken cancellationToken)
    {
        var idStr = command.Registro.LideradoId.ToString().ToLowerInvariant();
        var dataStr = command.Registro.Data.ToString("yyyy-MM-dd");

        var existente = await _dbContext.NineBoxes
            .FirstOrDefaultAsync(x => x.IdLiderado.ToLower() == idStr && x.Data == dataStr, cancellationToken);

        if (existente is null)
        {
            _dbContext.NineBoxes.Add(new NineBoxEntity
            {
                IdLiderado = idStr,
                Data = dataStr,
                Valor = command.Registro.Valor
            });
        }
        else
        {
            existente.Valor = command.Registro.Valor;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new StorageUnit();
    }
}

public sealed class RemoverNineBoxHandler
    : IStorageCommandHandler<RemoverNineBoxCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;
    public RemoverNineBoxHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;

    public async Task<StorageUnit> HandleAsync(RemoverNineBoxCommand command, CancellationToken cancellationToken)
    {
        var idStr = command.LideradoId.ToString().ToLowerInvariant();
        var dataStr = command.Data.ToString("yyyy-MM-dd");

        var existente = await _dbContext.NineBoxes
            .FirstOrDefaultAsync(x => x.IdLiderado.ToLower() == idStr && x.Data == dataStr, cancellationToken);

        if (existente is not null)
        {
            _dbContext.NineBoxes.Remove(existente);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return new StorageUnit();
    }
}

