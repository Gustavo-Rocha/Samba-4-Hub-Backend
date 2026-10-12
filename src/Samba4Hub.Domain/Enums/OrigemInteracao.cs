namespace Samba4Hub.Domain.Enums;

/// <summary>
/// Identifica a parte que iniciou o contato via WhatsApp na telemetria.
/// </summary>
public enum OrigemInteracao
{
    /// <summary>
    /// Contato iniciado pelo músico interessado.
    /// </summary>
    Musico = 1,

    /// <summary>
    /// Contato iniciado pelo contratante da vaga.
    /// </summary>
    Contratante = 2
}
