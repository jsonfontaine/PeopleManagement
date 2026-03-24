namespace PeopleManagement.Infrastructure.Persistence.Entities;

/// <summary>
/// Entidade persistida de registro 1:1.
/// </summary>
public sealed class OneOnOneEntity
{
    public string IdLiderado { get; set; } = string.Empty;

    public DateOnly Data { get; set; }

    public string Resumo { get; set; } = string.Empty;

    public string TarefasAcordadas { get; set; } = string.Empty;

    public string ProximosAssuntos { get; set; } = string.Empty;
}

