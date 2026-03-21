namespace PeopleManagement.Application.Abstractions.Models;

/// <summary>
/// Registro de atitude de um liderado.
/// </summary>
public sealed record AtitudeRegistro(
    Guid LideradoId,
    DateOnly Data,
    string Valor);

