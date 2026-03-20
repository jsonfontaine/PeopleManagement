using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Abstractions.Persistence;

/// <summary>
/// Porta de consulta da visao individual de um liderado.
/// </summary>
public interface IVisaoIndividualRepository
{
    Task<VisaoIndividualProjection?> ObterAsync(Guid lideradoId, CancellationToken cancellationToken);
}

