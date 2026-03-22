using Microsoft.Extensions.DependencyInjection;
using PeopleManagement.Application.Features.Dashboard;
using PeopleManagement.Application.Features.Disc;
using PeopleManagement.Application.Features.Liderados;
using PeopleManagement.Application.Features.PropHistorica;
using PeopleManagement.Application.Features.Tooltips;

namespace PeopleManagement.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<DashboardService>();
        services.AddScoped<IDashboardRepository, DashboardRepository>();

        services.AddScoped<DiscService>();
        services.AddScoped<IDiscRepository, DiscRepository>();

        services.AddScoped<LideradosService>();
        services.AddScoped<ILideradosRepository, LideradosRepository>();

        services.AddScoped<TooltipsService>();
        services.AddScoped<ITooltipsRepository, TooltipsRepository>();

        services.AddScoped<PropHistoricaService>();
        services.AddScoped<IPropHistoricaRepository, PropHistoricaRepository>();

        return services;
    }
}

