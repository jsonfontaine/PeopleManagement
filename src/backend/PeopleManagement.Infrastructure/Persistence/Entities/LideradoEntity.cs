namespace PeopleManagement.Infrastructure.Persistence.Entities;

/// <summary>
/// Entidade persistida de liderado.
/// </summary>
public sealed class LideradoEntity
{
    public Guid Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public DateTime DataCriacaoUtc { get; set; }
}

