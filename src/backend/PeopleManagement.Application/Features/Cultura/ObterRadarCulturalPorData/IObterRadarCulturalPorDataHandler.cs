namespace PeopleManagement.Application.Features.Cultura.ObterRadarCulturalPorData;

/// <summary>
/// Contrato de consulta de radar cultural por data.
/// </summary>
public interface IObterRadarCulturalPorDataHandler
{
    Task<ObterRadarCulturalPorDataResponse?> HandleAsync(ObterRadarCulturalPorDataQuery query, CancellationToken cancellationToken);
}

