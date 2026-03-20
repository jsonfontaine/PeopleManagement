namespace PeopleManagement.Application.Features.Liderados.ListarLiderados;

/// <summary>
/// DTO de resposta para listagem de liderados.
/// </summary>
public sealed record LideradoResumoResponse(Guid Id, string Nome, DateTime DataCriacaoUtc);

