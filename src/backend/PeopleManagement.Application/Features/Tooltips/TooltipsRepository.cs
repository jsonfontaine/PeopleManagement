using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Tooltips;

public sealed class TooltipsRepository : ITooltipsRepository
{
    private readonly IStorageCommandBus _storageCommandBus;

    public TooltipsRepository(IStorageCommandBus storageCommandBus)
    {
        _storageCommandBus = storageCommandBus;
    }

    public Task<IReadOnlyCollection<TooltipPropriedadeRegistro>> ListarAsync(CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ListarTooltipsQuery(), cancellationToken);

    public Task<TooltipPropriedadeRegistro?> ObterAsync(string nome, string valueObject, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ObterTooltipPorNomeValueObjectQuery(nome, valueObject), cancellationToken);

    public Task SalvarAsync(TooltipPropriedadeRegistro registro, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new SalvarTooltipCommand(registro), cancellationToken);
}

