using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Samba4Hub.Domain.Entities;

namespace Samba4Hub.Data.EntityTypeConfiguration;

/// <summary>
/// Mapeamento relacional da entidade <see cref="PerfilContratante"/> no PostgreSQL.
/// </summary>
public sealed class PerfilContratanteConfiguration : IEntityTypeConfiguration<PerfilContratante>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<PerfilContratante> builder)
    {
        builder.ToTable("perfil_contratante");

        builder.HasKey(c => c.IdPerfilContratante)
            .HasName("pk_perfil_contratante");

        builder.Property(c => c.IdPerfilContratante)
            .HasColumnName("id_perfil_contratante")
            .UseIdentityByDefaultColumn();

        builder.Property(c => c.UsuarioId)
            .HasColumnName("id_usuario")
            .IsRequired();

        builder.Property(c => c.NomeResponsavel)
            .HasColumnName("nome_responsavel")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Tipo)
            .HasColumnName("tipo")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(c => c.NomeComercial)
            .HasColumnName("nome_comercial")
            .HasMaxLength(120);

        builder.Property(c => c.Cnpj)
            .HasColumnName("cnpj")
            .HasMaxLength(14);

        builder.Property(c => c.Cidade)
            .HasColumnName("cidade")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Bairro)
            .HasColumnName("bairro")
            .HasMaxLength(80);

        builder.Property(c => c.Estado)
            .HasColumnName("estado")
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(c => c.DataCriacao)
            .HasColumnName("data_criacao")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(c => c.DataAtualizacao)
            .HasColumnName("data_atualizacao")
            .HasColumnType("timestamp with time zone");

        builder.HasIndex(c => c.UsuarioId)
            .IsUnique()
            .HasDatabaseName("ux_perfil_contratante_id_usuario");

        // Relacionamento com Vagas
        builder.HasMany(c => c.Vagas)
            .WithOne(v => v.Contratante)
            .HasForeignKey(v => v.ContratanteId)
            .HasConstraintName("fk_vaga_perfil_contratante")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
