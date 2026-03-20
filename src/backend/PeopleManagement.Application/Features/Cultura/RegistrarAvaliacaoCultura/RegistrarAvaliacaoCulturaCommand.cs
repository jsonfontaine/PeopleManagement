namespace PeopleManagement.Application.Features.Cultura.RegistrarAvaliacaoCultura;

/// <summary>
/// Comando para registrar uma avaliacao cultural.
/// </summary>
public sealed record RegistrarAvaliacaoCulturaCommand(
    Guid LideradoId,
    DateOnly Data,
    int AprenderEMelhorarSempre,
    int AtitudeDeDono,
    int BuscarMelhoresResultadosParaClientes,
    int EspiritoDeEquipe,
    int Excelencia,
    int FazerAcontecer,
    int InovarParaInspirar);

