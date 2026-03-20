namespace PeopleManagement.Application.Features.OneOnOnes.RegistrarOneOnOne;

/// <summary>
/// Resposta do caso de uso de registro 1:1.
/// </summary>
public sealed record RegistrarOneOnOneResponse(Guid LideradoId, DateOnly Data);

