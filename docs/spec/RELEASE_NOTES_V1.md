# Release Notes – V1 (Q1 2025)

## Status Geral
✅ **V1 IMPLEMENTADA E VALIDADA** — Projeto em produção local com todas as funcionalidades core operacionais.

---

## 1. Resumo de Mudanças Implementadas

### 1.1 Reorganização de UI: "Perfil e Classificação"

#### Mudança Principal
- **Antes:** Aba `Classificacao de Perfil` com sub-abas para DISC, Personalidade e Nine Box
- **Depois:** Aba renomeada para `Perfil e Classificacao` com **layout de 3 colunas independentes**

#### Estrutura Nova (Componente `ClassificacaoPerfilColumnsSection`)
Cada coluna (DISC, Personalidade, Nine Box) contém:
- Campo de data (editável)
- Campo de valor (editável)
- Tabela de histórico da propriedade (somente leitura)
- Botão "Salvar" individual

#### Comportamento Implementado
1. **Independência completa:** Cada coluna funciona de forma autônoma
   - Clique em "Salvar" persiste **apenas** a coluna correspondente
   - Sem validação cruzada entre colunas
   - Sem sincronização de data entre colunas
2. **Limpeza pós-salvar:** Após clicar em "Salvar":
   - Campos de data e valor são limpos
   - Foco retorna ao campo de data da coluna
   - Histórico é recarregado (nova linha incorporada)
3. **Independência de contexto:** Cada propriedade pode ser preenchida em tempo e contexto diferente

#### Impacto em Banco de Dados
- ❌ **Nenhum impacto:** As tabelas `DISC`, `Personalidade` e `NineBox` já existem independentemente
- As 3 propriedades continuam sendo persistidas separadamente
- Não há tabela agregada `ClassificacoesPerfil`

#### Impacto em API Backend
- ❌ **Nenhum impacto:** Endpoints já existem e funcionam independentemente
  - `GET /api/disc/{idLiderado}` → Lista histórico de DISC
  - `POST /api/disc/{idLiderado}` → Adiciona novo DISC
  - `GET /api/personalidade/{idLiderado}` → Lista histórico de Personalidade
  - `POST /api/personalidade/{idLiderado}` → Adiciona novo Personalidade
  - `GET /api/nine-box/{idLiderado}` → Lista histórico de Nine Box
  - `POST /api/nine-box/{idLiderado}` → Adiciona novo Nine Box

#### Impacto em Frontend
- ✅ **Mudança em componente:** `ClassificacaoPerfilColumnsSection` substitui `PropertyTabsSection` para seção específica
- ✅ **Mudança em estado:** Draft state reorganizado em `classificacaoPerfilDraft` com chaves por propriedade
- ✅ **Mudança em lógica:** Não há lógica de sincronização ou validação de estado global

---

## 2. Funcionalidades Operacionais em V1

### 2.1 Dashboard
✅ Cards de resumo por liderado
✅ Radar Cultural estático (última avaliação)
✅ Indicadores: perfil, nine box, feedbacks, 1:1, nota geral
✅ Navegação por clique em card

### 2.2 Seções por Aba
| Aba | Layout | Funcionalidades | Status |
|-----|--------|-----------------|--------|
| **Informações Pessoais** | 3 colunas fixas | Nome, data nascimento, estado civil, cargo, bio, gostos, redflags | ✅ |
| **Perfil e Classificação** | 3 colunas independentes | DISC, Personalidade, Nine Box com histórico | ✅ |
| **CHAVE** | Sub-abas | Conhecimentos, Habilidades, Atitudes, Valores, Expectativas | ✅ |
| **GROW / PDI** | Sub-abas | Metas, Situação Atual, Opções, Próximos Passos | ✅ |
| **SWOT** | Sub-abas | Fortalezas, Oportunidades, Fraquezas, Ameaças | ✅ |
| **Cultura** | Tabela + Radar | 7 pilares com dropdown de data e Radar animado | ✅ |
| **Feedbacks** | Tabela editável | Data, conteúdo, receptividade, polaridade | ✅ |
| **1:1** | Tabela editável | Data, resumo, tarefas, próximos assuntos | ✅ |

### 2.3 Tooltips
✅ Ícone de informação em labels
✅ Tooltip textual no hover
✅ Duplo clique para editar tooltip por campo
✅ Modal de edição com preload de conteúdo atual

### 2.4 Navegação
✅ Breadcrumb: Dashboard | Combobox de liderados
✅ Abas principais
✅ Carregamento lazy por aba

---

## 3. Fluxos Principais Atualizados

### Fluxo 7: Registrar Classificação de Perfil (NOVO)
1. Usuário acessa aba **"Perfil e Classificação"**
2. Sistema exibe painel com **3 colunas iguais** (DISC, Personalidade, Nine Box)
3. Usuário preenche **uma coluna** com data + valor
4. Usuário clica "Salvar" da coluna
5. Sistema persiste **apenas essa coluna** (independentemente)
6. Campos são limpos e foco retorna à data
7. Histórico é recarregado com novo registro

**Características:**
- Sem obrigatoriedade de preencher todas 3 colunas
- Sem sincronização de data entre colunas
- Cada coluna tem seu próprio botão Salvar
- Nenhuma dependência entre colunas

---

## 4. Especificações Técnicas Finalizadas

### Backend
- ✅ Padrão Vertical Slice (.NET 8.0)
- ✅ 7 Controllers: Dashboard, DISC, Personalidade, NineBox, Liderados, Feedbacks, Tooltips (+ PropHistorica)
- ✅ SQLite local com Entity Framework Core
- ✅ Endpoints RESTful sem autenticação

### Frontend
- ✅ React com Vite
- ✅ Componentes: ClassificacaoPerfilColumnsSection, PropertyTabsSection, RadarChart, MaskedDateInput
- ✅ State management com useState + useEffect
- ✅ CSS customizado (não Bootstrap) com variáveis CSS

### Dados
- ✅ Schema: Liderado, DISC, Personalidade, NineBox, CulturaGenial, Feedback, OneOnOne, PropriedadesHistoricas
- ✅ Sem tabela agregada para "Perfil e Classificação"
- ✅ Chave primária composta em históricos: (IdLiderado, Data)

---

## 5. Decisões Arquiteturais Documentadas

### ADR V1: Layout de 3 Colunas Independentes
**Decisão:** Reorganização de "Classificação de Perfil" em 3 colunas independentes

**Racional:**
- Melhor usabilidade: propriedades frequentemente preenchidas em contextos diferentes
- Independência de dados: reflete a regra de negócio (sem obrigatoriedade de preenchimento conjunto)
- Limpeza visual: elimina hierarquia desnecessária de sub-abas

**Implementação:**
- Frontend: Componente `ClassificacaoPerfilColumnsSection` com 3 colunas renderizadas em grid
- Backend: Endpoints já independentes (`/api/disc/`, `/api/personalidade/`, `/api/nine-box/`)
- Persistência: Tabelas separadas (nenhuma mudança de schema)

**Impacto:**
- Dados: ❌ Nenhum
- API: ❌ Nenhum
- Frontend: ✅ Mudança visual e de componente

---

## 6. Requisitos Funcionais Atualizados

### Novos RFs (V1)
- **RF-26:** Aba `Perfil e Classificacao` exibe painel com 3 colunas iguais (DISC, Personalidade, Nine Box)
- **RF-27:** Cada coluna funciona de forma 100% independente (sem validação cruzada)

### RFs Renomeados
- **RF-28:** `CHAVE`, `GROW / PDI` e `SWOT` exibem abas internas (antes "Classificação de Perfil")
- **RF-29:** Tabelas de histórico em sub-abas (antes "Classificação de Perfil")

### RFs Mantidos (compatibilidade)
- RF-01 a RF-25: Todos mantêm compatibilidade com V1

---

## 7. Checklist de Prontidão

- [x] Escopo e requisitos validados
- [x] Modelagem de domínio revisada
- [x] Diagramas (classes, sequência, atividade, dados) completos
- [x] Modelagem de dados (ER) validada
- [x] Contratos de API definidos
- [x] Aspectos transversais revisados
- [x] Cenários de teste definidos
- [x] Riscos técnicos identificados e mitigados
- [x] Documentação de PRD atualizada
- [x] Documentação de TSD atualizada
- [x] Release Notes preparadas

---

## 8. Observações de Compatibilidade

### Retrocompatibilidade
✅ **Total:** Dados existentes continuam funcionando sem migração

### Migração de Data
❌ **Não necessária:** Schema mantém-se idêntico

### Rollback
✅ **Possível:** Reversão para sub-abas mantém dados intactos

---

## 9. Próximas Versões (Roadmap)

### V1.1 (Melhorias QoL)
- [ ] Paginação de históricos longos
- [ ] Filtro por data em tabelas
- [ ] Exportação de dados por liderado (CSV/JSON)

### V2 (Evolução Arquitetural)
- [ ] Suporte multiusuário (autenticação)
- [ ] Backup automático
- [ ] Relatórios customizáveis

### V3 (Integração)
- [ ] API externa para sistemas RH
- [ ] Sincronização em nuvem (opcional)
- [ ] Aplicativo mobile

---

## 10. Suporte e Documentação

- ✅ PRD atualizado com RF-26 e RF-27
- ✅ TSD atualizado com ADR V1 e endpoints
- ✅ Componente `ClassificacaoPerfilColumnsSection` documentado em código
- ✅ Fluxo de usuário atualizado (Fluxo 7: Registrar Classificação de Perfil)

---

**Versão:** 1.0.0  
**Data de Lançamento:** Q1 2025  
**Status:** ✅ Production Ready  
**Suporte:** Versão anterior pode ser restaurada se necessário (rollback sem perda de dados)

