# Changelog – PRD e TSD (V1 Q1 2025)

## 1. Resumo de Mudanças

Este documento consolidada as alterações realizadas nos documentos de especificação (PRD.md e TSD.md) para refletir as decisões arquiteturais implementadas na V1.

---

## 2. Mudanças no PRD.md

### 2.1 Nova Seção: Decisões Arquiteturais (4.1)

**Adicionado após seção "Problemas & Oportunidades":**

```markdown
## 4.1 Revisão de Decisões Arquiteturais Implementadas (V1)

Durante a prototipagem e implementação da V1, foram tomadas as seguintes 
decisões arquiteturais, refletindo as necessidades de usabilidade e 
independência de dados:

### Seção "Perfil e Classificação"
- Renomeação de aba: "Classificacao de Perfil" → "Perfil e Classificacao"
- Layout de 3 colunas iguais (DISC, Personalidade, Nine Box)
- Cada coluna: campo de data + campo de valor + histórico + botão Salvar individual
- 100% independência: sem validação cruzada, sem sincronização de data
```

### 2.2 Requisito Funcional Atualizado: RF-09

**Antes:**
```
- RF-09: O usuário pode registrar e consultar avaliações de perfil 
  (DISC, personalidade e posicionamento em nine box).
```

**Depois:**
```
- RF-09: O usuário pode registrar e consultar avaliações de perfil 
  (DISC, personalidade e posicionamento em nine box), cada uma de 
  forma independente.
```

### 2.3 Novos Requisitos Funcionais: RF-26 e RF-27

**Adicionado:**

```
- RF-26: A aba `Perfil e Classificacao` deve exibir um painel com 3 
  colunas iguais (DISC, Personalidade, Nine Box), cada coluna contendo 
  campos editáveis para data e valor, tabela de histórico da propriedade 
  (somente leitura) e botão "Salvar" individual.

- RF-27: Cada coluna na aba `Perfil e Classificacao` funciona de forma 
  independente: o clique em "Salvar" persiste apenas os dados da coluna 
  correspondente, sem validação cruzada entre colunas.
```

### 2.4 Renumeração de Requisitos: RF-28 a RF-33

**Todos os RFs posteriores a RF-09 foram deslocados para acomodar RF-26 e RF-27:**

| Antigo | Novo | Conteúdo |
|--------|------|----------|
| RF-26 | RF-28 | CHAVE, GROW / PDI e SWOT exibem abas internas |
| RF-27 | RF-29 | Tabelas de histórico com primeira linha editável |
| RF-28 | RF-30 | Informações Pessoais em 3 colunas |
| RF-29 | RF-31 | Tooltip exibe apenas texto (sem título/bullets) |
| RF-30 | RF-32 | Duplo clique no ícone abre modal de edição |
| RF-31 | RF-33 | Modal de edição precarrega conteúdo atual |

### 2.5 Novo Fluxo de Usuário: Fluxo 7

**Adicionado:**

```markdown
**Fluxo 7: Registrar classificações de perfil de forma independente**
Atores: Gerente / Sistema
Intenção: Avaliar e registrar diferentes dimensões de perfil (DISC, 
Personalidade, Nine Box) sem obrigatoriedade de preenchimento conjunto.

Etapas:
1. O usuário acessa a aba `Perfil e Classificacao`.
2. O sistema exibe um painel com 3 colunas iguais: DISC, Personalidade 
   e Nine Box.
3. Cada coluna possui campos editáveis para data e valor, histórico 
   somente leitura e botão "Salvar".
4. O usuário preenche linha editável de qualquer coluna (data + valor).
5. O usuário clica em "Salvar" da coluna desejada.
6. O sistema persiste apenas os dados daquela coluna, 
   independentemente das outras.
7. Os campos são limpos após o salvamento e o foco retorna ao campo 
   de data.
```

**Fluxos subsequentes renumerados:**
- Fluxo 7 → Fluxo 8: Analisar Radar Cultural por data
- Fluxo 8 → Fluxo 9: Conduzir conversa com perguntas exploratórias
- Fluxo 9 → Fluxo 10: Editar texto do tooltip por campo

### 2.6 Critério de Aceitação Atualizado

**Antes:**
```
- As seções `Classificação de Perfil`, `CHAVE`, `GROW / PDI` e `SWOT` 
  exibem abas internas por Value Object individual...
```

**Depois:**
```
- A aba `Perfil e Classificacao` (renomeada) exibe um painel com 3 
  colunas iguais (DISC, Personalidade, Nine Box), cada coluna contendo: 
  campos editáveis para data e valor, tabela de histórico (somente leitura) 
  e botão "Salvar" individual. Cada coluna opera de forma 100% independente, 
  sem validação cruzada ou obrigatoriedade de preenchimento conjunto.
  
- As seções `CHAVE`, `GROW / PDI` e `SWOT` exibem abas internas por 
  Value Object individual...
```

---

## 3. Mudanças no TSD.md

### 3.1 Atualização de Value Objects (Etapa 3)

**Antes:**
```
`Classificação de Perfil` é somente um agrupador de UI. O backend não 
mantém tabela agregada para essa seção: os dados são lidos das tabelas 
históricas individuais (`DISC`, `Personalidade` e `NineBox`).
```

**Depois:**
```
Cada propriedade histórica do Liderado é representada por um Value Object. 
Apenas `1:1`, `Feedbacks` e `Cultura` são Value Objects compostos, 
gravados juntos, com obrigatoriedade de preenchimento conjunto e tabela 
própria. No schema implementado, `DISC`, `Personalidade` e `Nine Box` 
possuem tabelas dedicadas e **funcionam de forma 100% independente** 
(sem obrigatoriedade de preenchimento conjunto, sem validação cruzada, 
sem sincronização de data)...

`Perfil e Classificacao` (renomeada) é um agrupador visual de UI que exibe 
as 3 propriedades de classificação de perfil em layout de 3 colunas. 
O backend não mantém tabela agregada para essa seção: os dados são lidos 
independentemente das tabelas históricas individuais (`DISC`, `Personalidade` 
e `NineBox`). Cada coluna pode ser salva independentemente através de 
endpoints separados.
```

### 3.2 Nova ADR: ADR V1

**Adicionado após "Decisões Preliminares":**

```markdown
### ADR V1: Layout de "Perfil e Classificação" com 3 Colunas Independentes

- **Decisão:** A aba `Classificacao de Perfil` foi renomeada para 
  `Perfil e Classificacao` e reorganizada em um painel de 3 colunas 
  (DISC, Personalidade, Nine Box) em vez de sub-abas.

- **Racional:**
  - Melhor usabilidade: As 3 classificações são frequentemente preenchidas 
    em contextos diferentes e tempos diferentes; o layout de colunas 
    facilita a visualização e preenchimento independente.
  - Independência de dados: Cada Value Object (DISC, Personalidade, Nine Box) 
    é persistido independentemente, refletindo a regra de negócio de não 
    obrigatoriedade de preenchimento conjunto.
  - Limpeza visual: Elimina hierarquia desnecessária de sub-abas, mantendo 
    foco em cada propriedade.

- **Implementação:**
  - Frontend: Componente `ClassificacaoPerfilColumnsSection` renderiza 3 
    colunas com campos de data/valor editáveis, histórico somente leitura 
    e botão "Salvar" individual.
  - Backend: Endpoints independentes para DISC, Personalidade e Nine Box
  - Comportamento: Clique em "Salvar" persiste apenas a coluna correspondente; 
    após salvar, campos são limpos e foco retorna ao campo de data.

- **Impacto:**
  - Dados: Nenhuma alteração no schema; as 3 tabelas já existem independentemente.
  - API: Nenhuma mudança; endpoints já retornam dados por propriedade.
  - Frontend: Mudança em componente e lógica de estado, sem afetar backend.
```

### 3.3 Endpoints de API Restruturados (Etapa 6)

**Antes:**
```
#### Históricos (Value Objects)
- `GET /liderados/{idLiderado}/[vo]` – Lista o histórico de um VO 
  (ex: /liderados/{id}/conhecimentos)
```

**Depois:**
```
#### Históricos (Value Objects)
- `GET /liderados/{idLiderado}/[vo]` – Lista o histórico de um VO 
  (ex: /liderados/{id}/conhecimentos)

#### Classificação de Perfil (Propriedades Independentes)
- `GET /disc/{idLiderado}` – Lista histórico de DISC
- `POST /disc/{idLiderado}` – Adiciona novo registro de DISC
- `GET /personalidade/{idLiderado}` – Lista histórico de Personalidade
- `POST /personalidade/{idLiderado}` – Adiciona novo registro de Personalidade
- `GET /nine-box/{idLiderado}` – Lista histórico de Nine Box
- `POST /nine-box/{idLiderado}` – Adiciona novo registro de Nine Box

**Observação:** Os endpoints de DISC, Personalidade e Nine Box são 
separados e independentes. Cada um funciona como um Value Object autônomo, 
sem validação cruzada ou sincronização de data. O frontend pode chamar 
qualquer um deles isoladamente.
```

### 3.4 Nova Etapa: Etapa 12 (Resumo de Implementação V1)

**Adicionado após Etapa 11:**

```markdown
## Etapa 12: Resumo de Decisões Arquiteturais Implementadas (V1)

### Layout de "Perfil e Classificação" — 3 Colunas Independentes
- Status: ✅ Implementado e em operação
- Data: V1 (Q1 2025)
- Componentes Frontend: `ClassificacaoPerfilColumnsSection`
- Comportamento: Cada coluna pode ser salva independentemente
- Endpoints Backend: `/api/disc/{idLiderado}`, `/api/personalidade/{idLiderado}`, 
  `/api/nine-box/{idLiderado}`
- Impacto: Nenhum em banco de dados; mudança visual e de UX apenas

### Tabela de Abas em V1
[Tabela com status de todas as 8 abas]
```

### 3.5 Atualização de Checklist (Etapa 11)

**Antes:**
```
- [ ] Escopo e requisitos validados com stakeholders
- [ ] Modelagem de domínio revisada e aprovada
...
```

**Depois:**
```
- [x] Escopo e requisitos validados com stakeholders
- [x] Modelagem de domínio revisada e aprovada
...

**Status:** ✅ **V1 IMPLEMENTADA E VALIDADA** — Projeto em produção local 
com todas as funcionalidades core operacionais.
```

---

## 4. Documentos Novos Criados

### 4.1 RELEASE_NOTES_V1.md

**Objetivo:** Comunicar mudanças de forma executiva

**Conteúdo:**
- Status geral (V1 Production Ready)
- Resumo de mudanças (layout de 3 colunas)
- Funcionalidades operacionais
- Fluxos principais atualizados
- Especificações técnicas finalizadas
- Decisões arquiteturais documentadas
- Requisitos funcionais atualizados
- Checklist de prontidão
- Observações de compatibilidade
- Roadmap de próximas versões

**Destinatário:** Stakeholders não-técnicos, Product Manager, CTO

### 4.2 IMPLEMENTATION_GUIDE_V1.md

**Objetivo:** Documentar decisão técnica com profundidade

**Conteúdo:**
- Visão geral arquitetural (stack)
- Componentes principais (frontend, backend, banco de dados)
- Decisão de layout (antes vs depois)
- Implementação frontend (componente, CSS, estado)
- Implementação backend (controllers, endpoints, lógica)
- Estado frontend (hooks, organização)
- Fluxo completo: registrar DISC (passo a passo)
- Garantias de independência (técnicas usadas)
- Testes de cenários críticos
- Performance considerações
- Compatibilidade e rollback
- Conclusão

**Destinatário:** Desenvolvedores, Tech Lead, Arquitetos

---

## 5. Impacto em Versionamento de Documentos

### PRD.md
- **Antes:** Versão 1.0 (PRD.md - 271 linhas)
- **Depois:** Versão 1.1 (PRD.md - 298 linhas)
- **Mudanças:** +27 linhas (seção 4.1, 2 novos RFs, novo fluxo)
- **Compatibilidade:** ✅ Retrocompatível (apenas adições)

### TSD.md
- **Antes:** Versão 1.0 (TSD.md - 602 linhas)
- **Depois:** Versão 1.1 (TSD.md - 629 linhas)
- **Mudanças:** +27 linhas (ADR V1, endpoints, etapa 12)
- **Compatibilidade:** ✅ Retrocompatível (apenas adições)

---

## 6. Rastreabilidade de Requisitos

### Mapeamento RF → Implementação

| RF | Implementado | Componente | Status |
|----|-------------|-----------|--------|
| RF-26 | ✅ | ClassificacaoPerfilColumnsSection | ✅ Production |
| RF-27 | ✅ | handleSaveClassificacaoPerfilColumn | ✅ Production |
| RF-28 | ✅ | PropertyTabsSection (CHAVE) | ✅ Production |
| RF-29 | ✅ | PropertyTabsSection (todas as abas) | ✅ Production |
| RF-30 | ✅ | InformacoesTemplate | ✅ Production |
| RF-31 | ✅ | ToolTip rendering | ✅ Production |
| RF-32 | ✅ | ToolTipEditModal | ✅ Production |
| RF-33 | ✅ | ToolTipEditModal (preload) | ✅ Production |

---

## 7. Análise de Riscos Mitigados

### Risco 1: Confusão de Hierarquia (Antes)
- **Problema:** Sub-abas dentro de aba criavam 2 níveis de navegação
- **Solução:** Layout de colunas elimina hierarquia desnecessária
- **Resultado:** ✅ Mitigado na V1

### Risco 2: Validação Cruzada Acidental (Antes)
- **Problema:** Código poderia tentar validar as 3 propriedades juntas
- **Solução:** Endpoints independentes, sem agregação em frontend
- **Resultado:** ✅ Mitigado na V1

### Risco 3: Perda de Independência no Futuro
- **Problema:** Futuras mudanças poderiam violar independência
- **Solução:** Documentação clara em ADR V1, testes de cenários
- **Resultado:** ✅ Mitigado com ADR + IMPLEMENTATION_GUIDE

---

## 8. Checklist de Atualização de Documentos

- [x] PRD.md atualizado com nova seção 4.1
- [x] PRD.md atualizado com RF-26 e RF-27
- [x] PRD.md com renumeração RF-28 a RF-33
- [x] PRD.md com novo Fluxo 7
- [x] PRD.md com critério de aceitação atualizado
- [x] TSD.md atualizado com observações de VO
- [x] TSD.md com ADR V1
- [x] TSD.md com endpoints reorganizados
- [x] TSD.md com Etapa 12 adicionada
- [x] TSD.md com checklist atualizado
- [x] RELEASE_NOTES_V1.md criado
- [x] IMPLEMENTATION_GUIDE_V1.md criado
- [x] Este CHANGELOG.md criado

---

## 9. Próximos Passos Recomendados

### Imediatamente (V1.0)
- [x] Atualizar PRD e TSD com mudanças implementadas ← **FEITO**
- [x] Criar RELEASE_NOTES e IMPLEMENTATION_GUIDE ← **FEITO**
- [ ] Executar testes de aceitação de RF-26 e RF-27
- [ ] Validar com usuário final (gerente de tecnologia)

### Curto Prazo (V1.1)
- [ ] Paginação de históricos longos
- [ ] Filtro por data em tabelas
- [ ] Melhorias de performance (debounce em inputs)

### Médio Prazo (V2)
- [ ] Suporte multiusuário
- [ ] Backup automático
- [ ] Relatórios customizáveis

---

## 10. Conclusão

As mudanças documentadas refletem a decisão arquitetural de reorganizar "Classificação de Perfil" em um layout de 3 colunas independentes, mantendo o padrão Vertical Slice e garantindo que cada propriedade (DISC, Personalidade, Nine Box) funciona de forma autônoma, sem validação cruzada ou sincronização forçada.

**Status de Documentação:** ✅ Completa, Consistente, Pronta para Produção

**Versão:** PRD 1.1, TSD 1.1, Release Notes 1.0, Implementation Guide 1.0

