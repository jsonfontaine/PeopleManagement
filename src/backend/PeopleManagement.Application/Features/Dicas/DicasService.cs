using PeopleManagement.Application.Common;

namespace PeopleManagement.Application.Features.Dicas;

public sealed class DicasService
{
    private readonly IDicasRepository _repository;

    public DicasService(IDicasRepository repository)
    {
        _repository = repository;
    }

    public async Task<DicasRegistro> ObterAsync(CancellationToken cancellationToken)
    {
        var registro = await _repository.ObterAsync(cancellationToken);
        return registro ?? new DicasRegistro(string.Empty);
    }

    public Task SalvarAsync(string? conteudoHtml, CancellationToken cancellationToken)
    {
        if (conteudoHtml is null)
        {
            throw new RegraNegocioException("O conteudo de dicas e obrigatorio.");
        }

        return _repository.SalvarAsync(conteudoHtml, cancellationToken);
    }
}

