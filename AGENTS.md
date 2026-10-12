# Regras do Projeto Samba4Hub

## Diretriz de Armazenamento de Documentação Técnica (`docs/`)

Toda e qualquer documentação técnica gerada para o projeto deve ser persistida obrigatoriamente no diretório **`docs/`**, localizado na raiz da solução (no mesmo nível dos diretórios `src/`, `test/` e `.specs/`).

### 1. Escopo de Arquivos Obrigatórios em `docs/`
- **Planos de Implementação**: roteiros de tarefas, análises de gaps e planos aprovados.
- **Documentação de Arquitetura & Decisões**: Architecture Decision Records (ADRs), fluxos sistêmicos e contratos.
- **Fontes para Inteligência Artificial (ex: NotebookLM)**: documentos consolidados e autocontidos formatados para alimentar ferramentas externas de base de conhecimento.
- **Dicionários de Dados & Modelagem**: especificações de entidades, tabelas e regras de persistência.

### 2. Estrutura Padrão de `docs/`
```text
Backend-Samba4Hub/
├── docs/
│   ├── plans/           # Planos de implementação e roadmaps
│   ├── architecture/    # Decisões de design, ADRs e diagramas conceituais
│   ├── notebooklm/      # Documentos consolidados para base do NotebookLM
│   └── data-model/      # Dicionários de dados e especificações relacionais
├── src/
├── test/
└── .specs/
```

### 3. Regra de Execução do Agente
- Nunca mantenha planos, análises de arquitetura ou materiais de NotebookLM apenas na memória da sessão ou em diretórios voláteis de artefatos temporários.
- Sempre que um plano ou documento de modelagem for criado ou atualizado, grave ou sincronize uma cópia diretamente dentro de `docs/`.
