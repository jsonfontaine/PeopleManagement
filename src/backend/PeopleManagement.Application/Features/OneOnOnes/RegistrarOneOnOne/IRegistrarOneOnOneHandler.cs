namespace PeopleManagement.Application.Features.OneOnOnes.RegistrarOneOnOne;

/// <summary>
/// Contrato do handler para registro de 1:1.
/// </summary>
public interface IRegistrarOneOnOneHandler
{
    Task<RegistrarOneOnOneResponse> HandleAsync(RegistrarOneOnOneCommand command, CancellationToken cancellationToken);
}

