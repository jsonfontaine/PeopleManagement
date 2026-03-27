using PeopleManagement.Application.Features.Dashboard;
using PeopleManagement.Application.Features.Tooltips;

namespace PeopleManagement.Tests.Features.Dashboard;

public sealed class DashboardServiceTests
{
    [Fact]
    public async Task ObterAsync_DeveOrdenarCardsPorNome()
    {
        var service = new DashboardService(new FakeDashboardRepository(), new FakeTooltipsRepository());

        var response = await service.ObterAsync(CancellationToken.None);

        Assert.Equal("Ana", response.Cards.First().Nome);
        Assert.Equal("Bruno", response.Cards.Last().Nome);
    }

    private sealed class FakeDashboardRepository : IDashboardRepository
    {
        public Task<IReadOnlyCollection<DashboardCardProjection>> ListarCardsAsync(CancellationToken cancellationToken)
        {
            IReadOnlyCollection<DashboardCardProjection> cards =
            [
                new DashboardCardProjection("2", "Bruno", null, null, null, 0, 0, null, null),
                new DashboardCardProjection("1", "Ana", null, null, null, 0, 0, null, null)
            ];

            return Task.FromResult(cards);
        }
    }

    private sealed class FakeTooltipsRepository : ITooltipsRepository
    {
        public Task<IReadOnlyCollection<TooltipPropriedadeRegistro>> ListarAsync(CancellationToken cancellationToken)
            => Task.FromResult<IReadOnlyCollection<TooltipPropriedadeRegistro>>([]);

        public Task<TooltipPropriedadeRegistro?> ObterAsync(string nome, string valueObject, CancellationToken cancellationToken)
            => Task.FromResult<TooltipPropriedadeRegistro?>(null);

        public Task SalvarAsync(TooltipPropriedadeRegistro registro, CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}

