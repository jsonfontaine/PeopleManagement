using FluentAssertions;
using Moq;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Application.Features.Historico.ListarHistoricoAlteracoes;

namespace PeopleManagement.Tests.Application.Features.Historico.ListarHistoricoAlteracoes;

public sealed class ListarHistoricoAlteracoesHandlerTests
{
    [Fact]
    public async Task HandleAsync_DeveOrdenarHistoricoPorDataDesc()
    {
        var lideradoId = Guid.NewGuid();
        var repo = new Mock<IHistoricoAlteracaoRepository>();
        repo
            .Setup(x => x.ListarPorLideradoAsync(lideradoId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new[]
            {
                new HistoricoAlteracaoRegistro(lideradoId, "A", "A", null, "1", new DateTime(2024,1,1,0,0,0,DateTimeKind.Utc), "u"),
                new HistoricoAlteracaoRegistro(lideradoId, "A", "A", null, "2", new DateTime(2024,2,1,0,0,0,DateTimeKind.Utc), "u")
            });

        var handler = new ListarHistoricoAlteracoesHandler(repo.Object);

        var response = await handler.HandleAsync(new ListarHistoricoAlteracoesQuery(lideradoId), CancellationToken.None);

        response.Registros.First().ValorNovo.Should().Be("2");
    }
}

