using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Features.Cultura.ObterRadarCulturalPorData;

/// <summary>
/// Resposta da consulta do radar cultural por data.
/// </summary>
public sealed record ObterRadarCulturalPorDataResponse(RadarCulturalProjection Radar, IReadOnlyCollection<DateOnly> DatasDisponiveis);

