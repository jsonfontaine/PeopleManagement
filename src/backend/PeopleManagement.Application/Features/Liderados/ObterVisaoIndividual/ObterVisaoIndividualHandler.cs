using PeopleManagement.Application.Abstractions.Persistence;

namespace PeopleManagement.Application.Features.Liderados.ObterVisaoIndividual;

/// <summary>
/// Implementa a leitura da tela individual do liderado.
/// </summary>
public sealed class ObterVisaoIndividualHandler : IObterVisaoIndividualHandler
{
    private readonly IVisaoIndividualRepository _visaoIndividualRepository;

    public ObterVisaoIndividualHandler(IVisaoIndividualRepository visaoIndividualRepository)
    {
        _visaoIndividualRepository = visaoIndividualRepository;
    }

    public async Task<ObterVisaoIndividualResponse?> HandleAsync(ObterVisaoIndividualQuery query, CancellationToken cancellationToken)
    {
        var conteudo = await _visaoIndividualRepository.ObterAsync(query.LideradoId, cancellationToken);
        return conteudo is null ? null : new ObterVisaoIndividualResponse(conteudo);
    }
}

