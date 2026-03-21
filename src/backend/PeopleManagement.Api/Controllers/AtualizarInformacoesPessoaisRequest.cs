namespace PeopleManagement.Api.Controllers;

/// <summary>
/// Payload para atualizar dados pessoais do liderado.
/// </summary>
public sealed record AtualizarInformacoesPessoaisRequest(
    string Nome,
    DateOnly? DataNascimento,
    string? EstadoCivil,
    int? QuantidadeFilhos,
    DateOnly? DataContratacao,
    string? Cargo,
    DateOnly? DataInicioCargo,
    string? AspiracaoCarreira,
    string? GostosPessoais,
    string? RedFlags,
    string? Bio);

