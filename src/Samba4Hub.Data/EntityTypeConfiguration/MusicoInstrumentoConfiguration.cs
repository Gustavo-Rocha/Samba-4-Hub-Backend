using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Samba4Hub.Domain.Entities;

namespace Samba4Hub.Data.EntityTypeConfiguration;

/// <summary>
/// Mapeamento relacional da entidade associativa <see cref="MusicoInstrumento"/> no PostgreSQL.
/// </summary>
public sealed class MusicoInstrumentoConfiguration : IEntityTypeConfiguration<MusicoInstrumento>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<MusicoInstrumento> builder)
    {
        builder.ToTable("musico_instrumento");

        builder.HasKey(mi => new { mi.PerfilMusicoId, mi.InstrumentoId })
            .HasName("pk_musico_instrumento");

        builder.Property(mi => mi.PerfilMusicoId)
            .HasColumnName("id_perfil_musico")
            .IsRequired();

        builder.Property(mi => mi.InstrumentoId)
            .HasColumnName("id_instrumento")
            .IsRequired();

        builder.Property(mi => mi.EhPrincipal)
            .HasColumnName("eh_principal")
            .IsRequired();

        builder.HasOne(mi => mi.PerfilMusico)
            .WithMany(m => m.Instrumentos)
            .HasForeignKey(mi => mi.PerfilMusicoId)
            .HasConstraintName("fk_musico_instrumento_perfil_musico")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(mi => mi.Instrumento)
            .WithMany(i => i.Musicos)
            .HasForeignKey(mi => mi.InstrumentoId)
            .HasConstraintName("fk_musico_instrumento_instrumento")
            .OnDelete(DeleteBehavior.Cascade);

        // Índice único parcial: garante no máximo 1 instrumento principal por músico
        builder.HasIndex(mi => mi.PerfilMusicoId)
            .HasFilter("eh_principal = true")
            .IsUnique()
            .HasDatabaseName("ux_musico_instrumento_principal");
    }
}
