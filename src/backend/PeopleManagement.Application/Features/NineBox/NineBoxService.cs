using PeopleManagement.Application.Common;
using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.NineBox;

public sealed class NineBoxService
{
    private readonly INineBoxRepository _repository;

    public NineBoxService(INineBoxRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<NineBoxRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _repository.ListarAsync(lideradoId, cancellationToken);

    public async Task SalvarAsync(Guid lideradoId, string valor, DateOnly data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new RegraNegocioException("O valor de Nine Box e obrigatorio.");

        if (!await _repository.LideradoExisteAsync(lideradoId, cancellationToken))
            throw new RegraNegocioException("Liderado nao encontrado para registro de Nine Box.");

        await _repository.UpsertAsync(new NineBoxRegistro(lideradoId, data, valor.Trim()), cancellationToken);
    }

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _repository.RemoverAsync(lideradoId, data, cancellationToken);
}

public interface INineBoxRepository
{
    Task<IReadOnlyCollection<NineBoxRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task UpsertAsync(NineBoxRegistro registro, CancellationToken cancellationToken);
    Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken);
}

public sealed class NineBoxRepository : INineBoxRepository
{
    private readonly IStorageCommandBus _storageCommandBus;

    public NineBoxRepository(IStorageCommandBus storageCommandBus)
    {
        _storageCommandBus = storageCommandBus;
    }

    public Task<IReadOnlyCollection<NineBoxRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ListarNineBoxQuery(lideradoId), cancellationToken);

    public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new VerificarExistenciaLideradoNineBoxQuery(lideradoId), cancellationToken);

    public Task UpsertAsync(NineBoxRegistro registro, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new SalvarNineBoxCommand(registro), cancellationToken);

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new RemoverNineBoxCommand(lideradoId, data), cancellationToken);
}

public sealed record NineBoxRegistro(Guid LideradoId, DateOnly Data, string Valor);

public sealed record ListarNineBoxQuery(Guid LideradoId) : IStorageCommand<IReadOnlyCollection<NineBoxRegistro>>;
public sealed record VerificarExistenciaLideradoNineBoxQuery(Guid LideradoId) : IStorageCommand<bool>;
public sealed record SalvarNineBoxCommand(NineBoxRegistro Registro) : IStorageCommand<StorageUnit>;
public sealed record RemoverNineBoxCommand(Guid LideradoId, DateOnly Data) : IStorageCommand<StorageUnit>;

