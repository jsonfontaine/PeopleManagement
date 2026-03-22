using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Tooltips;

public sealed class TooltipsService
{
    private readonly ITooltipsRepository _repository;

    public TooltipsService(ITooltipsRepository repository)
    {
        _repository = repository;
    }

    public Task<TooltipResponse?> ObterAsync(string chaveCampo, CancellationToken cancellationToken)
    {
        return _repository.ObterAsync(chaveCampo, cancellationToken);
    }

    public Task SalvarAsync(string chaveCampo, string texto, CancellationToken cancellationToken)
    {
        return _repository.SalvarAsync(chaveCampo, texto, cancellationToken);
    }
}

public interface ITooltipsRepository
{
    Task<TooltipResponse?> ObterAsync(string chaveCampo, CancellationToken cancellationToken);
    Task SalvarAsync(string chaveCampo, string texto, CancellationToken cancellationToken);
}

public sealed class TooltipsRepository : ITooltipsRepository
{
    private readonly IStorageCommandBus _storageCommandBus;

    public TooltipsRepository(IStorageCommandBus storageCommandBus)
    {
        _storageCommandBus = storageCommandBus;
    }

    public Task<TooltipResponse?> ObterAsync(string chaveCampo, CancellationToken cancellationToken)
    {
        return _storageCommandBus.ExecuteAsync(new ObterTooltipQuery(chaveCampo), cancellationToken);
    }

    public Task SalvarAsync(string chaveCampo, string texto, CancellationToken cancellationToken)
    {
        return _storageCommandBus.ExecuteAsync(new SalvarTooltipCommand(chaveCampo, texto), cancellationToken);
    }
}

public sealed record TooltipResponse(string ChaveCampo, string Texto);

public sealed record ObterTooltipQuery(string ChaveCampo) : IStorageCommand<TooltipResponse?>;

public sealed record SalvarTooltipCommand(string ChaveCampo, string Texto) : IStorageCommand<StorageUnit>;

