using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Features.Historico.ListarHistoricoAlteracoes;

/// <summary>
/// Resposta da consulta de historico de alteracoes.
/// </summary>
public sealed record ListarHistoricoAlteracoesResponse(IReadOnlyCollection<HistoricoAlteracaoRegistro> Registros);

