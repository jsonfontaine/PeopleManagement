using FluentAssertions;
using Moq;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Application.Features.Cultura.RegistrarAvaliacaoCultura;
using PeopleManagement.Domain;

namespace PeopleManagement.Tests.Application.Features.Cultura.RegistrarAvaliacaoCultura;

public sealed class RegistrarAvaliacaoCulturaHandlerTests
{
    [Fact]
    public async Task HandleAsync_DeveLancarQuandoNotaForInvalida()
    {
        var repo = new Mock<ICulturaRepository>();
        var historico = new Mock<IHistoricoAlteracaoRepository>();
        var usuario = new Mock<IUsuarioContexto>();
        usuario.SetupGet(x => x.UsuarioAtual).Returns("gestor");

        var handler = new RegistrarAvaliacaoCulturaHandler(repo.Object, historico.Object, usuario.Object);

        Func<Task> act = async () => await handler.HandleAsync(
            new RegistrarAvaliacaoCulturaCommand(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), 11, 1, 1, 1, 1, 1, 1),
            CancellationToken.None);

        await act.Should().ThrowAsync<DomainException>();
    }

    [Fact]
    public async Task HandleAsync_DeveRegistrarQuandoNotasForemValidas()
    {
        var repo = new Mock<ICulturaRepository>();
        repo.Setup(x => x.AdicionarAvaliacaoAsync(It.IsAny<AvaliacaoCulturaRegistro>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var historico = new Mock<IHistoricoAlteracaoRepository>();
        historico.Setup(x => x.RegistrarAsync(It.IsAny<HistoricoAlteracaoRegistro>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var usuario = new Mock<IUsuarioContexto>();
        usuario.SetupGet(x => x.UsuarioAtual).Returns("gestor");

        var data = DateOnly.FromDateTime(DateTime.Today);
        var handler = new RegistrarAvaliacaoCulturaHandler(repo.Object, historico.Object, usuario.Object);

        var response = await handler.HandleAsync(
            new RegistrarAvaliacaoCulturaCommand(Guid.NewGuid(), data, 8, 8, 8, 8, 8, 8, 8),
            CancellationToken.None);

        response.Data.Should().Be(data);
    }
}

