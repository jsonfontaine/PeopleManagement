using PeopleManagement.Application.Common;
using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Personalidade;

public sealed class PersonalidadeService
{
    private readonly IPersonalidadeRepository _repository;

    public PersonalidadeService(IPersonalidadeRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<PersonalidadeRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _repository.ListarAsync(lideradoId, cancellationToken);

    public async Task SalvarAsync(Guid lideradoId, string valor, DateOnly data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new RegraNegocioException("O valor de Personalidade e obrigatorio.");

        if (!await _repository.LideradoExisteAsync(lideradoId, cancellationToken))
            throw new RegraNegocioException("Liderado nao encontrado para registro de Personalidade.");

        await _repository.UpsertAsync(new PersonalidadeRegistro(lideradoId, data, valor.Trim()), cancellationToken);
    }

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _repository.RemoverAsync(lideradoId, data, cancellationToken);
}

public interface IPersonalidadeRepository
{
    Task<IReadOnlyCollection<PersonalidadeRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken);
    Task UpsertAsync(PersonalidadeRegistro registro, CancellationToken cancellationToken);
    Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken);
}

public sealed class PersonalidadeRepository : IPersonalidadeRepository
{
    private readonly IStorageCommandBus _storageCommandBus;

    public PersonalidadeRepository(IStorageCommandBus storageCommandBus)
    {
        _storageCommandBus = storageCommandBus;
    }

    public Task<IReadOnlyCollection<PersonalidadeRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ListarPersonalidadeQuery(lideradoId), cancellationToken);

    public Task<bool> LideradoExisteAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new VerificarExistenciaLideradoPersonalidadeQuery(lideradoId), cancellationToken);

    public Task UpsertAsync(PersonalidadeRegistro registro, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new SalvarPersonalidadeCommand(registro), cancellationToken);

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new RemoverPersonalidadeCommand(lideradoId, data), cancellationToken);
}

public sealed record PersonalidadeRegistro(Guid LideradoId, DateOnly Data, string Valor);

public sealed record ListarPersonalidadeQuery(Guid LideradoId) : IStorageCommand<IReadOnlyCollection<PersonalidadeRegistro>>;
public sealed record VerificarExistenciaLideradoPersonalidadeQuery(Guid LideradoId) : IStorageCommand<bool>;
public sealed record SalvarPersonalidadeCommand(PersonalidadeRegistro Registro) : IStorageCommand<StorageUnit>;
public sealed record RemoverPersonalidadeCommand(Guid LideradoId, DateOnly Data) : IStorageCommand<StorageUnit>;

