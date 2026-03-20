namespace PeopleManagement.Application.Features.Feedbacks.RegistrarFeedback;

/// <summary>
/// Contrato do handler de registro de feedback.
/// </summary>
public interface IRegistrarFeedbackHandler
{
    Task<RegistrarFeedbackResponse> HandleAsync(RegistrarFeedbackCommand command, CancellationToken cancellationToken);
}

