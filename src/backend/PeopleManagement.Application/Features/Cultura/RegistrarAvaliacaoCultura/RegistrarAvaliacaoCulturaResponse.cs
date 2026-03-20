namespace PeopleManagement.Application.Features.Cultura.RegistrarAvaliacaoCultura;

/// <summary>
/// Resposta do registro da avaliacao cultural.
/// </summary>
public sealed record RegistrarAvaliacaoCulturaResponse(Guid LideradoId, DateOnly Data);

