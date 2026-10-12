using Samba4Hub.Domain.Enums;

namespace Samba4Hub.Domain.Entities;

/// <summary>
/// Candidatura e proposta financeira submetida por um músico para concorrer a uma vaga.
/// </summary>
public class Candidatura : BaseEntity
{
    /// <summary>
    /// Identificador único numérico da candidatura (PK).
    /// </summary>
    public long IdCandidatura { get; set; }

    /// <summary>
    /// Identificador da vaga almejada.
    /// </summary>
    public long VagaId { get; set; }

    /// <summary>
    /// Vaga pretendida.
    /// </summary>
    public Vaga Vaga { get; set; } = null!;

    /// <summary>
    /// Identificador do perfil do músico candidato (aponta para <see cref="PerfilMusico"/>).
    /// </summary>
    public long MusicoId { get; set; }

    /// <summary>
    /// Perfil do músico proponente.
    /// </summary>
    public PerfilMusico Musico { get; set; } = null!;

    /// <summary>
    /// Valor monetário proposto pelo músico para realizar a apresentação.
    /// </summary>
    public decimal ValorProposta { get; set; }

    /// <summary>
    /// Mensagem personalizada ou observação enviada ao contratante.
    /// </summary>
    public string? Mensagem { get; set; }

    /// <summary>
    /// Situação atual da proposta.
    /// </summary>
    public StatusCandidatura Status { get; set; } = StatusCandidatura.Pendente;

    /// <summary>
    /// Data e hora em formato UTC em que o contratante respondeu à proposta (aceitou ou recusou).
    /// </summary>
    public DateTimeOffset? DataResposta { get; set; }

    /// <summary>
    /// Registros de cliques e interações de contato geradas a partir desta candidatura.
    /// </summary>
    public ICollection<InteracaoContato> Interacoes { get; set; } = new List<InteracaoContato>();
}
