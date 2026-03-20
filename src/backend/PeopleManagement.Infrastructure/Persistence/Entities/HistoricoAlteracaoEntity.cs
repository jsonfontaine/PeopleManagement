namespace PeopleManagement.Infrastructure.Persistence.Entities;

/// <summary>
/// Entidade persistida do historico de alteracoes.
/// </summary>
public sealed class HistoricoAlteracaoEntity
{
    public Guid Id { get; set; }

    public Guid LideradoId { get; set; }

    public string Secao { get; set; } = string.Empty;

    public string Campo { get; set; } = string.Empty;

    public string? ValorAnterior { get; set; }

    public string ValorNovo { get; set; } = string.Empty;

    public DateTime DataAlteracaoUtc { get; set; }

    public string UsuarioResponsavel { get; set; } = string.Empty;
}

