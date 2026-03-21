using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Infrastructure.Persistence.Entities;

namespace PeopleManagement.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repositorio persistente de informacoes pessoais.
/// </summary>
public sealed class SqliteInformacoesPessoaisRepository : IInformacoesPessoaisRepository
{
    private readonly PeopleManagementDbContext _dbContext;

    public SqliteInformacoesPessoaisRepository(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<InformacoesPessoais?> ObterAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        var idStr = lideradoId.ToString();
        var entity = await _dbContext.InformacoesPessoais.AsNoTracking().FirstOrDefaultAsync(x => x.LideradoId == idStr, cancellationToken);
        return entity is null
            ? null
            : new InformacoesPessoais(
                entity.Nome,
                entity.DataNascimento,
                entity.EstadoCivil,
                entity.QuantidadeFilhos,
                entity.DataContratacao,
                entity.Cargo,
                entity.DataInicioCargo,
                entity.AspiracaoCarreira,
                entity.GostosPessoais,
                entity.RedFlags,
                entity.Bio);
    }

    public async Task SalvarAsync(Guid lideradoId, InformacoesPessoais informacoes, CancellationToken cancellationToken)
    {
        var idStr = lideradoId.ToString();
        var entity = await _dbContext.InformacoesPessoais.FirstOrDefaultAsync(x => x.LideradoId == idStr, cancellationToken);
        if (entity == null)
        {
            entity = new InformacoesPessoaisEntity { LideradoId = idStr };
            _dbContext.InformacoesPessoais.Add(entity);
        }
        entity.Nome = informacoes.Nome;
        entity.DataNascimento = informacoes.DataNascimento;
        entity.EstadoCivil = informacoes.EstadoCivil;
        entity.QuantidadeFilhos = informacoes.QuantidadeFilhos;
        entity.DataContratacao = informacoes.DataContratacao;
        entity.Cargo = informacoes.Cargo;
        entity.DataInicioCargo = informacoes.DataInicioCargo;
        entity.AspiracaoCarreira = informacoes.AspiracaoCarreira;
        entity.GostosPessoais = informacoes.GostosPessoais;
        entity.RedFlags = informacoes.RedFlags;
        entity.Bio = informacoes.Bio;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
