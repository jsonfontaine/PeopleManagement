using PeopleManagement.Application.Common.Storage;
namespace PeopleManagement.Application.Features.Dashboard;
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
