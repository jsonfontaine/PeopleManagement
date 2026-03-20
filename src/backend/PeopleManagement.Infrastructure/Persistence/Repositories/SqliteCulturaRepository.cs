using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Infrastructure.Persistence.Entities;

namespace PeopleManagement.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repositorio persistente de avaliacoes culturais.
/// </summary>
public sealed class SqliteCulturaRepository : ICulturaRepository
{
    private readonly PeopleManagementDbContext _dbContext;

    public SqliteCulturaRepository(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AdicionarAvaliacaoAsync(AvaliacaoCulturaRegistro registro, CancellationToken cancellationToken)
    {
        var existente = await _dbContext.CulturaAvaliacoes.FirstOrDefaultAsync(
            x => x.LideradoId == registro.LideradoId && x.Data == registro.Radar.Data,
            cancellationToken);

        if (existente is null)
        {
            existente = new CulturaAvaliacaoEntity
            {
                Id = Guid.NewGuid(),
                LideradoId = registro.LideradoId,
                Data = registro.Radar.Data
            };

            _dbContext.CulturaAvaliacoes.Add(existente);
        }

        existente.AprenderEMelhorarSempre = registro.Radar.AprenderEMelhorarSempre;
        existente.AtitudeDeDono = registro.Radar.AtitudeDeDono;
        existente.BuscarMelhoresResultadosParaClientes = registro.Radar.BuscarMelhoresResultadosParaClientes;
        existente.EspiritoDeEquipe = registro.Radar.EspiritoDeEquipe;
        existente.Excelencia = registro.Radar.Excelencia;
        existente.FazerAcontecer = registro.Radar.FazerAcontecer;
        existente.InovarParaInspirar = registro.Radar.InovarParaInspirar;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<RadarCulturalProjection?> ObterPorDataAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.CulturaAvaliacoes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.LideradoId == lideradoId && x.Data == data, cancellationToken);

        return entity is null ? null : Map(entity);
    }

    public async Task<IReadOnlyCollection<DateOnly>> ListarDatasDisponiveisAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        return await _dbContext.CulturaAvaliacoes
            .AsNoTracking()
            .Where(x => x.LideradoId == lideradoId)
            .OrderByDescending(x => x.Data)
            .Select(x => x.Data)
            .ToArrayAsync(cancellationToken);
    }

    private static RadarCulturalProjection Map(CulturaAvaliacaoEntity entity)
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

