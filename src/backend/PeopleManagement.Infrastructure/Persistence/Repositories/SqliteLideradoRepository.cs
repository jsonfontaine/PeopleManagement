using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Domain.Liderados;

namespace PeopleManagement.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repositorio de liderados com EF Core e SQLite.
/// </summary>
public sealed class SqliteLideradoRepository : ILideradoRepository
{
    private readonly PeopleManagementDbContext _dbContext;

    public SqliteLideradoRepository(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> ExistePorNomeAsync(string nome, CancellationToken cancellationToken)
    {
        var nomeNormalizado = nome.Trim().ToLower();
        return _dbContext.Liderados.AnyAsync(x => x.Nome.ToLower() == nomeNormalizado, cancellationToken);
    }

    public async Task AdicionarAsync(Liderado liderado, CancellationToken cancellationToken)
    {
        _dbContext.Liderados.Add(new Entities.LideradoEntity
        {
            Id = liderado.Id.ToString(),
            Nome = liderado.Nome,
            DataCriacaoUtc = liderado.DataCriacaoUtc
        });

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<Liderado>> ListarAsync(CancellationToken cancellationToken)
    {
        var liderados = await _dbContext.Liderados
            .AsNoTracking()
            .OrderBy(x => x.Nome)
            .ToListAsync(cancellationToken);

        return liderados
            .Select(x => Liderado.Reconstituir(Guid.Parse(x.Id), x.Nome, x.DataCriacaoUtc))
            .ToArray();
    }

    public async Task<Liderado?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var idStr = id.ToString().ToLowerInvariant();
        var liderado = await _dbContext.Liderados
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.ToLower() == idStr, cancellationToken);
        return liderado is null ? null : Liderado.Reconstituir(Guid.Parse(liderado.Id), liderado.Nome, liderado.DataCriacaoUtc);
    }

    public async Task SalvarDiscAsync(Guid lideradoId, string valor, DateOnly data)
    {
        var entity = new Entities.DiscEntity
        {
            IdLiderado = lideradoId.ToString(),
            Valor = valor,
            Data = data.ToString("yyyy-MM-dd")
        };
        
        var existing = await _dbContext.Discs.FirstOrDefaultAsync(d => d.IdLiderado == entity.IdLiderado && d.Data == entity.Data);
        if (existing != null)
        {
            existing.Valor = valor;
            _dbContext.Discs.Update(existing);
        }
        else
        {
            await _dbContext.Discs.AddAsync(entity);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<(string Valor, DateOnly Data)>> ListarDiscsAsync(Guid lideradoId)
    {
        var idLideradoStr = lideradoId.ToString();
        return await _dbContext.Discs
            .Where(d => d.IdLiderado == idLideradoStr)
            .OrderByDescending(d => d.Data)
            .Select(d => new ValueTuple<string, DateOnly>(d.Valor, DateOnly.Parse(d.Data)))
            .ToListAsync();
    }

    public async Task RemoverDiscAsync(Guid lideradoId, DateOnly data)
    {
        var dataStr = data.ToString("yyyy-MM-dd");
        var idLideradoStr = lideradoId.ToString();
        var entity = await _dbContext.Discs.FirstOrDefaultAsync(d => d.IdLiderado == idLideradoStr && d.Data == dataStr);
        if (entity != null)
        {
            _dbContext.Discs.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
    public async Task AtualizarAsync(Liderado liderado, CancellationToken cancellationToken)
    {
        var idStr = liderado.Id.ToString();
        var entity = await _dbContext.Liderados.FirstOrDefaultAsync(x => x.Id == idStr, cancellationToken);
        if (entity != null)
        {
            entity.Nome = liderado.Nome;
            // Add other property updates here if needed
            _dbContext.Liderados.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task RemoverAsync(Guid id, CancellationToken cancellationToken)
    {
        var idStr = id.ToString();
        var entity = await _dbContext.Liderados.FirstOrDefaultAsync(x => x.Id == idStr, cancellationToken);
        if (entity != null)
        {
            _dbContext.Liderados.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
