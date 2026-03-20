namespace PeopleManagement.Api.Endpoints.OneOnOnes;

/// <summary>
/// Payload para registrar encontro 1:1.
/// </summary>
public sealed record RegistrarOneOnOneRequest(DateOnly Data, string Resumo, string TarefasAcordadas, string ProximosAssuntos);

