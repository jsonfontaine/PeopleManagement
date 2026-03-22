namespace PeopleManagement.Application.Features.Dashboard;

public sealed class DashboardService
{
    private readonly IDashboardRepository _repository;

    public DashboardService(IDashboardRepository repository)
    {
        _repository = repository;
    }

    public async Task<ObterDashboardResponse> ObterAsync(CancellationToken cancellationToken)
    {
        var cards = await _repository.ListarCardsAsync(cancellationToken);
        return new ObterDashboardResponse(cards.OrderBy(x => x.Nome).ToArray());
    }
}


public sealed record ObterDashboardResponse(IReadOnlyCollection<DashboardCardProjection> Cards);

public sealed record DashboardCardProjection(
    string LideradoId,
    string Nome,
    string? Perfil,
    string? NineBox,
    int QuantidadeFeedbacks,
    int QuantidadeOneOnOnes,
    double? NotaGeral,
    object? UltimaAvaliacaoCultura);

