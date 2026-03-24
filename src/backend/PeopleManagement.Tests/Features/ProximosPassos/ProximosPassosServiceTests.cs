using PeopleManagement.Application.Common;
using PeopleManagement.Application.Features.ProximosPassos;

namespace PeopleManagement.Tests.Features.ProximosPassos;

public sealed class ProximosPassosServiceTests
{
    [Fact]
    public async Task SalvarAsync_DeveLancarExcecao_QuandoValorForInvalido()
    {
        var service = new ProximosPassosService(new FakeProximosPassosRepository(existeLiderado: true));

        await Assert.ThrowsAsync<RegraNegocioException>(() =>
            service.SalvarAsync(Guid.NewGuid(), "   ", new DateOnly(2026, 3, 23), CancellationToken.None));
    }

    [Fact]
    public async Task SalvarAsync_DeveLancarExcecao_QuandoLideradoNaoExiste()
    {
        var service = new ProximosPassosService(new FakeProximosPassosRepository(existeLiderado: false));

        await Assert.ThrowsAsync<RegraNegocioException>(() =>
            service.SalvarAsync(Guid.NewGuid(), "Iniciar mentoria", new DateOnly(2026, 3, 23), CancellationToken.None));
    }

    private sealed class FakeProximosPassosRepository : IProximosPassosRepository
    {
        private readonly bool _existeLiderado;

        public FakeProximosPassosRepository(bool existeLiderado)
        {
            _existeLiderado = existeLiderado;
        }

        public Task<IReadOnlyCollection<ProximosPassosRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
            => Task.FromResult<IReadOnlyCollection<ProximosPassosRegistro>>(Array.Empty<ProximosPassosRegistro>());

        public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
            => Task.FromResult(_existeLiderado);

        public Task UpsertAsync(ProximosPassosRegistro registro, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}

