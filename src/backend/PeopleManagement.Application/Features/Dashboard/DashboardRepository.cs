using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Dashboard;

public interface IDashboardRepository
{
    Task<IReadOnlyCollection<DashboardCardProjection>> ListarCardsAsync(CancellationToken cancellationToken);
}

public sealed class DashboardRepository : IDashboardRepository
{
    private readonly IStorageCommandBus _storageCommandBus;

    public DashboardRepository(IStorageCommandBus storageCommandBus)
    {
        _storageCommandBus = storageCommandBus;
    }

    public async Task<IReadOnlyCollection<DashboardCardProjection>> ListarCardsAsync(CancellationToken cancellationToken)
    {
        return await _storageCommandBus.ExecuteAsync(new ListarDashboardCardsQuery(), cancellationToken);
    }
}

public sealed record ListarDashboardCardsQuery : IStorageCommand<IReadOnlyCollection<DashboardCardProjection>>;


