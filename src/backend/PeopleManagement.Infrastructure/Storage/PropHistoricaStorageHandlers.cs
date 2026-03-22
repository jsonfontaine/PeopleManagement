using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.PropHistorica;
using PeopleManagement.Infrastructure.Persistence;
using PeopleManagement.Infrastructure.Persistence.Entities;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class ListarPropHistoricaHandler
    : IStorageCommandHandler<ListarPropHistoricaQuery, IReadOnlyCollection<PropHistoricaRegistro>>
{
    private readonly PeopleManagementDbContext _dbContext;
    public ListarPropHistoricaHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;

    public async Task<IReadOnlyCollection<PropHistoricaRegistro>> HandleAsync(
        ListarPropHistoricaQuery command, CancellationToken cancellationToken)
    {
        var idStr = command.LideradoId.ToString().ToLowerInvariant();
        var tipoNorm = command.Tipo.ToLowerInvariant();
        return await _dbContext.PropriedadesHistoricas
            .AsNoTracking()
            .Where(x => x.IdLiderado.ToLower() == idStr && x.Tipo.ToLower() == tipoNorm)
            .OrderByDescending(x => x.Data)
            .Select(x => new PropHistoricaRegistro(Guid.Parse(x.IdLiderado), x.Tipo, x.Data, x.Valor))
            .ToArrayAsync(cancellationToken);
    }
}

public sealed class VerificarExistenciaLideradoPropHistoricaHandler
    : IStorageCommandHandler<VerificarExistenciaLideradoPropHistoricaQuery, bool>
{
    private readonly PeopleManagementDbContext _dbContext;
    public VerificarExistenciaLideradoPropHistoricaHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;

    public Task<bool> HandleAsync(VerificarExistenciaLideradoPropHistoricaQuery command, CancellationToken cancellationToken)
    {
        var idStr = command.LideradoId.ToString().ToLowerInvariant();
        return _dbContext.Liderados.AsNoTracking().AnyAsync(x => x.Id.ToLower() == idStr, cancellationToken);
    }
}

public sealed class SalvarPropHistoricaHandler
    : IStorageCommandHandler<SalvarPropHistoricaCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;
    public SalvarPropHistoricaHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;

    public async Task<StorageUnit> HandleAsync(SalvarPropHistoricaCommand command, CancellationToken cancellationToken)
    {
        var idStr = command.Registro.LideradoId.ToString().ToLowerInvariant();
        var tipoNorm = command.Registro.Tipo.ToLowerInvariant();

        var existente = await _dbContext.PropriedadesHistoricas
            .FirstOrDefaultAsync(
                x => x.IdLiderado.ToLower() == idStr
                     && x.Tipo.ToLower() == tipoNorm
                     && x.Data == command.Registro.Data,
                cancellationToken);

        if (existente is null)
        {
            _dbContext.PropriedadesHistoricas.Add(new PropriedadeHistoricaEntity
            {
                IdLiderado = idStr,
                Tipo = command.Registro.Tipo,
                Data = command.Registro.Data,
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

public sealed class RemoverPropHistoricaHandler
    : IStorageCommandHandler<RemoverPropHistoricaCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;
    public RemoverPropHistoricaHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;

    public async Task<StorageUnit> HandleAsync(RemoverPropHistoricaCommand command, CancellationToken cancellationToken)
    {
        var idStr = command.LideradoId.ToString().ToLowerInvariant();
        var tipoNorm = command.Tipo.ToLowerInvariant();
        var entities = await _dbContext.PropriedadesHistoricas
            .Where(x => x.IdLiderado.ToLower() == idStr
                        && x.Tipo.ToLower() == tipoNorm
                        && x.Data == command.Data)
            .ToListAsync(cancellationToken);
        _dbContext.PropriedadesHistoricas.RemoveRange(entities);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new StorageUnit();
    }
}

public sealed class RemoverTodasPropHistoricaLideradoHandler
    : IStorageCommandHandler<RemoverTodasPropHistoricaLideradoCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;
    public RemoverTodasPropHistoricaLideradoHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;

    public async Task<StorageUnit> HandleAsync(RemoverTodasPropHistoricaLideradoCommand command, CancellationToken cancellationToken)
    {
        var idStr = command.LideradoId.ToString().ToLowerInvariant();
        var entities = await _dbContext.PropriedadesHistoricas
            .Where(x => x.IdLiderado.ToLower() == idStr)
            .ToListAsync(cancellationToken);
        _dbContext.PropriedadesHistoricas.RemoveRange(entities);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new StorageUnit();
    }
}

