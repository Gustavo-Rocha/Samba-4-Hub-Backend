using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Samba4Hub.Domain.Entities;

namespace Samba4Hub.Data.EntityTypeConfiguration;

/// <summary>
/// Mapeamento relacional da entidade <see cref="InteracaoContato"/> no PostgreSQL.
/// </summary>
public sealed class InteracaoContatoConfiguration : IEntityTypeConfiguration<InteracaoContato>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<InteracaoContato> builder)
    {
        builder.ToTable("interacao_contato");

        builder.HasKey(i => i.IdInteracaoContato)
            .HasName("pk_interacao_contato");

        builder.Property(i => i.IdInteracaoContato)
            .HasColumnName("id_interacao_contato")
            .UseIdentityByDefaultColumn();

        builder.Property(i => i.CandidaturaId)
            .HasColumnName("id_candidatura")
            .IsRequired();

        builder.Property(i => i.IniciadoPor)
            .HasColumnName("iniciado_por")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(i => i.DataClique)
            .HasColumnName("data_clique")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(i => i.DataCriacao)
            .HasColumnName("data_criacao")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(i => i.DataAtualizacao)
            .HasColumnName("data_atualizacao")
            .HasColumnType("timestamp with time zone");

        builder.HasIndex(i => i.CandidaturaId)
            .HasDatabaseName("ix_interacao_contato_id_candidatura");

        builder.HasOne(i => i.Candidatura)
            .WithMany(c => c.Interacoes)
            .HasForeignKey(i => i.CandidaturaId)
            .HasConstraintName("fk_interacao_contato_candidatura")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
