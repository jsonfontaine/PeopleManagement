using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Features.Liderados.ObterVisaoIndividual;

/// <summary>
/// Resposta da visao individual de um liderado.
/// </summary>
public sealed record ObterVisaoIndividualResponse(VisaoIndividualProjection Conteudo);

