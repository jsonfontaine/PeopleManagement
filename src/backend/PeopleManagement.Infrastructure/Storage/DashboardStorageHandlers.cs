using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Dashboard;
using PeopleManagement.Infrastructure.Persistence;

namespace PeopleManagement.Infrastructure.Storage;

public sealed class ListarDashboardCardsHandler : IStorageCommandHandler<ListarDashboardCardsQuery, IReadOnlyCollection<DashboardCardProjection>>
{
    private readonly PeopleManagementDbContext _dbContext;

    public ListarDashboardCardsHandler(PeopleManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<DashboardCardProjection>> HandleAsync(ListarDashboardCardsQuery command, CancellationToken cancellationToken)
    {
        var liderados = await _dbContext.Liderados.AsNoTracking().ToListAsync(cancellationToken);

        var perfilByLiderado = await _dbContext.Personalidades
            .AsNoTracking()
            .GroupBy(x => x.IdLiderado.ToLower())
            .Select(group => new
            {
                LideradoId = group.Key,
                Valor = group.OrderByDescending(x => x.Data).Select(x => x.Valor).FirstOrDefault()
            })
            .ToDictionaryAsync(x => x.LideradoId, x => x.Valor, cancellationToken);

        var nineBoxByLiderado = await _dbContext.NineBoxes
            .AsNoTracking()
            .GroupBy(x => x.IdLiderado.ToLower())
            .Select(group => new
            {
                LideradoId = group.Key,
                Valor = group.OrderByDescending(x => x.Data).Select(x => x.Valor).FirstOrDefault()
            })
            .ToDictionaryAsync(x => x.LideradoId, x => x.Valor, cancellationToken);

        var oneOnOneCountByLiderado = await _dbContext.OneOnOnes
            .AsNoTracking()
            .GroupBy(x => x.LideradoId.ToLower())
            .Select(group => new { LideradoId = group.Key, Quantidade = group.Count() })
            .ToDictionaryAsync(x => x.LideradoId, x => x.Quantidade, cancellationToken);

        var feedbackCountByLiderado = await _dbContext.Feedbacks
            .AsNoTracking()
            .GroupBy(x => x.LideradoId.ToLower())
            .Select(group => new { LideradoId = group.Key, Quantidade = group.Count() })
            .ToDictionaryAsync(x => x.LideradoId, x => x.Quantidade, cancellationToken);

        return liderados
            .Select(liderado => new DashboardCardProjection(
                liderado.Id.ToLowerInvariant(),
                liderado.Nome,
                perfilByLiderado.GetValueOrDefault(liderado.Id.ToLowerInvariant()),
                nineBoxByLiderado.GetValueOrDefault(liderado.Id.ToLowerInvariant()),
                feedbackCountByLiderado.GetValueOrDefault(liderado.Id.ToLowerInvariant(), 0),
                oneOnOneCountByLiderado.GetValueOrDefault(liderado.Id.ToLowerInvariant(), 0),
                null,
                null))
            .ToArray();
    }
}

