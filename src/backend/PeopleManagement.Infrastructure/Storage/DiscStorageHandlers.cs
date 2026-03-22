using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Disc;
using PeopleManagement.Infrastructure.Persistence;
using PeopleManagement.Infrastructure.Persistence.Entities;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class ListarDiscHandler : IStorageCommandHandler<ListarDiscQuery, IReadOnlyCollection<DiscRegistro>>
{
    private readonly PeopleManagementDbContext _dbContext;

    public ListarDiscHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<DiscRegistro>> HandleAsync(ListarDiscQuery command, CancellationToken cancellationToken)
    {
        var lideradoIdStr = command.LideradoId.ToString().ToLowerInvariant();

        return await _dbContext.Discs
            .AsNoTracking()
            .Where(d => d.IdLiderado.ToLower() == lideradoIdStr)
            .OrderByDescending(d => d.Data)
            .Select(d => new DiscRegistro(Guid.Parse(d.IdLiderado), DateOnly.Parse(d.Data), d.Valor))
            .ToArrayAsync(cancellationToken);
    }
}

public sealed class VerificarExistenciaLideradoDiscHandler : IStorageCommandHandler<VerificarExistenciaLideradoDiscQuery, bool>
{
    private readonly PeopleManagementDbContext _dbContext;

    public VerificarExistenciaLideradoDiscHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> HandleAsync(VerificarExistenciaLideradoDiscQuery command, CancellationToken cancellationToken)
    {
        var lideradoIdStr = command.LideradoId.ToString().ToLowerInvariant();
        return _dbContext.Liderados.AsNoTracking().AnyAsync(x => x.Id.ToLower() == lideradoIdStr, cancellationToken);
    }
}

public sealed class SalvarDiscHandler : IStorageCommandHandler<SalvarDiscCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;

    public SalvarDiscHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StorageUnit> HandleAsync(SalvarDiscCommand command, CancellationToken cancellationToken)
    {
        var lideradoIdStr = command.Registro.LideradoId.ToString().ToLowerInvariant();
        var dataStr = command.Registro.Data.ToString("yyyy-MM-dd");

        var existente = await _dbContext.Discs
            .FirstOrDefaultAsync(d => d.IdLiderado.ToLower() == lideradoIdStr && d.Data == dataStr, cancellationToken);

        if (existente is null)
        {
            _dbContext.Discs.Add(new DiscEntity
            {
                IdLiderado = lideradoIdStr,
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

public sealed class RemoverDiscHandler : IStorageCommandHandler<RemoverDiscCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;

    public RemoverDiscHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<StorageUnit> HandleAsync(RemoverDiscCommand command, CancellationToken cancellationToken)
    {
        var lideradoIdStr = command.LideradoId.ToString().ToLowerInvariant();
        var dataStr = command.Data.ToString("yyyy-MM-dd");

        var existente = await _dbContext.Discs
            .FirstOrDefaultAsync(d => d.IdLiderado.ToLower() == lideradoIdStr && d.Data == dataStr, cancellationToken);

        if (existente is not null)
        {
            _dbContext.Discs.Remove(existente);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return new StorageUnit();
    }
}

