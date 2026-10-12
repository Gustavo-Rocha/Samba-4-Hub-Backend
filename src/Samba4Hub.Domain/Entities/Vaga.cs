using Samba4Hub.Domain.Enums;

namespace Samba4Hub.Domain.Entities;

/// <summary>
/// Oportunidade ou evento de apresentação publicado por um contratante.
/// </summary>
public class Vaga : BaseEntity
{
    /// <summary>
    /// Identificador único numérico da oportunidade de vaga (PK).
    /// </summary>
    public long IdVaga { get; set; }

    /// <summary>
    /// Identificador do perfil de contratante responsável pela publicação.
    /// </summary>
    public long ContratanteId { get; set; }

    /// <summary>
    /// Perfil do contratante responsável.
    /// </summary>
    public PerfilContratante Contratante { get; set; } = null!;

    /// <summary>
    /// Título descritivo e chamativo da oportunidade (ex: "Cavaquinista para Roda de Samba aos Sábados").
    /// </summary>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>
    /// Descrição detalhada das exigências, repertório esperado e dinâmica da apresentação.
    /// </summary>
    public string Descricao { get; set; } = string.Empty;

    /// <summary>
    /// Vertente ou estilo predominante de samba e pagode do evento.
    /// </summary>
    public EstiloSamba EstiloPredominante { get; set; }

    /// <summary>
    /// Formato da formação musical solicitado (individual, duo/trio ou roda completa).
    /// </summary>
    public FormatoVaga FormatoSolicitado { get; set; }

    /// <summary>
    /// Data em que a apresentação musical ocorrerá.
    /// </summary>
    public DateOnly DataEvento { get; set; }

    /// <summary>
    /// Horário previsto de início da apresentação.
    /// </summary>
    public TimeOnly HoraInicio { get; set; }

    /// <summary>
    /// Horário previsto de encerramento da apresentação (pode cruzar a meia-noite).
    /// </summary>
    public TimeOnly HoraFim { get; set; }

    /// <summary>
    /// Logradouro ou endereço do local do evento.
    /// </summary>
    public string Logradouro { get; set; } = string.Empty;

    /// <summary>
    /// Bairro onde a apresentação acontecerá.
    /// </summary>
    public string Bairro { get; set; } = string.Empty;

    /// <summary>
    /// Cidade do local do evento.
    /// </summary>
    public string Cidade { get; set; } = string.Empty;

    /// <summary>
    /// Sigla do estado (UF) em duas letras maiúsculas.
    /// </summary>
    public string Estado { get; set; } = string.Empty;

    /// <summary>
    /// Valor monetário do cachê oferecido para a apresentação.
    /// </summary>
    public decimal? CacheOferecido { get; set; }

    /// <summary>
    /// Indica se o cachê da apresentação está em aberto para negociação com os candidatos.
    /// </summary>
    public bool CacheACombinar { get; set; }

    /// <summary>
    /// Indica se o estabelecimento ou contratante disponibiliza equipamento de som (PA, microfones, cabos).
    /// </summary>
    public bool PossuiEquipamentoSom { get; set; }

    /// <summary>
    /// Situação operacional atual da vaga.
    /// </summary>
    public StatusVaga Status { get; set; } = StatusVaga.Aberta;

    /// <summary>
    /// Relação de instrumentos musicais demandados para preenchimento desta vaga.
    /// </summary>
    public ICollection<VagaInstrumento> InstrumentosDemandados { get; set; } = new List<VagaInstrumento>();

    /// <summary>
    /// Candidaturas recebidas de músicos interessados.
    /// </summary>
    public ICollection<Candidatura> Candidaturas { get; set; } = new List<Candidatura>();
}
