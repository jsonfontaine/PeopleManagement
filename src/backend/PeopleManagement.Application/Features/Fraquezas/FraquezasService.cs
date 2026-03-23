using PeopleManagement.Application.Common;

namespace PeopleManagement.Application.Features.Fraquezas;

public sealed class FraquezasService
{
    private readonly IFraquezasRepository _repository;

    public FraquezasService(IFraquezasRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<FraquezasRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _repository.ListarAsync(lideradoId, cancellationToken);

    public async Task SalvarAsync(Guid lideradoId, string valor, DateOnly data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new RegraNegocioException("O valor de Fraquezas e obrigatorio.");

        if (!await _repository.LideradoExisteAsync(lideradoId, cancellationToken))
            throw new RegraNegocioException("Liderado nao encontrado para registro de Fraquezas.");

        await _repository.UpsertAsync(new FraquezasRegistro(lideradoId, data, valor.Trim()), cancellationToken);
    }

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _repository.RemoverAsync(lideradoId, data, cancellationToken);
}

