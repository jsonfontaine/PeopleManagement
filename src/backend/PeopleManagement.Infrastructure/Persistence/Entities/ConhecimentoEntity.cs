namespace PeopleManagement.Infrastructure.Persistence.Entities;

/// <summary>
/// Registro persistido de Conhecimentos por liderado e data.
/// </summary>
public sealed class ConhecimentoEntity
{
    public string IdLiderado { get; set; } = string.Empty;

    public string Valor { get; set; } = string.Empty;

    // Formato ISO yyyy-MM-dd para chave composta estavel em SQLite.
    public string Data { get; set; } = string.Empty;
}

