using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Liderados;
using PeopleManagement.Infrastructure.Persistence;
using PeopleManagement.Infrastructure.Persistence.Entities;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class ExisteLideradoPorNomeHandler : IStorageCommandHandler<ExisteLideradoPorNomeQuery, bool>
{
    private readonly PeopleManagementDbContext _dbContext;
    public ExisteLideradoPorNomeHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;
    public Task<bool> HandleAsync(ExisteLideradoPorNomeQuery command, CancellationToken cancellationToken)
        => _dbContext.Liderados.AsNoTracking().AnyAsync(x => x.Nome.ToLower() == command.NomeNormalizado, cancellationToken);
}

public sealed class ExisteLideradoPorIdHandler : IStorageCommandHandler<ExisteLideradoPorIdQuery, bool>
{
    private readonly PeopleManagementDbContext _dbContext;
    public ExisteLideradoPorIdHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;
    public Task<bool> HandleAsync(ExisteLideradoPorIdQuery command, CancellationToken cancellationToken)
    {
        var idStr = command.Id.ToString().ToLowerInvariant();
        return _dbContext.Liderados.AsNoTracking().AnyAsync(x => x.Id.ToLower() == idStr, cancellationToken);
    }
}

public sealed class AdicionarLideradoHandler : IStorageCommandHandler<AdicionarLideradoCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;
    public AdicionarLideradoHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;
    public async Task<StorageUnit> HandleAsync(AdicionarLideradoCommand command, CancellationToken cancellationToken)
    {
        _dbContext.Liderados.Add(new LideradoEntity
        {
            Id = command.Liderado.Id.ToString(),
            Nome = command.Liderado.Nome,
            DataCriacaoUtc = command.Liderado.DataCriacaoUtc
        });
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new StorageUnit();
    }
}

public sealed class ObterLideradoPorIdHandler : IStorageCommandHandler<ObterLideradoPorIdQuery, LideradoSlice?>
{
    private readonly PeopleManagementDbContext _dbContext;
    public ObterLideradoPorIdHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;
    public async Task<LideradoSlice?> HandleAsync(ObterLideradoPorIdQuery command, CancellationToken cancellationToken)
    {
        var idStr = command.Id.ToString().ToLowerInvariant();
        var entity = await _dbContext.Liderados.AsNoTracking().FirstOrDefaultAsync(x => x.Id.ToLower() == idStr, cancellationToken);
        return entity is null ? null : new LideradoSlice(Guid.Parse(entity.Id), entity.Nome, entity.DataCriacaoUtc);
    }
}

public sealed class AtualizarNomeLideradoHandler : IStorageCommandHandler<AtualizarNomeLideradoCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;
    public AtualizarNomeLideradoHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;
    public async Task<StorageUnit> HandleAsync(AtualizarNomeLideradoCommand command, CancellationToken cancellationToken)
    {
        var idStr = command.Id.ToString().ToLowerInvariant();
        var entity = await _dbContext.Liderados.FirstAsync(x => x.Id.ToLower() == idStr, cancellationToken);
        entity.Nome = command.Nome;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new StorageUnit();
    }
}

public sealed class RemoverLideradoComDependenciasHandler : IStorageCommandHandler<RemoverLideradoComDependenciasCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;
    public RemoverLideradoComDependenciasHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;
    public async Task<StorageUnit> HandleAsync(RemoverLideradoComDependenciasCommand command, CancellationToken cancellationToken)
    {
        var idStr = command.Id.ToString().ToLowerInvariant();
        var liderado = await _dbContext.Liderados.FirstAsync(x => x.Id.ToLower() == idStr, cancellationToken);
        var informacoesPessoais = await _dbContext.InformacoesPessoais.Where(x => x.LideradoId.ToLower() == idStr).ToListAsync(cancellationToken);
        var feedbacks = await _dbContext.Feedbacks.Where(x => x.LideradoId.ToLower() == idStr).ToListAsync(cancellationToken);
        var oneOnOnes = await _dbContext.OneOnOnes.Where(x => x.LideradoId.ToLower() == idStr).ToListAsync(cancellationToken);
        var classificacoesPerfil = await _dbContext.ClassificacoesPerfil.Where(x => x.LideradoId.ToLower() == idStr).ToListAsync(cancellationToken);
        var discs = await _dbContext.Discs.Where(x => x.IdLiderado.ToLower() == idStr).ToListAsync(cancellationToken);
        var culturas = await _dbContext.CulturaAvaliacoes.Where(x => x.LideradoId == command.Id).ToListAsync(cancellationToken);
        var propriedades = await _dbContext.PropriedadesHistoricas.Where(x => x.IdLiderado.ToLower() == idStr).ToListAsync(cancellationToken);
        _dbContext.InformacoesPessoais.RemoveRange(informacoesPessoais);
        _dbContext.Feedbacks.RemoveRange(feedbacks);
        _dbContext.OneOnOnes.RemoveRange(oneOnOnes);
        _dbContext.ClassificacoesPerfil.RemoveRange(classificacoesPerfil);
        _dbContext.Discs.RemoveRange(discs);
        _dbContext.CulturaAvaliacoes.RemoveRange(culturas);
        _dbContext.PropriedadesHistoricas.RemoveRange(propriedades);
        _dbContext.Liderados.Remove(liderado);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new StorageUnit();
    }
}

public sealed class ListarLideradosHandler : IStorageCommandHandler<ListarLideradosQuery, IReadOnlyCollection<LideradoResumoResponse>>
{
    private readonly PeopleManagementDbContext _dbContext;
    public ListarLideradosHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;
    public async Task<IReadOnlyCollection<LideradoResumoResponse>> HandleAsync(ListarLideradosQuery command, CancellationToken cancellationToken)
        => await _dbContext.Liderados.AsNoTracking().OrderBy(x => x.Nome).Select(x => new LideradoResumoResponse(Guid.Parse(x.Id), x.Nome, x.DataCriacaoUtc)).ToArrayAsync(cancellationToken);
}

public sealed class ObterDetalheLideradoHandler : IStorageCommandHandler<ObterDetalheLideradoQuery, ObterLideradoPorIdResponse?>
{
    private readonly PeopleManagementDbContext _dbContext;
    public ObterDetalheLideradoHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;
    public async Task<ObterLideradoPorIdResponse?> HandleAsync(ObterDetalheLideradoQuery command, CancellationToken cancellationToken)
    {
        var idStr = command.Id.ToString().ToLowerInvariant();
        var liderado = await _dbContext.Liderados.AsNoTracking().FirstOrDefaultAsync(x => x.Id.ToLower() == idStr, cancellationToken);
        return liderado is null ? null : new ObterLideradoPorIdResponse(Guid.Parse(liderado.Id), liderado.Nome, liderado.DataCriacaoUtc);
    }
}

public sealed class ObterVisaoIndividualHandler : IStorageCommandHandler<ObterVisaoIndividualQuery, ObterVisaoIndividualResponse?>
{
    private readonly PeopleManagementDbContext _dbContext;
    public ObterVisaoIndividualHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;
    public async Task<ObterVisaoIndividualResponse?> HandleAsync(ObterVisaoIndividualQuery command, CancellationToken cancellationToken)
    {
        var idStr = command.Id.ToString().ToLowerInvariant();
        var liderado = await _dbContext.Liderados.AsNoTracking().FirstOrDefaultAsync(x => x.Id.ToLower() == idStr, cancellationToken);
        if (liderado is null) return null;
        var informacoesEntity = await _dbContext.InformacoesPessoais.AsNoTracking().FirstOrDefaultAsync(x => x.LideradoId.ToLower() == idStr, cancellationToken);
        var informacoes = informacoesEntity is null
            ? new InformacoesPessoais(liderado.Nome, null, null, null, null, null, null, null, null, null, null)
            : new InformacoesPessoais(informacoesEntity.Nome, informacoesEntity.DataNascimento, informacoesEntity.EstadoCivil, informacoesEntity.QuantidadeFilhos, informacoesEntity.DataContratacao, informacoesEntity.Cargo, informacoesEntity.DataInicioCargo, informacoesEntity.AspiracaoCarreira, informacoesEntity.GostosPessoais, informacoesEntity.RedFlags, informacoesEntity.Bio);
        var feedbacksCount = await _dbContext.Feedbacks.AsNoTracking().CountAsync(x => x.LideradoId.ToLower() == idStr, cancellationToken);
        var oneOnOnesCount = await _dbContext.OneOnOnes.AsNoTracking().CountAsync(x => x.LideradoId.ToLower() == idStr, cancellationToken);
        var classificacaoPerfil = await _dbContext.ClassificacoesPerfil.AsNoTracking().FirstOrDefaultAsync(x => x.LideradoId.ToLower() == idStr, cancellationToken);
        var datasAvaliacaoCultura = await _dbContext.CulturaAvaliacoes.AsNoTracking().Where(x => x.LideradoId == command.Id).OrderByDescending(x => x.Data).Select(x => x.Data).ToArrayAsync(cancellationToken);
        var visao = new VisaoIndividualProjection(command.Id, informacoes, classificacaoPerfil?.Perfil, classificacaoPerfil?.NineBox, feedbacksCount, oneOnOnesCount, datasAvaliacaoCultura);
        return new ObterVisaoIndividualResponse(visao);
    }
}

public sealed class ListarFeedbacksHandler : IStorageCommandHandler<ListarFeedbacksQuery, IReadOnlyCollection<FeedbackRegistro>>
{
    private readonly PeopleManagementDbContext _dbContext;
    public ListarFeedbacksHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;
    public async Task<IReadOnlyCollection<FeedbackRegistro>> HandleAsync(ListarFeedbacksQuery command, CancellationToken cancellationToken)
    {
        var idStr = command.Id.ToString().ToLowerInvariant();
        return await _dbContext.Feedbacks.AsNoTracking().Where(x => x.LideradoId.ToLower() == idStr).OrderByDescending(x => x.Data).Select(x => new FeedbackRegistro(Guid.Parse(x.LideradoId), x.Data, x.Conteudo, x.Receptividade, x.Polaridade)).ToArrayAsync(cancellationToken);
    }
}

public sealed class CriarFeedbackHandler : IStorageCommandHandler<CriarFeedbackCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;
    public CriarFeedbackHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;
    public async Task<StorageUnit> HandleAsync(CriarFeedbackCommand command, CancellationToken cancellationToken)
    {
        _dbContext.Feedbacks.Add(new FeedbackEntity { Id = Guid.NewGuid().ToString(), LideradoId = command.Id.ToString(), Data = command.Input.Data, Conteudo = command.Input.Conteudo, Receptividade = command.Input.Receptividade, Polaridade = command.Input.Polaridade });
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new StorageUnit();
    }
}

public sealed class ListarOneOnOnesHandler : IStorageCommandHandler<ListarOneOnOnesQuery, IReadOnlyCollection<OneOnOneRegistro>>
{
    private readonly PeopleManagementDbContext _dbContext;
    public ListarOneOnOnesHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;
    public async Task<IReadOnlyCollection<OneOnOneRegistro>> HandleAsync(ListarOneOnOnesQuery command, CancellationToken cancellationToken)
    {
        var idStr = command.Id.ToString().ToLowerInvariant();
        return await _dbContext.OneOnOnes.AsNoTracking().Where(x => x.LideradoId.ToLower() == idStr).OrderByDescending(x => x.Data).Select(x => new OneOnOneRegistro(Guid.Parse(x.LideradoId), x.Data, x.Resumo, x.TarefasAcordadas, x.ProximosAssuntos)).ToArrayAsync(cancellationToken);
    }
}

public sealed class CriarOneOnOneHandler : IStorageCommandHandler<CriarOneOnOneCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;
    public CriarOneOnOneHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;
    public async Task<StorageUnit> HandleAsync(CriarOneOnOneCommand command, CancellationToken cancellationToken)
    {
        _dbContext.OneOnOnes.Add(new OneOnOneEntity { Id = Guid.NewGuid().ToString(), LideradoId = command.Id.ToString(), Data = command.Input.Data, Resumo = command.Input.Resumo, TarefasAcordadas = command.Input.TarefasAcordadas, ProximosAssuntos = command.Input.ProximosAssuntos });
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new StorageUnit();
    }
}

public sealed class SalvarCulturaHandler : IStorageCommandHandler<SalvarCulturaCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;
    public SalvarCulturaHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;
    public async Task<StorageUnit> HandleAsync(SalvarCulturaCommand command, CancellationToken cancellationToken)
    {
        var existente = await _dbContext.CulturaAvaliacoes.FirstOrDefaultAsync(x => x.LideradoId == command.Id && x.Data == command.Radar.Data, cancellationToken);
        if (existente is null)
        {
            existente = new CulturaAvaliacaoEntity { Id = Guid.NewGuid(), LideradoId = command.Id, Data = command.Radar.Data };
            _dbContext.CulturaAvaliacoes.Add(existente);
        }
        existente.AprenderEMelhorarSempre = command.Radar.AprenderEMelhorarSempre;
        existente.AtitudeDeDono = command.Radar.AtitudeDeDono;
        existente.BuscarMelhoresResultadosParaClientes = command.Radar.BuscarMelhoresResultadosParaClientes;
        existente.EspiritoDeEquipe = command.Radar.EspiritoDeEquipe;
        existente.Excelencia = command.Radar.Excelencia;
        existente.FazerAcontecer = command.Radar.FazerAcontecer;
        existente.InovarParaInspirar = command.Radar.InovarParaInspirar;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new StorageUnit();
    }
}

public sealed class ObterRadarCulturalHandler : IStorageCommandHandler<ObterRadarCulturalQuery, RadarCulturalResponse?>
{
    private readonly PeopleManagementDbContext _dbContext;
    public ObterRadarCulturalHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;
    public async Task<RadarCulturalResponse?> HandleAsync(ObterRadarCulturalQuery command, CancellationToken cancellationToken)
        => await _dbContext.CulturaAvaliacoes.AsNoTracking().Where(x => x.LideradoId == command.Id && x.Data == command.Data).Select(x => new RadarCulturalResponse(x.Data, x.AprenderEMelhorarSempre, x.AtitudeDeDono, x.BuscarMelhoresResultadosParaClientes, x.EspiritoDeEquipe, x.Excelencia, x.FazerAcontecer, x.InovarParaInspirar)).FirstOrDefaultAsync(cancellationToken);
}

public sealed class AtualizarInformacoesPessoaisHandler : IStorageCommandHandler<AtualizarInformacoesPessoaisCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;
    public AtualizarInformacoesPessoaisHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;
    public async Task<StorageUnit> HandleAsync(AtualizarInformacoesPessoaisCommand command, CancellationToken cancellationToken)
    {
        var idStr = command.Id.ToString().ToLowerInvariant();
        var liderado = await _dbContext.Liderados.FirstAsync(x => x.Id.ToLower() == idStr, cancellationToken);
        liderado.Nome = command.Input.Nome;
        var informacoes = await _dbContext.InformacoesPessoais.FirstOrDefaultAsync(x => x.LideradoId.ToLower() == idStr, cancellationToken);
        if (informacoes is null)
        {
            informacoes = new InformacoesPessoaisEntity { LideradoId = idStr };
            _dbContext.InformacoesPessoais.Add(informacoes);
        }
        informacoes.Nome = command.Input.Nome;
        informacoes.DataNascimento = command.Input.DataNascimento;
        informacoes.EstadoCivil = command.Input.EstadoCivil;
        informacoes.QuantidadeFilhos = command.Input.QuantidadeFilhos;
        informacoes.DataContratacao = command.Input.DataContratacao;
        informacoes.Cargo = command.Input.Cargo;
        informacoes.DataInicioCargo = command.Input.DataInicioCargo;
        informacoes.AspiracaoCarreira = command.Input.AspiracaoCarreira;
        informacoes.GostosPessoais = command.Input.GostosPessoais;
        informacoes.RedFlags = command.Input.RedFlags;
        informacoes.Bio = command.Input.Bio;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new StorageUnit();
    }
}

public sealed class AtualizarClassificacaoPerfilHandler : IStorageCommandHandler<AtualizarClassificacaoPerfilCommand, StorageUnit>
{
    private readonly PeopleManagementDbContext _dbContext;
    public AtualizarClassificacaoPerfilHandler(PeopleManagementDbContext dbContext) => _dbContext = dbContext;
    public async Task<StorageUnit> HandleAsync(AtualizarClassificacaoPerfilCommand command, CancellationToken cancellationToken)
    {
        var idStr = command.Id.ToString().ToLowerInvariant();
        var classificacao = await _dbContext.ClassificacoesPerfil.FirstOrDefaultAsync(x => x.LideradoId.ToLower() == idStr, cancellationToken);
        if (classificacao is null)
        {
            classificacao = new ClassificacaoPerfilEntity { LideradoId = idStr };
            _dbContext.ClassificacoesPerfil.Add(classificacao);
        }
        classificacao.Perfil = command.Perfil;
        classificacao.NineBox = command.NineBox;
        classificacao.DataAtualizacaoUtc = command.Data.ToDateTime(TimeOnly.MinValue);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new StorageUnit();
    }
}

