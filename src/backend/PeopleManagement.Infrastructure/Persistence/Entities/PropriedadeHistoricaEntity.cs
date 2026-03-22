namespace PeopleManagement.Infrastructure.Persistence.Entities;

public sealed class PropriedadeHistoricaEntity
{
    public string IdLiderado { get; set; } = default!;
    public string Tipo { get; set; } = default!;
    public DateOnly Data { get; set; }
    public string Valor { get; set; } = default!;
}

