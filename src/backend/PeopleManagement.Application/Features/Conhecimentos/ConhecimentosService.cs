using PeopleManagement.Application.Common;
namespace PeopleManagement.Application.Features.Conhecimentos;
public sealed class ConhecimentosService
{
    private readonly IConhecimentosRepository _repository;
    public ConhecimentosService(IConhecimentosRepository repository)
    {
        _repository = repository;
    }
    public Task<IReadOnlyCollection<ConhecimentosRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        return _repository.ListarAsync(lideradoId, cancellationToken);
    }
    public async Task SalvarAsync(Guid lideradoId, string valor, DateOnly data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new RegraNegocioException("O valor de Conhecimentos e obrigatorio.");
        }
        if (!await _repository.LideradoExisteAsync(lideradoId, cancellationToken))
        {
            throw new RegraNegocioException("Liderado nao encontrado para registro de Conhecimentos.");
        }
        await _repository.UpsertAsync(new ConhecimentosRegistro(lideradoId, data, valor.Trim()), cancellationToken);
    }
    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
    {
        return _repository.RemoverAsync(lideradoId, data, cancellationToken);
    }
}
