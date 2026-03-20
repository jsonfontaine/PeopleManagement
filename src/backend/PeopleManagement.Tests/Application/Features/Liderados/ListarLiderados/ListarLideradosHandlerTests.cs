using FluentAssertions;
using Moq;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Application.Features.Liderados.ListarLiderados;
using PeopleManagement.Domain.Liderados;

namespace PeopleManagement.Tests.Application.Features.Liderados.ListarLiderados;

public sealed class ListarLideradosHandlerTests
{
    [Fact]
    public async Task HandleAsync_DeveRetornarListaOrdenadaPorNome()
    {
        var repository = new Mock<ILideradoRepository>();
        var bruno = Liderado.Criar("Bruno");
        var ana = Liderado.Criar("Ana");

        repository
            .Setup(x => x.ListarAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new[] { bruno, ana });

        var handler = new ListarLideradosHandler(repository.Object);

        var result = await handler.HandleAsync(new ListarLideradosQuery(), CancellationToken.None);

        result.Should().HaveCount(2);
        result.Select(x => x.Nome).Should().ContainInOrder("Ana", "Bruno");
    }
}

