using FluentAssertions;
using Moq;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Application.Features.Feedbacks.ListarFeedbacks;

namespace PeopleManagement.Tests.Application.Features.Feedbacks.ListarFeedbacks;

public sealed class ListarFeedbacksHandlerTests
{
    [Fact]
    public async Task HandleAsync_DeveRetornarRegistrosOrdenadosPorDataDesc()
    {
        var lideradoId = Guid.NewGuid();
        var feedbackRepo = new Mock<IFeedbackRepository>();
        feedbackRepo
            .Setup(x => x.ListarPorLideradoAsync(lideradoId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new[]
            {
                new FeedbackRegistro(lideradoId, new DateOnly(2024, 1, 1), "A", "Alta", "Positivo"),
                new FeedbackRegistro(lideradoId, new DateOnly(2024, 2, 1), "B", "Alta", "Positivo")
            });

        var handler = new ListarFeedbacksHandler(feedbackRepo.Object);

        var response = await handler.HandleAsync(new ListarFeedbacksQuery(lideradoId), CancellationToken.None);

        response.Registros.First().Data.Should().Be(new DateOnly(2024, 2, 1));
    }
}

