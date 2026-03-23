using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PeopleManagement.Application.Common.Storage;
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

        services.AddScoped<IStorageCommandHandler<ListarPersonalidadeQuery, IReadOnlyCollection<PersonalidadeRegistro>>, ListarPersonalidadeHandler>();
        services.AddScoped<IStorageCommandHandler<VerificarExistenciaLideradoPersonalidadeQuery, bool>, VerificarExistenciaLideradoPersonalidadeHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarPersonalidadeCommand, StorageUnit>, SalvarPersonalidadeHandler>();
        services.AddScoped<IStorageCommandHandler<RemoverPersonalidadeCommand, StorageUnit>, RemoverPersonalidadeHandler>();

        services.AddScoped<IStorageCommandHandler<ListarNineBoxQuery, IReadOnlyCollection<NineBoxRegistro>>, ListarNineBoxHandler>();
        services.AddScoped<IStorageCommandHandler<VerificarExistenciaLideradoNineBoxQuery, bool>, VerificarExistenciaLideradoNineBoxHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarNineBoxCommand, StorageUnit>, SalvarNineBoxHandler>();
        services.AddScoped<IStorageCommandHandler<RemoverNineBoxCommand, StorageUnit>, RemoverNineBoxHandler>();

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

        services.AddScoped<IStorageCommandHandler<ListarConhecimentosQuery, IReadOnlyCollection<ConhecimentosRegistro>>, ListarConhecimentosHandler>();
        services.AddScoped<IStorageCommandHandler<VerificarExistenciaLideradoConhecimentosQuery, bool>, VerificarExistenciaLideradoConhecimentosHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarConhecimentosCommand, StorageUnit>, SalvarConhecimentosHandler>();
        services.AddScoped<IStorageCommandHandler<RemoverConhecimentosCommand, StorageUnit>, RemoverConhecimentosHandler>();

        services.AddScoped<IStorageCommandHandler<ListarHabilidadesQuery, IReadOnlyCollection<HabilidadesRegistro>>, ListarHabilidadesHandler>();
        services.AddScoped<IStorageCommandHandler<VerificarExistenciaLideradoHabilidadesQuery, bool>, VerificarExistenciaLideradoHabilidadesHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarHabilidadesCommand, StorageUnit>, SalvarHabilidadesHandler>();
        services.AddScoped<IStorageCommandHandler<RemoverHabilidadesCommand, StorageUnit>, RemoverHabilidadesHandler>();

        services.AddScoped<IStorageCommandHandler<ListarAtitudesQuery, IReadOnlyCollection<AtitudesRegistro>>, ListarAtitudesHandler>();
        services.AddScoped<IStorageCommandHandler<VerificarExistenciaLideradoAtitudesQuery, bool>, VerificarExistenciaLideradoAtitudesHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarAtitudesCommand, StorageUnit>, SalvarAtitudesHandler>();
        services.AddScoped<IStorageCommandHandler<RemoverAtitudesCommand, StorageUnit>, RemoverAtitudesHandler>();

        services.AddScoped<IStorageCommandHandler<ListarValoresQuery, IReadOnlyCollection<ValoresRegistro>>, ListarValoresHandler>();
        services.AddScoped<IStorageCommandHandler<VerificarExistenciaLideradoValoresQuery, bool>, VerificarExistenciaLideradoValoresHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarValoresCommand, StorageUnit>, SalvarValoresHandler>();
        services.AddScoped<IStorageCommandHandler<RemoverValoresCommand, StorageUnit>, RemoverValoresHandler>();

        services.AddScoped<IStorageCommandHandler<ListarExpectativasQuery, IReadOnlyCollection<ExpectativasRegistro>>, ListarExpectativasHandler>();
        services.AddScoped<IStorageCommandHandler<VerificarExistenciaLideradoExpectativasQuery, bool>, VerificarExistenciaLideradoExpectativasHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarExpectativasCommand, StorageUnit>, SalvarExpectativasHandler>();
        services.AddScoped<IStorageCommandHandler<RemoverExpectativasCommand, StorageUnit>, RemoverExpectativasHandler>();

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

