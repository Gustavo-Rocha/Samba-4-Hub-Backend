namespace Samba4Hub.Domain.Enums;

/// <summary>
/// Define o tipo de perfil do usuário na plataforma.
/// </summary>
public enum TipoUsuario
{
    /// <summary>
    /// Usuário atua publicando oportunidades e contratando músicos.
    /// </summary>
    Contratante = 1,

    /// <summary>
    /// Usuário atua como músico instrumentista buscando vagas.
    /// </summary>
    Musico = 2,

    /// <summary>
    /// Usuário atua simultaneamente como contratante e músico.
    /// </summary>
    Ambos = 3
}
