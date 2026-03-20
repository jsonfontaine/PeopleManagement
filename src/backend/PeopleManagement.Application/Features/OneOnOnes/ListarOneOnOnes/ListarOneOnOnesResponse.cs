using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Features.OneOnOnes.ListarOneOnOnes;

/// <summary>
/// Resposta da listagem de 1:1.
/// </summary>
public sealed record ListarOneOnOnesResponse(IReadOnlyCollection<OneOnOneRegistro> Registros);

