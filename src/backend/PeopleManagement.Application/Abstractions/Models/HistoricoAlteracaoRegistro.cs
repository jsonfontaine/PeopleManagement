namespace PeopleManagement.Application.Abstractions.Models;

/// <summary>
/// Registro de historico de alteracao em qualquer secao do liderado.
/// </summary>
public sealed record HistoricoAlteracaoRegistro(
    Guid LideradoId,
    string Secao,
    string Campo,
    string? ValorAnterior,
    string ValorNovo,
    DateTime DataAlteracaoUtc,
    string UsuarioResponsavel);

