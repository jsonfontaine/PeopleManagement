using PeopleManagement.Application.Abstractions.Persistence;

namespace PeopleManagement.Application.Features.Liderados.ListarLiderados;

/// <summary>
/// Implementa o caso de uso de listagem de liderados.
/// </summary>
public sealed class ListarLideradosHandler : IListarLideradosHandler
{
    private readonly ILideradoRepository _lideradoRepository;

    public ListarLideradosHandler(ILideradoRepository lideradoRepository)
    {
        _lideradoRepository = lideradoRepository;
    }

    public async Task<IReadOnlyCollection<LideradoResumoResponse>> HandleAsync(ListarLideradosQuery query, CancellationToken cancellationToken)
    {
        var liderados = await _lideradoRepository.ListarAsync(cancellationToken);

        return liderados
            .Select(x => new LideradoResumoResponse(x.Id, x.Nome, x.DataCriacaoUtc))
            .OrderBy(x => x.Nome)
            .ToArray();
    }
}

