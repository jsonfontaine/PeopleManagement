using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Domain;

namespace PeopleManagement.Application.Features.Liderados.RemoverLiderado;

public sealed class RemoverLideradoHandler : IRemoverLideradoHandler
{
    private readonly ILideradoRepository _repository;
    private readonly ILogger<RemoverLideradoHandler> _logger;

    public RemoverLideradoHandler(ILideradoRepository repository, ILogger<RemoverLideradoHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task HandleAsync(RemoverLideradoCommand command, CancellationToken cancellationToken)
    {
        var liderado = await _repository.ObterPorIdAsync(command.Id, cancellationToken);
        if (liderado is null)
        {
            throw new DomainException($"Liderado com id {command.Id} não encontrado.");
        }
        await _repository.RemoverAsync(command.Id, cancellationToken);
        _logger.LogInformation("Liderado removido. Id={Id}", command.Id);
    }
}

