using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Samba4Hub.Domain.Entities;

namespace Samba4Hub.Data.EntityTypeConfiguration;

/// <summary>
/// Mapeamento relacional da entidade <see cref="Instrumento"/> no PostgreSQL.
/// </summary>
public sealed class InstrumentoConfiguration : IEntityTypeConfiguration<Instrumento>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Instrumento> builder)
    {
        builder.ToTable("instrumento");

        builder.HasKey(i => i.IdInstrumento)
            .HasName("pk_instrumento");

        builder.Property(i => i.IdInstrumento)
            .HasColumnName("id_instrumento")
            .UseIdentityByDefaultColumn()
            .HasIdentityOptions(startValue: 1000);

        builder.Property(i => i.Nome)
            .HasColumnName("nome")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(i => i.Categoria)
            .HasColumnName("categoria")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(i => i.DataCriacao)
            .HasColumnName("data_criacao")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(i => i.DataAtualizacao)
            .HasColumnName("data_atualizacao")
            .HasColumnType("timestamp with time zone");

        builder.HasIndex(i => i.Nome)
            .IsUnique()
            .HasDatabaseName("ux_instrumento_nome");
    }
}
