namespace PeopleManagement.Application.Abstractions.Models;

/// <summary>
/// Registro de conhecimento de um liderado.
/// </summary>
public sealed record ConhecimentoRegistro(
    Guid LideradoId,
    DateOnly Data,
    string Valor);

