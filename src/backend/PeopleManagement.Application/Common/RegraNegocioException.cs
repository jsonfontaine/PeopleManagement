namespace PeopleManagement.Application.Common;

/// <summary>
/// Excecao para falhas de validacao/regra da camada de aplicacao.
/// </summary>
public sealed class RegraNegocioException : Exception
{
    public RegraNegocioException(string message) : base(message)
    {
    }
}

