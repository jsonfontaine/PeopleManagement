using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;

namespace PeopleManagement.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repositorio de leitura do dashboard a partir do SQLite.
/// </summary>
public sealed class SqliteDashboardRepository : IDashboardRepository
{
    private readonly PeopleManagementDbContext _dbContext;

    public SqliteDashboardRepository(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<DashboardCardProjection>> ListarCardsAsync(CancellationToken cancellationToken)
    {
        var liderados = await _dbContext.Liderados.AsNoTracking().ToListAsync(cancellationToken);

        var oneOnOneCountByLiderado = await _dbContext.OneOnOnes
            .AsNoTracking()
            .GroupBy(x => x.LideradoId.ToLower())
            .Select(group => new { LideradoId = group.Key, Quantidade = group.Count() })
            .ToDictionaryAsync(x => x.LideradoId, x => x.Quantidade, cancellationToken);

        var feedbackCountByLiderado = await _dbContext.Feedbacks
            .AsNoTracking()
            .GroupBy(x => x.LideradoId.ToLower())
            .Select(group => new { LideradoId = group.Key, Quantidade = group.Count() })
            .ToDictionaryAsync(x => x.LideradoId, x => x.Quantidade, cancellationToken);

        return liderados.Select(liderado =>
            new DashboardCardProjection(
                liderado.Id.ToLowerInvariant(),
                liderado.Nome,
                null, // Perfil
                null, // NineBox
                feedbackCountByLiderado.GetValueOrDefault(liderado.Id.ToLowerInvariant(), 0),
                oneOnOneCountByLiderado.GetValueOrDefault(liderado.Id.ToLowerInvariant(), 0),
                null, // Nota geral
                null  // Radar cultural
            )).OrderBy(x => x.Nome).ToArray();
    }
}
