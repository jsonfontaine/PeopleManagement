namespace PeopleManagement.Application.Features.Tooltips.SalvarTooltip;

/// <summary>
/// Contrato do handler de salvamento de tooltip.
/// </summary>
public interface ISalvarTooltipHandler
{
    Task<SalvarTooltipResponse> HandleAsync(SalvarTooltipCommand command, CancellationToken cancellationToken);
}

