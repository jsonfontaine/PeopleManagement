namespace PeopleManagement.Application.Abstractions.Models;

/// <summary>
/// Registro de acompanhamento 1:1.
/// </summary>
public sealed record OneOnOneRegistro(
    Guid LideradoId,
    DateOnly Data,
    string Resumo,
    string TarefasAcordadas,
    string ProximosAssuntos);

