namespace PeopleManagement.Application.Features.Liderados.CriarLiderado;

/// <summary>
/// Contrato do handler de criacao de liderado.
/// </summary>
public interface ICriarLideradoHandler
{
    Task<CriarLideradoResponse> HandleAsync(CriarLideradoCommand command, CancellationToken cancellationToken);
}

