namespace PeopleManagement.Application.Features.Liderados.ObterLideradoPorId;

/// <summary>
/// DTO de resposta da consulta por id.
/// </summary>
public sealed record ObterLideradoPorIdResponse(Guid Id, string Nome, DateTime DataCriacaoUtc);

