using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Samba4Hub.Domain.Entities;

namespace Samba4Hub.Data.Interceptors;

/// <summary>
/// Interceptor do EF Core para preenchimento automático das datas de auditoria (DataCriacao e DataAtualizacao) em UTC.
/// </summary>
public sealed class AuditableEntityInterceptor(TimeProvider? timeProvider = null) : SaveChangesInterceptor
{
    private readonly TimeProvider _timeProvider = timeProvider ?? TimeProvider.System;

    /// <inheritdoc />
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        AtualizarDatasAuditoria(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    /// <inheritdoc />
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        AtualizarDatasAuditoria(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void AtualizarDatasAuditoria(DbContext? context)
    {
        if (context is null)
        {
            return;
        }

        var utcNow = _timeProvider.GetUtcNow();

        foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.DataCriacao = utcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.DataAtualizacao = utcNow;
            }
        }
    }
}
