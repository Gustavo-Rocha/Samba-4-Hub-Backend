namespace Samba4Hub.Domain.Enums;

/// <summary>
/// Formato da formação musical demandada pelo contratante.
/// </summary>
public enum FormatoVaga
{
    /// <summary>
    /// Vaga para instrumentista individual para completar uma formação.
    /// </summary>
    MusicoApoioIndividual = 1,

    /// <summary>
    /// Contratação de uma roda ou grupo completo formado.
    /// </summary>
    RodaCompleta = 2,

    /// <summary>
    /// Formação reduzida em formato de duo ou trio acústico.
    /// </summary>
    DuoOuTrio = 3
}
