namespace PeopleManagement.Api.Controllers;

/// <summary>
/// Payload para atualizar classificacao de perfil do liderado.
/// </summary>
public sealed record AtualizarClassificacaoPerfilRequest(
    string Perfil,
    string NineBox,
    DateOnly Data);

