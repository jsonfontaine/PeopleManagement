namespace PeopleManagement.Application.Features.Liderados.ListarLiderados;

/// <summary>
/// Contrato do handler de listagem de liderados.
/// </summary>
public interface IListarLideradosHandler
{
    Task<IReadOnlyCollection<LideradoResumoResponse>> HandleAsync(ListarLideradosQuery query, CancellationToken cancellationToken);
}

