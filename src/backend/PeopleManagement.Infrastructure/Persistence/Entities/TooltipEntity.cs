namespace PeopleManagement.Infrastructure.Persistence.Entities;

/// <summary>
/// Texto persistido de tooltip por chave de campo.
/// </summary>
public sealed class TooltipEntity
{
    public string ChaveCampo { get; set; } = string.Empty;

    public string Texto { get; set; } = string.Empty;
}

