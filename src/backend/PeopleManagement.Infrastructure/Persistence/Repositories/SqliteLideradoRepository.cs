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
            Id = liderado.Id,
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
            .Select(x => Liderado.Reconstituir(x.Id, x.Nome, x.DataCriacaoUtc))
            .ToArray();
    }

    public async Task<Liderado?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var liderado = await _dbContext.Liderados.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return liderado is null ? null : Liderado.Reconstituir(liderado.Id, liderado.Nome, liderado.DataCriacaoUtc);
    }
}

