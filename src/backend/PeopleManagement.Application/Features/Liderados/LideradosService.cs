using PeopleManagement.Application.Common;
namespace PeopleManagement.Application.Features.Liderados;
public sealed class LideradosService
{
    private readonly ILideradosRepository _repository;
    public LideradosService(ILideradosRepository repository)
    {
        _repository = repository;
    }
    public async Task<CriarLideradoResponse> CriarAsync(string nome, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new RegraNegocioException("O nome do liderado e obrigatorio.");
        }
        var nomeNormalizado = nome.Trim().ToLowerInvariant();
        if (await _repository.ExistePorNomeAsync(nomeNormalizado, cancellationToken))
        {
            throw new RegraNegocioException("Ja existe um liderado com este nome.");
        }
        var liderado = new LideradoSlice(Guid.NewGuid(), nome.Trim(), DateTime.UtcNow);
        await _repository.AdicionarAsync(liderado, cancellationToken);
        return new CriarLideradoResponse(liderado.Id, liderado.Nome, liderado.DataCriacaoUtc);
    }
    public async Task AtualizarNomeAsync(Guid id, string nome, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new RegraNegocioException("O nome do liderado e obrigatorio.");
        }
        var liderado = await _repository.ObterPorIdAsync(id, cancellationToken)
            ?? throw new RegraNegocioException($"Liderado com id {id} nao encontrado.");
        await _repository.AtualizarNomeAsync(liderado.Id, nome.Trim(), cancellationToken);
    }
    public async Task RemoverAsync(Guid id, CancellationToken cancellationToken)
    {
        if (!await _repository.ExistePorIdAsync(id, cancellationToken))
        {
            throw new RegraNegocioException($"Liderado com id {id} nao encontrado.");
        }
        await _repository.RemoverComDependenciasAsync(id, cancellationToken);
    }
    public Task<IReadOnlyCollection<LideradoResumoResponse>> ListarAsync(CancellationToken cancellationToken)
        => _repository.ListarAsync(cancellationToken);
    public Task<ObterLideradoPorIdResponse?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        => _repository.ObterDetalheAsync(id, cancellationToken);
    public Task<ObterVisaoIndividualResponse?> ObterVisaoIndividualAsync(Guid id, CancellationToken cancellationToken)
        => _repository.ObterVisaoIndividualAsync(id, cancellationToken);
    public Task<IReadOnlyCollection<FeedbackRegistro>> ListarFeedbacksAsync(Guid id, CancellationToken cancellationToken)
        => _repository.ListarFeedbacksAsync(id, cancellationToken);
    public Task CriarFeedbackAsync(Guid id, CriarFeedbackInput input, CancellationToken cancellationToken)
        => _repository.CriarFeedbackAsync(id, input, cancellationToken);
    public Task<IReadOnlyCollection<OneOnOneRegistro>> ListarOneOnOnesAsync(Guid id, CancellationToken cancellationToken)
        => _repository.ListarOneOnOnesAsync(id, cancellationToken);
    public Task CriarOneOnOneAsync(Guid id, CriarOneOnOneInput input, CancellationToken cancellationToken)
        => _repository.CriarOneOnOneAsync(id, input, cancellationToken);
    public async Task SalvarCulturaAsync(Guid id, RadarCulturalResponse radar, CancellationToken cancellationToken)
    {
        if (!await _repository.ExistePorIdAsync(id, cancellationToken))
        {
            throw new RegraNegocioException("Liderado nao encontrado.");
        }
        await _repository.SalvarCulturaAsync(id, radar, cancellationToken);
    }
    public Task<RadarCulturalResponse?> ObterRadarCulturalAsync(Guid id, DateOnly data, CancellationToken cancellationToken)
        => _repository.ObterRadarCulturalAsync(id, data, cancellationToken);
    public async Task AtualizarInformacoesPessoaisAsync(Guid id, AtualizarInformacoesPessoaisInput input, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(input.Nome))
        {
            throw new RegraNegocioException("O nome do liderado e obrigatorio.");
        }
        if (!await _repository.ExistePorIdAsync(id, cancellationToken))
        {
            throw new RegraNegocioException("Liderado nao encontrado.");
        }
        await _repository.AtualizarInformacoesPessoaisAsync(id, input with { Nome = input.Nome.Trim() }, cancellationToken);
    }
    public async Task AtualizarClassificacaoPerfilAsync(Guid id, string perfil, string nineBox, DateOnly data, CancellationToken cancellationToken)
    {
        _ = data;
        if (!await _repository.ExistePorIdAsync(id, cancellationToken))
        {
            throw new RegraNegocioException("Liderado nao encontrado.");
        }
        if (string.IsNullOrWhiteSpace(perfil) && string.IsNullOrWhiteSpace(nineBox))
        {
            return;
        }
    }
}
