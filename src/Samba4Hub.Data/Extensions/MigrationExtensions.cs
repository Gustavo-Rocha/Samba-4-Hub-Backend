using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Samba4Hub.Data.Context;

namespace Samba4Hub.Data.Extensions;

/// <summary>
/// Métodos de extensão para execução segura e automatizada de migrações do banco de dados na inicialização.
/// </summary>
public static class MigrationExtensions
{
    private const string ConnectionStringName = "samba4hubdb";

    /// <summary>
    /// Executa as migrações pendentes do banco de dados assincronamente durante o startup da aplicação em ambiente de desenvolvimento.
    /// </summary>
    /// <param name="host">Instância do host em inicialização.</param>
    /// <param name="cancellationToken">Token de cancelamento da operação.</param>
    /// <returns>Task assíncrona representando a execução da migração.</returns>
    /// <exception cref="Exception">Propaga exceções ocorridas durante a conexão ou migração para abortar o startup com falha.</exception>
    public static async Task ApplyDatabaseMigrationsAsync(this IHost host, CancellationToken cancellationToken = default)
    {
        var environment = host.Services.GetRequiredService<IHostEnvironment>();
        var loggerFactory = host.Services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Samba4Hub.DatabaseMigration");

        // 1. Condição de Ativação Estrita: Apenas em ambiente Development
        if (!environment.IsDevelopment())
        {
            logger.LogInformation(
                "Ambiente '{EnvironmentName}' detectado. Migrações automáticas de banco de dados são executadas estritamente em Development. Inicialização prosseguindo sem migração.",
                environment.EnvironmentName);
            return;
        }

        // 2. Validação da Connection String
        var configuration = host.Services.GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString(ConnectionStringName);

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            logger.LogWarning(
                "A Connection String '{ConnectionStringName}' não foi configurada ou está vazia. Nenhuma migração será aplicada.",
                ConnectionStringName);
            return;
        }

        logger.LogInformation(
            "Iniciando verificação e aplicação de migrações pendentes do banco de dados no PostgreSQL (banco '{ConnectionStringName}')...",
            ConnectionStringName);

        try
        {
            await using var scope = host.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<Samba4HubDbContext>();

            await dbContext.Database.MigrateAsync(cancellationToken);

            logger.LogInformation("Migrações do banco de dados PostgreSQL aplicadas com sucesso!");
        }
        catch (Exception ex)
        {
            logger.LogCritical(
                ex,
                "Falha crítica ao aplicar as migrações do banco de dados no PostgreSQL. Abortando inicialização da aplicação.");
            throw;
        }
    }
}
