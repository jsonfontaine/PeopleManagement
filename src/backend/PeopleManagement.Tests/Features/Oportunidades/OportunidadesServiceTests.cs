using PeopleManagement.Application.Common;
using PeopleManagement.Application.Features.Oportunidades;

namespace PeopleManagement.Tests.Features.Oportunidades;

public sealed class OportunidadesServiceTests
{
    [Fact]
    public async Task SalvarAsync_DeveLancarExcecao_QuandoValorForInvalido()
    {
        var service = new OportunidadesService(new FakeOportunidadesRepository(existeLiderado: true));

        await Assert.ThrowsAsync<RegraNegocioException>(() =>
            service.SalvarAsync(Guid.NewGuid(), "   ", new DateOnly(2026, 3, 23), CancellationToken.None));
    }

    [Fact]
    public async Task SalvarAsync_DeveLancarExcecao_QuandoLideradoNaoExiste()
    {
        var service = new OportunidadesService(new FakeOportunidadesRepository(existeLiderado: false));

        await Assert.ThrowsAsync<RegraNegocioException>(() =>
            service.SalvarAsync(Guid.NewGuid(), "Liderança técnica", new DateOnly(2026, 3, 23), CancellationToken.None));
    }

    private sealed class FakeOportunidadesRepository : IOportunidadesRepository
    {
        private readonly bool _existeLiderado;

        public FakeOportunidadesRepository(bool existeLiderado)
        {
            _existeLiderado = existeLiderado;
        }

        public Task<IReadOnlyCollection<OportunidadesRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
            => Task.FromResult<IReadOnlyCollection<OportunidadesRegistro>>(Array.Empty<OportunidadesRegistro>());

        public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
            => Task.FromResult(_existeLiderado);

        public Task UpsertAsync(OportunidadesRegistro registro, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}

