namespace PeopleManagement.Application.Features.OneOnOnes.ListarOneOnOnes;

/// <summary>
/// Contrato da query de listagem de 1:1.
/// </summary>
public interface IListarOneOnOnesHandler
{
    Task<ListarOneOnOnesResponse> HandleAsync(ListarOneOnOnesQuery query, CancellationToken cancellationToken);
}

