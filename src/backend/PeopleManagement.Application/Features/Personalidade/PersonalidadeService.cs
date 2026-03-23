using PeopleManagement.Application.Common;
namespace PeopleManagement.Application.Features.Personalidade;
public sealed class PersonalidadeService
{
    private readonly IPersonalidadeRepository _repository;
    public PersonalidadeService(IPersonalidadeRepository repository)
    {
        _repository = repository;
    }
    public Task<IReadOnlyCollection<PersonalidadeRegistro>> ListarAsync(Guid lideradoId, CancellationToken cancellationToken)
        => _repository.ListarAsync(lideradoId, cancellationToken);
    public async Task SalvarAsync(Guid lideradoId, string valor, DateOnly data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new RegraNegocioException("O valor de Personalidade e obrigatorio.");
        if (!await _repository.LideradoExisteAsync(lideradoId, cancellationToken))
            throw new RegraNegocioException("Liderado nao encontrado para registro de Personalidade.");
        await _repository.UpsertAsync(new PersonalidadeRegistro(lideradoId, data, valor.Trim()), cancellationToken);
    }
    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
        => _repository.RemoverAsync(lideradoId, data, cancellationToken);
}
