using PeopleManagement.Application.Common;

namespace PeopleManagement.Application.Features.Fortalezas;

public sealed class FortalezasService
{
    private readonly IFortalezasRepository _repository;

    public FortalezasService(IFortalezasRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<FortalezasRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _repository.ListarAsync(lideradoId, cancellationToken);

    public async Task SalvarAsync(Guid lideradoId, string valor, DateOnly data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new RegraNegocioException("O valor de Fortalezas e obrigatorio.");

        if (!await _repository.LideradoExisteAsync(lideradoId, cancellationToken))
            throw new RegraNegocioException("Liderado nao encontrado para registro de Fortalezas.");

        await _repository.UpsertAsync(new FortalezasRegistro(lideradoId, data, valor.Trim()), cancellationToken);
    }

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _repository.RemoverAsync(lideradoId, data, cancellationToken);
}

