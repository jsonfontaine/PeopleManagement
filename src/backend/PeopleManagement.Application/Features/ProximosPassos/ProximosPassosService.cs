using PeopleManagement.Application.Common;

namespace PeopleManagement.Application.Features.ProximosPassos;

public sealed class ProximosPassosService
{
    private readonly IProximosPassosRepository _repository;

    public ProximosPassosService(IProximosPassosRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<ProximosPassosRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _repository.ListarAsync(lideradoId, cancellationToken);

    public async Task SalvarAsync(Guid lideradoId, string valor, DateOnly data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new RegraNegocioException("O valor de Proximos Passos e obrigatorio.");

        if (!await _repository.LideradoExisteAsync(lideradoId, cancellationToken))
            throw new RegraNegocioException("Liderado nao encontrado para registro de Proximos Passos.");

        await _repository.UpsertAsync(new ProximosPassosRegistro(lideradoId, data, valor.Trim()), cancellationToken);
    }

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _repository.RemoverAsync(lideradoId, data, cancellationToken);
}

