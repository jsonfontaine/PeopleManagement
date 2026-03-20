namespace PeopleManagement.Application.Features.ClassificacaoPerfil.SalvarClassificacaoPerfil;

/// <summary>
/// Resposta da operacao de classificacao de perfil.
/// </summary>
public sealed record SalvarClassificacaoPerfilResponse(Guid LideradoId, string Perfil, string NineBox, string? Disc);

