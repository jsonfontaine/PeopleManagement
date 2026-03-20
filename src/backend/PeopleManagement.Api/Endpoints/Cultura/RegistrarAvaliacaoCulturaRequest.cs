namespace PeopleManagement.Api.Endpoints.Cultura;

/// <summary>
/// Payload para registrar avaliacao de cultura.
/// </summary>
public sealed record RegistrarAvaliacaoCulturaRequest(
    DateOnly Data,
    int AprenderEMelhorarSempre,
    int AtitudeDeDono,
    int BuscarMelhoresResultadosParaClientes,
    int EspiritoDeEquipe,
    int Excelencia,
    int FazerAcontecer,
    int InovarParaInspirar);

