namespace PeopleManagement.Application.Features.Cultura.ObterRadarCulturalPorData;

/// <summary>
/// Query para recuperar o radar cultural por data.
/// </summary>
public sealed record ObterRadarCulturalPorDataQuery(Guid LideradoId, DateOnly Data);

