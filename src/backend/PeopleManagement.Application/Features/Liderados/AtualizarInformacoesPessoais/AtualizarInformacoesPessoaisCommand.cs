using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Features.Liderados.AtualizarInformacoesPessoais;

/// <summary>
/// Comando para atualizar dados pessoais do liderado.
/// </summary>
public sealed record AtualizarInformacoesPessoaisCommand(Guid LideradoId, InformacoesPessoais Informacoes);

