using Microsoft.Extensions.Logging;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Domain;
using PeopleManagement.Domain.Liderados;

namespace PeopleManagement.Application.Features.Liderados.CriarLiderado;

/// <summary>
/// Implementa o caso de uso de criacao de liderado.
/// </summary>
public sealed class CriarLideradoHandler : ICriarLideradoHandler
{
    private readonly ILideradoRepository _lideradoRepository;
    private readonly ILogger<CriarLideradoHandler> _logger;

    public CriarLideradoHandler(ILideradoRepository lideradoRepository, ILogger<CriarLideradoHandler> logger)
    {
        _lideradoRepository = lideradoRepository;
        _logger = logger;
    }

    public async Task<CriarLideradoResponse> HandleAsync(CriarLideradoCommand command, CancellationToken cancellationToken)
    {
        if (await _lideradoRepository.ExistePorNomeAsync(command.Nome, cancellationToken))
        {
            throw new DomainException("Ja existe um liderado com este nome.");
        }

        var liderado = Liderado.Criar(command.Nome);

        await _lideradoRepository.AdicionarAsync(liderado, cancellationToken);

        _logger.LogInformation("Liderado criado com sucesso. Id={LideradoId}, Nome={Nome}", liderado.Id, liderado.Nome);

        return new CriarLideradoResponse(liderado.Id, liderado.Nome, liderado.DataCriacaoUtc);
    }
}

