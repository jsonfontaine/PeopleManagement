using PeopleManagement.Application.Common;
using PeopleManagement.Application.Features.Liderados;

namespace PeopleManagement.Tests.Features.Liderados;

public sealed class LideradosServiceTests
{
    [Fact]
    public async Task CriarAsync_DeveLancarExcecao_QuandoNomeJaExistir()
    {
        var service = new LideradosService(new FakeLideradosRepository(existeNome: true));

        await Assert.ThrowsAsync<RegraNegocioException>(() =>
            service.CriarAsync("Ana", CancellationToken.None));
    }

    [Fact]
    public async Task AtualizarClassificacaoPerfilAsync_DevePermitirSalvarApenasPerfil()
    {
        var service = new LideradosService(new FakeLideradosRepository(existeNome: false, existeId: true));

        await service.AtualizarClassificacaoPerfilAsync(Guid.NewGuid(), "Executor", "", new DateOnly(2026, 3, 23), CancellationToken.None);
    }

    [Fact]
    public async Task AtualizarClassificacaoPerfilAsync_DevePermitirSalvarApenasNineBox()
    {
        var service = new LideradosService(new FakeLideradosRepository(existeNome: false, existeId: true));

        await service.AtualizarClassificacaoPerfilAsync(Guid.NewGuid(), "", "High Performer", new DateOnly(2026, 3, 23), CancellationToken.None);
    }

    private sealed class FakeLideradosRepository : ILideradosRepository
    {
        private readonly bool _existeNome;
        private readonly bool _existeId;

        public FakeLideradosRepository(bool existeNome, bool existeId = true)
        {
            _existeNome = existeNome;
            _existeId = existeId;
        }

        public Task<bool> ExistePorNomeAsync(string nomeNormalizado, CancellationToken cancellationToken)
            => Task.FromResult(_existeNome);

        public Task<bool> ExistePorIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(_existeId);

        public Task AdicionarAsync(LideradoSlice liderado, CancellationToken cancellationToken) => Task.CompletedTask;
        public Task<LideradoSlice?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken) => Task.FromResult<LideradoSlice?>(new LideradoSlice(id, "Ana", DateTime.UtcNow));
        public Task AtualizarNomeAsync(Guid id, string nome, CancellationToken cancellationToken) => Task.CompletedTask;
        public Task RemoverComDependenciasAsync(Guid id, CancellationToken cancellationToken) => Task.CompletedTask;
        public Task<IReadOnlyCollection<LideradoResumoResponse>> ListarAsync(CancellationToken cancellationToken) => Task.FromResult<IReadOnlyCollection<LideradoResumoResponse>>(Array.Empty<LideradoResumoResponse>());
        public Task<ObterLideradoPorIdResponse?> ObterDetalheAsync(Guid id, CancellationToken cancellationToken) => Task.FromResult<ObterLideradoPorIdResponse?>(null);
        public Task<ObterVisaoIndividualResponse?> ObterVisaoIndividualAsync(Guid id, CancellationToken cancellationToken) => Task.FromResult<ObterVisaoIndividualResponse?>(null);
        public Task<IReadOnlyCollection<FeedbackRegistro>> ListarFeedbacksAsync(Guid id, CancellationToken cancellationToken) => Task.FromResult<IReadOnlyCollection<FeedbackRegistro>>(Array.Empty<FeedbackRegistro>());
        public Task CriarFeedbackAsync(Guid id, CriarFeedbackInput input, CancellationToken cancellationToken) => Task.CompletedTask;
        public Task<IReadOnlyCollection<OneOnOneRegistro>> ListarOneOnOnesAsync(Guid id, CancellationToken cancellationToken) => Task.FromResult<IReadOnlyCollection<OneOnOneRegistro>>(Array.Empty<OneOnOneRegistro>());
        public Task CriarOneOnOneAsync(Guid id, CriarOneOnOneInput input, CancellationToken cancellationToken) => Task.CompletedTask;
        public Task SalvarCulturaAsync(Guid id, RadarCulturalResponse radar, CancellationToken cancellationToken) => Task.CompletedTask;
        public Task<RadarCulturalResponse?> ObterRadarCulturalAsync(Guid id, DateOnly data, CancellationToken cancellationToken) => Task.FromResult<RadarCulturalResponse?>(null);
        public Task AtualizarInformacoesPessoaisAsync(Guid id, AtualizarInformacoesPessoaisInput input, CancellationToken cancellationToken) => Task.CompletedTask;
    }
}

