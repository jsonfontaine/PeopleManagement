namespace PeopleManagement.Application.Features.Tooltips.ObterTooltip;

/// <summary>
/// Contrato da query de tooltip.
/// </summary>
public interface IObterTooltipHandler
{
    Task<ObterTooltipResponse?> HandleAsync(ObterTooltipQuery query, CancellationToken cancellationToken);
}

