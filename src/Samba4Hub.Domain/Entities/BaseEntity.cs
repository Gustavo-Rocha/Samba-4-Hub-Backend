namespace Samba4Hub.Domain.Entities;

/// <summary>
/// Entidade base para todas as entidades operacionais que possuem chave primária numérica identity e auditoria temporal.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Data e hora de criação do registro em formato UTC.
    /// </summary>
    public DateTimeOffset DataCriacao { get; set; }

    /// <summary>
    /// Data e hora da última atualização do registro em formato UTC.
    /// </summary>
    public DateTimeOffset? DataAtualizacao { get; set; }
}
