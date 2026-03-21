namespace PeopleManagement.Application.Abstractions.Models;

/// <summary>
/// Registro de habilidade de um liderado.
/// </summary>
public sealed record HabilidadeRegistro(
    Guid LideradoId,
    DateOnly Data,
    string Valor);

