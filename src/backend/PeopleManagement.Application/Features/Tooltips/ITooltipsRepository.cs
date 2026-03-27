namespace PeopleManagement.Application.Features.Tooltips;

public interface ITooltipsRepository
{
    Task<IReadOnlyCollection<TooltipPropriedadeRegistro>> ListarAsync(CancellationToken cancellationToken);
    Task<TooltipPropriedadeRegistro?> ObterAsync(string nome, string valueObject, CancellationToken cancellationToken);
    Task SalvarAsync(TooltipPropriedadeRegistro registro, CancellationToken cancellationToken);
}

