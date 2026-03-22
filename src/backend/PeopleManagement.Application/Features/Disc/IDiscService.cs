using PeopleManagement.Application.Abstractions.Models;

namespace PeopleManagement.Application.Features.Disc;

/// <summary>
/// Servico de aplicacao para casos de uso de DISC.
/// </summary>
public interface IDiscService
{
    Task<IReadOnlyCollection<DiscRegistro>> ListarPorLideradoAsync(Guid lideradoId, CancellationToken cancellationToken);

    Task SalvarAsync(Guid lideradoId, string valor, DateOnly data, CancellationToken cancellationToken);

    Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken);
}

