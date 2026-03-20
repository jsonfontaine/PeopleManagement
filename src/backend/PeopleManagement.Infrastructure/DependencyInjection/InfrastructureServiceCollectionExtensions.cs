using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Infrastructure.Persistence;
using PeopleManagement.Infrastructure.Persistence.Repositories;

namespace PeopleManagement.Infrastructure.DependencyInjection;

/// <summary>
/// Registro dos adapters da camada de infraestrutura.
/// </summary>
public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, string contentRootPath)
    {
        var connectionString = BuildConnectionString(configuration, contentRootPath);

        services.AddDbContext<PeopleManagementDbContext>(options => options.UseSqlite(connectionString));
        services.AddScoped<ILideradoRepository, SqliteLideradoRepository>();
        services.AddScoped<IDashboardRepository, SqliteDashboardRepository>();
        services.AddScoped<IVisaoIndividualRepository, SqliteVisaoIndividualRepository>();
        services.AddScoped<IFeedbackRepository, SqliteFeedbackRepository>();
        services.AddScoped<IOneOnOneRepository, SqliteOneOnOneRepository>();
        services.AddScoped<IClassificacaoPerfilRepository, SqliteClassificacaoPerfilRepository>();
        services.AddScoped<ICulturaRepository, SqliteCulturaRepository>();
        services.AddScoped<ITooltipRepository, SqliteTooltipRepository>();
        services.AddScoped<IInformacoesPessoaisRepository, SqliteInformacoesPessoaisRepository>();
        services.AddScoped<IHistoricoAlteracaoRepository, SqliteHistoricoAlteracaoRepository>();
        services.AddScoped<IUsuarioContexto, UsuarioContextoPadrao>();

        return services;
    }

    private static string BuildConnectionString(IConfiguration configuration, string contentRootPath)
    {
        var configuredConnectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(configuredConnectionString))
        {
            var defaultPath = Path.Combine(contentRootPath, "App_Data", "peoplemanagement.db");
            Directory.CreateDirectory(Path.GetDirectoryName(defaultPath)!);
            return $"Data Source={defaultPath}";
        }

        const string prefix = "Data Source=";
        if (!configuredConnectionString.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
        {
            return configuredConnectionString;
        }

        var dbPath = configuredConnectionString[prefix.Length..].Trim();
        if (!Path.IsPathRooted(dbPath))
        {
            dbPath = Path.GetFullPath(Path.Combine(contentRootPath, dbPath));
        }

        Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
        return $"Data Source={dbPath}";
    }
}

