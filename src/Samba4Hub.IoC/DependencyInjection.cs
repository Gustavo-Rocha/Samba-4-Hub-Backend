using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Samba4Hub.Data.Context;
using Samba4Hub.Data.Extensions;
using Samba4Hub.Data.Interceptors;

namespace Samba4Hub.IoC;

/// <summary>
/// Métodos de extensão para registro dos serviços de infraestrutura e persistência de dados.
/// </summary>
public static class DependencyInjection
{
    private const string DefaultConnectionName = "samba4hubdb";

    /// <summary>
    /// Registra toda a infraestrutura da aplicação (banco de dados, interceptors e serviços) via <see cref="IServiceCollection"/>.
    /// Ponto de entrada chamado via builder.Services.AddInfrastructure(builder.Configuration) no Program.cs.
    /// </summary>
    /// <param name="services">Coleção de serviços da aplicação (builder.Services).</param>
    /// <param name="configuration">Configurações da aplicação (builder.Configuration).</param>
    /// <param name="connectionName">Nome da conexão no appsettings / Aspire (padrão: "samba4hubdb").</param>
    /// <returns>A coleção de serviços para encadeamento.</returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        string connectionName = DefaultConnectionName)
    {
        // 1. Registro do interceptor de auditoria temporal UTC (singleton)
        services.AddSingleton<AuditableEntityInterceptor>();

        // 2. Resolução da string de conexão do PostgreSQL
        var connectionString = configuration.GetConnectionString(connectionName)
            ?? configuration[$"ConnectionStrings:{connectionName}"]
            ?? "Host=localhost;Database=samba4hubdb;Username=postgres;Password=postgres";

        // 3. Registro do DbContext do EF Core com PostgreSQL
        services.AddDbContext<Samba4HubDbContext>((serviceProvider, options) =>
        {
            var interceptor = serviceProvider.GetService<AuditableEntityInterceptor>();
            if (interceptor is not null)
            {
                options.AddInterceptors(interceptor);
            }

            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(typeof(Samba4HubDbContext).Assembly.FullName);
            });
        });

        return services;
    }

    /// <summary>
    /// Registra toda a infraestrutura da aplicação (alias semântico via <see cref="IServiceCollection"/>).
    /// </summary>
    public static IServiceCollection AddSamba4HubInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        string connectionName = DefaultConnectionName)
    {
        return services.AddInfrastructure(configuration, connectionName);
    }

    /// <summary>
    /// Registra toda a infraestrutura da aplicação (banco de dados, persistência e interceptors).
    /// Ponto de entrada chamado via builder.AddInfrastructure() no Program.cs.
    /// </summary>
    /// <param name="builder">Builder do host da aplicação (Aspire / WebApplication).</param>
    /// <param name="connectionName">Nome da conexão configurada no orquestrador (padrão: "samba4hubdb").</param>
    /// <returns>O próprio builder para encadeamento.</returns>
    public static IHostApplicationBuilder AddInfrastructure(
        this IHostApplicationBuilder builder,
        string connectionName = DefaultConnectionName)
    {
        return builder.AddSamba4HubData(connectionName);
    }

    /// <summary>
    /// Registra toda a infraestrutura da aplicação (alias semântico para <see cref="AddInfrastructure"/>).
    /// </summary>
    public static IHostApplicationBuilder AddSamba4HubInfrastructure(
        this IHostApplicationBuilder builder,
        string connectionName = DefaultConnectionName)
    {
        return builder.AddInfrastructure(connectionName);
    }

    /// <summary>
    /// Registra o contexto <see cref="Samba4HubDbContext"/>, o interceptor de auditoria e a resiliência do Npgsql no host da aplicação.
    /// </summary>
    /// <param name="builder">Builder do host da aplicação (Aspire / WebApplication).</param>
    /// <param name="connectionName">Nome da conexão configurada no orquestrador (padrão: "samba4hubdb").</param>
    /// <returns>O próprio builder para encadeamento.</returns>
    public static IHostApplicationBuilder AddSamba4HubData(
        this IHostApplicationBuilder builder,
        string connectionName = DefaultConnectionName)
    {
        // Registro do interceptor de auditoria temporal UTC
        builder.Services.AddSingleton<AuditableEntityInterceptor>();

        // Registro do DbContext com suporte nativo a resiliência, telemetria e health checks do Aspire
        builder.AddNpgsqlDbContext<Samba4HubDbContext>(connectionName);

        return builder;
    }

    /// <summary>
    /// Encaminha a execução das migrações do banco de dados na camada de dados sem exigir que a API referencie a camada Data diretamente.
    /// </summary>
    /// <param name="host">Instância do host em execução.</param>
    /// <param name="cancellationToken">Token de cancelamento.</param>
    /// <returns>Task assíncrona da execução da migração.</returns>
    public static Task ApplySamba4HubDatabaseMigrationsAsync(
        this IHost host,
        CancellationToken cancellationToken = default)
    {
        return host.ApplyDatabaseMigrationsAsync(cancellationToken);
    }
}
