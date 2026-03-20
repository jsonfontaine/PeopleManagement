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
        var feedbackCounts = await _dbContext.Feedbacks.AsNoTracking()
            .GroupBy(x => x.LideradoId)
            .Select(x => new { LideradoId = x.Key, Quantidade = x.Count() })
            .ToDictionaryAsync(x => x.LideradoId, x => x.Quantidade, cancellationToken);

        var oneOnOneCounts = await _dbContext.OneOnOnes.AsNoTracking()
            .GroupBy(x => x.LideradoId)
            .Select(x => new { LideradoId = x.Key, Quantidade = x.Count() })
            .ToDictionaryAsync(x => x.LideradoId, x => x.Quantidade, cancellationToken);

        var classificacoes = await _dbContext.ClassificacoesPerfil.AsNoTracking()
            .ToDictionaryAsync(x => x.LideradoId, cancellationToken);

        var culturas = await _dbContext.CulturaAvaliacoes.AsNoTracking().ToListAsync(cancellationToken);
        var ultimasCulturas = culturas
            .GroupBy(x => x.LideradoId)
            .ToDictionary(
                x => x.Key,
                x => x.OrderByDescending(y => y.Data).Select(MapRadar).FirstOrDefault());

        return liderados.Select(liderado =>
        {
            classificacoes.TryGetValue(liderado.Id, out var classificacao);

            return new DashboardCardProjection(
                liderado.Id,
                liderado.Nome,
                classificacao?.Perfil,
                classificacao?.NineBox,
                feedbackCounts.GetValueOrDefault(liderado.Id),
                oneOnOneCounts.GetValueOrDefault(liderado.Id),
                null,
                ultimasCulturas.GetValueOrDefault(liderado.Id));
        })
            .OrderBy(x => x.Nome)
            .ToArray();
    }

    private static RadarCulturalProjection MapRadar(Entities.CulturaAvaliacaoEntity entity)
    {
        return new RadarCulturalProjection(
            entity.Data,
            entity.AprenderEMelhorarSempre,
            entity.AtitudeDeDono,
            entity.BuscarMelhoresResultadosParaClientes,
            entity.EspiritoDeEquipe,
            entity.Excelencia,
            entity.FazerAcontecer,
            entity.InovarParaInspirar);
    }
}

