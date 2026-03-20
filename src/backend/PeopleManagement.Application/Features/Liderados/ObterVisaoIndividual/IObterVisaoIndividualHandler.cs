namespace PeopleManagement.Application.Features.Liderados.ObterVisaoIndividual;

/// <summary>
/// Contrato da query de visao individual.
/// </summary>
public interface IObterVisaoIndividualHandler
{
    Task<ObterVisaoIndividualResponse?> HandleAsync(ObterVisaoIndividualQuery query, CancellationToken cancellationToken);
}

