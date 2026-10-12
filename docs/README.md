# Documentação Técnica do Samba4Hub

Diretório central de documentação, decisões de arquitetura e planos de implementação do projeto.

---

## Estrutura de Pastas

- [plans/](file:///c:/Dev/Projeto%20Samba4Hub/Backend-Samba4Hub/docs/plans): Planos de implementação detalhados e roadmaps técnicos.
  - [plano-implementacao-mvp-fase1.md](file:///c:/Dev/Projeto%20Samba4Hub/Backend-Samba4Hub/docs/plans/plano-implementacao-mvp-fase1.md): Plano de execução da modelagem de dados da Fase 1 (MVP) com EF Core 10, PostgreSQL e Aspire.
- [notebooklm/](file:///c:/Dev/Projeto%20Samba4Hub/Backend-Samba4Hub/docs/notebooklm): Documentos de referência consolidados e autocontidos formatados para ingestão em ferramentas de base de conhecimento como o Google NotebookLM.
  - [samba4hub_modelagem_notebooklm.md](file:///c:/Dev/Projeto%20Samba4Hub/Backend-Samba4Hub/docs/notebooklm/samba4hub_modelagem_notebooklm.md): Dicionário de dados, glossário, regras de negócio e FAQ para NotebookLM.
- [architecture/](file:///c:/Dev/Projeto%20Samba4Hub/Backend-Samba4Hub/docs/architecture): Registros de decisões arquiteturais (ADRs), fluxos sistêmicos e diagramas C4.
- [data-model/](file:///c:/Dev/Projeto%20Samba4Hub/Backend-Samba4Hub/docs/data-model): Diagramas relacionais (ER) e especificações técnicas de persistência.

---

## Regra de Governança
Conforme definido em [AGENTS.md](file:///c:/Dev/Projeto%20Samba4Hub/Backend-Samba4Hub/AGENTS.md) e [.agents/rules/documentation-storage.md](file:///c:/Dev/Projeto%20Samba4Hub/Backend-Samba4Hub/.agents/rules/documentation-storage.md), todo novo plano, decisão de design ou modelo gerado pelo assistente ou pela equipe deve ser versionado aqui dentro de `docs/`.
