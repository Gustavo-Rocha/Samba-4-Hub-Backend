using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Samba4Hub.Domain.Entities;

namespace Samba4Hub.Data.EntityTypeConfiguration;

/// <summary>
/// Mapeamento relacional da entidade <see cref="Usuario"/> no PostgreSQL.
/// </summary>
public sealed class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuario");

        builder.HasKey(u => u.IdUsuario)
            .HasName("pk_usuario");

        builder.Property(u => u.IdUsuario)
            .HasColumnName("id_usuario")
            .UseIdentityByDefaultColumn();

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(u => u.SenhaHash)
            .HasColumnName("senha_hash")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(u => u.Telefone)
            .HasColumnName("telefone")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(u => u.CpfCnpj)
            .HasColumnName("cpf_cnpj")
            .HasMaxLength(14)
            .IsRequired();

        builder.Property(u => u.Tipo)
            .HasColumnName("tipo")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(u => u.DataCriacao)
            .HasColumnName("data_criacao")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(u => u.DataAtualizacao)
            .HasColumnName("data_atualizacao")
            .HasColumnType("timestamp with time zone");

        // Índices únicos
        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasDatabaseName("ux_usuario_email");

        builder.HasIndex(u => u.CpfCnpj)
            .IsUnique()
            .HasDatabaseName("ux_usuario_cpf_cnpj");

        // Relacionamentos 1:1 opcionais
        builder.HasOne(u => u.PerfilMusico)
            .WithOne(m => m.Usuario)
            .HasForeignKey<PerfilMusico>(m => m.UsuarioId)
            .HasConstraintName("fk_perfil_musico_usuario")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.PerfilContratante)
            .WithOne(c => c.Usuario)
            .HasForeignKey<PerfilContratante>(c => c.UsuarioId)
            .HasConstraintName("fk_perfil_contratante_usuario")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
