namespace PeopleManagement.Infrastructure.Persistence.Entities;

/// <summary>
/// Registro persistido de DISC (value object) por liderado e data.
/// </summary>
public sealed class DiscEntity
{
    public string IdLiderado { get; set; } = string.Empty;

    public string Valor { get; set; } = string.Empty;

    // Formato ISO yyyy-MM-dd para chave composta estável em SQLite.
    public string Data { get; set; } = string.Empty;
}
