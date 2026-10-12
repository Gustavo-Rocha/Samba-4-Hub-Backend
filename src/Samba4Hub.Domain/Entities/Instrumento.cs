using Samba4Hub.Domain.Enums;

namespace Samba4Hub.Domain.Entities;

/// <summary>
/// Catálogo geral de instrumentos musicais utilizados em samba e pagode.
/// </summary>
public class Instrumento : BaseEntity
{
    /// <summary>
    /// Identificador único numérico do instrumento (PK).
    /// </summary>
    public long IdInstrumento { get; set; }

    /// <summary>
    /// Nome oficial padronizado do instrumento (ex: Cavaquinho, Pandeiro, Surdo).
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Categoria técnica do instrumento (Harmonia/Cordas, Percussão ou Vocal).
    /// </summary>
    public CategoriaInstrumento Categoria { get; set; }

    /// <summary>
    /// Relação de músicos que tocam este instrumento.
    /// </summary>
    public ICollection<MusicoInstrumento> Musicos { get; set; } = new List<MusicoInstrumento>();

    /// <summary>
    /// Relação de vagas que demandam este instrumento.
    /// </summary>
    public ICollection<VagaInstrumento> Vagas { get; set; } = new List<VagaInstrumento>();
}
