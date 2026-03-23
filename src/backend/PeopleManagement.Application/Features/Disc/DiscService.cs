using PeopleManagement.Application.Common;
namespace PeopleManagement.Application.Features.Disc;
public sealed class DiscService
{
    private readonly IDiscRepository _repository;
    public DiscService(IDiscRepository repository)
    {
        _repository = repository;
    }
    public Task<IReadOnlyCollection<DiscRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
    {
        return _repository.ListarAsync(lideradoId, cancellationToken);
    }
    public async Task SalvarAsync(Guid lideradoId, string valor, DateOnly data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new RegraNegocioException("O valor DISC e obrigatorio.");
        }
        if (!await _repository.LideradoExisteAsync(lideradoId, cancellationToken))
        {
            throw new RegraNegocioException("Liderado nao encontrado para registro DISC.");
        }
        await _repository.UpsertAsync(new DiscRegistro(lideradoId, data, valor.Trim()), cancellationToken);
    }
    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
    {
        return _repository.RemoverAsync(lideradoId, data, cancellationToken);
    }
}
