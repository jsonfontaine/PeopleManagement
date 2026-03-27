using PeopleManagement.Application.Common;

namespace PeopleManagement.Application.Features.Tooltips;

public sealed class TooltipsService
{
    private readonly ITooltipsRepository _repository;

    public TooltipsService(ITooltipsRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyCollection<TooltipPropriedadeRegistro>> ListarAsync(CancellationToken cancellationToken)
        => _repository.ListarAsync(cancellationToken);

    public Task<TooltipPropriedadeRegistro?> ObterAsync(string nome, string valueObject, CancellationToken cancellationToken)
        => _repository.ObterAsync(NormalizeKey(nome, "nome"), NormalizeKey(valueObject, "value object"), cancellationToken);

    public Task SalvarAsync(string nome, string valueObject, string tooltip, CancellationToken cancellationToken)
    {
        var nomeNormalizado = NormalizeKey(nome, "nome");
        var valueObjectNormalizado = NormalizeKey(valueObject, "value object");
        if (string.IsNullOrWhiteSpace(tooltip))
        {
            throw new RegraNegocioException("O tooltip e obrigatorio.");
        }

        return _repository.SalvarAsync(
            new TooltipPropriedadeRegistro(nomeNormalizado, valueObjectNormalizado, tooltip.Trim()),
            cancellationToken);
    }

    private static string NormalizeKey(string? input, string label)
    {
        var normalized = (input ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(normalized))
        {
            throw new RegraNegocioException($"O {label} e obrigatorio.");
        }

        return normalized;
    }
}

