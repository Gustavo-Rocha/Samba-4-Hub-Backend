---
title: Plano de Scaffolding para Normalização do Samba4Hub
version: 1.0
date_created: 2026-10-10
owner: Gustavo
tags: [scaffolding, dotnet, structure, commands]
---

# Introduction

Este documento especifica os comandos exatos de terminal (`git mv`, `dotnet new`, `dotnet reference`, edições de arquivos) para executar a reestruturação da solução `Samba4Hub` aprovada na Fase 1 (`.specs/01-design.md`).

## 1. Purpose & Scope

- **Objetivo**: detalhar o roteiro passo a passo de execução técnica para reestruturar as pastas fisicamente no disco e refazer o arquivo de solução `.slnx`.
- **Precondição**: repositório limpo, processo `dotnet run` encerrado.

## 2. Roteiro de Comandos e Ações

### Passo 1: Movimentação Física dos Arquivos e Projetos Existentes (`git mv`)

Executado a partir de `c:\Dev\Projeto Samba4Hub\Backend-Samba4Hub`:

```powershell
# Criar diretórios de topo
New-Item -ItemType Directory -Path "src" -Force
New-Item -ItemType Directory -Path "test" -Force

# Mover arquivos da raiz da solução
git mv "Samba4Hub/Samba4Hub.slnx" "Samba4Hub.slnx"
git mv "Samba4Hub/aspire.config.json" "aspire.config.json"
git mv "Samba4Hub/.dockerignore" ".dockerignore"

# Mover projetos para /src e /test
git mv "Samba4Hub/Samba4Hub" "src/Samba4Hub.Api"
git mv "Samba4Hub/Samba4Hub.Domain" "src/Samba4Hub.Domain"
git mv "Samba4Hub/Samba4Hub.Infra" "src/Samba4Hub.Data"
git mv "Samba4Hub/Samba4Hub.Shareable" "src/Samba4Hub.Shareable"
git mv "Samba4Hub/Samba4Hub.ServiceDefaults" "src/Samba4Hub.ServiceDefaults"
git mv "Samba4Hub/Samba4Hub.AppHost" "src/Samba4Hub.AppHost"
git mv "Samba4Hub/Samba4Hub.Testes" "test/Samba4Hub.Api.Tests"

# Remover pasta intermediária se vazia
Remove-Item -Path "Samba4Hub" -Force -ErrorAction SilentlyContinue
```

### Passo 2: Renomeação de Arquivos de Projeto `.csproj`

```powershell
# Renomear arquivos .csproj internos
git mv "src/Samba4Hub.Api/Samba4Hub.csproj" "src/Samba4Hub.Api/Samba4Hub.Api.csproj"
git mv "src/Samba4Hub.Data/Samba4Hub.Infra.csproj" "src/Samba4Hub.Data/Samba4Hub.Data.csproj"
git mv "test/Samba4Hub.Api.Tests/Samba4Hub.Testes.csproj" "test/Samba4Hub.Api.Tests/Samba4Hub.Api.Tests.csproj"

if (Test-Path "src/Samba4Hub.Api/Samba4Hub.csproj.user") {
    git mv "src/Samba4Hub.Api/Samba4Hub.csproj.user" "src/Samba4Hub.Api/Samba4Hub.Api.csproj.user"
}
```

### Passo 3: Criação dos Novos Projetos (Scaffolding `dotnet new`)

```powershell
# Projeto IoC (classlib em /src)
dotnet new classlib -n Samba4Hub.IoC -o "src/Samba4Hub.IoC" --framework net10.0

# Projetos de Teste (xunit em /test)
dotnet new xunit -n Samba4Hub.Domain.Tests -o "test/Samba4Hub.Domain.Tests" --framework net10.0
dotnet new xunit -n Samba4Hub.Data.Tests -o "test/Samba4Hub.Data.Tests" --framework net10.0
```

### Passo 4: Atualização de Namespaces e Referências Internas

1. **`src/Samba4Hub.Data/Class1.cs`**: namespace alterado de `Samba4Hub.Infra` para `Samba4Hub.Data`.
2. **`test/Samba4Hub.Api.Tests/UnitTest1.cs`**: namespace alterado de `Samba4Hub.Testes` para `Samba4Hub.Api.Tests`.
3. **`src/Samba4Hub.AppHost/AppHost.cs`**:
   ```csharp
   var builder = DistributedApplication.CreateBuilder(args);
   builder.AddProject<Projects.Samba4Hub_Api>("samba4hub");
   builder.Build().Run();
   ```
4. **`aspire.config.json`**:
   ```json
   {
     "appHost": {
       "path": "src/Samba4Hub.AppHost/Samba4Hub.AppHost.csproj"
     }
   }
   ```
5. **`src/Samba4Hub.Api/Dockerfile`**: ajustar instruções `COPY` e `WORKDIR` para refletir a nova estrutura `src/`.

### Passo 5: Configuração do Grafo de Referências de Projetos (`dotnet add reference`)

```powershell
# Referências do Api
dotnet add "src/Samba4Hub.Api/Samba4Hub.Api.csproj" reference "src/Samba4Hub.IoC/Samba4Hub.IoC.csproj"
dotnet add "src/Samba4Hub.Api/Samba4Hub.Api.csproj" reference "src/Samba4Hub.ServiceDefaults/Samba4Hub.ServiceDefaults.csproj"
dotnet add "src/Samba4Hub.Api/Samba4Hub.Api.csproj" reference "src/Samba4Hub.Shareable/Samba4Hub.Shareable.csproj"

# Referências do IoC
dotnet add "src/Samba4Hub.IoC/Samba4Hub.IoC.csproj" reference "src/Samba4Hub.Data/Samba4Hub.Data.csproj"
dotnet add "src/Samba4Hub.IoC/Samba4Hub.IoC.csproj" reference "src/Samba4Hub.Domain/Samba4Hub.Domain.csproj"

# Referências do Data
dotnet add "src/Samba4Hub.Data/Samba4Hub.Data.csproj" reference "src/Samba4Hub.Domain/Samba4Hub.Domain.csproj"

# Referências do Domain
dotnet add "src/Samba4Hub.Domain/Samba4Hub.Domain.csproj" reference "src/Samba4Hub.Shareable/Samba4Hub.Shareable.csproj"

# Referências do AppHost
dotnet add "src/Samba4Hub.AppHost/Samba4Hub.AppHost.csproj" reference "src/Samba4Hub.Api/Samba4Hub.Api.csproj"

# Referências dos Testes
dotnet add "test/Samba4Hub.Api.Tests/Samba4Hub.Api.Tests.csproj" reference "src/Samba4Hub.Api/Samba4Hub.Api.csproj"
dotnet add "test/Samba4Hub.Domain.Tests/Samba4Hub.Domain.Tests.csproj" reference "src/Samba4Hub.Domain/Samba4Hub.Domain.csproj"
dotnet add "test/Samba4Hub.Data.Tests/Samba4Hub.Data.Tests.csproj" reference "src/Samba4Hub.Data/Samba4Hub.Data.csproj"
```

### Passo 6: Atualização do Soluton File (`Samba4Hub.slnx`)

Estrutura final do `.slnx`:

```xml
<Solution>
  <Folder Name="/src/">
    <Folder Name="/src/01-Apresentation/">
      <Project Path="src/Samba4Hub.Api/Samba4Hub.Api.csproj" />
    </Folder>
    <Folder Name="/src/02-Domain/">
      <Project Path="src/Samba4Hub.Domain/Samba4Hub.Domain.csproj" />
    </Folder>
    <Folder Name="/src/03-Infraestructure/">
      <Project Path="src/Samba4Hub.Data/Samba4Hub.Data.csproj" />
    </Folder>
    <Folder Name="/src/04-Shareable/">
      <Project Path="src/Samba4Hub.Shareable/Samba4Hub.Shareable.csproj" />
      <Project Path="src/Samba4Hub.IoC/Samba4Hub.IoC.csproj" />
      <Project Path="src/Samba4Hub.ServiceDefaults/Samba4Hub.ServiceDefaults.csproj" />
    </Folder>
    <Project Path="src/Samba4Hub.AppHost/Samba4Hub.AppHost.csproj" />
  </Folder>
  <Folder Name="/test/">
    <Folder Name="/test/99-Testes/">
      <Project Path="test/Samba4Hub.Api.Tests/Samba4Hub.Api.Tests.csproj" />
      <Project Path="test/Samba4Hub.Domain.Tests/Samba4Hub.Domain.Tests.csproj" />
      <Project Path="test/Samba4Hub.Data.Tests/Samba4Hub.Data.Tests.csproj" />
    </Folder>
  </Folder>
</Solution>
```

### Passo 7: Validação e Compilação Final

```powershell
dotnet restore Samba4Hub.slnx
dotnet build Samba4Hub.slnx
dotnet test Samba4Hub.slnx
```

---

## 3. Confirmação do Plano

Aguardando sua confirmação para iniciar a execução do plano acima.
