using PeopleManagement.Application.Common;

namespace PeopleManagement.Application.Features.Expectativas;

public sealed class ExpectativasService
{
    private readonly IExpectativasRepository _repository;

    public ExpectativasService(IExpectativasRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<ExpectativasRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        return _repository.ListarAsync(lideradoId, cancellationToken);
    }

    public async Task SalvarAsync(Guid lideradoId, string valor, DateOnly data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new RegraNegocioException("O valor de Expectativas e obrigatorio.");
        }

        if (!await _repository.LideradoExisteAsync(lideradoId, cancellationToken))
        {
            throw new RegraNegocioException("Liderado nao encontrado para registro de Expectativas.");
        }

        await _repository.UpsertAsync(new ExpectativasRegistro(lideradoId, data, valor.Trim()), cancellationToken);
    }

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
    {
        return _repository.RemoverAsync(lideradoId, data, cancellationToken);
    }
}

