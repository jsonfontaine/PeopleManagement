using PeopleManagement.Application.Abstractions.Persistence;

namespace PeopleManagement.Application.Features.Cultura.ObterRadarCulturalPorData;

/// <summary>
/// Implementa a leitura do radar cultural com filtro por data.
/// </summary>
public sealed class ObterRadarCulturalPorDataHandler : IObterRadarCulturalPorDataHandler
{
    private readonly ICulturaRepository _culturaRepository;

    public ObterRadarCulturalPorDataHandler(ICulturaRepository culturaRepository)
    {
        _culturaRepository = culturaRepository;
    }

    public async Task<ObterRadarCulturalPorDataResponse?> HandleAsync(ObterRadarCulturalPorDataQuery query, CancellationToken cancellationToken)
    {
        var radar = await _culturaRepository.ObterPorDataAsync(query.LideradoId, query.Data, cancellationToken);
        if (radar is null)
        {
            return null;
        }

        var datas = await _culturaRepository.ListarDatasDisponiveisAsync(query.LideradoId, cancellationToken);
        return new ObterRadarCulturalPorDataResponse(radar, datas.OrderByDescending(x => x).ToArray());
    }
}

