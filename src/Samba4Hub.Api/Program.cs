using Samba4Hub.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Registro da infraestrutura de dados e serviços via IServiceCollection
builder.Services.AddInfrastructure(builder.Configuration);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Execução segura e assíncrona das migrações do banco em ambiente de desenvolvimento
await app.ApplySamba4HubDatabaseMigrationsAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

