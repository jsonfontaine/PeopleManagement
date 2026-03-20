namespace PeopleManagement.Application.Features.Tooltips.ObterTooltip;

/// <summary>
/// Resposta com o texto de tooltip configurado.
/// </summary>
public sealed record ObterTooltipResponse(string ChaveCampo, string Texto);

