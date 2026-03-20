namespace PeopleManagement.Application.Features.Cultura.RegistrarAvaliacaoCultura;

/// <summary>
/// Contrato do handler de registro da cultura.
/// </summary>
public interface IRegistrarAvaliacaoCulturaHandler
{
    Task<RegistrarAvaliacaoCulturaResponse> HandleAsync(RegistrarAvaliacaoCulturaCommand command, CancellationToken cancellationToken);
}

