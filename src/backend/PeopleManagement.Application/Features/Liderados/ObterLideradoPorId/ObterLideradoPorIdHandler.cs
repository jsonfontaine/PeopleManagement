using PeopleManagement.Application.Abstractions.Persistence;

namespace PeopleManagement.Application.Features.Liderados.ObterLideradoPorId;

/// <summary>
/// Implementa o caso de uso de consulta de liderado por id.
/// </summary>
public sealed class ObterLideradoPorIdHandler : IObterLideradoPorIdHandler
{
    private readonly ILideradoRepository _lideradoRepository;

    public ObterLideradoPorIdHandler(ILideradoRepository lideradoRepository)
    {
        _lideradoRepository = lideradoRepository;
    }

    public async Task<ObterLideradoPorIdResponse?> HandleAsync(ObterLideradoPorIdQuery query, CancellationToken cancellationToken)
    {
        var liderado = await _lideradoRepository.ObterPorIdAsync(query.Id, cancellationToken);

        if (liderado is null)
        {
            return null;
        }

        return new ObterLideradoPorIdResponse(liderado.Id, liderado.Nome, liderado.DataCriacaoUtc);
    }
}

