using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Samba4Hub.Domain.Entities;

namespace Samba4Hub.Data.EntityTypeConfiguration;

/// <summary>
/// Mapeamento relacional da entidade associativa <see cref="VagaInstrumento"/> no PostgreSQL.
/// </summary>
public sealed class VagaInstrumentoConfiguration : IEntityTypeConfiguration<VagaInstrumento>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<VagaInstrumento> builder)
    {
        builder.ToTable("vaga_instrumento");

        builder.HasKey(vi => new { vi.VagaId, vi.InstrumentoId })
            .HasName("pk_vaga_instrumento");

        builder.Property(vi => vi.VagaId)
            .HasColumnName("id_vaga")
            .IsRequired();

        builder.Property(vi => vi.InstrumentoId)
            .HasColumnName("id_instrumento")
            .IsRequired();

        builder.HasOne(vi => vi.Vaga)
            .WithMany(v => v.InstrumentosDemandados)
            .HasForeignKey(vi => vi.VagaId)
            .HasConstraintName("fk_vaga_instrumento_vaga")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(vi => vi.Instrumento)
            .WithMany(i => i.Vagas)
            .HasForeignKey(vi => vi.InstrumentoId)
            .HasConstraintName("fk_vaga_instrumento_instrumento")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
