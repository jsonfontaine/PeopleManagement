namespace PeopleManagement.Domain.Liderados;

/// <summary>
/// Entidade raiz que representa um liderado do gestor.
/// </summary>
public sealed class Liderado
{
    private Liderado(Guid id, string nome, DateTime dataCriacaoUtc)
    {
        Id = id;
        Nome = nome;
        DataCriacaoUtc = dataCriacaoUtc;
    }

    public Guid Id { get; }

    public string Nome { get; private set; }

    public DateTime DataCriacaoUtc { get; }

    public static Liderado Criar(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new DomainException("O nome do liderado e obrigatorio.");
        }

        return new Liderado(Guid.NewGuid(), nome.Trim(), DateTime.UtcNow);
    }

    public static Liderado Reconstituir(Guid id, string nome, DateTime dataCriacaoUtc)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new DomainException("O nome do liderado e obrigatorio.");
        }

        return new Liderado(id, nome.Trim(), dataCriacaoUtc);
    }

    public void AtualizarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new DomainException("O nome do liderado e obrigatorio.");
        }

        Nome = nome.Trim();
    }
}

