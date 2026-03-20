using FluentAssertions;
using Moq;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Application.Features.Liderados.ObterVisaoIndividual;

namespace PeopleManagement.Tests.Application.Features.Liderados.ObterVisaoIndividual;

public sealed class ObterVisaoIndividualHandlerTests
{
    [Fact]
    public async Task HandleAsync_DeveRetornarConteudo_QuandoLideradoExistir()
    {
        var lideradoId = Guid.NewGuid();
        var repository = new Mock<IVisaoIndividualRepository>();
        repository
            .Setup(x => x.ObterAsync(lideradoId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new VisaoIndividualProjection(
                lideradoId,
                new InformacoesPessoais("Ana", null, null, null, null, null, null, null, null, null, null),
                null,
                null,
                2,
                1,
                Array.Empty<DateOnly>()));

        var handler = new ObterVisaoIndividualHandler(repository.Object);

        var response = await handler.HandleAsync(new ObterVisaoIndividualQuery(lideradoId), CancellationToken.None);

        response.Should().NotBeNull();
        response!.Conteudo.InformacoesPessoais.Nome.Should().Be("Ana");
    }
}

