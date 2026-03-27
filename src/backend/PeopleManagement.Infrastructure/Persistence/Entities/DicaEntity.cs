namespace PeopleManagement.Infrastructure.Persistence.Entities;

/// <summary>
/// Conteudo global de dicas em HTML.
/// </summary>
public sealed class DicaEntity
{
    public int Id { get; set; }

    public string ConteudoHtml { get; set; } = string.Empty;
}

