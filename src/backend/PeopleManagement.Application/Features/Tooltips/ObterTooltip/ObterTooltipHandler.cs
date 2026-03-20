using PeopleManagement.Application.Abstractions.Persistence;

namespace PeopleManagement.Application.Features.Tooltips.ObterTooltip;

/// <summary>
/// Implementa a leitura de tooltips de apoio a conversa.
/// </summary>
public sealed class ObterTooltipHandler : IObterTooltipHandler
{
    private readonly ITooltipRepository _tooltipRepository;

    public ObterTooltipHandler(ITooltipRepository tooltipRepository)
    {
        _tooltipRepository = tooltipRepository;
    }

    public async Task<ObterTooltipResponse?> HandleAsync(ObterTooltipQuery query, CancellationToken cancellationToken)
    {
        var registro = await _tooltipRepository.ObterPorChaveAsync(query.ChaveCampo, cancellationToken);
        return registro is null ? null : new ObterTooltipResponse(registro.ChaveCampo, registro.Texto);
    }
}

