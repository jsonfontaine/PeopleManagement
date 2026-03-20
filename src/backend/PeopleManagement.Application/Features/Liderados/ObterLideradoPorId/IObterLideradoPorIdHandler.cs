namespace PeopleManagement.Application.Features.Liderados.ObterLideradoPorId;

/// <summary>
/// Contrato do handler de consulta por id.
/// </summary>
public interface IObterLideradoPorIdHandler
{
    Task<ObterLideradoPorIdResponse?> HandleAsync(ObterLideradoPorIdQuery query, CancellationToken cancellationToken);
}

