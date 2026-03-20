namespace PeopleManagement.Application.Features.OneOnOnes.RegistrarOneOnOne;

/// <summary>
/// Comando para registrar um acompanhamento 1:1.
/// </summary>
public sealed record RegistrarOneOnOneCommand(
    Guid LideradoId,
    DateOnly Data,
    string Resumo,
    string TarefasAcordadas,
    string ProximosAssuntos);

