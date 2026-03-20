using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Application.Features.Liderados.CriarLiderado;
using PeopleManagement.Domain;
using PeopleManagement.Domain.Liderados;

namespace PeopleManagement.Tests.Application.Features.Liderados.CriarLiderado;

public sealed class CriarLideradoHandlerTests
{
    [Fact]
    public async Task HandleAsync_DeveCriarLiderado_QuandoNomeForValido()
    {
        var repository = new Mock<ILideradoRepository>();
        repository
            .Setup(x => x.ExistePorNomeAsync("Ana", It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        repository
            .Setup(x => x.AdicionarAsync(It.IsAny<Liderado>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var logger = new Mock<ILogger<CriarLideradoHandler>>();
        var handler = new CriarLideradoHandler(repository.Object, logger.Object);

        var response = await handler.HandleAsync(new CriarLideradoCommand("Ana"), CancellationToken.None);

        response.Nome.Should().Be("Ana");
        response.Id.Should().NotBe(Guid.Empty);

        repository.Verify(x => x.AdicionarAsync(It.IsAny<Liderado>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_DeveLancarDomainException_QuandoNomeJaExistir()
    {
        var repository = new Mock<ILideradoRepository>();
        repository
            .Setup(x => x.ExistePorNomeAsync("Ana", It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var logger = new Mock<ILogger<CriarLideradoHandler>>();
        var handler = new CriarLideradoHandler(repository.Object, logger.Object);

        Func<Task> act = async () => await handler.HandleAsync(new CriarLideradoCommand("Ana"), CancellationToken.None);

        await act.Should().ThrowAsync<DomainException>().WithMessage("Ja existe um liderado com este nome.");
    }
}

