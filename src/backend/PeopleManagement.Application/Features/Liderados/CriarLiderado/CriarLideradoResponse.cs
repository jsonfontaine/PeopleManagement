namespace PeopleManagement.Application.Features.Liderados.CriarLiderado;

/// <summary>
/// Resposta do caso de uso de criacao de liderado.
/// </summary>
public sealed record CriarLideradoResponse(Guid Id, string Nome, DateTime DataCriacaoUtc);

