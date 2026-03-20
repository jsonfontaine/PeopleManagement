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
            Id = Guid.NewGuid(),
            LideradoId = registro.LideradoId,
            Data = registro.Data,
            Resumo = registro.Resumo,
            TarefasAcordadas = registro.TarefasAcordadas,
            ProximosAssuntos = registro.ProximosAssuntos
        });

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<OneOnOneRegistro>> ListarPorLideradoAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        var registros = await _dbContext.OneOnOnes
            .AsNoTracking()
            .Where(x => x.LideradoId == lideradoId)
            .OrderByDescending(x => x.Data)
            .ToListAsync(cancellationToken);

        return registros.Select(x => new OneOnOneRegistro(x.LideradoId, x.Data, x.Resumo, x.TarefasAcordadas, x.ProximosAssuntos)).ToArray();
    }
}

