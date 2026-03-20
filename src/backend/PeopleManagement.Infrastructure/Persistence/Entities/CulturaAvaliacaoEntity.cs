namespace PeopleManagement.Infrastructure.Persistence.Entities;

/// <summary>
/// Entidade persistida de avaliacao cultural.
/// </summary>
public sealed class CulturaAvaliacaoEntity
{
    public Guid Id { get; set; }

    public Guid LideradoId { get; set; }

    public DateOnly Data { get; set; }

    public int AprenderEMelhorarSempre { get; set; }

    public int AtitudeDeDono { get; set; }

    public int BuscarMelhoresResultadosParaClientes { get; set; }

    public int EspiritoDeEquipe { get; set; }

    public int Excelencia { get; set; }

    public int FazerAcontecer { get; set; }

    public int InovarParaInspirar { get; set; }
}

