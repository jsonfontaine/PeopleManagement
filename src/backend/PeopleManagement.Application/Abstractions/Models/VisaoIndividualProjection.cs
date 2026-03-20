namespace PeopleManagement.Application.Abstractions.Models;

/// <summary>
/// Projecao consolidada para tela individual do liderado.
/// </summary>
public sealed record VisaoIndividualProjection(
    Guid LideradoId,
    InformacoesPessoais InformacoesPessoais,
    string? Perfil,
    string? NineBox,
    int QuantidadeFeedbacks,
    int QuantidadeOneOnOnes,
    IReadOnlyCollection<DateOnly> DatasAvaliacaoCultura);

