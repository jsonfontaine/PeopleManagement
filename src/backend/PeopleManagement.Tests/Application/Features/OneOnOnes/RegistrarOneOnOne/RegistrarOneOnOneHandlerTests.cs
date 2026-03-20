using FluentAssertions;
using Moq;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Application.Features.OneOnOnes.RegistrarOneOnOne;
using PeopleManagement.Domain;

namespace PeopleManagement.Tests.Application.Features.OneOnOnes.RegistrarOneOnOne;

public sealed class RegistrarOneOnOneHandlerTests
{
    [Fact]
    public async Task HandleAsync_DeveLancarQuandoResumoNaoForInformado()
    {
        var repo = new Mock<IOneOnOneRepository>();
        var historico = new Mock<IHistoricoAlteracaoRepository>();
        var usuario = new Mock<IUsuarioContexto>();
        usuario.SetupGet(x => x.UsuarioAtual).Returns("gestor");

        var handler = new RegistrarOneOnOneHandler(repo.Object, historico.Object, usuario.Object);

        Func<Task> act = async () => await handler.HandleAsync(
            new RegistrarOneOnOneCommand(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), " ", "tarefa", "assunto"),
            CancellationToken.None);

        await act.Should().ThrowAsync<DomainException>();
    }

    [Fact]
    public async Task HandleAsync_DeveRegistrarQuandoResumoForValido()
    {
        var repo = new Mock<IOneOnOneRepository>();
        repo.Setup(x => x.AdicionarAsync(It.IsAny<OneOnOneRegistro>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var historico = new Mock<IHistoricoAlteracaoRepository>();
        historico.Setup(x => x.RegistrarAsync(It.IsAny<HistoricoAlteracaoRegistro>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var usuario = new Mock<IUsuarioContexto>();
        usuario.SetupGet(x => x.UsuarioAtual).Returns("gestor");

        var handler = new RegistrarOneOnOneHandler(repo.Object, historico.Object, usuario.Object);

        var response = await handler.HandleAsync(
            new RegistrarOneOnOneCommand(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), "Resumo", "tarefa", "assunto"),
            CancellationToken.None);

        response.Data.Should().Be(DateOnly.FromDateTime(DateTime.Today));
    }
}

