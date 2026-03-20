using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Infrastructure.Persistence.Entities;

namespace PeopleManagement.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repositorio persistente do historico de alteracoes.
/// </summary>
public sealed class SqliteHistoricoAlteracaoRepository : IHistoricoAlteracaoRepository
{
    private readonly PeopleManagementDbContext _dbContext;

    public SqliteHistoricoAlteracaoRepository(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task RegistrarAsync(HistoricoAlteracaoRegistro registro, CancellationToken cancellationToken)
    {
        _dbContext.HistoricoAlteracoes.Add(new HistoricoAlteracaoEntity
        {
            Id = Guid.NewGuid(),
            LideradoId = registro.LideradoId,
            Secao = registro.Secao,
            Campo = registro.Campo,
            ValorAnterior = registro.ValorAnterior,
            ValorNovo = registro.ValorNovo,
            DataAlteracaoUtc = registro.DataAlteracaoUtc,
            UsuarioResponsavel = registro.UsuarioResponsavel
        });

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<HistoricoAlteracaoRegistro>> ListarPorLideradoAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        var registros = await _dbContext.HistoricoAlteracoes
            .AsNoTracking()
            .Where(x => x.LideradoId == lideradoId)
            .OrderByDescending(x => x.DataAlteracaoUtc)
            .ToListAsync(cancellationToken);

        return registros.Select(x => new HistoricoAlteracaoRegistro(
            x.LideradoId,
            x.Secao,
            x.Campo,
            x.ValorAnterior,
            x.ValorNovo,
            x.DataAlteracaoUtc,
            x.UsuarioResponsavel)).ToArray();
    }
}

