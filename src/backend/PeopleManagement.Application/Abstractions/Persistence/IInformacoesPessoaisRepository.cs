using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Abstractions.Persistence;

/// <summary>
/// Porta de persistencia para dados pessoais do liderado.
/// </summary>
public interface IInformacoesPessoaisRepository
{
    Task<InformacoesPessoais?> ObterAsync(Guid lideradoId, CancellationToken cancellationToken);

    Task SalvarAsync(Guid lideradoId, InformacoesPessoais informacoes, CancellationToken cancellationToken);
}

