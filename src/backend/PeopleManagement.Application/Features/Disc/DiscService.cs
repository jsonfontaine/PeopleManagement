using PeopleManagement.Application.Common;
using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Disc;

public sealed class DiscService
{
    private readonly IDiscRepository _repository;

    public DiscService(IDiscRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<DiscRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        return _repository.ListarAsync(lideradoId, cancellationToken);
    }

    public async Task SalvarAsync(Guid lideradoId, string valor, DateOnly data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new RegraNegocioException("O valor DISC e obrigatorio.");
        }

        if (!await _repository.LideradoExisteAsync(lideradoId, cancellationToken))
        {
            throw new RegraNegocioException("Liderado nao encontrado para registro DISC.");
        }

        await _repository.UpsertAsync(new DiscRegistro(lideradoId, data, valor.Trim()), cancellationToken);
    }

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
    {
        return _repository.RemoverAsync(lideradoId, data, cancellationToken);
    }
}

public interface IDiscRepository
{
    Task<IReadOnlyCollection<DiscRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task UpsertAsync(DiscRegistro registro, CancellationToken cancellationToken);
    Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken);
}

public sealed class DiscRepository : IDiscRepository
{
    private readonly IStorageCommandBus _storageCommandBus;

    public DiscRepository(IStorageCommandBus storageCommandBus)
    {
        _storageCommandBus = storageCommandBus;
    }

    public async Task<IReadOnlyCollection<DiscRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        return await _storageCommandBus.ExecuteAsync(new ListarDiscQuery(lideradoId), cancellationToken);
    }

    public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        return _storageCommandBus.ExecuteAsync(new VerificarExistenciaLideradoDiscQuery(lideradoId), cancellationToken);
    }

    public Task UpsertAsync(DiscRegistro registro, CancellationToken cancellationToken)
    {
        return _storageCommandBus.ExecuteAsync(new SalvarDiscCommand(registro), cancellationToken);
    }

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
    {
        return _storageCommandBus.ExecuteAsync(new RemoverDiscCommand(lideradoId, data), cancellationToken);
    }
}

public sealed record DiscRegistro(Guid LideradoId, DateOnly Data, string Valor);

public sealed record ListarDiscQuery(Guid LideradoId) : IStorageCommand<IReadOnlyCollection<DiscRegistro>>;

public sealed record VerificarExistenciaLideradoDiscQuery(Guid LideradoId) : IStorageCommand<bool>;

public sealed record SalvarDiscCommand(DiscRegistro Registro) : IStorageCommand<StorageUnit>;

public sealed record RemoverDiscCommand(Guid LideradoId, DateOnly Data) : IStorageCommand<StorageUnit>;

