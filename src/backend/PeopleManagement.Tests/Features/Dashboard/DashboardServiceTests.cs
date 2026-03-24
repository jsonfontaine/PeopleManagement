using PeopleManagement.Application.Features.Dashboard;

namespace PeopleManagement.Tests.Features.Dashboard;

public sealed class DashboardServiceTests
{
    [Fact]
    public async Task ObterAsync_DeveOrdenarCardsPorNome()
    {
        var service = new DashboardService(new FakeDashboardRepository());

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
}

