namespace PeopleManagement.Infrastructure.Persistence.Entities;

/// <summary>
/// Propriedade de tooltip persistida por Nome e ValueObject.
/// </summary>
public sealed class TooltipEntity
{
    public string Nome { get; set; } = string.Empty;

    public string ValueObject { get; set; } = string.Empty;

    public string Tooltip { get; set; } = string.Empty;
}

