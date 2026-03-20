using PeopleManagement.Application.Abstractions.Persistence;

namespace PeopleManagement.Application.Features.OneOnOnes.ListarOneOnOnes;

/// <summary>
/// Implementa a consulta de registros 1:1 do liderado.
/// </summary>
public sealed class ListarOneOnOnesHandler : IListarOneOnOnesHandler
{
    private readonly IOneOnOneRepository _oneOnOneRepository;

    public ListarOneOnOnesHandler(IOneOnOneRepository oneOnOneRepository)
    {
        _oneOnOneRepository = oneOnOneRepository;
    }

    public async Task<ListarOneOnOnesResponse> HandleAsync(ListarOneOnOnesQuery query, CancellationToken cancellationToken)
    {
        var registros = await _oneOnOneRepository.ListarPorLideradoAsync(query.LideradoId, cancellationToken);
        return new ListarOneOnOnesResponse(registros.OrderByDescending(x => x.Data).ToArray());
    }
}

