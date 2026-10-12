using Samba4Hub.Domain.Enums;

namespace Samba4Hub.Domain.Entities;

/// <summary>
/// Registro de telemetria de interação e clique no botão de contato direto via WhatsApp.
/// </summary>
public class InteracaoContato : BaseEntity
{
    /// <summary>
    /// Identificador único numérico da interação de contato (PK).
    /// </summary>
    public long IdInteracaoContato { get; set; }

    /// <summary>
    /// Identificador da candidatura que motivou o contato.
    /// </summary>
    public long CandidaturaId { get; set; }

    /// <summary>
    /// Candidatura associada.
    /// </summary>
    public Candidatura Candidatura { get; set; } = null!;

    /// <summary>
    /// Parte que originou o clique de contato (Músico ou Contratante).
    /// </summary>
    public OrigemInteracao IniciadoPor { get; set; }

    /// <summary>
    /// Data e hora exata em formato UTC em que o botão foi clicado.
    /// </summary>
    public DateTimeOffset DataClique { get; set; }
}
