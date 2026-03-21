using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Infrastructure.Persistence.Entities;

namespace PeopleManagement.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repositorio de classificacao de perfil com EF Core e SQLite.
/// </summary>
public sealed class SqliteClassificacaoPerfilRepository : IClassificacaoPerfilRepository
{
    private readonly PeopleManagementDbContext _dbContext;

    public SqliteClassificacaoPerfilRepository(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ClassificacaoPerfilRegistro?> ObterAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        var idStr = lideradoId.ToString().ToLowerInvariant();
        var entity = await _dbContext.ClassificacoesPerfil
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.LideradoId.ToLower() == idStr, cancellationToken);
        return entity is null
            ? null
            : new ClassificacaoPerfilRegistro(
                Guid.Parse(entity.LideradoId),
                entity.Perfil,
                entity.NineBox,
                entity.Disc,
                entity.DataAtualizacaoUtc);
    }

    public async Task SalvarAsync(ClassificacaoPerfilRegistro registro, CancellationToken cancellationToken)
    {
        var idStr = registro.LideradoId.ToString().ToLowerInvariant();
        var entity = await _dbContext.ClassificacoesPerfil
            .FirstOrDefaultAsync(x => x.LideradoId.ToLower() == idStr, cancellationToken);
        if (entity is null)
        {
            entity = new ClassificacaoPerfilEntity { LideradoId = idStr };
            _dbContext.ClassificacoesPerfil.Add(entity);
        }
        entity.Perfil = registro.Perfil;
        entity.NineBox = registro.NineBox;
        entity.Disc = registro.Disc;
        entity.DataAtualizacaoUtc = registro.DataAtualizacaoUtc;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
