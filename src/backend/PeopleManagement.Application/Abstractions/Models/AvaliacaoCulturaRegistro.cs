namespace PeopleManagement.Application.Abstractions.Models;

/// <summary>
/// Registro de avaliacao cultural por data.
/// </summary>
public sealed record AvaliacaoCulturaRegistro(
    Guid LideradoId,
    RadarCulturalProjection Radar);

