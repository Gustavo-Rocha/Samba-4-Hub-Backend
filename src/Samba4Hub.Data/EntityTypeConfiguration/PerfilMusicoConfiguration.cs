using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Samba4Hub.Domain.Entities;

namespace Samba4Hub.Data.EntityTypeConfiguration;

/// <summary>
/// Mapeamento relacional da entidade <see cref="PerfilMusico"/> no PostgreSQL.
/// </summary>
public sealed class PerfilMusicoConfiguration : IEntityTypeConfiguration<PerfilMusico>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<PerfilMusico> builder)
    {
        builder.ToTable("perfil_musico");

        builder.HasKey(m => m.IdPerfilMusico)
            .HasName("pk_perfil_musico");

        builder.Property(m => m.IdPerfilMusico)
            .HasColumnName("id_perfil_musico")
            .UseIdentityByDefaultColumn();

        builder.Property(m => m.UsuarioId)
            .HasColumnName("id_usuario")
            .IsRequired();

        builder.Property(m => m.NomeArtistico)
            .HasColumnName("nome_artistico")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(m => m.Bio)
            .HasColumnName("bio")
            .HasMaxLength(1000);

        builder.Property(m => m.Modalidade)
            .HasColumnName("modalidade")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(m => m.CacheMinimo)
            .HasColumnName("cache_minimo")
            .HasPrecision(10, 2);

        builder.Property(m => m.CacheGrupoMinimo)
            .HasColumnName("cache_grupo_minimo")
            .HasPrecision(10, 2);

        builder.Property(m => m.Cidade)
            .HasColumnName("cidade")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(m => m.Estado)
            .HasColumnName("estado")
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(m => m.LinkInstagram)
            .HasColumnName("link_instagram")
            .HasMaxLength(255);

        builder.Property(m => m.LinkYoutube)
            .HasColumnName("link_youtube")
            .HasMaxLength(255);

        builder.Property(m => m.Ativo)
            .HasColumnName("ativo")
            .IsRequired();

        builder.Property(m => m.DataCriacao)
            .HasColumnName("data_criacao")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(m => m.DataAtualizacao)
            .HasColumnName("data_atualizacao")
            .HasColumnType("timestamp with time zone");

        builder.HasIndex(m => m.UsuarioId)
            .IsUnique()
            .HasDatabaseName("ux_perfil_musico_id_usuario");

        // Relacionamentos
        builder.HasMany(m => m.Instrumentos)
            .WithOne(mi => mi.PerfilMusico)
            .HasForeignKey(mi => mi.PerfilMusicoId)
            .HasConstraintName("fk_musico_instrumento_perfil_musico")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(m => m.Candidaturas)
            .WithOne(c => c.Musico)
            .HasForeignKey(c => c.MusicoId)
            .HasConstraintName("fk_candidatura_perfil_musico")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
