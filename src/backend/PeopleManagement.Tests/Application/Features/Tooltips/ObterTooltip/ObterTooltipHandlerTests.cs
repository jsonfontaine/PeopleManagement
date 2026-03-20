using FluentAssertions;
using Moq;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Application.Features.Tooltips.ObterTooltip;

namespace PeopleManagement.Tests.Application.Features.Tooltips.ObterTooltip;

public sealed class ObterTooltipHandlerTests
{
    [Fact]
    public async Task HandleAsync_DeveRetornarTooltipQuandoExistir()
    {
        var repo = new Mock<ITooltipRepository>();
        repo.Setup(x => x.ObterPorChaveAsync("campo.nome", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TooltipRegistro("campo.nome", "Texto"));

        var handler = new ObterTooltipHandler(repo.Object);

        var response = await handler.HandleAsync(new ObterTooltipQuery("campo.nome"), CancellationToken.None);

        response.Should().NotBeNull();
        response!.Texto.Should().Be("Texto");
    }
}

