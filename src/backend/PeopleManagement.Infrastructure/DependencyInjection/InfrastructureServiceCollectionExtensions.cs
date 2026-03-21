using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using PeopleManagement.Application.Abstractions.Persistence;
using PeopleManagement.Infrastructure.Persistence;
using PeopleManagement.Infrastructure.Persistence.Repositories;
using Microsoft.Data.Sqlite;
using System.Data;

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
        services.AddScoped<IDbConnection>(_ => new SqliteConnection(connectionString));
        services.AddScoped<ILideradoRepository, SqliteLideradoRepository>();
        services.AddScoped<IDashboardRepository, SqliteDashboardRepository>();
        // Removidos repositórios de features excluídas
        services.AddScoped<IFeedbackRepository, SqliteFeedbackRepository>();
        services.AddScoped<IOneOnOneRepository, SqliteOneOnOneRepository>();
        services.AddScoped<IInformacoesPessoaisRepository, SqliteInformacoesPessoaisRepository>();

        services.AddScoped<IUsuarioContexto, UsuarioContextoPadrao>();
        services.AddScoped<PeopleManagement.Application.Abstractions.Persistence.IConhecimentoRepository, PeopleManagement.Infrastructure.Persistence.Repositories.SqliteConhecimentoRepository>();
        services.AddScoped<IHabilidadeRepository, SqliteHabilidadeRepository>();
        services.AddScoped<IAtitudeRepository, SqliteAtitudeRepository>();
        services.AddScoped<IValorRepository, SqliteValorRepository>();
        // services.AddScoped<IDiscRepository, SqliteDiscRepository>(); // Repositório obsoleto, não registrar mais

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
