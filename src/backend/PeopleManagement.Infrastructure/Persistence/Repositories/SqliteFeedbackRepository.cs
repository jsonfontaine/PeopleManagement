using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Infrastructure.Persistence.Entities;

namespace PeopleManagement.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repositorio persistente de feedbacks.
/// </summary>
public sealed class SqliteFeedbackRepository : IFeedbackRepository
{
    private readonly PeopleManagementDbContext _dbContext;

    public SqliteFeedbackRepository(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AdicionarAsync(FeedbackRegistro registro, CancellationToken cancellationToken)
    {
        _dbContext.Feedbacks.Add(new FeedbackEntity
        {
            Id = Guid.NewGuid(),
            LideradoId = registro.LideradoId,
            Data = registro.Data,
            Conteudo = registro.Conteudo,
            Receptividade = registro.Receptividade,
            Polaridade = registro.Polaridade
        });

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<FeedbackRegistro>> ListarPorLideradoAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        var registros = await _dbContext.Feedbacks
            .AsNoTracking()
            .Where(x => x.LideradoId == lideradoId)
            .OrderByDescending(x => x.Data)
            .ToListAsync(cancellationToken);

        return registros.Select(x => new FeedbackRegistro(x.LideradoId, x.Data, x.Conteudo, x.Receptividade, x.Polaridade)).ToArray();
    }
}

