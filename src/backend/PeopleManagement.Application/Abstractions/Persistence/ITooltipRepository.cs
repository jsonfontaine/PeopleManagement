using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Abstractions.Persistence;

/// <summary>
/// Porta de persistencia para textos de tooltip.
/// </summary>
public interface ITooltipRepository
{
    Task<TooltipRegistro?> ObterPorChaveAsync(string chaveCampo, CancellationToken cancellationToken);

    Task SalvarAsync(TooltipRegistro registro, CancellationToken cancellationToken);
}

