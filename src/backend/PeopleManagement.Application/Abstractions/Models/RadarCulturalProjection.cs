namespace PeopleManagement.Application.Abstractions.Models;

/// <summary>
/// Representa os valores do radar cultural em uma data.
/// </summary>
public sealed record RadarCulturalProjection(
    DateOnly Data,
    int AprenderEMelhorarSempre,
    int AtitudeDeDono,
    int BuscarMelhoresResultadosParaClientes,
    int EspiritoDeEquipe,
    int Excelencia,
    int FazerAcontecer,
    int InovarParaInspirar);

