using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Domain;
using PeopleManagement.Domain.Liderados;

namespace PeopleManagement.Application.Features.Liderados.AtualizarLiderado;

public sealed class AtualizarLideradoHandler : IAtualizarLideradoHandler
{
    private readonly ILideradoRepository _repository;
    private readonly ILogger<AtualizarLideradoHandler> _logger;

    public AtualizarLideradoHandler(ILideradoRepository repository, ILogger<AtualizarLideradoHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task HandleAsync(AtualizarLideradoCommand command, CancellationToken cancellationToken)
    {
        var liderado = await _repository.ObterPorIdAsync(command.Id, cancellationToken);
        if (liderado is null)
        {
            throw new DomainException($"Liderado com id {command.Id} não encontrado.");
        }
        liderado.AtualizarNome(command.Nome);
        await _repository.AtualizarAsync(liderado, cancellationToken);
        _logger.LogInformation("Liderado atualizado. Id={Id}", command.Id);
    }
}
