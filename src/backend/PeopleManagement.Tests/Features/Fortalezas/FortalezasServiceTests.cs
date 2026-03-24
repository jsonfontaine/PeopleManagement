 using PeopleManagement.Application.Common;
using PeopleManagement.Application.Features.Fortalezas;

namespace PeopleManagement.Tests.Features.Fortalezas;

public sealed class FortalezasServiceTests
{
    [Fact]
    public async Task SalvarAsync_DeveLancarExcecao_QuandoValorForInvalido()
    {
        var service = new FortalezasService(new FakeFortalezasRepository(existeLiderado: true));

        await Assert.ThrowsAsync<RegraNegocioException>(() =>
            service.SalvarAsync(Guid.NewGuid(), "   ", new DateOnly(2026, 3, 23), CancellationToken.None));
    }

    [Fact]
    public async Task SalvarAsync_DeveLancarExcecao_QuandoLideradoNaoExiste()
    {
        var service = new FortalezasService(new FakeFortalezasRepository(existeLiderado: false));

        await Assert.ThrowsAsync<RegraNegocioException>(() =>
            service.SalvarAsync(Guid.NewGuid(), "Comunicação", new DateOnly(2026, 3, 23), CancellationToken.None));
    }

    private sealed class FakeFortalezasRepository : IFortalezasRepository
    {
        private readonly bool _existeLiderado;

        public FakeFortalezasRepository(bool existeLiderado)
        {
            _existeLiderado = existeLiderado;
        }

        public Task<IReadOnlyCollection<FortalezasRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
            => Task.FromResult<IReadOnlyCollection<FortalezasRegistro>>(Array.Empty<FortalezasRegistro>());

        public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
            => Task.FromResult(_existeLiderado);

        public Task UpsertAsync(FortalezasRegistro registro, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}

