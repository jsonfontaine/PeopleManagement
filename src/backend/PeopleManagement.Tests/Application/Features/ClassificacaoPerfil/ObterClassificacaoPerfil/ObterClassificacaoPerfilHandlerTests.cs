using FluentAssertions;
using Moq;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Application.Features.ClassificacaoPerfil.ObterClassificacaoPerfil;

namespace PeopleManagement.Tests.Application.Features.ClassificacaoPerfil.ObterClassificacaoPerfil;

public sealed class ObterClassificacaoPerfilHandlerTests
{
    [Fact]
    public async Task HandleAsync_DeveRetornarClassificacaoQuandoExistir()
    {
        var lideradoId = Guid.NewGuid();
        var repo = new Mock<IClassificacaoPerfilRepository>();
        repo.Setup(x => x.ObterAsync(lideradoId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ClassificacaoPerfilRegistro(lideradoId, "Analista", "3C", DateTime.UtcNow));

        var handler = new ObterClassificacaoPerfilHandler(repo.Object);

        var response = await handler.HandleAsync(new ObterClassificacaoPerfilQuery(lideradoId), CancellationToken.None);

        response.Should().NotBeNull();
        response!.Perfil.Should().Be("Analista");
        response.NineBox.Should().Be("3C");
    }
}

