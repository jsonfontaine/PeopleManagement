using PeopleManagement.Application.Abstractions.Persistence;

namespace PeopleManagement.Application.Features.Historico.ListarHistoricoAlteracoes;

/// <summary>
/// Implementa a leitura do historico completo de alteracoes.
/// </summary>
public sealed class ListarHistoricoAlteracoesHandler : IListarHistoricoAlteracoesHandler
{
    private readonly IHistoricoAlteracaoRepository _historicoAlteracaoRepository;

    public ListarHistoricoAlteracoesHandler(IHistoricoAlteracaoRepository historicoAlteracaoRepository)
    {
        _historicoAlteracaoRepository = historicoAlteracaoRepository;
    }

    public async Task<ListarHistoricoAlteracoesResponse> HandleAsync(ListarHistoricoAlteracoesQuery query, CancellationToken cancellationToken)
    {
        var registros = await _historicoAlteracaoRepository.ListarPorLideradoAsync(query.LideradoId, cancellationToken);
        return new ListarHistoricoAlteracoesResponse(registros.OrderByDescending(x => x.DataAlteracaoUtc).ToArray());
    }
}

