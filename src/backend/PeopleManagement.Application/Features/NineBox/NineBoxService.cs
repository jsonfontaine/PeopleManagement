using PeopleManagement.Application.Common;
namespace PeopleManagement.Application.Features.NineBox;
public sealed class NineBoxService
{
    private readonly INineBoxRepository _repository;
    public NineBoxService(INineBoxRepository repository)
    {
        _repository = repository;
    }
    public Task<IReadOnlyCollection<NineBoxRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _repository.ListarAsync(lideradoId, cancellationToken);
    public async Task SalvarAsync(Guid lideradoId, string valor, DateOnly data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new RegraNegocioException("O valor de Nine Box e obrigatorio.");
        if (!await _repository.LideradoExisteAsync(lideradoId, cancellationToken))
            throw new RegraNegocioException("Liderado nao encontrado para registro de Nine Box.");
        await _repository.UpsertAsync(new NineBoxRegistro(lideradoId, data, valor.Trim()), cancellationToken);
    }
    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _repository.RemoverAsync(lideradoId, data, cancellationToken);
}
