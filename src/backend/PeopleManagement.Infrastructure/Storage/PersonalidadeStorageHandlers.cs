using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Personalidade;
using PeopleManagement.Infrastructure.Persistence;
using PeopleManagement.Infrastructure.Persistence.Entities;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class ListarPersonalidadeHandler
    : IStorageCommandHandler<ListarPersonalidadeQuery, IReadOnlyCollection<PersonalidadeRegistro>>
{
    private readonly PeopleManagementDbContext _dbContext;
    public ListarPersonalidadeHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;

    public async Task<IReadOnlyCollection<PersonalidadeRegistro>> HandleAsync(
        ListarPersonalidadeQuery command, CancellationToken cancellationToken)
    {
        var idStr = command.LideradoId.ToString().ToLowerInvariant();
        return await _dbContext.Personalidades
            .AsNoTracking()
            .Where(x => x.IdLiderado.ToLower() == idStr)
            .OrderByDescending(x => x.Data)
            .Select(x => new PersonalidadeRegistro(Guid.Parse(x.IdLiderado), DateOnly.Parse(x.Data), x.Valor))
            .ToArrayAsync(cancellationToken);
    }
}

public sealed class VerificarExistenciaLideradoPersonalidadeHandler
    : IStorageCommandHandler<VerificarExistenciaLideradoPersonalidadeQuery, bool>
{
    private readonly PeopleManagementDbContext _dbContext;
    public VerificarExistenciaLideradoPersonalidadeHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;

    public Task<bool> HandleAsync(VerificarExistenciaLideradoPersonalidadeQuery command, CancellationToken cancellationToken)
    {
        var idStr = command.LideradoId.ToString().ToLowerInvariant();
        return _dbContext.Liderados.AsNoTracking().AnyAsync(x => x.Id.ToLower() == idStr, cancellationToken);
    }
}

public sealed class SalvarPersonalidadeHandler
    : IStorageCommandHandler<SalvarPersonalidadeCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;
    public SalvarPersonalidadeHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;

    public async Task<StorageUnit> HandleAsync(SalvarPersonalidadeCommand command, CancellationToken cancellationToken)
    {
        var idStr = command.Registro.LideradoId.ToString().ToLowerInvariant();
        var dataStr = command.Registro.Data.ToString("yyyy-MM-dd");

        var existente = await _dbContext.Personalidades
            .FirstOrDefaultAsync(x => x.IdLiderado.ToLower() == idStr && x.Data == dataStr, cancellationToken);

        if (existente is null)
        {
            _dbContext.Personalidades.Add(new PersonalidadeEntity
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

public sealed class RemoverPersonalidadeHandler
    : IStorageCommandHandler<RemoverPersonalidadeCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;
    public RemoverPersonalidadeHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;

    public async Task<StorageUnit> HandleAsync(RemoverPersonalidadeCommand command, CancellationToken cancellationToken)
    {
        var idStr = command.LideradoId.ToString().ToLowerInvariant();
        var dataStr = command.Data.ToString("yyyy-MM-dd");

        var existente = await _dbContext.Personalidades
            .FirstOrDefaultAsync(x => x.IdLiderado.ToLower() == idStr && x.Data == dataStr, cancellationToken);

        if (existente is not null)
        {
            _dbContext.Personalidades.Remove(existente);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return new StorageUnit();
    }
}

