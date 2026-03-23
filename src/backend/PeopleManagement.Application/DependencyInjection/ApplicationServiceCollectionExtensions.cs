using Microsoft.Extensions.DependencyInjection;
using PeopleManagement.Application.Features.Atitudes;
using PeopleManagement.Application.Features.Conhecimentos;
using PeopleManagement.Application.Features.Dashboard;
using PeopleManagement.Application.Features.Disc;
using PeopleManagement.Application.Features.Expectativas;
using PeopleManagement.Application.Features.Habilidades;
using PeopleManagement.Application.Features.Liderados;
using PeopleManagement.Application.Features.NineBox;
using PeopleManagement.Application.Features.Personalidade;
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

        return services;
    }
}

