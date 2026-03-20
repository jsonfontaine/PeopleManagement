namespace PeopleManagement.Application.Features.Feedbacks.RegistrarFeedback;

/// <summary>
/// Resposta do caso de uso de registro de feedback.
/// </summary>
public sealed record RegistrarFeedbackResponse(Guid LideradoId, DateOnly Data, string Polaridade);

