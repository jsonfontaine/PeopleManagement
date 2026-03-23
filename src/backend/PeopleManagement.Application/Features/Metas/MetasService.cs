using PeopleManagement.Application.Common;

namespace PeopleManagement.Application.Features.Metas;

public sealed class MetasService
{
    private readonly IMetasRepository _repository;

    public MetasService(IMetasRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<MetasRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _repository.ListarAsync(lideradoId, cancellationToken);

    public async Task SalvarAsync(Guid lideradoId, string valor, DateOnly data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new RegraNegocioException("O valor de Metas e obrigatorio.");

        if (!await _repository.LideradoExisteAsync(lideradoId, cancellationToken))
            throw new RegraNegocioException("Liderado nao encontrado para registro de Metas.");

        await _repository.UpsertAsync(new MetasRegistro(lideradoId, data, valor.Trim()), cancellationToken);
    }

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _repository.RemoverAsync(lideradoId, data, cancellationToken);
}

