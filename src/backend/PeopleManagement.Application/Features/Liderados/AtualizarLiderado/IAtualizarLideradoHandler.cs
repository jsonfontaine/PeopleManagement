using System.Threading;
using System.Threading.Tasks;

namespace PeopleManagement.Application.Features.Liderados.AtualizarLiderado;

public interface IAtualizarLideradoHandler
{
    Task HandleAsync(AtualizarLideradoCommand command, CancellationToken cancellationToken);
}

