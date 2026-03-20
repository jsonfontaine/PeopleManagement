using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;

namespace PeopleManagement.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repositorio de leitura da visao individual do liderado.
/// </summary>
public sealed class SqliteVisaoIndividualRepository : IVisaoIndividualRepository
{
    private readonly PeopleManagementDbContext _dbContext;

    public SqliteVisaoIndividualRepository(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<VisaoIndividualProjection?> ObterAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        var liderado = await _dbContext.Liderados.AsNoTracking().FirstOrDefaultAsync(x => x.Id == lideradoId, cancellationToken);
        if (liderado is null)
        {
            return null;
        }

        var informacoes = await _dbContext.InformacoesPessoais.AsNoTracking().FirstOrDefaultAsync(x => x.LideradoId == lideradoId, cancellationToken);
        var classificacao = await _dbContext.ClassificacoesPerfil.AsNoTracking().FirstOrDefaultAsync(x => x.LideradoId == lideradoId, cancellationToken);
        var quantidadeFeedbacks = await _dbContext.Feedbacks.AsNoTracking().CountAsync(x => x.LideradoId == lideradoId, cancellationToken);
        var quantidadeOneOnOnes = await _dbContext.OneOnOnes.AsNoTracking().CountAsync(x => x.LideradoId == lideradoId, cancellationToken);
        var datasCultura = await _dbContext.CulturaAvaliacoes.AsNoTracking()
            .Where(x => x.LideradoId == lideradoId)
            .OrderByDescending(x => x.Data)
            .Select(x => x.Data)
            .ToArrayAsync(cancellationToken);

        var informacoesProjection = informacoes is null
            ? new InformacoesPessoais(liderado.Nome, null, null, null, null, null, null, null, null, null, null)
            : new InformacoesPessoais(
                informacoes.Nome,
                informacoes.DataNascimento,
                informacoes.EstadoCivil,
                informacoes.QuantidadeFilhos,
                informacoes.DataContratacao,
                informacoes.Cargo,
                informacoes.DataInicioCargo,
                informacoes.AspiracaoCarreira,
                informacoes.GostosPessoais,
                informacoes.RedFlags,
                informacoes.Bio);

        return new VisaoIndividualProjection(
            liderado.Id,
            informacoesProjection,
            classificacao?.Perfil,
            classificacao?.NineBox,
            quantidadeFeedbacks,
            quantidadeOneOnOnes,
            datasCultura);
    }
}

