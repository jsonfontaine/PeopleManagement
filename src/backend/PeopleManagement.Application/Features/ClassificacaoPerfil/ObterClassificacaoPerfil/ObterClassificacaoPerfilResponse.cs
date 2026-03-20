namespace PeopleManagement.Application.Features.ClassificacaoPerfil.ObterClassificacaoPerfil;

/// <summary>
/// Resposta com classificacao de perfil atual do liderado.
/// </summary>
public sealed record ObterClassificacaoPerfilResponse(
    Guid LideradoId,
    string Perfil,
    string NineBox,
    string? Disc,
    DateTime DataAtualizacaoUtc);

