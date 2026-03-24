namespace PeopleManagement.Infrastructure.Persistence.Entities;

/// <summary>
/// Entidade persistida de feedback.
/// </summary>
public sealed class FeedbackEntity
{
    public string IdLiderado { get; set; } = string.Empty;

    public DateOnly Data { get; set; }

    public string Conteudo { get; set; } = string.Empty;

    public string Receptividade { get; set; } = string.Empty;

    public string Polaridade { get; set; } = string.Empty;
}

