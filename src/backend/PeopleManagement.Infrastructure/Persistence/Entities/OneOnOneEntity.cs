namespace PeopleManagement.Infrastructure.Persistence.Entities;

/// <summary>
/// Entidade persistida de registro 1:1.
/// </summary>
public sealed class OneOnOneEntity
{
    public Guid Id { get; set; }

    public Guid LideradoId { get; set; }

    public DateOnly Data { get; set; }

    public string Resumo { get; set; } = string.Empty;

    public string TarefasAcordadas { get; set; } = string.Empty;

    public string ProximosAssuntos { get; set; } = string.Empty;
}

