namespace PeopleManagement.Infrastructure.Persistence.Entities;

/// <summary>
/// Entidade persistida de tooltip por campo.
/// </summary>
public sealed class TooltipEntity
{
    public string ChaveCampo { get; set; } = string.Empty;

    public string Texto { get; set; } = string.Empty;
}

