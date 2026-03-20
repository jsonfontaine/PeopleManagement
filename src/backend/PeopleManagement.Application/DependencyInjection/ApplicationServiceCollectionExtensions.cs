using Microsoft.Extensions.DependencyInjection;
using PeopleManagement.Application.Features.ClassificacaoPerfil.ObterClassificacaoPerfil;
using PeopleManagement.Application.Features.ClassificacaoPerfil.SalvarClassificacaoPerfil;
using PeopleManagement.Application.Features.Cultura.ObterRadarCulturalPorData;
using PeopleManagement.Application.Features.Cultura.RegistrarAvaliacaoCultura;
using PeopleManagement.Application.Features.Dashboard.ObterDashboard;
using PeopleManagement.Application.Features.Feedbacks.ListarFeedbacks;
using PeopleManagement.Application.Features.Feedbacks.RegistrarFeedback;
using PeopleManagement.Application.Features.Historico.ListarHistoricoAlteracoes;
using PeopleManagement.Application.Features.Liderados.AtualizarInformacoesPessoais;
using PeopleManagement.Application.Features.Liderados.CriarLiderado;
using PeopleManagement.Application.Features.Liderados.ListarLiderados;
using PeopleManagement.Application.Features.Liderados.ObterLideradoPorId;
using PeopleManagement.Application.Features.Liderados.ObterVisaoIndividual;
using PeopleManagement.Application.Features.OneOnOnes.ListarOneOnOnes;
using PeopleManagement.Application.Features.OneOnOnes.RegistrarOneOnOne;
using PeopleManagement.Application.Features.Tooltips.ObterTooltip;
using PeopleManagement.Application.Features.Tooltips.SalvarTooltip;

namespace PeopleManagement.Application.DependencyInjection;

/// <summary>
/// Registro dos casos de uso da camada de aplicacao.
/// </summary>
public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IObterClassificacaoPerfilHandler, ObterClassificacaoPerfilHandler>();
        services.AddScoped<ISalvarClassificacaoPerfilHandler, SalvarClassificacaoPerfilHandler>();
        services.AddScoped<IObterDashboardHandler, ObterDashboardHandler>();
        services.AddScoped<ICriarLideradoHandler, CriarLideradoHandler>();
        services.AddScoped<IListarLideradosHandler, ListarLideradosHandler>();
        services.AddScoped<IObterLideradoPorIdHandler, ObterLideradoPorIdHandler>();
        services.AddScoped<IObterVisaoIndividualHandler, ObterVisaoIndividualHandler>();
        services.AddScoped<IAtualizarInformacoesPessoaisHandler, AtualizarInformacoesPessoaisHandler>();
        services.AddScoped<IRegistrarFeedbackHandler, RegistrarFeedbackHandler>();
        services.AddScoped<IListarFeedbacksHandler, ListarFeedbacksHandler>();
        services.AddScoped<IRegistrarOneOnOneHandler, RegistrarOneOnOneHandler>();
        services.AddScoped<IListarOneOnOnesHandler, ListarOneOnOnesHandler>();
        services.AddScoped<IRegistrarAvaliacaoCulturaHandler, RegistrarAvaliacaoCulturaHandler>();
        services.AddScoped<IObterRadarCulturalPorDataHandler, ObterRadarCulturalPorDataHandler>();
        services.AddScoped<IObterTooltipHandler, ObterTooltipHandler>();
        services.AddScoped<ISalvarTooltipHandler, SalvarTooltipHandler>();
        services.AddScoped<IListarHistoricoAlteracoesHandler, ListarHistoricoAlteracoesHandler>();

        return services;
    }
}

