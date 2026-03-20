namespace PeopleManagement.Application.Features.Historico.ListarHistoricoAlteracoes;

/// <summary>
/// Contrato da query de historico.
/// </summary>
public interface IListarHistoricoAlteracoesHandler
{
    Task<ListarHistoricoAlteracoesResponse> HandleAsync(ListarHistoricoAlteracoesQuery query, CancellationToken cancellationToken);
}

