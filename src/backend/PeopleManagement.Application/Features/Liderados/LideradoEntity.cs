namespace PeopleManagement.Application.Features.Liderados;

/// <summary>
/// Entidade persistida de liderado da feature.
/// </summary>
public sealed class LideradoEntity
{
    public string Id { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public DateTime DataCriacaoUtc { get; set; }
}

