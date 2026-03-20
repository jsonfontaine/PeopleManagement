using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Abstractions.Persistence;

/// <summary>
/// Porta de persistencia para avaliacoes culturais.
/// </summary>
public interface ICulturaRepository
{
    Task AdicionarAvaliacaoAsync(AvaliacaoCulturaRegistro registro, CancellationToken cancellationToken);

    Task<RadarCulturalProjection?> ObterPorDataAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<DateOnly>> ListarDatasDisponiveisAsync(Guid lideradoId, CancellationToken cancellationToken);
}

