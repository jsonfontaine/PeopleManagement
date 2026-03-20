using FluentAssertions;
using Moq;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Application.Features.OneOnOnes.ListarOneOnOnes;

namespace PeopleManagement.Tests.Application.Features.OneOnOnes.ListarOneOnOnes;

public sealed class ListarOneOnOnesHandlerTests
{
    [Fact]
    public async Task HandleAsync_DeveOrdenarRegistrosPorDataDesc()
    {
        var lideradoId = Guid.NewGuid();
        var repo = new Mock<IOneOnOneRepository>();
        repo
            .Setup(x => x.ListarPorLideradoAsync(lideradoId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new[]
            {
                new OneOnOneRegistro(lideradoId, new DateOnly(2024, 1, 1), "A", "T1", "P1"),
                new OneOnOneRegistro(lideradoId, new DateOnly(2024, 3, 1), "B", "T2", "P2")
            });

        var handler = new ListarOneOnOnesHandler(repo.Object);

        var response = await handler.HandleAsync(new ListarOneOnOnesQuery(lideradoId), CancellationToken.None);

        response.Registros.First().Data.Should().Be(new DateOnly(2024, 3, 1));
    }
}

