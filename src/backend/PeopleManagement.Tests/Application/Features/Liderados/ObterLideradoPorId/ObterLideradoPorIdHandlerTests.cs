using FluentAssertions;
using Moq;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Application.Features.Liderados.ObterLideradoPorId;
using PeopleManagement.Domain.Liderados;

namespace PeopleManagement.Tests.Application.Features.Liderados.ObterLideradoPorId;

public sealed class ObterLideradoPorIdHandlerTests
{
    [Fact]
    public async Task HandleAsync_DeveRetornarLideradoQuandoEncontrado()
    {
        var liderado = Liderado.Criar("Ana");
        var repo = new Mock<ILideradoRepository>();
        repo.Setup(x => x.ObterPorIdAsync(liderado.Id, It.IsAny<CancellationToken>())).ReturnsAsync(liderado);

        var handler = new ObterLideradoPorIdHandler(repo.Object);

        var response = await handler.HandleAsync(new ObterLideradoPorIdQuery(liderado.Id), CancellationToken.None);

        response.Should().NotBeNull();
        response!.Nome.Should().Be("Ana");
    }
}

