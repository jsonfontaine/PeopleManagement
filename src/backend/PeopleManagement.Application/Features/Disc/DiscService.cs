using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Domain;

namespace PeopleManagement.Application.Features.Disc;

/// <summary>
/// Servico de aplicacao da feature DISC.
/// </summary>
public sealed class DiscService : IDiscService
{
    private readonly IDiscRepository _discRepository;
    private readonly ILideradoRepository _lideradoRepository;

    public DiscService(IDiscRepository discRepository, ILideradoRepository lideradoRepository)
    {
        _discRepository = discRepository;
        _lideradoRepository = lideradoRepository;
    }

    public Task<IReadOnlyCollection<DiscRegistro>> ListarPorLideradoAsync(
        Guid lideradoId,
        CancellationToken cancellationToken)
    {
        return _discRepository.ListarPorLideradoAsync(lideradoId, cancellationToken);
    }

    public async Task SalvarAsync(Guid lideradoId, string valor, DateOnly data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new DomainException("O valor DISC e obrigatorio.");
        }

        var liderado = await _lideradoRepository.ObterPorIdAsync(lideradoId, cancellationToken);
        if (liderado is null)
        {
            throw new DomainException("Liderado nao encontrado para registro DISC.");
        }

        await _discRepository.SalvarAsync(
            new DiscRegistro(lideradoId, data, valor.Trim()),
            cancellationToken);
    }

    public Task RemoverAsync(Guid lideradoId, DateOnly data, CancellationToken cancellationToken)
    {
        return _discRepository.RemoverAsync(lideradoId, data, cancellationToken);
    }
}
