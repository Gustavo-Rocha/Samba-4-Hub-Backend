namespace Samba4Hub.Domain.Enums;

/// <summary>
/// Status da candidatura enviada pelo músico para uma vaga.
/// </summary>
public enum StatusCandidatura
{
    /// <summary>
    /// Proposta pendente de avaliação pelo contratante.
    /// </summary>
    Pendente = 1,

    /// <summary>
    /// Proposta aceita pelo contratante para o evento.
    /// </summary>
    Aceita = 2,

    /// <summary>
    /// Proposta recusada pelo contratante.
    /// </summary>
    Recusada = 3,

    /// <summary>
    /// Candidatura cancelada pelo próprio músico antes do fechamento.
    /// </summary>
    CanceladaPeloMusico = 4
}
