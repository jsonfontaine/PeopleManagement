using Microsoft.Extensions.DependencyInjection;
using PeopleManagement.Application.Features.Ameacas;
using PeopleManagement.Application.Features.Atitudes;
using PeopleManagement.Application.Features.Conhecimentos;
using PeopleManagement.Application.Features.Cultura;
using PeopleManagement.Application.Features.Dashboard;
using PeopleManagement.Application.Features.Disc;
using PeopleManagement.Application.Features.Expectativas;
using PeopleManagement.Application.Features.Feedbacks;
using PeopleManagement.Application.Features.Fortalezas;
using PeopleManagement.Application.Features.Fraquezas;
using PeopleManagement.Application.Features.Habilidades;
using PeopleManagement.Application.Features.Liderados;
using PeopleManagement.Application.Features.Metas;
using PeopleManagement.Application.Features.NineBox;
using PeopleManagement.Application.Features.Opcoes;
using PeopleManagement.Application.Features.Oportunidades;
using PeopleManagement.Application.Features.OneOnOnes;
using PeopleManagement.Application.Features.Personalidade;
using PeopleManagement.Application.Features.ProximosPassos;
using PeopleManagement.Application.Features.SituacaoAtual;
using PeopleManagement.Application.Features.Valores;

namespace PeopleManagement.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<DashboardService>();
        services.AddScoped<IDashboardRepository, DashboardRepository>();

        services.AddScoped<DiscService>();
        services.AddScoped<IDiscRepository, DiscRepository>();

        services.AddScoped<PersonalidadeService>();
        services.AddScoped<IPersonalidadeRepository, PersonalidadeRepository>();

        services.AddScoped<NineBoxService>();
        services.AddScoped<INineBoxRepository, NineBoxRepository>();

        services.AddScoped<LideradosService>();
        services.AddScoped<ILideradosRepository, LideradosRepository>();

        services.AddScoped<ConhecimentosService>();
        services.AddScoped<IConhecimentosRepository, ConhecimentosRepository>();

        services.AddScoped<HabilidadesService>();
        services.AddScoped<IHabilidadesRepository, HabilidadesRepository>();

        services.AddScoped<AtitudesService>();
        services.AddScoped<IAtitudesRepository, AtitudesRepository>();

        services.AddScoped<ValoresService>();
        services.AddScoped<IValoresRepository, ValoresRepository>();

        services.AddScoped<ExpectativasService>();
        services.AddScoped<IExpectativasRepository, ExpectativasRepository>();

        services.AddScoped<MetasService>();
        services.AddScoped<IMetasRepository, MetasRepository>();

        services.AddScoped<SituacaoAtualService>();
        services.AddScoped<ISituacaoAtualRepository, SituacaoAtualRepository>();

        services.AddScoped<OpcoesService>();
        services.AddScoped<IOpcoesRepository, OpcoesRepository>();

        services.AddScoped<ProximosPassosService>();
        services.AddScoped<IProximosPassosRepository, ProximosPassosRepository>();

        services.AddScoped<FortalezasService>();
        services.AddScoped<IFortalezasRepository, FortalezasRepository>();

        services.AddScoped<OportunidadesService>();
        services.AddScoped<IOportunidadesRepository, OportunidadesRepository>();

        services.AddScoped<FraquezasService>();
        services.AddScoped<IFraquezasRepository, FraquezasRepository>();

        services.AddScoped<AmeacasService>();
        services.AddScoped<IAmeacasRepository, AmeacasRepository>();

        services.AddScoped<CulturaService>();
        services.AddScoped<ICulturaRepository, CulturaRepository>();

        services.AddScoped<FeedbacksService>();
        services.AddScoped<IFeedbacksRepository, FeedbacksRepository>();

        services.AddScoped<OneOnOnesService>();
        services.AddScoped<IOneOnOnesRepository, OneOnOnesRepository>();

        return services;
    }
}

