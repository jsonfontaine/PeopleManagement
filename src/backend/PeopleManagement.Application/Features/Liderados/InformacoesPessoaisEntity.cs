namespace PeopleManagement.Application.Features.Liderados;

/// <summary>
/// Entidade persistida das informacoes pessoais do liderado na feature.
/// </summary>
public sealed class InformacoesPessoaisEntity
{
    public string IdLiderado { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public DateOnly? DataNascimento { get; set; }

    public string? EstadoCivil { get; set; }

    public int? QuantidadeFilhos { get; set; }

    public DateOnly? DataContratacao { get; set; }

    public string? Cargo { get; set; }

    public DateOnly? DataInicioCargo { get; set; }

    public string? AspiracaoCarreira { get; set; }

    public string? GostosPessoais { get; set; }

    public string? RedFlags { get; set; }

    public string? Bio { get; set; }
}

