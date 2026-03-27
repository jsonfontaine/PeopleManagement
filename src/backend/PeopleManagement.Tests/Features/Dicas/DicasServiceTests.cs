using PeopleManagement.Application.Common;
using PeopleManagement.Application.Features.Dicas;

namespace PeopleManagement.Tests.Features.Dicas;

public sealed class DicasServiceTests
{
    [Fact]
    public async Task ObterAsync_DeveRetornarVazioQuandoNaoExisteRegistro()
    {
        var service = new DicasService(new FakeDicasRepository());

        var result = await service.ObterAsync(CancellationToken.None);

        Assert.Equal(string.Empty, result.ConteudoHtml);
    }

    [Fact]
    public async Task SalvarAsync_DeveValidarConteudoNulo()
    {
        var service = new DicasService(new FakeDicasRepository());

        var ex = await Assert.ThrowsAsync<RegraNegocioException>(() =>
            service.SalvarAsync(null, CancellationToken.None));

        Assert.Equal("O conteudo de dicas e obrigatorio.", ex.Message);
    }

    private sealed class FakeDicasRepository : IDicasRepository
    {
        public Task<DicasRegistro?> ObterAsync(CancellationToken cancellationToken)
            => Task.FromResult<DicasRegistro?>(null);

        public Task SalvarAsync(string conteudoHtml, CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}

