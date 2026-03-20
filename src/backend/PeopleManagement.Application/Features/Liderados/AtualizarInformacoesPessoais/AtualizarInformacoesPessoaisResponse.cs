namespace PeopleManagement.Application.Features.Liderados.AtualizarInformacoesPessoais;

/// <summary>
/// Resposta do caso de uso de atualizacao dos dados pessoais.
/// </summary>
public sealed record AtualizarInformacoesPessoaisResponse(Guid LideradoId, string NomeAtualizado);

