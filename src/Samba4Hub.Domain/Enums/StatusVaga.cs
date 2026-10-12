namespace Samba4Hub.Domain.Enums;

/// <summary>
/// Ciclo de vida operacional da vaga de apresentação.
/// </summary>
public enum StatusVaga
{
    /// <summary>
    /// Vaga aberta e recebendo ativamente candidaturas de músicos.
    /// </summary>
    Aberta = 1,

    /// <summary>
    /// Vaga suspensa temporariamente pelo contratante para avaliação.
    /// </summary>
    Pausada = 2,

    /// <summary>
    /// Vaga preenchida após aceitação de proposta de um músico.
    /// </summary>
    Preenchida = 3,

    /// <summary>
    /// Vaga cancelada pelo contratante.
    /// </summary>
    Cancelada = 4,

    /// <summary>
    /// Vaga expirada devido à ocorrência da data do evento sem contratação.
    /// </summary>
    Expirada = 5
}
