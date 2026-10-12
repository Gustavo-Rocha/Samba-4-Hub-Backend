using Microsoft.EntityFrameworkCore;
using Samba4Hub.Domain.Entities;
using Samba4Hub.Domain.Enums;

namespace Samba4Hub.Data.Seed;

/// <summary>
/// Provedor da taxonomia inicial de instrumentos tradicionais de samba e pagode para seed do EF Core.
/// </summary>
public static class InstrumentoSeed
{
    private static readonly DateTimeOffset DataPadraoSeed = new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

    /// <summary>
    /// Aplica os registros de seed da taxonomia de instrumentos via <see cref="ModelBuilder.Entity{TEntity}()"/>.
    /// </summary>
    public static void SeedInstrumentos(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Instrumento>().HasData(
            // Harmonia e Cordas
            CriarInstrumento(1, "Cavaquinho", CategoriaInstrumento.HarmoniaCordas),
            CriarInstrumento(2, "Banjo", CategoriaInstrumento.HarmoniaCordas),
            CriarInstrumento(3, "Violão 6 Cordas", CategoriaInstrumento.HarmoniaCordas),
            CriarInstrumento(4, "Violão 7 Cordas", CategoriaInstrumento.HarmoniaCordas),
            CriarInstrumento(5, "Bandolim", CategoriaInstrumento.HarmoniaCordas),

            // Percussão
            CriarInstrumento(6, "Pandeiro", CategoriaInstrumento.Percussao),
            CriarInstrumento(7, "Surdo", CategoriaInstrumento.Percussao),
            CriarInstrumento(8, "Tantã", CategoriaInstrumento.Percussao),
            CriarInstrumento(9, "Repique de Mão", CategoriaInstrumento.Percussao),
            CriarInstrumento(10, "Rebolo", CategoriaInstrumento.Percussao),
            CriarInstrumento(11, "Tamborim", CategoriaInstrumento.Percussao),
            CriarInstrumento(12, "Reco-reco", CategoriaInstrumento.Percussao),
            CriarInstrumento(13, "Ganzá", CategoriaInstrumento.Percussao),
            CriarInstrumento(14, "Cuíca", CategoriaInstrumento.Percussao),
            CriarInstrumento(15, "Agogô", CategoriaInstrumento.Percussao),
            CriarInstrumento(16, "Timbal", CategoriaInstrumento.Percussao),
            CriarInstrumento(17, "Atabaque", CategoriaInstrumento.Percussao),

            // Vocal
            CriarInstrumento(18, "Voz Principal", CategoriaInstrumento.Vocal),
            CriarInstrumento(19, "Backing Vocal", CategoriaInstrumento.Vocal)
        );
    }

    private static object CriarInstrumento(long id, string nome, CategoriaInstrumento categoria)
    {
        return new
        {
            IdInstrumento = id,
            Nome = nome,
            Categoria = categoria,
            DataCriacao = DataPadraoSeed
        };
    }
}
