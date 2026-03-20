using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Domain;

namespace PeopleManagement.Application.Features.Tooltips.SalvarTooltip;

/// <summary>
/// Implementa criacao e edicao de tooltip por campo.
/// </summary>
public sealed class SalvarTooltipHandler : ISalvarTooltipHandler
{
    private readonly ITooltipRepository _tooltipRepository;

    public SalvarTooltipHandler(ITooltipRepository tooltipRepository)
    {
        _tooltipRepository = tooltipRepository;
    }

    public async Task<SalvarTooltipResponse> HandleAsync(SalvarTooltipCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.ChaveCampo))
        {
            throw new DomainException("A chave do campo do tooltip e obrigatoria.");
        }

        await _tooltipRepository.SalvarAsync(new TooltipRegistro(command.ChaveCampo.Trim(), command.Texto.Trim()), cancellationToken);
        return new SalvarTooltipResponse(command.ChaveCampo.Trim(), command.Texto.Trim());
    }
}

