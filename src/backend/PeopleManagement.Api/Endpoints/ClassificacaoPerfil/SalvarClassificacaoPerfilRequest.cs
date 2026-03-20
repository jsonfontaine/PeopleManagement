namespace PeopleManagement.Api.Endpoints.ClassificacaoPerfil;

/// <summary>
/// Request da classificacao de perfil.
/// </summary>
 public sealed record SalvarClassificacaoPerfilRequest(string Perfil, string NineBox, string? Disc);

