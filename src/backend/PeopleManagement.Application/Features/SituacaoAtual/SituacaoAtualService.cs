using PeopleManagement.Application.Common;

namespace PeopleManagement.Application.Features.SituacaoAtual;

public sealed class SituacaoAtualService
{
    private readonly ISituacaoAtualRepository _repository;

    public SituacaoAtualService(ISituacaoAtualRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<SituacaoAtualRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _repository.ListarAsync(lideradoId, cancellationToken);

    public async Task SalvarAsync(Guid lideradoId, string valor, DateOnly data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new RegraNegocioException("O valor de Situacao Atual e obrigatorio.");

        if (!await _repository.LideradoExisteAsync(lideradoId, cancellationToken))
            throw new RegraNegocioException("Liderado nao encontrado para registro de Situacao Atual.");

        await _repository.UpsertAsync(new SituacaoAtualRegistro(lideradoId, data, valor.Trim()), cancellationToken);
    }

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _repository.RemoverAsync(lideradoId, data, cancellationToken);
}

