using Microsoft.EntityFrameworkCore;
using PeopleManagement.Infrastructure.Persistence.Entities;

namespace PeopleManagement.Infrastructure.Persistence;

/// <summary>
/// Contexto de dados principal da aplicacao.
/// </summary>
public sealed class PeopleManagementDbContext : DbContext
{
    public PeopleManagementDbContext(DbContextOptions<PeopleManagementDbContext> options) : base(options)
    {
    }

    public DbSet<LideradoEntity> Liderados => Set<LideradoEntity>();

    public DbSet<InformacoesPessoaisEntity> InformacoesPessoais => Set<InformacoesPessoaisEntity>();

    public DbSet<FeedbackEntity> Feedbacks => Set<FeedbackEntity>();

    public DbSet<OneOnOneEntity> OneOnOnes => Set<OneOnOneEntity>();

    public DbSet<ClassificacaoPerfilEntity> ClassificacoesPerfil => Set<ClassificacaoPerfilEntity>();

    public DbSet<CulturaAvaliacaoEntity> CulturaAvaliacoes => Set<CulturaAvaliacaoEntity>();

    public DbSet<TooltipEntity> Tooltips => Set<TooltipEntity>();

    public DbSet<HistoricoAlteracaoEntity> HistoricoAlteracoes => Set<HistoricoAlteracaoEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LideradoEntity>(builder =>
        {
            builder.ToTable("Liderados");
            builder.HasKey(x => x.Id);
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

        modelBuilder.Entity<ClassificacaoPerfilEntity>(builder =>
        {
            builder.ToTable("ClassificacoesPerfil");
            builder.HasKey(x => x.LideradoId);
            builder.Property(x => x.Perfil).IsRequired().HasMaxLength(100);
            builder.Property(x => x.NineBox).IsRequired().HasMaxLength(100);
            builder.Property(x => x.DataAtualizacaoUtc).IsRequired();
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

        modelBuilder.Entity<HistoricoAlteracaoEntity>(builder =>
        {
            builder.ToTable("HistoricoAlteracoes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Secao).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Campo).IsRequired().HasMaxLength(150);
            builder.Property(x => x.ValorNovo).IsRequired();
            builder.Property(x => x.UsuarioResponsavel).IsRequired().HasMaxLength(150);
            builder.HasIndex(x => new { x.LideradoId, x.DataAlteracaoUtc });
        });
    }
}

