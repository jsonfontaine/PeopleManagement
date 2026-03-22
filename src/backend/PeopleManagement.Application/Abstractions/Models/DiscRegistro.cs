namespace PeopleManagement.Application.Abstractions.Models;

/// <summary>
/// Value Object de registro DISC por liderado e data.
/// </summary>
public sealed record DiscRegistro(
    Guid LideradoId,
    DateOnly Data,
    string Valor);

