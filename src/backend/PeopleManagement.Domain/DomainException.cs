namespace PeopleManagement.Domain;

/// <summary>
/// Excecao de regra de dominio.
/// </summary>
public sealed class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }
}

