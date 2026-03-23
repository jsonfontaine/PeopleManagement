using PeopleManagement.Application.Common;
using PeopleManagement.Application.Features.Conhecimentos;

namespace PeopleManagement.Tests.Features.Conhecimentos;

public sealed class ConhecimentosServiceTests
{
    [Fact]
    public async Task SalvarAsync_DeveLancarExcecao_QuandoValorForInvalido()
    {
        var service = new ConhecimentosService(new FakeConhecimentosRepository(existeLiderado: true));

        await Assert.ThrowsAsync<RegraNegocioException>(() =>
            service.SalvarAsync(Guid.NewGuid(), "   ", new DateOnly(2026, 3, 23), CancellationToken.None));
    }

    [Fact]
    public async Task SalvarAsync_DeveLancarExcecao_QuandoLideradoNaoExiste()
    {
        var service = new ConhecimentosService(new FakeConhecimentosRepository(existeLiderado: false));

        await Assert.ThrowsAsync<RegraNegocioException>(() =>
            service.SalvarAsync(Guid.NewGuid(), "C#", new DateOnly(2026, 3, 23), CancellationToken.None));
    }

    private sealed class FakeConhecimentosRepository : IConhecimentosRepository
    {
        private readonly bool _existeLiderado;

        public FakeConhecimentosRepository(bool existeLiderado)
        {
            _existeLiderado = existeLiderado;
        }

        public Task<IReadOnlyCollection<ConhecimentosRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
            => Task.FromResult<IReadOnlyCollection<ConhecimentosRegistro>>(Array.Empty<ConhecimentosRegistro>());

        public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
            => Task.FromResult(_existeLiderado);

        public Task UpsertAsync(ConhecimentosRegistro registro, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}

