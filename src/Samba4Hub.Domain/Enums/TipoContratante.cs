namespace Samba4Hub.Domain.Enums;

/// <summary>
/// Define a natureza jurídica do contratante.
/// </summary>
public enum TipoContratante
{
    /// <summary>
    /// Contratante individual (Pessoa Física).
    /// </summary>
    PessoaFisica = 1,

    /// <summary>
    /// Bar, restaurante ou casa de shows.
    /// </summary>
    BarCasaNoturna = 2,

    /// <summary>
    /// Empresa ou agência corporativa de eventos.
    /// </summary>
    Empresa = 3
}
