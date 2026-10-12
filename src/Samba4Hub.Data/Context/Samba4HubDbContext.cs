using Microsoft.EntityFrameworkCore;
using Samba4Hub.Data.Interceptors;
using Samba4Hub.Data.Seed;
using Samba4Hub.Domain.Entities;

namespace Samba4Hub.Data.Context;

/// <summary>
/// Contexto principal do Entity Framework Core para o Samba4Hub.
/// </summary>
public class Samba4HubDbContext : DbContext
{
    private readonly AuditableEntityInterceptor? _auditableInterceptor;

    /// <summary>
    /// Construtor utilizado na injeção de dependência e tempo de execução.
    /// </summary>
    public Samba4HubDbContext(
        DbContextOptions<Samba4HubDbContext> options,
        AuditableEntityInterceptor? auditableInterceptor = null)
        : base(options)
    {
        _auditableInterceptor = auditableInterceptor;
    }

    /// <summary>
    /// Contas de usuário da plataforma.
    /// </summary>
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    /// <summary>
    /// Perfis de músicos instrumentistas.
    /// </summary>
    public DbSet<PerfilMusico> PerfisMusicos => Set<PerfilMusico>();

    /// <summary>
    /// Perfis de contratantes.
    /// </summary>
    public DbSet<PerfilContratante> PerfisContratantes => Set<PerfilContratante>();

    /// <summary>
    /// Catálogo de instrumentos de samba e pagode.
    /// </summary>
    public DbSet<Instrumento> Instrumentos => Set<Instrumento>();

    /// <summary>
    /// Mapeamento associativo de músicos e seus instrumentos.
    /// </summary>
    public DbSet<MusicoInstrumento> MusicosInstrumentos => Set<MusicoInstrumento>();

    /// <summary>
    /// Oportunidades e vagas para apresentações musicais.
    /// </summary>
    public DbSet<Vaga> Vagas => Set<Vaga>();

    /// <summary>
    /// Mapeamento associativo de vagas e instrumentos demandados.
    /// </summary>
    public DbSet<VagaInstrumento> VagasInstrumentos => Set<VagaInstrumento>();

    /// <summary>
    /// Candidaturas e propostas financeiras de músicos.
    /// </summary>
    public DbSet<Candidatura> Candidaturas => Set<Candidatura>();

    /// <summary>
    /// Telemetria de cliques no botão de contato direto via WhatsApp.
    /// </summary>
    public DbSet<InteracaoContato> InteracoesContatos => Set<InteracaoContato>();

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_auditableInterceptor is not null)
        {
            optionsBuilder.AddInterceptors(_auditableInterceptor);
        }

        base.OnConfiguring(optionsBuilder);
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplica todas as classes IEntityTypeConfiguration do assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Samba4HubDbContext).Assembly);

        // Aplica a taxonomia inicial de instrumentos
        InstrumentoSeed.SeedInstrumentos(modelBuilder);
    }
}
