using PeopleManagement.Application.Common;
using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.PropHistorica;

public sealed class PropHistoricaService
{
    private readonly IPropHistoricaRepository _repository;

    public PropHistoricaService(IPropHistoricaRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<PropHistoricaRegistro>> ListarAsync(Guid lideradoId, string tipo, CancellationToken cancellationToken)
        => _repository.ListarAsync(lideradoId, tipo, cancellationToken);

    public async Task SalvarAsync(Guid lideradoId, string tipo, string valor, DateOnly data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new RegraNegocioException("O valor e obrigatorio.");

        if (!await _repository.LideradoExisteAsync(lideradoId, cancellationToken))
            throw new RegraNegocioException("Liderado nao encontrado.");

        await _repository.UpsertAsync(new PropHistoricaRegistro(lideradoId, tipo, data, valor.Trim()), cancellationToken);
    }

    public Task RemoverAsync(Guid lideradoId, string tipo, DateOnly data, CancellationToken cancellationToken)
        => _repository.RemoverAsync(lideradoId, tipo, data, cancellationToken);
}

public interface IPropHistoricaRepository
{
    Task<IReadOnlyCollection<PropHistoricaRegistro>> ListarAsync(Guid lideradoId, string tipo, CancellationToken cancellationToken);
    Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task UpsertAsync(PropHistoricaRegistro registro, CancellationToken cancellationToken);
    Task RemoverAsync(Guid lideradoId, string tipo, DateOnly data, CancellationToken cancellationToken);
}

public sealed class PropHistoricaRepository : IPropHistoricaRepository
{
    private readonly IStorageCommandBus _storageCommandBus;

    public PropHistoricaRepository(IStorageCommandBus storageCommandBus)
    {
        _storageCommandBus = storageCommandBus;
    }

    public Task<IReadOnlyCollection<PropHistoricaRegistro>> ListarAsync(Guid lideradoId, string tipo, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ListarPropHistoricaQuery(lideradoId, tipo), cancellationToken);

    public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new VerificarExistenciaLideradoPropHistoricaQuery(lideradoId), cancellationToken);

    public Task UpsertAsync(PropHistoricaRegistro registro, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new SalvarPropHistoricaCommand(registro), cancellationToken);

    public Task RemoverAsync(Guid lideradoId, string tipo, DateOnly data, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new RemoverPropHistoricaCommand(lideradoId, tipo, data), cancellationToken);
}

public sealed record PropHistoricaRegistro(Guid LideradoId, string Tipo, DateOnly Data, string Valor);

public sealed record ListarPropHistoricaQuery(Guid LideradoId, string Tipo) : IStorageCommand<IReadOnlyCollection<PropHistoricaRegistro>>;
public sealed record VerificarExistenciaLideradoPropHistoricaQuery(Guid LideradoId) : IStorageCommand<bool>;
public sealed record SalvarPropHistoricaCommand(PropHistoricaRegistro Registro) : IStorageCommand<StorageUnit>;
public sealed record RemoverPropHistoricaCommand(Guid LideradoId, string Tipo, DateOnly Data) : IStorageCommand<StorageUnit>;
public sealed record RemoverTodasPropHistoricaLideradoCommand(Guid LideradoId) : IStorageCommand<StorageUnit>;

