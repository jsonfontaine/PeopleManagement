using PeopleManagement.Application.Common.Storage;

namespace PeopleManagement.Application.Features.Dicas;

public sealed class DicasRepository : IDicasRepository
{
    private readonly IStorageCommandBus _storageCommandBus;

    public DicasRepository(IStorageCommandBus storageCommandBus)
    {
        _storageCommandBus = storageCommandBus;
    }

    public Task<DicasRegistro?> ObterAsync(CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new ObterDicasQuery(), cancellationToken);

    public Task SalvarAsync(string conteudoHtml, CancellationToken cancellationToken)
        => _storageCommandBus.ExecuteAsync(new SalvarDicasCommand(conteudoHtml), cancellationToken);
}

