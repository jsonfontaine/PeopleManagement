using PeopleManagement.Application.Common;

namespace PeopleManagement.Application.Features.Ameacas;

public sealed class AmeacasService
{
    private readonly IAmeacasRepository _repository;

    public AmeacasService(IAmeacasRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<AmeacasRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _repository.ListarAsync(lideradoId, cancellationToken);

    public async Task SalvarAsync(Guid lideradoId, string valor, DateOnly data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new RegraNegocioException("O valor de Ameacas e obrigatorio.");

        if (!await _repository.LideradoExisteAsync(lideradoId, cancellationToken))
            throw new RegraNegocioException("Liderado nao encontrado para registro de Ameacas.");

        await _repository.UpsertAsync(new AmeacasRegistro(lideradoId, data, valor.Trim()), cancellationToken);
    }

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _repository.RemoverAsync(lideradoId, data, cancellationToken);
}

