using Microsoft.Extensions.DependencyInjection;
using PeopleManagement.Application.Features.Dashboard.ObterDashboard;
using PeopleManagement.Application.Features.Disc;
using PeopleManagement.Application.Features.Liderados.AtualizarLiderado;
using PeopleManagement.Application.Features.Liderados.CriarLiderado;
using PeopleManagement.Application.Features.Liderados.ListarLiderados;
using PeopleManagement.Application.Features.Liderados.ObterLideradoPorId;
using PeopleManagement.Application.Features.Liderados.RemoverLiderado;

namespace PeopleManagement.Application.DependencyInjection;

/// <summary>
/// Registro dos casos de uso da camada de aplicacao.
/// </summary>
public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IObterDashboardHandler, ObterDashboardHandler>();
        services.AddScoped<IDiscService, DiscService>();
        services.AddScoped<ICriarLideradoHandler, CriarLideradoHandler>();
        services.AddScoped<IListarLideradosHandler, ListarLideradosHandler>();
        services.AddScoped<IObterLideradoPorIdHandler, ObterLideradoPorIdHandler>();
        services.AddScoped<IAtualizarLideradoHandler, AtualizarLideradoHandler>();
        services.AddScoped<IRemoverLideradoHandler, RemoverLideradoHandler>();
        // Adicione aqui outros handlers das features Disc, Dashboard e Liderados se necessário
        return services;
    }
}
