using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Samba4Hub.Domain.Entities;

namespace Samba4Hub.Data.EntityTypeConfiguration;

/// <summary>
/// Mapeamento relacional da entidade <see cref="Candidatura"/> no PostgreSQL.
/// </summary>
public sealed class CandidaturaConfiguration : IEntityTypeConfiguration<Candidatura>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Candidatura> builder)
    {
        builder.ToTable("candidatura");

        builder.HasKey(c => c.IdCandidatura)
            .HasName("pk_candidatura");

        builder.Property(c => c.IdCandidatura)
            .HasColumnName("id_candidatura")
            .UseIdentityByDefaultColumn();

        builder.Property(c => c.VagaId)
            .HasColumnName("id_vaga")
            .IsRequired();

        builder.Property(c => c.MusicoId)
            .HasColumnName("id_perfil_musico")
            .IsRequired();

        builder.Property(c => c.ValorProposta)
            .HasColumnName("valor_proposta")
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(c => c.Mensagem)
            .HasColumnName("mensagem")
            .HasMaxLength(500);

        builder.Property(c => c.Status)
            .HasColumnName("status")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(c => c.DataResposta)
            .HasColumnName("data_resposta")
            .HasColumnType("timestamp with time zone");

        builder.Property(c => c.DataCriacao)
            .HasColumnName("data_criacao")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(c => c.DataAtualizacao)
            .HasColumnName("data_atualizacao")
            .HasColumnType("timestamp with time zone");

        // Chave/índice único composto para evitar candidaturas duplicadas do mesmo músico na mesma vaga
        builder.HasIndex(c => new { c.VagaId, c.MusicoId })
            .IsUnique()
            .HasDatabaseName("ux_candidatura_id_vaga_id_perfil_musico");

        // Relacionamentos
        builder.HasOne(c => c.Vaga)
            .WithMany(v => v.Candidaturas)
            .HasForeignKey(c => c.VagaId)
            .HasConstraintName("fk_candidatura_vaga")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Musico)
            .WithMany(m => m.Candidaturas)
            .HasForeignKey(c => c.MusicoId)
            .HasConstraintName("fk_candidatura_perfil_musico")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Interacoes)
            .WithOne(i => i.Candidatura)
            .HasForeignKey(i => i.CandidaturaId)
            .HasConstraintName("fk_interacao_contato_candidatura")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
