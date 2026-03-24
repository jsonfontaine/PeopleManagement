using PeopleManagement.Application.Common;
using PeopleManagement.Application.Features.SituacaoAtual;

namespace PeopleManagement.Tests.Features.SituacaoAtual;

public sealed class SituacaoAtualServiceTests
{
    [Fact]
    public async Task SalvarAsync_DeveLancarExcecao_QuandoValorForInvalido()
    {
        var service = new SituacaoAtualService(new FakeSituacaoAtualRepository(existeLiderado: true));

        await Assert.ThrowsAsync<RegraNegocioException>(() =>
            service.SalvarAsync(Guid.NewGuid(), "   ", new DateOnly(2026, 3, 23), CancellationToken.None));
    }

    [Fact]
    public async Task SalvarAsync_DeveLancarExcecao_QuandoLideradoNaoExiste()
    {
        var service = new SituacaoAtualService(new FakeSituacaoAtualRepository(existeLiderado: false));

        await Assert.ThrowsAsync<RegraNegocioException>(() =>
            service.SalvarAsync(Guid.NewGuid(), "Em desenvolvimento", new DateOnly(2026, 3, 23), CancellationToken.None));
    }

    private sealed class FakeSituacaoAtualRepository : ISituacaoAtualRepository
    {
        private readonly bool _existeLiderado;

        public FakeSituacaoAtualRepository(bool existeLiderado)
        {
            _existeLiderado = existeLiderado;
        }

        public Task<IReadOnlyCollection<SituacaoAtualRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
            => Task.FromResult<IReadOnlyCollection<SituacaoAtualRegistro>>(Array.Empty<SituacaoAtualRegistro>());

        public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
            => Task.FromResult(_existeLiderado);

        public Task UpsertAsync(SituacaoAtualRegistro registro, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}

