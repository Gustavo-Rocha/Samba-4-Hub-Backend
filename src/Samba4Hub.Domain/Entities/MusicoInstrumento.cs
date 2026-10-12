namespace Samba4Hub.Domain.Entities;

/// <summary>
/// Tabela associativa pura que relaciona um músico aos instrumentos que ele toca.
/// </summary>
public class MusicoInstrumento
{
    /// <summary>
    /// Identificador do perfil de músico (parte da chave primária composta).
    /// </summary>
    public long PerfilMusicoId { get; set; }

    /// <summary>
    /// Perfil de músico associado.
    /// </summary>
    public PerfilMusico PerfilMusico { get; set; } = null!;

    /// <summary>
    /// Identificador do instrumento musical (parte da chave primária composta).
    /// </summary>
    public long InstrumentoId { get; set; }

    /// <summary>
    /// Instrumento musical associado.
    /// </summary>
    public Instrumento Instrumento { get; set; } = null!;

    /// <summary>
    /// Indica se este é o instrumento de atuação principal do músico.
    /// </summary>
    public bool EhPrincipal { get; set; }
}
