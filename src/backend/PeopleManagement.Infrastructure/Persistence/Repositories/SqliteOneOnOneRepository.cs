using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Infrastructure.Persistence.Entities;

namespace PeopleManagement.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repositorio persistente de 1:1.
/// </summary>
public sealed class SqliteOneOnOneRepository : IOneOnOneRepository
{
    private readonly PeopleManagementDbContext _dbContext;

    public SqliteOneOnOneRepository(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AdicionarAsync(OneOnOneRegistro registro, CancellationToken cancellationToken)
    {
        _dbContext.OneOnOnes.Add(new OneOnOneEntity
        {
            Id = Guid.NewGuid().ToString(),
            LideradoId = registro.LideradoId.ToString(),
            Data = registro.Data,
            Resumo = registro.Resumo,
            TarefasAcordadas = registro.TarefasAcordadas,
            ProximosAssuntos = registro.ProximosAssuntos
        });

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<OneOnOneRegistro>> ListarPorLideradoAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        var lideradoIdStr = lideradoId.ToString().ToLowerInvariant();
        var registros = await _dbContext.OneOnOnes
            .AsNoTracking()
            .Where(x => x.LideradoId.ToLower() == lideradoIdStr)
            .OrderByDescending(x => x.Data)
            .ToListAsync(cancellationToken);

        return registros.Select(x => new OneOnOneRegistro(Guid.Parse(x.LideradoId), x.Data, x.Resumo, x.TarefasAcordadas, x.ProximosAssuntos)).ToArray();
    }
}
