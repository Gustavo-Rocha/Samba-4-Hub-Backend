using Samba4Hub.Domain.Enums;

namespace Samba4Hub.Domain.Entities;

/// <summary>
/// Perfil artístico de um usuário atuando como músico instrumentista.
/// </summary>
public class PerfilMusico : BaseEntity
{
    /// <summary>
    /// Identificador único numérico do perfil de músico (PK).
    /// </summary>
    public long IdPerfilMusico { get; set; }

    /// <summary>
    /// Identificador da conta de usuário proprietária deste perfil.
    /// </summary>
    public long UsuarioId { get; set; }

    /// <summary>
    /// Conta de usuário proprietária.
    /// </summary>
    public Usuario Usuario { get; set; } = null!;

    /// <summary>
    /// Nome artístico público exibido nas vagas e propostas.
    /// </summary>
    public string NomeArtistico { get; set; } = string.Empty;

    /// <summary>
    /// Biografia artística, histórico e trajetória musical.
    /// </summary>
    public string? Bio { get; set; }

    /// <summary>
    /// Modalidade de atuação artística (solo, grupo ou ambos).
    /// </summary>
    public ModalidadeAtuacao Modalidade { get; set; }

    /// <summary>
    /// Cachê mínimo pretendido para apresentações individuais.
    /// </summary>
    public decimal? CacheMinimo { get; set; }

    /// <summary>
    /// Cachê mínimo pretendido para apresentações de grupo/roda completa.
    /// </summary>
    public decimal? CacheGrupoMinimo { get; set; }

    /// <summary>
    /// Cidade base de atuação do músico.
    /// </summary>
    public string Cidade { get; set; } = string.Empty;

    /// <summary>
    /// Sigla do estado (UF) em duas letras maiúsculas.
    /// </summary>
    public string Estado { get; set; } = string.Empty;

    /// <summary>
    /// Link público do perfil no Instagram para portfólio.
    /// </summary>
    public string? LinkInstagram { get; set; }

    /// <summary>
    /// Link público de canal ou vídeo de apresentação no YouTube.
    /// </summary>
    public string? LinkYoutube { get; set; }

    /// <summary>
    /// Indica se o perfil do músico está ativo para receber propostas e candidatar-se a vagas.
    /// </summary>
    public bool Ativo { get; set; } = true;

    /// <summary>
    /// Instrumentos musicais dominados pelo músico.
    /// </summary>
    public ICollection<MusicoInstrumento> Instrumentos { get; set; } = new List<MusicoInstrumento>();

    /// <summary>
    /// Candidaturas realizadas pelo músico para vagas abertas.
    /// </summary>
    public ICollection<Candidatura> Candidaturas { get; set; } = new List<Candidatura>();
}
