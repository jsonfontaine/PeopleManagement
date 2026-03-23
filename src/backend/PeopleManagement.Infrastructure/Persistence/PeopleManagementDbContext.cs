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
    public DbSet<PersonalidadeEntity> Personalidades => Set<PersonalidadeEntity>();
    public DbSet<NineBoxEntity> NineBoxes => Set<NineBoxEntity>();
    public DbSet<PropriedadeHistoricaEntity> PropriedadesHistoricas => Set<PropriedadeHistoricaEntity>();

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

        modelBuilder.Entity<PropriedadeHistoricaEntity>(builder =>
        {
            builder.ToTable("PropriedadesHistoricas");
            builder.HasKey(x => new { x.IdLiderado, x.Tipo, x.Data });
            builder.Property(x => x.IdLiderado).IsRequired();
            builder.Property(x => x.Tipo).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Valor).IsRequired();
            builder.Property(x => x.Data).IsRequired();
            builder.HasIndex(x => new { x.IdLiderado, x.Tipo });
        });
    }
}

