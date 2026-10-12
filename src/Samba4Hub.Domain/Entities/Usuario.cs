using Samba4Hub.Domain.Enums;

namespace Samba4Hub.Domain.Entities;

/// <summary>
/// Representa a conta de acesso principal de um usuário na plataforma.
/// </summary>
public class Usuario : BaseEntity
{
    /// <summary>
    /// Identificador único numérico do usuário (PK).
    /// </summary>
    public long IdUsuario { get; set; }

    /// <summary>
    /// Endereço de e-mail institucional ou pessoal do usuário.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Hash criptográfico da senha de acesso do usuário.
    /// </summary>
    public string SenhaHash { get; set; } = string.Empty;

    /// <summary>
    /// Número de telefone principal com DDD no padrão E.164.
    /// </summary>
    public string Telefone { get; set; } = string.Empty;

    /// <summary>
    /// Documento fiscal (CPF ou CNPJ alfanumérico) higienizado sem máscara.
    /// </summary>
    public string CpfCnpj { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de perfil de atuação da conta.
    /// </summary>
    public TipoUsuario Tipo { get; set; }

    /// <summary>
    /// Perfil de atuação como músico instrumentista (relação 1:1 opcional).
    /// </summary>
    public PerfilMusico? PerfilMusico { get; set; }

    /// <summary>
    /// Perfil de atuação como contratante de apresentações (relação 1:1 opcional).
    /// </summary>
    public PerfilContratante? PerfilContratante { get; set; }
}
