using FluentAssertions;
using Moq;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Application.Features.ClassificacaoPerfil.SalvarClassificacaoPerfil;
using PeopleManagement.Domain;

namespace PeopleManagement.Tests.Application.Features.ClassificacaoPerfil.SalvarClassificacaoPerfil;

public sealed class SalvarClassificacaoPerfilHandlerTests
{
    [Fact]
    public async Task HandleAsync_DeveLancarQuandoPerfilForInvalido()
    {
        var repo = new Mock<IClassificacaoPerfilRepository>();
        var historico = new Mock<IHistoricoAlteracaoRepository>();
        var usuario = new Mock<IUsuarioContexto>();
        usuario.SetupGet(x => x.UsuarioAtual).Returns("gestor");

        var handler = new SalvarClassificacaoPerfilHandler(repo.Object, historico.Object, usuario.Object);

        Func<Task> act = async () => await handler.HandleAsync(
            new SalvarClassificacaoPerfilCommand(Guid.NewGuid(), " ", "2C"),
            CancellationToken.None);

        await act.Should().ThrowAsync<DomainException>();
    }

    [Fact]
    public async Task HandleAsync_DeveSalvarEClassificarNoHistoricoQuandoValorMudar()
    {
        var lideradoId = Guid.NewGuid();
        var repo = new Mock<IClassificacaoPerfilRepository>();
        repo.Setup(x => x.ObterAsync(lideradoId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ClassificacaoPerfilRegistro(lideradoId, "Analista", "1A", DateTime.UtcNow));

        var historico = new Mock<IHistoricoAlteracaoRepository>();
        var usuario = new Mock<IUsuarioContexto>();
        usuario.SetupGet(x => x.UsuarioAtual).Returns("gestor");

        var handler = new SalvarClassificacaoPerfilHandler(repo.Object, historico.Object, usuario.Object);

        var response = await handler.HandleAsync(
            new SalvarClassificacaoPerfilCommand(lideradoId, "Especialista", "2B"),
            CancellationToken.None);

        response.Perfil.Should().Be("Especialista");
        response.NineBox.Should().Be("2B");
        historico.Verify(x => x.RegistrarAsync(It.IsAny<HistoricoAlteracaoRegistro>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
    }
}

