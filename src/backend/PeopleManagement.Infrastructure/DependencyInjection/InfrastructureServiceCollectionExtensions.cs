using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AppCultura = PeopleManagement.Application.Features.Cultura;
using AppFeedbacks = PeopleManagement.Application.Features.Feedbacks;
using AppOneOnOnes = PeopleManagement.Application.Features.OneOnOnes;
using PeopleManagement.Application.Common.Storage;
using PeopleManagement.Application.Features.Ameacas;
using PeopleManagement.Application.Features.Atitudes;
using PeopleManagement.Application.Features.Conhecimentos;
using PeopleManagement.Application.Features.Dashboard;
using PeopleManagement.Application.Features.Dicas;
using PeopleManagement.Application.Features.Disc;
using PeopleManagement.Application.Features.Expectativas;
using PeopleManagement.Application.Features.Fortalezas;
using PeopleManagement.Application.Features.Fraquezas;
using PeopleManagement.Application.Features.Habilidades;
using PeopleManagement.Application.Features.Liderados;
using PeopleManagement.Application.Features.Metas;
using PeopleManagement.Application.Features.NineBox;
using PeopleManagement.Application.Features.Opcoes;
using PeopleManagement.Application.Features.Oportunidades;
using PeopleManagement.Application.Features.Personalidade;
using PeopleManagement.Application.Features.ProximosPassos;
using PeopleManagement.Application.Features.SituacaoAtual;
using PeopleManagement.Application.Features.Tooltips;
using PeopleManagement.Application.Features.Valores;
using InfraCultura = PeopleManagement.Infrastructure.Storage.Cultura;
using InfraFeedbacks = PeopleManagement.Infrastructure.Storage.Feedbacks;
using InfraOneOnOnes = PeopleManagement.Infrastructure.Storage.OneOnOnes;
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

        services.AddScoped<IStorageCommandHandler<ObterDicasQuery, DicasRegistro?>, ObterDicasHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarDicasCommand, StorageUnit>, SalvarDicasHandler>();

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

        services.AddScoped<IStorageCommandHandler<ListarMetasQuery, IReadOnlyCollection<MetasRegistro>>, ListarMetasHandler>();
        services.AddScoped<IStorageCommandHandler<VerificarExistenciaLideradoMetasQuery, bool>, VerificarExistenciaLideradoMetasHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarMetasCommand, StorageUnit>, SalvarMetasHandler>();
        services.AddScoped<IStorageCommandHandler<RemoverMetasCommand, StorageUnit>, RemoverMetasHandler>();

        services.AddScoped<IStorageCommandHandler<ListarSituacaoAtualQuery, IReadOnlyCollection<SituacaoAtualRegistro>>, ListarSituacaoAtualHandler>();
        services.AddScoped<IStorageCommandHandler<VerificarExistenciaLideradoSituacaoAtualQuery, bool>, VerificarExistenciaLideradoSituacaoAtualHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarSituacaoAtualCommand, StorageUnit>, SalvarSituacaoAtualHandler>();
        services.AddScoped<IStorageCommandHandler<RemoverSituacaoAtualCommand, StorageUnit>, RemoverSituacaoAtualHandler>();

        services.AddScoped<IStorageCommandHandler<ListarOpcoesQuery, IReadOnlyCollection<OpcoesRegistro>>, ListarOpcoesHandler>();
        services.AddScoped<IStorageCommandHandler<VerificarExistenciaLideradoOpcoesQuery, bool>, VerificarExistenciaLideradoOpcoesHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarOpcoesCommand, StorageUnit>, SalvarOpcoesHandler>();
        services.AddScoped<IStorageCommandHandler<RemoverOpcoesCommand, StorageUnit>, RemoverOpcoesHandler>();

        services.AddScoped<IStorageCommandHandler<ListarProximosPassosQuery, IReadOnlyCollection<ProximosPassosRegistro>>, ListarProximosPassosHandler>();
        services.AddScoped<IStorageCommandHandler<VerificarExistenciaLideradoProximosPassosQuery, bool>, VerificarExistenciaLideradoProximosPassosHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarProximosPassosCommand, StorageUnit>, SalvarProximosPassosHandler>();
        services.AddScoped<IStorageCommandHandler<RemoverProximosPassosCommand, StorageUnit>, RemoverProximosPassosHandler>();

        services.AddScoped<IStorageCommandHandler<ListarFortalezasQuery, IReadOnlyCollection<FortalezasRegistro>>, ListarFortalezasHandler>();
        services.AddScoped<IStorageCommandHandler<VerificarExistenciaLideradoFortalezasQuery, bool>, VerificarExistenciaLideradoFortalezasHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarFortalezasCommand, StorageUnit>, SalvarFortalezasHandler>();
        services.AddScoped<IStorageCommandHandler<RemoverFortalezasCommand, StorageUnit>, RemoverFortalezasHandler>();

        services.AddScoped<IStorageCommandHandler<ListarOportunidadesQuery, IReadOnlyCollection<OportunidadesRegistro>>, ListarOportunidadesHandler>();
        services.AddScoped<IStorageCommandHandler<VerificarExistenciaLideradoOportunidadesQuery, bool>, VerificarExistenciaLideradoOportunidadesHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarOportunidadesCommand, StorageUnit>, SalvarOportunidadesHandler>();
        services.AddScoped<IStorageCommandHandler<RemoverOportunidadesCommand, StorageUnit>, RemoverOportunidadesHandler>();

        services.AddScoped<IStorageCommandHandler<ListarFraquezasQuery, IReadOnlyCollection<FraquezasRegistro>>, ListarFraquezasHandler>();
        services.AddScoped<IStorageCommandHandler<VerificarExistenciaLideradoFraquezasQuery, bool>, VerificarExistenciaLideradoFraquezasHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarFraquezasCommand, StorageUnit>, SalvarFraquezasHandler>();
        services.AddScoped<IStorageCommandHandler<RemoverFraquezasCommand, StorageUnit>, RemoverFraquezasHandler>();

        services.AddScoped<IStorageCommandHandler<ListarAmeacasQuery, IReadOnlyCollection<AmeacasRegistro>>, ListarAmeacasHandler>();
        services.AddScoped<IStorageCommandHandler<VerificarExistenciaLideradoAmeacasQuery, bool>, VerificarExistenciaLideradoAmeacasHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarAmeacasCommand, StorageUnit>, SalvarAmeacasHandler>();
        services.AddScoped<IStorageCommandHandler<RemoverAmeacasCommand, StorageUnit>, RemoverAmeacasHandler>();

        services.AddScoped<IStorageCommandHandler<AppCultura.ListarCulturaQuery, IReadOnlyCollection<AppCultura.CulturaRegistro>>, InfraCultura.ListarCulturaFeatureHandler>();
        services.AddScoped<IStorageCommandHandler<AppCultura.ObterCulturaPorDataQuery, AppCultura.CulturaRegistro?>, InfraCultura.ObterCulturaPorDataFeatureHandler>();
        services.AddScoped<IStorageCommandHandler<AppCultura.VerificarExistenciaLideradoCulturaQuery, bool>, InfraCultura.VerificarExistenciaLideradoCulturaFeatureHandler>();
        services.AddScoped<IStorageCommandHandler<AppCultura.SalvarCulturaCommand, StorageUnit>, InfraCultura.SalvarCulturaFeatureHandler>();
        services.AddScoped<IStorageCommandHandler<AppCultura.RemoverCulturaCommand, StorageUnit>, InfraCultura.RemoverCulturaFeatureHandler>();

        services.AddScoped<IStorageCommandHandler<AppFeedbacks.ListarFeedbacksQuery, IReadOnlyCollection<AppFeedbacks.FeedbacksRegistro>>, InfraFeedbacks.ListarFeedbacksFeatureHandler>();
        services.AddScoped<IStorageCommandHandler<AppFeedbacks.VerificarExistenciaLideradoFeedbacksQuery, bool>, InfraFeedbacks.VerificarExistenciaLideradoFeedbacksFeatureHandler>();
        services.AddScoped<IStorageCommandHandler<AppFeedbacks.SalvarFeedbacksCommand, StorageUnit>, InfraFeedbacks.SalvarFeedbacksFeatureHandler>();
        services.AddScoped<IStorageCommandHandler<AppFeedbacks.RemoverFeedbacksCommand, StorageUnit>, InfraFeedbacks.RemoverFeedbacksFeatureHandler>();

        services.AddScoped<IStorageCommandHandler<AppOneOnOnes.ListarOneOnOnesQuery, IReadOnlyCollection<AppOneOnOnes.OneOnOnesRegistro>>, InfraOneOnOnes.ListarOneOnOnesFeatureHandler>();
        services.AddScoped<IStorageCommandHandler<AppOneOnOnes.VerificarExistenciaLideradoOneOnOnesQuery, bool>, InfraOneOnOnes.VerificarExistenciaLideradoOneOnOnesFeatureHandler>();
        services.AddScoped<IStorageCommandHandler<AppOneOnOnes.SalvarOneOnOnesCommand, StorageUnit>, InfraOneOnOnes.SalvarOneOnOnesFeatureHandler>();
        services.AddScoped<IStorageCommandHandler<AppOneOnOnes.RemoverOneOnOnesCommand, StorageUnit>, InfraOneOnOnes.RemoverOneOnOnesFeatureHandler>();

        services.AddScoped<IStorageCommandHandler<ListarTooltipsQuery, IReadOnlyCollection<TooltipPropriedadeRegistro>>, ListarTooltipsHandler>();
        services.AddScoped<IStorageCommandHandler<ObterTooltipPorNomeValueObjectQuery, TooltipPropriedadeRegistro?>, ObterTooltipPorNomeValueObjectHandler>();
        services.AddScoped<IStorageCommandHandler<SalvarTooltipCommand, StorageUnit>, SalvarTooltipHandler>();

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

