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

    public async Task AtualizarAsync(Liderado liderado, CancellationToken cancellationToken)
    {
        var idStr = liderado.Id.ToString().ToLowerInvariant();
        var entity = await _dbContext.Liderados
            .FirstOrDefaultAsync(x => x.Id.ToLower() == idStr, cancellationToken);
        if (entity != null)
        {
            entity.Nome = liderado.Nome;
            _dbContext.Liderados.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task RemoverAsync(Guid id, CancellationToken cancellationToken)
    {
        var idStr = id.ToString().ToLowerInvariant();

        var entity = await _dbContext.Liderados
            .FirstOrDefaultAsync(x => x.Id.ToLower() == idStr, cancellationToken);

        if (entity is null)
        {
            return;
        }

        var informacoesPessoais = await _dbContext.InformacoesPessoais
            .Where(x => x.LideradoId.ToLower() == idStr)
            .ToListAsync(cancellationToken);
        var feedbacks = await _dbContext.Feedbacks
            .Where(x => x.LideradoId.ToLower() == idStr)
            .ToListAsync(cancellationToken);
        var oneOnOnes = await _dbContext.OneOnOnes
            .Where(x => x.LideradoId.ToLower() == idStr)
            .ToListAsync(cancellationToken);
        var classificacoesPerfil = await _dbContext.ClassificacoesPerfil
            .Where(x => x.LideradoId.ToLower() == idStr)
            .ToListAsync(cancellationToken);
        var discs = await _dbContext.Discs
            .Where(x => x.IdLiderado.ToLower() == idStr)
            .ToListAsync(cancellationToken);
        var culturas = await _dbContext.CulturaAvaliacoes
            .Where(x => x.LideradoId == id)
            .ToListAsync(cancellationToken);

        _dbContext.InformacoesPessoais.RemoveRange(informacoesPessoais);
        _dbContext.Feedbacks.RemoveRange(feedbacks);
        _dbContext.OneOnOnes.RemoveRange(oneOnOnes);
        _dbContext.ClassificacoesPerfil.RemoveRange(classificacoesPerfil);
        _dbContext.Discs.RemoveRange(discs);
        _dbContext.CulturaAvaliacoes.RemoveRange(culturas);
        _dbContext.Liderados.Remove(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
