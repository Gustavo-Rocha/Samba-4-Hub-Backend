using Samba4Hub.Domain.Enums;

namespace Samba4Hub.Domain.Entities;

/// <summary>
/// Perfil corporativo ou pessoal de um usuário atuando como contratante de músicos.
/// </summary>
public class PerfilContratante : BaseEntity
{
    /// <summary>
    /// Identificador único numérico do perfil de contratante (PK).
    /// </summary>
    public long IdPerfilContratante { get; set; }

    /// <summary>
    /// Identificador da conta de usuário proprietária deste perfil.
    /// </summary>
    public long UsuarioId { get; set; }

    /// <summary>
    /// Conta de usuário proprietária.
    /// </summary>
    public Usuario Usuario { get; set; } = null!;

    /// <summary>
    /// Nome completo da pessoa física responsável pela contratação.
    /// </summary>
    public string NomeResponsavel { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de contratante (Pessoa Física, Bar/Casa Noturna ou Empresa).
    /// </summary>
    public TipoContratante Tipo { get; set; }

    /// <summary>
    /// Razão social ou nome fantasia comercial (aplicável a pessoas jurídicas).
    /// </summary>
    public string? NomeComercial { get; set; }

    /// <summary>
    /// CNPJ corporativo (alfanumérico ou numérico) opcional para estabelecimentos comerciais.
    /// </summary>
    public string? Cnpj { get; set; }

    /// <summary>
    /// Cidade onde o estabelecimento ou contratante está sediado.
    /// </summary>
    public string Cidade { get; set; } = string.Empty;

    /// <summary>
    /// Bairro de localização do contratante.
    /// </summary>
    public string? Bairro { get; set; }

    /// <summary>
    /// Sigla do estado (UF) em duas letras maiúsculas.
    /// </summary>
    public string Estado { get; set; } = string.Empty;

    /// <summary>
    /// Vagas de eventos publicadas por este contratante.
    /// </summary>
    public ICollection<Vaga> Vagas { get; set; } = new List<Vaga>();
}
