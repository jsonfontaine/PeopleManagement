namespace PeopleManagement.Infrastructure.Persistence.Entities;

/// <summary>
/// Entidade persistida da classificacao de perfil.
/// </summary>
public sealed class ClassificacaoPerfilEntity
{
    public Guid LideradoId { get; set; }

    public string Perfil { get; set; } = string.Empty;

    public string NineBox { get; set; } = string.Empty;

    public string? Disc { get; set; }

    public DateTime DataAtualizacaoUtc { get; set; }
}

