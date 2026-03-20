using PeopleManagement.Application.Abstractions.Models;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Domain;

namespace PeopleManagement.Application.Features.Cultura.RegistrarAvaliacaoCultura;

/// <summary>
/// Implementa o registro das avaliacoes da secao de cultura.
/// </summary>
public sealed class RegistrarAvaliacaoCulturaHandler : IRegistrarAvaliacaoCulturaHandler
{
    private readonly ICulturaRepository _culturaRepository;
    private readonly IHistoricoAlteracaoRepository _historicoAlteracaoRepository;
    private readonly IUsuarioContexto _usuarioContexto;

    public RegistrarAvaliacaoCulturaHandler(
        ICulturaRepository culturaRepository,
        IHistoricoAlteracaoRepository historicoAlteracaoRepository,
        IUsuarioContexto usuarioContexto)
    {
        _culturaRepository = culturaRepository;
        _historicoAlteracaoRepository = historicoAlteracaoRepository;
        _usuarioContexto = usuarioContexto;
    }

    public async Task<RegistrarAvaliacaoCulturaResponse> HandleAsync(RegistrarAvaliacaoCulturaCommand command, CancellationToken cancellationToken)
    {
        ValidarNota(command.AprenderEMelhorarSempre);
        ValidarNota(command.AtitudeDeDono);
        ValidarNota(command.BuscarMelhoresResultadosParaClientes);
        ValidarNota(command.EspiritoDeEquipe);
        ValidarNota(command.Excelencia);
        ValidarNota(command.FazerAcontecer);
        ValidarNota(command.InovarParaInspirar);

        var radar = new RadarCulturalProjection(
            command.Data,
            command.AprenderEMelhorarSempre,
            command.AtitudeDeDono,
            command.BuscarMelhoresResultadosParaClientes,
            command.EspiritoDeEquipe,
            command.Excelencia,
            command.FazerAcontecer,
            command.InovarParaInspirar);

        await _culturaRepository.AdicionarAvaliacaoAsync(new AvaliacaoCulturaRegistro(command.LideradoId, radar), cancellationToken);

        await _historicoAlteracaoRepository.RegistrarAsync(
            new HistoricoAlteracaoRegistro(
                command.LideradoId,
                "Cultura",
                "Avaliacao",
                null,
                $"Avaliacao registrada em {command.Data:yyyy-MM-dd}",
                DateTime.UtcNow,
                _usuarioContexto.UsuarioAtual),
            cancellationToken);

        return new RegistrarAvaliacaoCulturaResponse(command.LideradoId, command.Data);
    }

    private static void ValidarNota(int valor)
    {
        if (valor is < 0 or > 10)
        {
            throw new DomainException("Os pilares de cultura devem ter notas entre 0 e 10.");
        }
    }
}

