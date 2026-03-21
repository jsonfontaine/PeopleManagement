using FluentAssertions;
using Moq;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Application.Features.Dashboard.ObterDashboard;

namespace PeopleManagement.Tests.Application.Features.Dashboard.ObterDashboard;

public sealed class ObterDashboardHandlerTests
{
    [Fact]
    public async Task HandleAsync_DeveRetornarCardsOrdenadosPorNome()
    {
        var repository = new Mock<IDashboardRepository>();
        repository
            .Setup(x => x.ListarCardsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new[]
            {
                new DashboardCardProjection(Guid.NewGuid().ToString(), "Zeca", null, null, 0, 0, null, null),
                new DashboardCardProjection(Guid.NewGuid().ToString(), "Ana", null, null, 0, 0, null, null)
            });

        var handler = new ObterDashboardHandler(repository.Object);

        var response = await handler.HandleAsync(new ObterDashboardQuery(), CancellationToken.None);

        response.Cards.Select(x => x.Nome).Should().ContainInOrder("Ana", "Zeca");
    }
}
