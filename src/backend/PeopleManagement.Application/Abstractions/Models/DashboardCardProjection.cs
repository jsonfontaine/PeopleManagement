namespace PeopleManagement.Application.Abstractions.Models;

/// <summary>
/// Projecao do card de resumo exibido no dashboard.
/// </summary>
public sealed record DashboardCardProjection(
    string LideradoId, // Alterado para string para compatibilidade com o tipo do banco
    string Nome,
    string? Perfil,
    string? NineBox,
    int QuantidadeFeedbacks,
    int QuantidadeOneOnOnes,
    double? NotaGeral,
    RadarCulturalProjection? UltimaAvaliacaoCultura);
