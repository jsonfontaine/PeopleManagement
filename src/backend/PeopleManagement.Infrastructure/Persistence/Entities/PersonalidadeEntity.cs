namespace PeopleManagement.Infrastructure.Persistence.Entities;

/// <summary>
/// Registro persistido de Personalidade (value object) por liderado e data.
/// </summary>
public sealed class PersonalidadeEntity
{
    public string IdLiderado { get; set; } = string.Empty;

    public string Valor { get; set; } = string.Empty;

    // Formato ISO yyyy-MM-dd para chave composta estável em SQLite.
    public string Data { get; set; } = string.Empty;
}

