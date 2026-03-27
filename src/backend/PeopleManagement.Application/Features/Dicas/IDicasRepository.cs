namespace PeopleManagement.Application.Features.Dicas;

public interface IDicasRepository
{
    Task<DicasRegistro?> ObterAsync(CancellationToken cancellationToken);
    Task SalvarAsync(string conteudoHtml, CancellationToken cancellationToken);
}

