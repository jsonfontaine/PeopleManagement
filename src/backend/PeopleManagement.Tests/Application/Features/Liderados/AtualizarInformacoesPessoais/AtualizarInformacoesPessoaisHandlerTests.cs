using FluentAssertions;
using Moq;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Application.Features.Liderados.AtualizarInformacoesPessoais;

namespace PeopleManagement.Tests.Application.Features.Liderados.AtualizarInformacoesPessoais;

public sealed class AtualizarInformacoesPessoaisHandlerTests
{
    [Fact]
    public async Task HandleAsync_DeveSalvarDadosERegistrarHistorico()
    {
        var lideradoId = Guid.NewGuid();
        var informacoes = new InformacoesPessoais("Ana", null, null, null, null, null, null, null, null, null, null);

        var informacoesRepo = new Mock<IInformacoesPessoaisRepository>();
        informacoesRepo.Setup(x => x.SalvarAsync(lideradoId, informacoes, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var historicoRepo = new Mock<IHistoricoAlteracaoRepository>();
        historicoRepo.Setup(x => x.RegistrarAsync(It.IsAny<HistoricoAlteracaoRegistro>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var usuario = new Mock<IUsuarioContexto>();
        usuario.SetupGet(x => x.UsuarioAtual).Returns("gestor");

        var handler = new AtualizarInformacoesPessoaisHandler(informacoesRepo.Object, historicoRepo.Object, usuario.Object);

        var response = await handler.HandleAsync(new AtualizarInformacoesPessoaisCommand(lideradoId, informacoes), CancellationToken.None);

        response.NomeAtualizado.Should().Be("Ana");
        historicoRepo.Verify(x => x.RegistrarAsync(It.IsAny<HistoricoAlteracaoRegistro>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}

