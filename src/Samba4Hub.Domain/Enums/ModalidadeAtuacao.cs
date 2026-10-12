namespace Samba4Hub.Domain.Enums;

/// <summary>
/// Define a modalidade artística de atuação do músico.
/// </summary>
public enum ModalidadeAtuacao
{
    /// <summary>
    /// Atuação individual / músico de apoio.
    /// </summary>
    Solo = 1,

    /// <summary>
    /// Banda, grupo ou roda de samba completa formada.
    /// </summary>
    GrupoCompleto = 2,

    /// <summary>
    /// Atua tanto como instrumentista individual quanto com grupo próprio.
    /// </summary>
    Ambos = 3
}
