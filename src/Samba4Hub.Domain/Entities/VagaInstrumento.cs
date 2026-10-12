namespace Samba4Hub.Domain.Entities;

/// <summary>
/// Tabela associativa pura que relaciona uma vaga aos instrumentos demandados.
/// </summary>
public class VagaInstrumento
{
    /// <summary>
    /// Identificador da vaga (parte da chave primária composta).
    /// </summary>
    public long VagaId { get; set; }

    /// <summary>
    /// Vaga associada.
    /// </summary>
    public Vaga Vaga { get; set; } = null!;

    /// <summary>
    /// Identificador do instrumento musical demandado (parte da chave primária composta).
    /// </summary>
    public long InstrumentoId { get; set; }

    /// <summary>
    /// Instrumento musical demandado.
    /// </summary>
    public Instrumento Instrumento { get; set; } = null!;
}
