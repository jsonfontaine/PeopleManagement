using PeopleManagement.Application.Common;

namespace PeopleManagement.Application.Features.Oportunidades;

public sealed class OportunidadesService
{
    private readonly IOportunidadesRepository _repository;

    public OportunidadesService(IOportunidadesRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<OportunidadesRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _repository.ListarAsync(lideradoId, cancellationToken);

    public async Task SalvarAsync(Guid lideradoId, string valor, DateOnly data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new RegraNegocioException("O valor de Oportunidades e obrigatorio.");

        if (!await _repository.LideradoExisteAsync(lideradoId, cancellationToken))
            throw new RegraNegocioException("Liderado nao encontrado para registro de Oportunidades.");

        await _repository.UpsertAsync(new OportunidadesRegistro(lideradoId, data, valor.Trim()), cancellationToken);
    }

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _repository.RemoverAsync(lideradoId, data, cancellationToken);
}

