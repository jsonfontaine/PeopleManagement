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
    public async Task CriarFeedbackAsync(Guid id, CriarFeedbackInput input, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(input.Conteudo))
        {
            throw new RegraNegocioException("O conteudo de Feedbacks e obrigatorio.");
        }
        if (string.IsNullOrWhiteSpace(input.Receptividade))
        {
            throw new RegraNegocioException("A receptividade de Feedbacks e obrigatoria.");
        }
        if (string.IsNullOrWhiteSpace(input.Polaridade))
        {
            throw new RegraNegocioException("A polaridade de Feedbacks e obrigatoria.");
        }
        var polaridade = new[] { "Positivo", "Negativo" }
            .FirstOrDefault(x => x.Equals(input.Polaridade.Trim(), StringComparison.OrdinalIgnoreCase));
        if (polaridade is null)
        {
            throw new RegraNegocioException("A polaridade de Feedbacks deve ser Positivo ou Negativo.");
        }
        if (!await _repository.ExistePorIdAsync(id, cancellationToken))
        {
            throw new RegraNegocioException("Liderado nao encontrado para registro de Feedbacks.");
        }
        await _repository.CriarFeedbackAsync(id, input with
        {
            Conteudo = input.Conteudo.Trim(),
            Receptividade = input.Receptividade.Trim(),
            Polaridade = polaridade
        }, cancellationToken);
    }
    public Task<IReadOnlyCollection<OneOnOneRegistro>> ListarOneOnOnesAsync(Guid id, CancellationToken cancellationToken)
        => _repository.ListarOneOnOnesAsync(id, cancellationToken);
    public async Task CriarOneOnOneAsync(Guid id, CriarOneOnOneInput input, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(input.Resumo))
        {
            throw new RegraNegocioException("O resumo de 1:1 e obrigatorio.");
        }
        if (string.IsNullOrWhiteSpace(input.TarefasAcordadas))
        {
            throw new RegraNegocioException("As tarefas acordadas de 1:1 sao obrigatorias.");
        }
        if (string.IsNullOrWhiteSpace(input.ProximosAssuntos))
        {
            throw new RegraNegocioException("Os proximos assuntos de 1:1 sao obrigatorios.");
        }
        if (!await _repository.ExistePorIdAsync(id, cancellationToken))
        {
            throw new RegraNegocioException("Liderado nao encontrado para registro de 1:1.");
        }
        await _repository.CriarOneOnOneAsync(id, input with
        {
            Resumo = input.Resumo.Trim(),
            TarefasAcordadas = input.TarefasAcordadas.Trim(),
            ProximosAssuntos = input.ProximosAssuntos.Trim()
        }, cancellationToken);
    }
    public async Task SalvarCulturaAsync(Guid id, RadarCulturalResponse radar, CancellationToken cancellationToken)
    {
        if (!await _repository.ExistePorIdAsync(id, cancellationToken))
        {
            throw new RegraNegocioException("Liderado nao encontrado.");
        }
        foreach (var nota in new[]
        {
            radar.AprenderEMelhorarSempre,
            radar.AtitudeDeDono,
            radar.BuscarMelhoresResultadosParaClientes,
            radar.EspiritoDeEquipe,
            radar.Excelencia,
            radar.FazerAcontecer,
            radar.InovarParaInspirar
        })
        {
            if (nota < 0 || nota > 10)
            {
                throw new RegraNegocioException("Os valores de Cultura devem estar entre 0 e 10.");
            }
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
