# Samba4Hub — Documentação da Modelagem de Dados do MVP (Fase 1)

Documento de referência autocontido sobre o banco de dados do Samba4Hub. Ele foi escrito para ser usado como fonte em um NotebookLM: cada seção pode ser lida isoladamente e usa linguagem explícita, sem depender de diagramas visuais.

- Versão do documento: 2.0 (Modelo Lean de MVP)
- Data: 11 de outubro de 2026
- Responsável: Gustavo
- Repositório: Samba-4-Hub-Backend
- Status: Planejamento refinado; enums tipados no C# persistidos como inteiros no PostgreSQL com validação de payload na API via FluentValidation.

---

## 1. O que é o Samba4Hub

O Samba4Hub é uma plataforma que conecta **músicos de samba e pagode** a **contratantes** (pessoas físicas, bares, casas noturnas e empresas). No MVP (Produto Mínimo Viável), a Fase 1 cobre:

1. Cadastro de usuários com documento fiscal (CPF ou CNPJ alfanumérico).
2. Perfil de músico (instrumentos, cachê, cidade, redes sociais).
3. Perfil de contratante (pessoa física ou jurídica).
4. Publicação de vagas (eventos que precisam de músicos).
5. Candidatura de músicos às vagas, com proposta de valor.
6. Registro (telemetria) dos cliques no botão de contato via WhatsApp.

---

## 2. Glossário

| Termo | Significado |
|---|---|
| MVP | Produto Mínimo Viável: primeira versão funcional e enxuta do sistema. |
| PK | Primary Key (chave primária): coluna que identifica unicamente uma linha. |
| FK | Foreign Key (chave estrangeira): coluna que aponta para a PK de outra tabela. |
| PK composta | Chave primária formada por duas colunas juntas (usada em tabelas associativas). |
| Enum | Tipo enumerado no C# que representa um conjunto fixo de constantes nomeadas. |
| FluentValidation | Biblioteca .NET para validação expressiva de requisições HTTP antes de chegarem ao domínio. |
| Identity | Recurso do PostgreSQL que gera automaticamente o próximo número sequencial da PK. |
| Migration | Arquivo gerado pelo Entity Framework que descreve e executa alterações no banco de dados. |
| EF Core | Entity Framework Core: ORM oficial do .NET. |
| Aspire | .NET Aspire: orquestrador local de containers (PostgreSQL) e microsserviços. |
| snake_case | Padrão de nomes em minúsculas separadas por sublinhado, ex: `data_evento`. |

---

## 3. Stack Técnica e Versões

| Item | Versão |
|---|---|
| Runtime | .NET 10 (`net10.0`), SDK 10.0.401, C# 14 |
| Banco de dados | PostgreSQL 16 ou superior |
| ORM | Entity Framework Core 10.0.12 |
| Provider PostgreSQL | Npgsql.EntityFrameworkCore.PostgreSQL 10.0.3 |
| Orquestração | .NET Aspire 13.5.4 |
| Validação de Entrada | FluentValidation |
| Ferramenta de Migrations | dotnet-ef 10.0.12 (ferramenta local) |

---

## 4. Decisão Arquitetural: Enums como Inteiros e Validação via FluentValidation

Para manter o projeto **enxuto, de alto desempenho e rápido de evoluir no MVP**:

1. **No C# (Domínio)**:
   - Os enums continuam sendo tipos fortemente tipados (`StatusVaga`, `TipoUsuario`, etc.), garantindo type-safety em regras de negócio como `if (vaga.Status == StatusVaga.Aberta)`.
2. **Na Camada de API (Borda da Aplicação)**:
   - O **FluentValidation** intercepta o payload JSON recebido. Ele valida se o valor enviado pertence ao enum (`RuleFor(x => x.Status).IsInEnum()`). Caso o valor seja inválido ou inexistente, a requisição é rejeitada com código HTTP `400 Bad Request` antes de bater no banco.
3. **No PostgreSQL (Persistência)**:
   - Os enums são persistidos diretamente como colunas do tipo `integer` (4 bytes).
   - Não há tabelas de lookup redundantes e não há constraints CHECK artificiais no banco para enums, mantendo o schema limpo, com zero JOINs nas consultas de listagem e máxima velocidade.

---

## 5. Catálogo dos 9 Enums do Sistema

| Enum C# | Coluna no Banco | Valores Numéricos | Significado |
|---|---|---|---|
| `TipoUsuario` | `usuario.tipo` | 1=Contratante, 2=Musico, 3=Ambos | Perfil de atuação da conta |
| `TipoContratante` | `perfil_contratante.tipo` | 1=PessoaFisica, 2=BarCasaNoturna, 3=Empresa | Natureza jurídica do contratante |
| `ModalidadeAtuacao` | `perfil_musico.modalidade` | 1=Solo, 2=GrupoCompleto, 3=Ambos | Forma como o músico se apresenta |
| `CategoriaInstrumento`| `instrumento.categoria` | 1=HarmoniaCordas, 2=Percussao, 3=Vocal | Classificação do instrumento |
| `EstiloSamba` | `vaga.estilo_predominante` | 1=SambaDeRaiz, 2=Pagode90, 3=PagodeAtual, 4=PartidoAlto, 5=Geral | Vertente musical do evento |
| `FormatoVaga` | `vaga.formato_solicitado` | 1=MusicoApoioIndividual, 2=RodaCompleta, 3=DuoOuTrio | Quantidade de músicos demandada |
| `StatusVaga` | `vaga.status` | 1=Aberta, 2=Pausada, 3=Preenchida, 4=Cancelada, 5=Expirada | Ciclo de vida da vaga |
| `StatusCandidatura` | `candidatura.status` | 1=Pendente, 2=Aceita, 3=Recusada, 4=CanceladaPeloMusico | Situação da proposta do músico |
| `OrigemInteracao` | `interacao_contato.iniciado_por` | 1=Musico, 2=Contratante | Quem clicou no botão WhatsApp |

---

## 6. Dicionário das Tabelas de Dados

### 6.1 `usuario`
- `id` (bigint identity PK)
- `email` (varchar(150), único)
- `senha_hash` (varchar(255))
- `telefone` (varchar(20), formato E.164)
### 6.1 `usuario`
- `id_usuario` (bigint identity PK)
- `email` (varchar(150), único)
- `senha_hash` (varchar(255))
- `telefone` (varchar(20))
- `cpf_cnpj` (varchar(14), único, aceita CPF numérico ou CNPJ alfanumérico)
- `tipo` (integer, enum TipoUsuario)
- `data_criacao` e `data_atualizacao` (timestamptz UTC)

### 6.2 `perfil_musico`
- `id_perfil_musico` (bigint identity PK)
- `id_usuario` (bigint FK apontando para `usuario.id_usuario`, único, relação 1:1)
- `nome_artistico` (varchar(100))
- `bio` (varchar(1000), opcional)
- `modalidade` (integer, enum ModalidadeAtuacao)
- `cache_minimo` e `cache_grupo_minimo` (numeric(10,2), opcionais)
- `cidade` (varchar(100)) e `estado` (varchar(2))
- `link_instagram` e `link_youtube` (varchar(255), opcionais)
- `ativo` (boolean)

### 6.3 `perfil_contratante`
- `id_perfil_contratante` (bigint identity PK)
- `id_usuario` (bigint FK apontando para `usuario.id_usuario`, único, relação 1:1)
- `nome_responsavel` (varchar(100))
- `tipo` (integer, enum TipoContratante)
- `nome_comercial` (varchar(120), opcional se PJ)
- `cnpj` (varchar(14), opcional para PF, CNPJ alfanumérico se PJ)
- `cidade` (varchar(100)), `bairro` (varchar(80), opcional), `estado` (varchar(2))

### 6.4 `instrumento` (Tabela de Catálogo)
- `id_instrumento` (bigint identity PK, inicia em 1000 para não colidir com o seed inicial)
- `nome` (varchar(50), único)
- `categoria` (integer, enum CategoriaInstrumento)

### 6.5 `musico_instrumento` (Associativa com PK Composta)
- `id_perfil_musico` (bigint FK para `perfil_musico.id_perfil_musico`, parte da PK)
- `id_instrumento` (bigint FK para `instrumento.id_instrumento`, parte da PK)
- `eh_principal` (boolean)
- *Índice parcial*: Garante apenas 1 instrumento principal por músico (`WHERE eh_principal = true`).

### 6.6 `vaga`
- `id_vaga` (bigint identity PK)
- `id_perfil_contratante` (bigint FK apontando para `perfil_contratante.id_perfil_contratante`)
- `titulo` (varchar(120)), `descricao` (varchar(4000))
- `estilo_predominante` (integer, enum EstiloSamba)
- `formato_solicitado` (integer, enum FormatoVaga)
- `data_evento` (date), `hora_inicio` (time), `hora_fim` (time)
- `logradouro` (varchar(200)), `bairro` (varchar(80)), `cidade` (varchar(100)), `estado` (varchar(2))
- `cache_oferecido` (numeric(10,2), opcional), `cache_a_combinar` (boolean), `possui_equipamento_som` (boolean)
- `status` (integer, enum StatusVaga)
- *Índice composto de busca*: `(status, cidade, data_evento)` para consultas instantâneas no mural de vagas.

### 6.7 `vaga_instrumento` (Associativa com PK Composta)
- `id_vaga` (bigint FK para `vaga.id_vaga`, parte da PK)
- `id_instrumento` (bigint FK para `instrumento.id_instrumento`, parte da PK)

### 6.8 `candidatura`
- `id_candidatura` (bigint identity PK)
- `id_vaga` (bigint FK para `vaga.id_vaga`)
- `id_perfil_musico` (bigint FK para `perfil_musico.id_perfil_musico`)
- `valor_proposta` (numeric(10,2))
- `mensagem` (varchar(500), opcional)
- `status` (integer, enum StatusCandidatura)
- `data_resposta` (timestamptz UTC, opcional)
- *Índice único*: `(id_vaga, id_perfil_musico)` impede candidaturas duplicadas.

### 6.9 `interacao_contato` (Telemetria do WhatsApp)
- `id_interacao_contato` (bigint identity PK)
- `id_candidatura` (bigint FK para `candidatura.id_candidatura`)
- `iniciado_por` (integer, enum OrigemInteracao)
- `data_clique` (timestamptz UTC)

---

## 7. Validação de Documento Fiscal (CPF e CNPJ Alfanumérico)

A validação é provida diretamente através de métodos de extensão no `string` (`DocumentoFiscalExtensions` em `Samba4Hub.Domain.Extensions`), sem overhead de alocação de ValueObjects para strings simples:
- `SanitizarDocumentoFiscal()`: remove pontuações mantendo letras maiúsculas e números.
- `EhCpfValido()`: valida CPFs tradicionais com 11 dígitos numéricos (com verificação de dígitos e bloqueio de sequências repetidas).
- `EhCnpjValido()`: valida CNPJs com 14 caracteres no formato tradicional numérico e no formato **alfanumérico** (vigente desde julho de 2026 pela IN RFB 2.229/2024).
- O algoritmo calcula os dígitos verificadores convertendo letras pelo código ASCII menos 48 (A = 17, B = 18 ... Z = 42) com pesos e módulo 11.
- Exemplos de teste:
  - CPF: `52998224725`
  - CNPJ tradicional: `11222333000181`
  - CNPJ alfanumérico: `12ABC34501DE35`

---

## 8. Ciclo de Migrações e Inicialização em Desenvolvimento

- A extensão `ApplyDatabaseMigrationsAsync` executa `await dbContext.Database.MigrateAsync()` apenas quando o ambiente for `Development` e houver string de conexão válida.
- No .NET Aspire, a API aguarda (`WaitFor`) a saúde do container do PostgreSQL antes de inicializar.
- O arquivo `init_schema.sql` gerado pelo `dotnet ef migrations script` serve exclusivamente como documentação e referência técnica do schema gerado.
