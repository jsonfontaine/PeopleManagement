using PeopleManagement.Application.Features.Tooltips;

namespace PeopleManagement.Application.Features.Dashboard;

public sealed class DashboardService
{
    private readonly IDashboardRepository _repository;
    private readonly ITooltipsRepository _tooltipsRepository;

    public DashboardService(IDashboardRepository repository, ITooltipsRepository tooltipsRepository)
    {
        _repository = repository;
        _tooltipsRepository = tooltipsRepository;
    }

    public async Task<ObterDashboardResponse> ObterAsync(CancellationToken cancellationToken)
    {
        var cards = await _repository.ListarCardsAsync(cancellationToken);
        var tooltips = await _tooltipsRepository.ListarAsync(cancellationToken);

        return new ObterDashboardResponse(
            cards.OrderBy(x => x.Nome).ToArray(),
            tooltips.OrderBy(x => x.Nome).ThenBy(x => x.ValueObject).ToArray());
    }
}
