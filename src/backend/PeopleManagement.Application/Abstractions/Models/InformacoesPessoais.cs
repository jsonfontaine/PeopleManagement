namespace PeopleManagement.Application.Abstractions.Models;

/// <summary>
/// Dados cadastrais e pessoais do liderado.
/// </summary>
public sealed record InformacoesPessoais(
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

