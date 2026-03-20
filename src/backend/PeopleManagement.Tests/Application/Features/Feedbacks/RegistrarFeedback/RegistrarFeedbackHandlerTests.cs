using FluentAssertions;
using Moq;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Application.Features.Feedbacks.RegistrarFeedback;
using PeopleManagement.Domain;

namespace PeopleManagement.Tests.Application.Features.Feedbacks.RegistrarFeedback;

public sealed class RegistrarFeedbackHandlerTests
{
    [Fact]
    public async Task HandleAsync_DeveLancarQuandoPolaridadeForInvalida()
    {
        var feedbackRepo = new Mock<IFeedbackRepository>();
        var historicoRepo = new Mock<IHistoricoAlteracaoRepository>();
        var usuario = new Mock<IUsuarioContexto>();
        usuario.SetupGet(x => x.UsuarioAtual).Returns("gestor");

        var handler = new RegistrarFeedbackHandler(feedbackRepo.Object, historicoRepo.Object, usuario.Object);

        Func<Task> act = async () => await handler.HandleAsync(
            new RegistrarFeedbackCommand(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), "conteudo", "boa", "Neutro"),
            CancellationToken.None);

        await act.Should().ThrowAsync<DomainException>();
    }

    [Fact]
    public async Task HandleAsync_DeveRegistrarFeedbackQuandoDadosForemValidos()
    {
        var feedbackRepo = new Mock<IFeedbackRepository>();
        feedbackRepo.Setup(x => x.AdicionarAsync(It.IsAny<FeedbackRegistro>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var historicoRepo = new Mock<IHistoricoAlteracaoRepository>();
        historicoRepo.Setup(x => x.RegistrarAsync(It.IsAny<HistoricoAlteracaoRegistro>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var usuario = new Mock<IUsuarioContexto>();
        usuario.SetupGet(x => x.UsuarioAtual).Returns("gestor");

        var handler = new RegistrarFeedbackHandler(feedbackRepo.Object, historicoRepo.Object, usuario.Object);

        var response = await handler.HandleAsync(
            new RegistrarFeedbackCommand(Guid.NewGuid(), DateOnly.FromDateTime(DateTime.Today), "conteudo", "boa", "Positivo"),
            CancellationToken.None);

        response.Polaridade.Should().Be("Positivo");
        feedbackRepo.Verify(x => x.AdicionarAsync(It.IsAny<FeedbackRegistro>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}

