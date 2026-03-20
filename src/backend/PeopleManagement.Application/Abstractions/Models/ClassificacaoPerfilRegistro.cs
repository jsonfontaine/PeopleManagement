namespace PeopleManagement.Application.Abstractions.Models;

/// <summary>
/// Registro atual da classificacao de perfil do liderado.
/// </summary>
public sealed record ClassificacaoPerfilRegistro(
    Guid LideradoId,
    string Perfil,
    string NineBox,
    string? Disc,
    DateTime DataAtualizacaoUtc);

