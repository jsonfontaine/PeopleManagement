using PeopleManagement.Application.Common;

namespace PeopleManagement.Application.Features.Valores;

public sealed class ValoresService
{
    private readonly IValoresRepository _repository;

    public ValoresService(IValoresRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<ValoresRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        return _repository.ListarAsync(lideradoId, cancellationToken);
    }

    public async Task SalvarAsync(Guid lideradoId, string valor, DateOnly data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new RegraNegocioException("O valor de Valores e obrigatorio.");
        }

        if (!await _repository.LideradoExisteAsync(lideradoId, cancellationToken))
        {
            throw new RegraNegocioException("Liderado nao encontrado para registro de Valores.");
        }

        await _repository.UpsertAsync(new ValoresRegistro(lideradoId, data, valor.Trim()), cancellationToken);
    }

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
    {
        return _repository.RemoverAsync(lideradoId, data, cancellationToken);
    }
}

