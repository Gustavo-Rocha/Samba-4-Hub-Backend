---
trigger: always_on
description: Garante que toda documentação de arquitetura, decisões, planos e fontes para NotebookLM sejam armazenadas na pasta docs/ na raiz do repositório (mesmo nível de src/).
---

# Regra de Persistência de Documentação em `docs/`

Qualquer documentação técnica gerada (arquitetura, decisões de design, modelagem relacional, planos de implementação e arquivos para o NotebookLM) DEVE ser salva na pasta **`docs/`** na raiz do projeto (no mesmo nível de `src/`, `test/` e `.specs/`).

## Organização:
- `docs/plans/`: Planos de implementação detalhados.
- `docs/architecture/`: Desenho de soluções, ADRs e diagramas.
- `docs/notebooklm/`: Materiais de texto consolidados e autocontidos para alimentação de bases de conhecimento (ex: NotebookLM).
- `docs/data-model/`: Modelagem de dados, diagramas ER e dicionários.

O agente nunca deve deixar documentações relevantes apenas em artefatos efêmeros ou pastas temporárias sem persistir na pasta `docs/`.
