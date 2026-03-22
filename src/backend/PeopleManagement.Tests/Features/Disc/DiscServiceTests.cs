using PeopleManagement.Application.Common;
using PeopleManagement.Application.Features.Disc;

namespace PeopleManagement.Tests.Features.Disc;

public sealed class DiscServiceTests
{
    [Fact]
    public async Task SalvarAsync_DeveLancarExcecao_QuandoValorForInvalido()
    {
        var service = new DiscService(new FakeDiscRepository(existeLiderado: true));

        await Assert.ThrowsAsync<RegraNegocioException>(() =>
            service.SalvarAsync(Guid.NewGuid(), "   ", new DateOnly(2026, 1, 10), CancellationToken.None));
    }

    private sealed class FakeDiscRepository : IDiscRepository
    {
        private readonly bool _existeLiderado;

        public FakeDiscRepository(bool existeLiderado)
        {
            _existeLiderado = existeLiderado;
        }

        public Task<IReadOnlyCollection<DiscRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
            => Task.FromResult<IReadOnlyCollection<DiscRegistro>>(Array.Empty<DiscRegistro>());

        public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
            => Task.FromResult(_existeLiderado);

        public Task UpsertAsync(DiscRegistro registro, CancellationToken cancellationToken) => Task.CompletedTask;

        public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken) => Task.CompletedTask;
    }
}

