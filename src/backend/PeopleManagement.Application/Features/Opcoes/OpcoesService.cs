using PeopleManagement.Application.Common;

namespace PeopleManagement.Application.Features.Opcoes;

public sealed class OpcoesService
{
    private readonly IOpcoesRepository _repository;

    public OpcoesService(IOpcoesRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<OpcoesRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _repository.ListarAsync(lideradoId, cancellationToken);

    public async Task SalvarAsync(Guid lideradoId, string valor, DateOnly data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new RegraNegocioException("O valor de Opcoes e obrigatorio.");

        if (!await _repository.LideradoExisteAsync(lideradoId, cancellationToken))
            throw new RegraNegocioException("Liderado nao encontrado para registro de Opcoes.");

        await _repository.UpsertAsync(new OpcoesRegistro(lideradoId, data, valor.Trim()), cancellationToken);
    }

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _repository.RemoverAsync(lideradoId, data, cancellationToken);
}

