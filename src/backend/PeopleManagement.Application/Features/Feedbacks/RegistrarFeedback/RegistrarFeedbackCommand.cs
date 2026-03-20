namespace PeopleManagement.Application.Features.Feedbacks.RegistrarFeedback;

/// <summary>
/// Comando para registrar feedback no historico do liderado.
/// </summary>
public sealed record RegistrarFeedbackCommand(
    Guid LideradoId,
    DateOnly Data,
    string Conteudo,
    string Receptividade,
    string Polaridade);

