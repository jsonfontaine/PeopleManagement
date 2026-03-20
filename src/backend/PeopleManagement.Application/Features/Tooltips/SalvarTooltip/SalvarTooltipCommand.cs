namespace PeopleManagement.Application.Features.Tooltips.SalvarTooltip;

/// <summary>
/// Comando para criar ou atualizar tooltip de campo.
/// </summary>
public sealed record SalvarTooltipCommand(string ChaveCampo, string Texto);

