namespace PeopleManagement.Application.Features.ClassificacaoPerfil.SalvarClassificacaoPerfil;

/// <summary>
/// Comando para salvar classificacao de perfil do liderado.
/// </summary>
public sealed record SalvarClassificacaoPerfilCommand(Guid LideradoId, string Perfil, string NineBox, string? Disc);

