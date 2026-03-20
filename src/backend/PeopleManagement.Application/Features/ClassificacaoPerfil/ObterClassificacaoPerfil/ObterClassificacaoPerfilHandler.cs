using PeopleManagement.Application.Abstractions.Persistence;

namespace PeopleManagement.Application.Features.ClassificacaoPerfil.ObterClassificacaoPerfil;

/// <summary>
/// Consulta a classificacao de perfil atual do liderado.
/// </summary>
public sealed class ObterClassificacaoPerfilHandler : IObterClassificacaoPerfilHandler
{
    private readonly IClassificacaoPerfilRepository _classificacaoPerfilRepository;

    public ObterClassificacaoPerfilHandler(IClassificacaoPerfilRepository classificacaoPerfilRepository)
    {
        _classificacaoPerfilRepository = classificacaoPerfilRepository;
    }

    public async Task<ObterClassificacaoPerfilResponse?> HandleAsync(ObterClassificacaoPerfilQuery query, CancellationToken cancellationToken)
    {
        var classificacao = await _classificacaoPerfilRepository.ObterAsync(query.LideradoId, cancellationToken);
        if (classificacao is null)
        {
            return null;
        }

        return new ObterClassificacaoPerfilResponse(
            classificacao.LideradoId,
            classificacao.Perfil,
            classificacao.NineBox,
            classificacao.Disc,
            classificacao.DataAtualizacaoUtc);
    }
}
