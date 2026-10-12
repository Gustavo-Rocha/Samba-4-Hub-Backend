using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Samba4Hub.Domain.Entities;

namespace Samba4Hub.Data.EntityTypeConfiguration;

/// <summary>
/// Mapeamento relacional da entidade <see cref="Vaga"/> no PostgreSQL.
/// </summary>
public sealed class VagaConfiguration : IEntityTypeConfiguration<Vaga>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Vaga> builder)
    {
        builder.ToTable("vaga");

        builder.HasKey(v => v.IdVaga)
            .HasName("pk_vaga");

        builder.Property(v => v.IdVaga)
            .HasColumnName("id_vaga")
            .UseIdentityByDefaultColumn();

        builder.Property(v => v.ContratanteId)
            .HasColumnName("id_perfil_contratante")
            .IsRequired();

        builder.Property(v => v.Titulo)
            .HasColumnName("titulo")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(v => v.Descricao)
            .HasColumnName("descricao")
            .HasMaxLength(4000)
            .IsRequired();

        builder.Property(v => v.EstiloPredominante)
            .HasColumnName("estilo_predominante")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(v => v.FormatoSolicitado)
            .HasColumnName("formato_solicitado")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(v => v.DataEvento)
            .HasColumnName("data_evento")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(v => v.HoraInicio)
            .HasColumnName("hora_inicio")
            .HasColumnType("time without time zone")
            .IsRequired();

        builder.Property(v => v.HoraFim)
            .HasColumnName("hora_fim")
            .HasColumnType("time without time zone")
            .IsRequired();

        builder.Property(v => v.Logradouro)
            .HasColumnName("logradouro")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(v => v.Bairro)
            .HasColumnName("bairro")
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(v => v.Cidade)
            .HasColumnName("cidade")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(v => v.Estado)
            .HasColumnName("estado")
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(v => v.CacheOferecido)
            .HasColumnName("cache_oferecido")
            .HasPrecision(10, 2);

        builder.Property(v => v.CacheACombinar)
            .HasColumnName("cache_a_combinar")
            .IsRequired();

        builder.Property(v => v.PossuiEquipamentoSom)
            .HasColumnName("possui_equipamento_som")
            .IsRequired();

        builder.Property(v => v.Status)
            .HasColumnName("status")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(v => v.DataCriacao)
            .HasColumnName("data_criacao")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(v => v.DataAtualizacao)
            .HasColumnName("data_atualizacao")
            .HasColumnType("timestamp with time zone");

        // Índice composto estratégico para aceleração de buscas no mural de vagas
        builder.HasIndex(v => new { v.Status, v.Cidade, v.DataEvento })
            .HasDatabaseName("ix_vaga_status_cidade_data_evento");

        // Relacionamentos
        builder.HasMany(v => v.InstrumentosDemandados)
            .WithOne(vi => vi.Vaga)
            .HasForeignKey(vi => vi.VagaId)
            .HasConstraintName("fk_vaga_instrumento_vaga")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(v => v.Candidaturas)
            .WithOne(c => c.Vaga)
            .HasForeignKey(c => c.VagaId)
            .HasConstraintName("fk_candidatura_vaga")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
