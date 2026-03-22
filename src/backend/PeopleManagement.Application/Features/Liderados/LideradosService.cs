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
        if (string.IsNullOrWhiteSpace(perfil) || string.IsNullOrWhiteSpace(nineBox))
        {
            throw new RegraNegocioException("Perfil e Nine Box sao obrigatorios.");
        }

        if (!await _repository.ExistePorIdAsync(id, cancellationToken))
        {
            throw new RegraNegocioException("Liderado nao encontrado.");
        }

        await _repository.AtualizarClassificacaoPerfilAsync(id, perfil.Trim(), nineBox.Trim(), data, cancellationToken);
    }
}

public interface ILideradosRepository
{
    Task<bool> ExistePorNomeAsync(string nomeNormalizado, CancellationToken cancellationToken);
    Task<bool> ExistePorIdAsync(Guid id, CancellationToken cancellationToken);
    Task AdicionarAsync(LideradoSlice liderado, CancellationToken cancellationToken);
    Task<LideradoSlice?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task AtualizarNomeAsync(Guid id, string nome, CancellationToken cancellationToken);
    Task RemoverComDependenciasAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<LideradoResumoResponse>> ListarAsync(CancellationToken cancellationToken);
    Task<ObterLideradoPorIdResponse?> ObterDetalheAsync(Guid id, CancellationToken cancellationToken);
    Task<ObterVisaoIndividualResponse?> ObterVisaoIndividualAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<FeedbackRegistro>> ListarFeedbacksAsync(Guid id, CancellationToken cancellationToken);
    Task CriarFeedbackAsync(Guid id, CriarFeedbackInput input, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<OneOnOneRegistro>> ListarOneOnOnesAsync(Guid id, CancellationToken cancellationToken);
    Task CriarOneOnOneAsync(Guid id, CriarOneOnOneInput input, CancellationToken cancellationToken);
    Task SalvarCulturaAsync(Guid id, RadarCulturalResponse radar, CancellationToken cancellationToken);
    Task<RadarCulturalResponse?> ObterRadarCulturalAsync(Guid id, DateOnly data, CancellationToken cancellationToken);
    Task AtualizarInformacoesPessoaisAsync(Guid id, AtualizarInformacoesPessoaisInput input, CancellationToken cancellationToken);
    Task AtualizarClassificacaoPerfilAsync(Guid id, string perfil, string nineBox, DateOnly data, CancellationToken cancellationToken);
}


public sealed record LideradoSlice(Guid Id, string Nome, DateTime DataCriacaoUtc);

public sealed record CriarLideradoResponse(Guid Id, string Nome, DateTime DataCriacaoUtc);
public sealed record LideradoResumoResponse(Guid Id, string Nome, DateTime DataCriacaoUtc);
public sealed record ObterLideradoPorIdResponse(Guid Id, string Nome, DateTime DataCriacaoUtc);

public sealed record CriarFeedbackInput(DateOnly Data, string Conteudo, string Receptividade, string Polaridade);
public sealed record FeedbackRegistro(Guid LideradoId, DateOnly Data, string Conteudo, string Receptividade, string Polaridade);

public sealed record CriarOneOnOneInput(DateOnly Data, string Resumo, string TarefasAcordadas, string ProximosAssuntos);
public sealed record OneOnOneRegistro(Guid LideradoId, DateOnly Data, string Resumo, string TarefasAcordadas, string ProximosAssuntos);

public sealed record AtualizarInformacoesPessoaisInput(
    string Nome,
    DateOnly? DataNascimento,
    string? EstadoCivil,
    int? QuantidadeFilhos,
    DateOnly? DataContratacao,
    string? Cargo,
    DateOnly? DataInicioCargo,
    string? AspiracaoCarreira,
    string? GostosPessoais,
    string? RedFlags,
    string? Bio);

public sealed record InformacoesPessoais(
    string Nome,
    DateOnly? DataNascimento,
    string? EstadoCivil,
    int? QuantidadeFilhos,
    DateOnly? DataContratacao,
    string? Cargo,
    DateOnly? DataInicioCargo,
    string? AspiracaoCarreira,
    string? GostosPessoais,
    string? RedFlags,
    string? Bio);

public sealed record VisaoIndividualProjection(
    Guid LideradoId,
    InformacoesPessoais InformacoesPessoais,
    string? Perfil,
    string? NineBox,
    int QuantidadeFeedbacks,
    int QuantidadeOneOnOnes,
    IReadOnlyCollection<DateOnly> DatasAvaliacaoCultura);

public sealed record ObterVisaoIndividualResponse(VisaoIndividualProjection Conteudo);

public sealed record RadarCulturalResponse(
    DateOnly Data,
    int AprenderEMelhorarSempre,
    int AtitudeDeDono,
    int BuscarMelhoresResultadosParaClientes,
    int EspiritoDeEquipe,
    int Excelencia,
    int FazerAcontecer,
    int InovarParaInspirar);

