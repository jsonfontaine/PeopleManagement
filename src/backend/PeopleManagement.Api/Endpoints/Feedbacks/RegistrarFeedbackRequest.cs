namespace PeopleManagement.Api.Endpoints.Feedbacks;

/// <summary>
/// Payload para registrar feedback.
/// </summary>
public sealed record RegistrarFeedbackRequest(DateOnly Data, string Conteudo, string Receptividade, string Polaridade);

