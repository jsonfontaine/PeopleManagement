namespace PeopleManagement.Application.Abstractions.Models;

/// <summary>
/// Registro de feedback de um liderado.
/// </summary>
public sealed record FeedbackRegistro(
    Guid LideradoId,
    DateOnly Data,
    string Conteudo,
    string Receptividade,
    string Polaridade);

