using Microsoft.EntityFrameworkCore;
using PeopleManagement.Application.Features.Liderados;
using PeopleManagement.Infrastructure.Persistence.Entities;

namespace PeopleManagement.Infrastructure.Persistence;

public sealed class PeopleManagementDbContext : DbContext
{
    public PeopleManagementDbContext(DbContextOptions<PeopleManagementDbContext> options) : base(options)
    {
    }

    public DbSet<LideradoEntity> Liderados => Set<LideradoEntity>();
    public DbSet<InformacoesPessoaisEntity> InformacoesPessoais => Set<InformacoesPessoaisEntity>();
    public DbSet<FeedbackEntity> Feedbacks => Set<FeedbackEntity>();
    public DbSet<OneOnOneEntity> OneOnOnes => Set<OneOnOneEntity>();
    public DbSet<CulturaAvaliacaoEntity> CulturaAvaliacoes => Set<CulturaAvaliacaoEntity>();
    public DbSet<TooltipEntity> Tooltips => Set<TooltipEntity>();
    public DbSet<DiscEntity> Discs => Set<DiscEntity>();
    public DbSet<ConhecimentoEntity> Conhecimentos => Set<ConhecimentoEntity>();
    public DbSet<HabilidadeEntity> Habilidades => Set<HabilidadeEntity>();
    public DbSet<AtitudeEntity> Atitudes => Set<AtitudeEntity>();
    public DbSet<ValorEntity> Valores => Set<ValorEntity>();
    public DbSet<ExpectativaEntity> Expectativas => Set<ExpectativaEntity>();
    public DbSet<MetaEntity> Metas => Set<MetaEntity>();
    public DbSet<SituacaoAtualEntity> SituacaoAtual => Set<SituacaoAtualEntity>();
    public DbSet<OpcaoEntity> Opcoes => Set<OpcaoEntity>();
    public DbSet<ProximoPassoEntity> ProximosPassos => Set<ProximoPassoEntity>();
    public DbSet<FortalezaEntity> Fortalezas => Set<FortalezaEntity>();
    public DbSet<OportunidadeEntity> Oportunidades => Set<OportunidadeEntity>();
    public DbSet<FraquezaEntity> Fraquezas => Set<FraquezaEntity>();
    public DbSet<AmeacaEntity> Ameacas => Set<AmeacaEntity>();
    public DbSet<PersonalidadeEntity> Personalidades => Set<PersonalidadeEntity>();
    public DbSet<NineBoxEntity> NineBoxes => Set<NineBoxEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LideradoEntity>(builder =>
        {
            builder.ToTable("Liderados");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().HasConversion(v => v, v => v);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(200);
            builder.Property(x => x.DataCriacaoUtc).IsRequired();
            builder.HasIndex(x => x.Nome);
        });

        modelBuilder.Entity<InformacoesPessoaisEntity>(builder =>
        {
            builder.ToTable("InformacoesPessoais");
            builder.HasKey(x => x.LideradoId);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(200);
            builder.Property(x => x.EstadoCivil).HasMaxLength(100);
            builder.Property(x => x.Cargo).HasMaxLength(200);
            builder.Property(x => x.AspiracaoCarreira).HasMaxLength(300);
        });

        modelBuilder.Entity<FeedbackEntity>(builder =>
        {
            builder.ToTable("Feedbacks");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Conteudo).IsRequired();
            builder.Property(x => x.Receptividade).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Polaridade).IsRequired().HasMaxLength(50);
            builder.HasIndex(x => new { x.LideradoId, x.Data });
        });

        modelBuilder.Entity<OneOnOneEntity>(builder =>
        {
            builder.ToTable("OneOnOnes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Resumo).IsRequired();
            builder.Property(x => x.TarefasAcordadas).IsRequired();
            builder.Property(x => x.ProximosAssuntos).IsRequired();
            builder.HasIndex(x => new { x.LideradoId, x.Data });
        });


        modelBuilder.Entity<CulturaAvaliacaoEntity>(builder =>
        {
            builder.ToTable("CulturaAvaliacoes");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.LideradoId, x.Data }).IsUnique();
        });

        modelBuilder.Entity<TooltipEntity>(builder =>
        {
            builder.ToTable("Tooltips");
            builder.HasKey(x => x.ChaveCampo);
            builder.Property(x => x.Texto).IsRequired();
        });

        modelBuilder.Entity<DiscEntity>(builder =>
        {
            builder.ToTable("Disc");
            builder.HasKey(x => new { x.IdLiderado, x.Data });
            builder.Property(x => x.IdLiderado).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Data).IsRequired();
        });

        modelBuilder.Entity<ConhecimentoEntity>(builder =>
        {
            builder.ToTable("Conhecimento");
            builder.HasKey(x => new { x.IdLiderado, x.Data });
            builder.Property(x => x.IdLiderado).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Data).IsRequired();
        });

        modelBuilder.Entity<HabilidadeEntity>(builder =>
        {
            builder.ToTable("Habilidade");
            builder.HasKey(x => new { x.IdLiderado, x.Data });
            builder.Property(x => x.IdLiderado).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Data).IsRequired();
        });

        modelBuilder.Entity<AtitudeEntity>(builder =>
        {
            builder.ToTable("Atitude");
            builder.HasKey(x => new { x.IdLiderado, x.Data });
            builder.Property(x => x.IdLiderado).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Data).IsRequired();
        });

        modelBuilder.Entity<ValorEntity>(builder =>
        {
            builder.ToTable("Valor");
            builder.HasKey(x => new { x.IdLiderado, x.Data });
            builder.Property(x => x.IdLiderado).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Data).IsRequired();
        });

        modelBuilder.Entity<ExpectativaEntity>(builder =>
        {
            builder.ToTable("Expectativa");
            builder.HasKey(x => new { x.IdLiderado, x.Data });
            builder.Property(x => x.IdLiderado).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Data).IsRequired();
        });

        modelBuilder.Entity<PersonalidadeEntity>(builder =>
        {
            builder.ToTable("Personalidade");
            builder.HasKey(x => new { x.IdLiderado, x.Data });
            builder.Property(x => x.IdLiderado).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Data).IsRequired();
        });

        modelBuilder.Entity<NineBoxEntity>(builder =>
        {
            builder.ToTable("NineBox");
            builder.HasKey(x => new { x.IdLiderado, x.Data });
            builder.Property(x => x.IdLiderado).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Data).IsRequired();
        });

        modelBuilder.Entity<MetaEntity>(builder =>
        {
            builder.ToTable("Meta");
            builder.HasKey(x => new { x.IdLiderado, x.Data });
            builder.Property(x => x.IdLiderado).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Data).IsRequired();
        });

        modelBuilder.Entity<SituacaoAtualEntity>(builder =>
        {
            builder.ToTable("SituacaoAtual");
            builder.HasKey(x => new { x.IdLiderado, x.Data });
            builder.Property(x => x.IdLiderado).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Data).IsRequired();
        });

        modelBuilder.Entity<OpcaoEntity>(builder =>
        {
            builder.ToTable("Opcao");
            builder.HasKey(x => new { x.IdLiderado, x.Data });
            builder.Property(x => x.IdLiderado).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Data).IsRequired();
        });

        modelBuilder.Entity<ProximoPassoEntity>(builder =>
        {
            builder.ToTable("ProximoPasso");
            builder.HasKey(x => new { x.IdLiderado, x.Data });
            builder.Property(x => x.IdLiderado).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Data).IsRequired();
        });

        modelBuilder.Entity<FortalezaEntity>(builder =>
        {
            builder.ToTable("Fortaleza");
            builder.HasKey(x => new { x.IdLiderado, x.Data });
            builder.Property(x => x.IdLiderado).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Data).IsRequired();
        });

        modelBuilder.Entity<OportunidadeEntity>(builder =>
        {
            builder.ToTable("Oportunidade");
            builder.HasKey(x => new { x.IdLiderado, x.Data });
            builder.Property(x => x.IdLiderado).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Data).IsRequired();
        });

        modelBuilder.Entity<FraquezaEntity>(builder =>
        {
            builder.ToTable("Fraqueza");
            builder.HasKey(x => new { x.IdLiderado, x.Data });
            builder.Property(x => x.IdLiderado).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Data).IsRequired();
        });

        modelBuilder.Entity<AmeacaEntity>(builder =>
        {
            builder.ToTable("Ameaca");
            builder.HasKey(x => new { x.IdLiderado, x.Data });
            builder.Property(x => x.IdLiderado).IsRequired();
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Data).IsRequired();
        });

    }
}

