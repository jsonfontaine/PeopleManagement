using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Dashboard;
using PeopleManagement.Application.Features.Disc;
using PeopleManagement.Application.Features.Liderados;
using PeopleManagement.Application.Features.PropHistorica;
using PeopleManagement.Application.Features.Tooltips;
using PeopleManagement.Infrastructure.Persistence;
using PeopleManagement.Infrastructure.Storage;

namespace PeopleManagement.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, string contentRootPath)
    {
        var connectionString = BuildConnectionString(configuration, contentRootPath);

        services.AddDbContext<PeopleManagementDbContext>(options =>
            options.UseSqlite(connectionString, sqlite => sqlite.MigrationsAssembly("PeopleManagement.Infrastructure")));

        services.AddScoped<IStorageCommandBus, SqliteStorageCommandBus>();

        services.AddScoped<IStorageCommandHandler<ListarDashboardCardsQuery, IReadOnlyCollection<DashboardCardProjection>>, ListarDashboardCardsHandler>();

        services.AddScoped<IStorageCommandHandler<ListarDiscQuery, IReadOnlyCollection<DiscRegistro>>, ListarDiscHandler>();
        services.AddScoped<IStorageCommandHandler<VerificarExistenciaLideradoDiscQuery, bool>, VerificarExistenciaLideradoDiscHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarDiscCommand, StorageUnit>, SalvarDiscHandler>();
        services.AddScoped<IStorageCommandHandler<RemoverDiscCommand, StorageUnit>, RemoverDiscHandler>();

        services.AddScoped<IStorageCommandHandler<ExisteLideradoPorNomeQuery, bool>, ExisteLideradoPorNomeHandler>();
        services.AddScoped<IStorageCommandHandler<ExisteLideradoPorIdQuery, bool>, ExisteLideradoPorIdHandler>();
        services.AddScoped<IStorageCommandHandler<AdicionarLideradoCommand, StorageUnit>, AdicionarLideradoHandler>();
        services.AddScoped<IStorageCommandHandler<ObterLideradoPorIdQuery, LideradoSlice?>, ObterLideradoPorIdHandler>();
        services.AddScoped<IStorageCommandHandler<AtualizarNomeLideradoCommand, StorageUnit>, AtualizarNomeLideradoHandler>();
        services.AddScoped<IStorageCommandHandler<RemoverLideradoComDependenciasCommand, StorageUnit>, RemoverLideradoComDependenciasHandler>();
        services.AddScoped<IStorageCommandHandler<ListarLideradosQuery, IReadOnlyCollection<LideradoResumoResponse>>, ListarLideradosHandler>();
        services.AddScoped<IStorageCommandHandler<ObterDetalheLideradoQuery, ObterLideradoPorIdResponse?>, ObterDetalheLideradoHandler>();
        services.AddScoped<IStorageCommandHandler<ObterVisaoIndividualQuery, ObterVisaoIndividualResponse?>, ObterVisaoIndividualHandler>();
        services.AddScoped<IStorageCommandHandler<ListarFeedbacksQuery, IReadOnlyCollection<FeedbackRegistro>>, ListarFeedbacksHandler>();
        services.AddScoped<IStorageCommandHandler<CriarFeedbackCommand, StorageUnit>, CriarFeedbackHandler>();
        services.AddScoped<IStorageCommandHandler<ListarOneOnOnesQuery, IReadOnlyCollection<OneOnOneRegistro>>, ListarOneOnOnesHandler>();
        services.AddScoped<IStorageCommandHandler<CriarOneOnOneCommand, StorageUnit>, CriarOneOnOneHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarCulturaCommand, StorageUnit>, SalvarCulturaHandler>();
        services.AddScoped<IStorageCommandHandler<ObterRadarCulturalQuery, RadarCulturalResponse?>, ObterRadarCulturalHandler>();
        services.AddScoped<IStorageCommandHandler<AtualizarInformacoesPessoaisCommand, StorageUnit>, AtualizarInformacoesPessoaisHandler>();
        services.AddScoped<IStorageCommandHandler<AtualizarClassificacaoPerfilCommand, StorageUnit>, AtualizarClassificacaoPerfilHandler>();

        services.AddScoped<IStorageCommandHandler<ObterTooltipQuery, TooltipResponse?>, ObterTooltipHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarTooltipCommand, StorageUnit>, SalvarTooltipHandler>();

        services.AddScoped<IStorageCommandHandler<ListarPropHistoricaQuery, IReadOnlyCollection<PropHistoricaRegistro>>, ListarPropHistoricaHandler>();
        services.AddScoped<IStorageCommandHandler<VerificarExistenciaLideradoPropHistoricaQuery, bool>, VerificarExistenciaLideradoPropHistoricaHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarPropHistoricaCommand, StorageUnit>, SalvarPropHistoricaHandler>();
        services.AddScoped<IStorageCommandHandler<RemoverPropHistoricaCommand, StorageUnit>, RemoverPropHistoricaHandler>();
        services.AddScoped<IStorageCommandHandler<RemoverTodasPropHistoricaLideradoCommand, StorageUnit>, RemoverTodasPropHistoricaLideradoHandler>();

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

