using PeopleManagement.Application.Common;
using PeopleManagement.Application.Features.Personalidade;

namespace PeopleManagement.Tests.Features.Personalidade;

public sealed class PersonalidadeServiceTests
{
    [Fact]
    public async Task SalvarAsync_DeveLancarExcecao_QuandoValorForInvalido()
    {
        var service = new PersonalidadeService(new FakePersonalidadeRepository(existeLiderado: true));

        await Assert.ThrowsAsync<RegraNegocioException>(() =>
            service.SalvarAsync(Guid.NewGuid(), "   ", new DateOnly(2026, 3, 23), CancellationToken.None));
    }

    [Fact]
    public async Task SalvarAsync_DeveLancarExcecao_QuandoLideradoNaoExiste()
    {
        var service = new PersonalidadeService(new FakePersonalidadeRepository(existeLiderado: false));

        await Assert.ThrowsAsync<RegraNegocioException>(() =>
            service.SalvarAsync(Guid.NewGuid(), "Analítico", new DateOnly(2026, 3, 23), CancellationToken.None));
    }

    private sealed class FakePersonalidadeRepository : IPersonalidadeRepository
    {
        private readonly bool _existeLiderado;

        public FakePersonalidadeRepository(bool existeLiderado)
        {
            _existeLiderado = existeLiderado;
        }

        public Task<IReadOnlyCollection<PersonalidadeRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
            => Task.FromResult<IReadOnlyCollection<PersonalidadeRegistro>>(Array.Empty<PersonalidadeRegistro>());

        public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
            => Task.FromResult(_existeLiderado);

        public Task UpsertAsync(PersonalidadeRegistro registro, CancellationToken cancellationToken)
            => Task.CompletedTask;

        public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}

