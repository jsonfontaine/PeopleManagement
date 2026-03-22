using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Liderados;

public sealed class LideradosRepository : ILideradosRepository
{
    private readonly IStorageCommandBus _storageCommandBus;

    public LideradosRepository(IStorageCommandBus storageCommandBus)
    {
        _storageCommandBus = storageCommandBus;
    }

    public Task<bool> ExistePorNomeAsync(string nomeNormalizado, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ExisteLideradoPorNomeQuery(nomeNormalizado), cancellationToken);

    public Task<bool> ExistePorIdAsync(Guid id, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ExisteLideradoPorIdQuery(id), cancellationToken);

    public Task AdicionarAsync(LideradoSlice liderado, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new AdicionarLideradoCommand(liderado), cancellationToken);

    public Task<LideradoSlice?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ObterLideradoPorIdQuery(id), cancellationToken);

    public Task AtualizarNomeAsync(Guid id, string nome, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new AtualizarNomeLideradoCommand(id, nome), cancellationToken);

    public Task RemoverComDependenciasAsync(Guid id, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new RemoverLideradoComDependenciasCommand(id), cancellationToken);

    public Task<IReadOnlyCollection<LideradoResumoResponse>> ListarAsync(CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ListarLideradosQuery(), cancellationToken);

    public Task<ObterLideradoPorIdResponse?> ObterDetalheAsync(Guid id, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ObterDetalheLideradoQuery(id), cancellationToken);

    public Task<ObterVisaoIndividualResponse?> ObterVisaoIndividualAsync(Guid id, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ObterVisaoIndividualQuery(id), cancellationToken);

    public Task<IReadOnlyCollection<FeedbackRegistro>> ListarFeedbacksAsync(Guid id, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ListarFeedbacksQuery(id), cancellationToken);

    public Task CriarFeedbackAsync(Guid id, CriarFeedbackInput input, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new CriarFeedbackCommand(id, input), cancellationToken);

    public Task<IReadOnlyCollection<OneOnOneRegistro>> ListarOneOnOnesAsync(Guid id, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ListarOneOnOnesQuery(id), cancellationToken);

    public Task CriarOneOnOneAsync(Guid id, CriarOneOnOneInput input, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new CriarOneOnOneCommand(id, input), cancellationToken);

    public Task SalvarCulturaAsync(Guid id, RadarCulturalResponse radar, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new SalvarCulturaCommand(id, radar), cancellationToken);

    public Task<RadarCulturalResponse?> ObterRadarCulturalAsync(Guid id, DateOnly data, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ObterRadarCulturalQuery(id, data), cancellationToken);

    public Task AtualizarInformacoesPessoaisAsync(Guid id, AtualizarInformacoesPessoaisInput input, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new AtualizarInformacoesPessoaisCommand(id, input), cancellationToken);

    public Task AtualizarClassificacaoPerfilAsync(Guid id, string perfil, string nineBox, DateOnly data, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new AtualizarClassificacaoPerfilCommand(id, perfil, nineBox, data), cancellationToken);
}

public sealed record ExisteLideradoPorNomeQuery(string NomeNormalizado) : IStorageCommand<bool>;
public sealed record ExisteLideradoPorIdQuery(Guid Id) : IStorageCommand<bool>;
public sealed record AdicionarLideradoCommand(LideradoSlice Liderado) : IStorageCommand<StorageUnit>;
public sealed record ObterLideradoPorIdQuery(Guid Id) : IStorageCommand<LideradoSlice?>;
public sealed record AtualizarNomeLideradoCommand(Guid Id, string Nome) : IStorageCommand<StorageUnit>;
public sealed record RemoverLideradoComDependenciasCommand(Guid Id) : IStorageCommand<StorageUnit>;
public sealed record ListarLideradosQuery : IStorageCommand<IReadOnlyCollection<LideradoResumoResponse>>;
public sealed record ObterDetalheLideradoQuery(Guid Id) : IStorageCommand<ObterLideradoPorIdResponse?>;
public sealed record ObterVisaoIndividualQuery(Guid Id) : IStorageCommand<ObterVisaoIndividualResponse?>;
public sealed record ListarFeedbacksQuery(Guid Id) : IStorageCommand<IReadOnlyCollection<FeedbackRegistro>>;
public sealed record CriarFeedbackCommand(Guid Id, CriarFeedbackInput Input) : IStorageCommand<StorageUnit>;
public sealed record ListarOneOnOnesQuery(Guid Id) : IStorageCommand<IReadOnlyCollection<OneOnOneRegistro>>;
public sealed record CriarOneOnOneCommand(Guid Id, CriarOneOnOneInput Input) : IStorageCommand<StorageUnit>;
public sealed record SalvarCulturaCommand(Guid Id, RadarCulturalResponse Radar) : IStorageCommand<StorageUnit>;
public sealed record ObterRadarCulturalQuery(Guid Id, DateOnly Data) : IStorageCommand<RadarCulturalResponse?>;
public sealed record AtualizarInformacoesPessoaisCommand(Guid Id, AtualizarInformacoesPessoaisInput Input) : IStorageCommand<StorageUnit>;
public sealed record AtualizarClassificacaoPerfilCommand(Guid Id, string Perfil, string NineBox, DateOnly Data) : IStorageCommand<StorageUnit>;

