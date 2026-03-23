using PeopleManagement.Application.Common;

namespace PeopleManagement.Application.Features.Atitudes;

public sealed class AtitudesService
{
    private readonly IAtitudesRepository _repository;

    public AtitudesService(IAtitudesRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<AtitudesRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        return _repository.ListarAsync(lideradoId, cancellationToken);
    }

    public async Task SalvarAsync(Guid lideradoId, string valor, DateOnly data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new RegraNegocioException("O valor de Atitudes e obrigatorio.");
        }

        if (!await _repository.LideradoExisteAsync(lideradoId, cancellationToken))
        {
            throw new RegraNegocioException("Liderado nao encontrado para registro de Atitudes.");
        }

        await _repository.UpsertAsync(new AtitudesRegistro(lideradoId, data, valor.Trim()), cancellationToken);
    }

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
    {
        return _repository.RemoverAsync(lideradoId, data, cancellationToken);
    }
}

