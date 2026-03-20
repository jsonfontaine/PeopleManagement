using FluentAssertions;
using Moq;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Application.Features.Cultura.ObterRadarCulturalPorData;

namespace PeopleManagement.Tests.Application.Features.Cultura.ObterRadarCulturalPorData;

public sealed class ObterRadarCulturalPorDataHandlerTests
{
    [Fact]
    public async Task HandleAsync_DeveRetornarRadarComDatasDisponiveis()
    {
        var lideradoId = Guid.NewGuid();
        var data = new DateOnly(2024, 1, 1);
        var repo = new Mock<ICulturaRepository>();
        repo.Setup(x => x.ObterPorDataAsync(lideradoId, data, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RadarCulturalProjection(data, 7, 7, 7, 7, 7, 7, 7));
        repo.Setup(x => x.ListarDatasDisponiveisAsync(lideradoId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new[] { data, new DateOnly(2024, 2, 1) });

        var handler = new ObterRadarCulturalPorDataHandler(repo.Object);

        var response = await handler.HandleAsync(new ObterRadarCulturalPorDataQuery(lideradoId, data), CancellationToken.None);

        response.Should().NotBeNull();
        response!.DatasDisponiveis.Should().HaveCount(2);
    }
}

