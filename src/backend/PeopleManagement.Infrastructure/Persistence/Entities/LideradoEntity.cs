namespace PeopleManagement.Infrastructure.Persistence.Entities;

/// <summary>
/// Entidade persistida de liderado.
/// </summary>
public sealed class LideradoEntity
{
    public string Id { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public DateTime DataCriacaoUtc { get; set; }
}

