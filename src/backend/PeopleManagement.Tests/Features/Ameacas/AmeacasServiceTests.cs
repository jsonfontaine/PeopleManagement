using PeopleManagement.Application.Common;
using PeopleManagement.Application.Features.Ameacas;

namespace PeopleManagement.Tests.Features.Ameacas;

public sealed class AmeacasServiceTests
{
    [Fact]
    public async Task SalvarAsync_DeveLancarExcecao_QuandoValorForInvalido()
    {
        var service = new AmeacasService(new FakeAmeacasRepository(existeLiderado: true));

        await Assert.ThrowsAsync<RegraNegocioException>(() =>
            service.SalvarAsync(Guid.NewGuid(), "   ", new DateOnly(2026, 3, 23), CancellationToken.None));
    }

    [Fact]
    public async Task SalvarAsync_DeveLancarExcecao_QuandoLideradoNaoExiste()
    {
        var service = new AmeacasService(new FakeAmeacasRepository(existeLiderado: false));

        await Assert.ThrowsAsync<RegraNegocioException>(() =>
            service.SalvarAsync(Guid.NewGuid(), "Alta rotatividade no mercado", new DateOnly(2026, 3, 23), CancellationToken.None));
    }

    private sealed class FakeAmeacasRepository : IAmeacasRepository
    {
        private readonly bool _existeLiderado;

        public FakeAmeacasRepository(bool existeLiderado)
        {
            _existeLiderado = existeLiderado;
        }

        public Task<IReadOnlyCollection<AmeacasRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
            => Task.FromResult<IReadOnlyCollection<AmeacasRegistro>>(Array.Empty<AmeacasRegistro>());

        public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
            => Task.FromResult(_existeLiderado);

        public Task UpsertAsync(AmeacasRegistro registro, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}

