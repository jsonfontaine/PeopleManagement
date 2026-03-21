using System.Threading;
using System.Threading.Tasks;

namespace PeopleManagement.Application.Features.Liderados.RemoverLiderado;

public interface IRemoverLideradoHandler
{
    Task HandleAsync(RemoverLideradoCommand command, CancellationToken cancellationToken);
}

