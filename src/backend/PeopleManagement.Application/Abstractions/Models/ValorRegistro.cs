namespace PeopleManagement.Application.Abstractions.Models;

/// <summary>
/// Registro de valor de um liderado.
/// </summary>
public sealed record ValorRegistro(
    Guid LideradoId,
    DateOnly Data,
    string Valor);

