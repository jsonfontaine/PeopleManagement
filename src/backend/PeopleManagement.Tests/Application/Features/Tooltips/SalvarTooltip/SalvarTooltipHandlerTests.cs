using FluentAssertions;
using Moq;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Application.Features.Tooltips.SalvarTooltip;

namespace PeopleManagement.Tests.Application.Features.Tooltips.SalvarTooltip;

public sealed class SalvarTooltipHandlerTests
{
    [Fact]
    public async Task HandleAsync_DeveSalvarTooltipComSucesso()
    {
        var repo = new Mock<ITooltipRepository>();
        repo.Setup(x => x.SalvarAsync(It.IsAny<TooltipRegistro>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var handler = new SalvarTooltipHandler(repo.Object);

        var response = await handler.HandleAsync(new SalvarTooltipCommand("campo.nome", "Texto"), CancellationToken.None);

        response.ChaveCampo.Should().Be("campo.nome");
        repo.Verify(x => x.SalvarAsync(It.IsAny<TooltipRegistro>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}

