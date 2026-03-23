# PRD – Ferramenta de Gestão de Especialistas e Coordenadores

## 1. Visão Geral
Você, como gerente de tecnologia, faz a gestão funcional e o acompanhamento de desenvolvimento (PDI) de especialistas e coordenadores. Hoje, utiliza planilhas individuais para cada liderado, o que gera retrabalho ao atualizar ou incluir novos campos. Seu objetivo é criar uma ferramenta web, de uso individual e local, que centralize o histórico e dados estruturados de cada pessoa, facilitando a visualização, inclusão de novas pessoas e o acompanhamento contínuo, eliminando o esforço manual de manutenção de múltiplas planilhas. Não há necessidade de autenticação, múltiplos usuários ou controle de acesso neste momento. A navegação será focada em visualizar e gerenciar uma pessoa por vez, com filtro simples e funcionalidade para adicionar novos liderados.

## 2. Escopo (In / Out)

**Escopo In (Incluído):**
- Cadastro de novos liderados (especialistas e coordenadores)
- Visualização do histórico individual de cada pessoa
- Registro e acompanhamento de PDI (Plano de Desenvolvimento Individual)
- Organização de dados estruturados (feedbacks, metas, competências, transcrições de 1:1)
- Filtro simples para selecionar e visualizar uma pessoa por vez
- Edição e atualização dos dados de cada liderado
- Dashboard simples como página inicial, com visão consolidada de todos os liderados
- Dashboard inicial com um card de resumo por liderado, exibido em lista horizontal com rolagem quando necessário
- Cada card de resumo do dashboard exibe nome do liderado, indicadores principais e um Radar Cultural estático baseado na última avaliação disponível
- Clique em um card do dashboard abre a visão individual do liderado correspondente
- Visão do liderado organizada por abas de seção (melhor uso de espaço e foco)
- Na visão individual, o painel de resumo lateral exibe o nome do liderado como cabeçalho
- Navegação estilo "caminho" (breadcrumb) no topo da área de conteúdo: `Dashboard | [combobox de liderados]`; o combobox permite trocar de liderado sem retornar ao dashboard
- Seções `1:1`, `Feedbacks` e `Cultura` são Value Objects compostos, gravados juntos, cada um com sua própria tabela e regras de obrigatoriedade de preenchimento dos campos principais.
- Seções `Classificação de Perfil`, `CHAVE`, `GROW / PDI` e `SWOT` possuem histórico por Value Object individual. No schema implementado, `DISC`, `Personalidade` e `Nine Box` possuem tabelas próprias; já os itens de `CHAVE`, `GROW / PDI` e `SWOT` são persistidos na tabela genérica `PropriedadesHistoricas`, identificados por `Tipo`. Não há obrigatoriedade de preenchimento conjunto para essas propriedades.
- `Classificação de Perfil` é apenas um agrupador visual no frontend; não existe tabela agregada `ClassificacoesPerfil` no banco.
- Para tabelas de histórico por propriedade, a primeira linha é editável para novo registro e as linhas históricas são somente leitura
- Seção `Informacoes Pessoais` com layout dedicado em 3 colunas e distribuição fixa de campos
- Tooltip informativo sem título fixo e sem bullets, com quebra de linha simples
- Edição de tooltip por duplo clique no ícone de informação, com modal de edição e carregamento prévio do texto atual
- Radar Cultural baseado em avaliações de Cultura, com seleção de data, navegação por scroll do mouse e transição animada

**Escopo Out (Fora):**
- Acesso multiusuário, autenticação ou controle de permissões
- Funcionalidades de notificação por e-mail ou alertas automáticos
- Relatórios avançados ou dashboards customizáveis
- Integração com sistemas externos (ex: RH, ERP)
- Acesso dos próprios liderados à ferramenta
- Funcionalidades mobile ou aplicativo dedicado
- Workflow de aprovação ou comentários colaborativos

## 3. Personas

**Nome:** Gerente de Tecnologia
**Motivação:** Facilitar o acompanhamento do desenvolvimento e histórico dos liderados, centralizando informações e reduzindo retrabalho manual.
**Dor:** Atualização trabalhosa de múltiplas planilhas, risco de perda de informações, dificuldade em consolidar dados para tomada de decisão.
**Sucesso:** Visualização rápida do histórico e evolução de cada liderado, facilidade para registrar e acompanhar PDIs, dashboard consolidado para visão geral.

## 4. Problemas & Oportunidades

- **Problema:** Atualização manual e trabalhosa de múltiplas planilhas individuais para cada liderado, dificultando a padronização e aumentando o risco de inconsistências e perda de informações.
- **Oportunidade:** Centralizar todo o histórico e dados estruturados em uma ferramenta web, facilitando o acompanhamento, a evolução dos liderados e a tomada de decisão baseada em dados consolidados.

## 4.1 Revisão de Decisões Arquiteturais Implementadas (V1)

Durante a prototipagem e implementação da V1, foram tomadas as seguintes decisões arquiteturais, refletindo as necessidades de usabilidade e independência de dados:

### Seção "Perfil e Classificação"
- **Renomeação de aba:** A aba anterior "Classificacao de Perfil" foi renomeada para **"Perfil e Classificacao"**, alinhada ao contexto de classificações de perfil comportamentais.
- **Layout de 3 colunas:** Em vez de sub-abas, a seção agora exibe um painel com 3 colunas iguais, cada coluna representando um Value Object individual:
  - Coluna 1: **DISC**
  - Coluna 2: **Personalidade**
  - Coluna 3: **Nine Box**
- **Estrutura de cada coluna:**
  - Campo de data (editável) + campo de valor (editável)
  - Tabela de histórico da propriedade, mostrando registros anteriores (somente leitura)
  - Botão "Salvar" individual para a coluna
- **Comportamento de salvamento:** Cada coluna opera de forma **100% independente**:
  - O clique em "Salvar" persiste apenas os dados da coluna correspondente
  - A data é individual por coluna (não compartilhada)
  - Após salvar, os campos de edição são limpos e foco retorna ao campo de data
  - Nenhuma dependência ou validação cruzada entre colunas

### Independência de Value Objects
- **Regra de negócio reforçada:** Cada Value Object (DISC, Personalidade, Nine Box) é persistido independentemente no banco de dados, sem obrigatoriedade de preenchimento conjunto.
- **Sem agregação em UI:** O frontend não força preencher todas as 3 colunas simultaneamente; cada uma pode ser registrada em tempo e contexto diferente.
- **Histórico isolado:** O histórico de cada coluna é carregado e exibido isoladamente, facilitando rastreamento evolutivo por classificação.

## 5. Requisitos Funcionais
- RF-01: O usuário pode cadastrar um novo liderado informando somente o nome.
- RF-02: O usuário pode visualizar um dashboard inicial com visão consolidada de todos os liderados por meio de cards de resumo, um para cada liderado.
- RF-03: Ao clicar em um card do dashboard, o usuário pode abrir a visão individual e editar o histórico do liderado selecionado.
- RF-04: A tela individual do liderado deve organizar as seções em abas navegáveis.
- RF-05: O usuário pode registrar e atualizar informações pessoais do liderado (ex.: gostos pessoais, bio, red flags, estado civil, filhos, cargo e aspiração de carreira).
- RF-06: O usuário pode registrar e consultar avaliações no modelo C.H.A.V.E (conhecimentos, habilidades, atitudes, valores e expectativas).
- RF-07: O usuário pode registrar e acompanhar informações de desenvolvimento no formato GROW/PDI (metas, situação atual, opções e próximos passos).
- RF-08: O usuário pode registrar análises SWOT do liderado.
- RF-09: O usuário pode registrar e consultar avaliações de perfil (DISC, personalidade e posicionamento em nine box), cada uma de forma independente.
- RF-10: O usuário pode registrar fatos e observações relevantes com data.
- RF-11: A seção `Cultura` deve substituir `Avaliações Gerais` e registrar avaliações 360 com os pilares: Aprender e Melhorar Sempre; Atitude de Dono; Buscar os melhores resultados para os clientes; Espírito de Equipe; Excelência; Fazer Acontecer; Inovar para Inspirar.
- RF-12: Na seção `Cultura`, os pilares devem ser registrados como valores numéricos por data de avaliação.
- RF-13: O usuário pode registrar feedbacks com data, conteúdo, receptividade e polaridade, em formato de histórico tabular com linha editável para novo registro.
- RF-14: O usuário pode registrar acompanhamentos de 1:1 em formato de histórico tabular com: data, resumo, tarefas acordadas e próximos assuntos para o encontro seguinte, com linha editável para novo registro.
- RF-15: Cada card de resumo, tanto no dashboard quanto na visão individual, deve exibir os indicadores principais do liderado (perfil, nine box, quantidade de feedbacks, quantidade de 1:1 e nota geral).
- RF-16: O sistema exibe um Radar Cultural (teia de aranha) com as sete dimensões culturais; no dashboard, cada card mostra uma versão estática baseada na última avaliação do liderado.
- RF-17: O Radar Cultural da visão individual deve permitir escolher a data da avaliação via dropdown com todas as datas cadastradas.
- RF-18: O Radar Cultural da visão individual deve permitir alterar a data selecionada via scroll do mouse no seletor de data.
- RF-19: Ao alterar a data da avaliação cultural na visão individual, o Radar Cultural deve atualizar os dados com animação de transição.
- RF-20: Em qualquer tela que exiba uma informação tratada pelo sistema, o sistema deve exibir um ícone de informação ao lado do respectivo label.
- RF-21: Ao passar o mouse sobre o ícone de informação, o sistema deve exibir um tooltip textual configurado para o tipo de informação, sem título fixo e sem bullets.
- RF-22: Ao dar duplo clique no ícone de informação, o sistema deve abrir um modal para incluir/editar o texto do tooltip daquele campo, já preenchido com o conteúdo atual.
- RF-23: O sistema deve manter um histórico completo de todas as alterações realizadas em qualquer informação do liderado, incluindo data/hora da alteração, valor anterior, novo valor e usuário responsável. O histórico deve ser consultável pelo usuário em cada seção, permitindo acompanhar a evolução das informações ao longo do tempo.
- RF-24: A visão individual do liderado deve exibir, no topo da área de conteúdo, uma barra de navegação estilo breadcrumb com: um link acionável "Dashboard" (retorna ao dashboard) e um combobox com todos os liderados cadastrados (permite alternar de liderado sem sair da visão individual).
- RF-25: O painel lateral de resumo da visão individual deve exibir o nome do liderado como título do painel.
- RF-26: A aba `Perfil e Classificacao` deve exibir um painel com 3 colunas iguais (DISC, Personalidade, Nine Box), cada coluna contendo campos editáveis para data e valor, tabela de histórico da propriedade (somente leitura) e botão "Salvar" individual.
- RF-27: Cada coluna na aba `Perfil e Classificacao` funciona de forma independente: o clique em "Salvar" persiste apenas os dados da coluna correspondente, sem validação cruzada entre colunas.
- RF-28: As seções `CHAVE`, `GROW / PDI` e `SWOT` devem exibir as propriedades em abas internas, cada aba contendo uma tabela de histórico no formato `Data | <Propriedade>`.
- RF-29: Nas tabelas de histórico por propriedade (`CHAVE`, `GROW / PDI`, `SWOT`), a primeira linha deve ser editável para novo registro e as linhas subsequentes devem ser somente leitura.
- RF-30: A seção `Informacoes Pessoais` deve ser exibida em três colunas com a seguinte distribuição: coluna 1 (`Nome`, `Data de nascimento`, `Estado civil`, `Quantidade de filhos`, `Data de contratacao`, `Cargo`, `Data de inicio do cargo`, `Aspiracao (Carreira Y)`), coluna 2 (`Gostos pessoais`, `Red Flags`) e coluna 3 (`BIO`).
- RF-31: Ao passar o mouse sobre o ícone de informação, o tooltip deve exibir apenas o texto configurado para o tipo de informação, sem título fixo e sem formatação em bullets.
- RF-32: Ao dar duplo clique no ícone de informação, o sistema deve abrir um modal para incluir/editar o texto do tooltip daquele campo.
- RF-33: O modal de edição de tooltip deve carregar previamente o mesmo conteúdo textual atualmente exibido no tooltip do campo selecionado.

## 6. Requisitos Não Funcionais + Ranking de Qualidades
**Requisitos Não Funcionais**
- RNF-01: A navegação entre dashboard, visão individual do liderado e formulários principais deve ocorrer de forma fluida, com carregamento percebido como imediato ou quase imediato para uso cotidiano.
- RNF-02: Toda informação registrada ou alterada deve permanecer salva localmente de forma consistente, evitando perda de histórico após fechamento ou reabertura da ferramenta.
- RNF-03: A interface deve ser simples o suficiente para permitir uso recorrente sem necessidade de treinamento formal, priorizando clareza dos campos, organização por contexto e baixo esforço para registro e consulta.
- RNF-04: A ferramenta deve funcionar integralmente em ambiente local, sem depender de conexão com serviços externos para uso normal.
- RNF-05: O sistema deve preservar a estrutura dos dados por liderado e por tipo de informação, evitando mistura de registros entre pessoas ou categorias.
- RNF-06: A solução deve permitir evolução futura dos tipos de informação e das perguntas exploratórias sem exigir retrabalho elevado em toda a base já cadastrada.
- RNF-07: Os dados devem permanecer restritos ao ambiente local de uso, sem exposição automática para terceiros ou envio externo não intencional.

**Ranking de Qualidades**
1. **Confiabilidade** — O valor principal da ferramenta está no histórico acumulado; se houver risco de perda, inconsistência ou corrupção dos dados, a ferramenta perde seu propósito central.
2. **Usabilidade** — A ferramenta será usada com frequência no dia a dia de gestão; se for difícil de usar, tende a virar mais uma camada de atrito em vez de substituir bem a planilha.
3. **Desempenho** — Consultas rápidas e registro fluido são importantes durante preparação e condução de 1:1, além de favorecer adoção contínua.

## 7. Fluxos de Usuário (User Journeys)

**Fluxo 1: Acessar dashboard consolidado**  
Atores: Gerente / Sistema  
Intenção: Obter visão geral dos liderados e decidir quem acompanhar.  
Etapas:
- O usuário abre a ferramenta.
- O sistema exibe o dashboard inicial com uma sequência horizontal de cards de resumo, um por liderado.
- Cada card apresenta nome do liderado, indicadores principais e um Radar Cultural estático com base na última avaliação disponível.
- O usuário percorre os cards horizontalmente quando necessário e identifica rapidamente quem deseja consultar.
- Ao clicar em um card, o sistema abre a visão individual do liderado correspondente.

**Fluxo 2: Cadastrar novo liderado**  
Atores: Gerente / Sistema  
Intenção: Iniciar acompanhamento de uma nova pessoa.  
Etapas:
- O usuário acessa a opção de adicionar novo liderado.
- O sistema solicita apenas o nome.
- O usuário informa o nome e confirma.
- O sistema cria o registro e o exibe no dashboard.

**Fluxo 3: Navegar na visão individual por abas**  
Atores: Gerente / Sistema  
Intenção: Consultar e editar informações sem sobrecarga visual.  
Etapas:
- O usuário seleciona um liderado no dashboard por meio de um card de resumo.
- O sistema abre a visão individual exibindo o nome do liderado no painel lateral e um breadcrumb no topo: `Dashboard | [nome do liderado selecionado no combobox]`.
- O usuário troca entre abas para consultar/editar os dados de cada contexto.
- O usuário pode retornar ao dashboard clicando em "Dashboard" no breadcrumb, ou alternar para outro liderado diretamente pelo combobox do breadcrumb.

**Fluxo 4: Registrar 1:1 em histórico tabular**  
Atores: Gerente / Sistema  
Intenção: Registrar evolução contínua de conversas 1:1.  
Etapas:
- O usuário acessa a aba `1:1`.
- O sistema mostra histórico em tabela e uma linha editável para novo registro.
- O usuário preenche data, resumo, tarefas acordadas e próximos assuntos.
- O sistema salva o novo registro no histórico da seção.

**Fluxo 5: Registrar feedback em histórico tabular**  
Atores: Gerente / Sistema  
Intenção: Consolidar feedbacks formais com contexto temporal.  
Etapas:
- O usuário acessa a aba `Feedbacks`.
- O sistema mostra histórico em tabela e uma linha editável para novo registro.
- O usuário preenche data, conteúdo, receptividade e polaridade.
- O campo polaridade é um combobox com as opções `Positivo` e `Negativo`.
- O sistema salva o novo registro no histórico da seção.

**Fluxo 6: Registrar avaliação de Cultura**  
Atores: Gerente / Sistema  
Intenção: Avaliar aderência cultural em pilares objetivos.  
Etapas:
- O usuário acessa a aba `Cultura`.
- O sistema mostra tabela com os sete pilares culturais como colunas numéricas.
- O usuário preenche uma nova avaliação por data.
- O sistema salva a avaliação no histórico da seção.

**Fluxo 7: Registrar classificações de perfil de forma independente**  
Atores: Gerente / Sistema  
Intenção: Avaliar e registrar diferentes dimensões de perfil (DISC, Personalidade, Nine Box) sem obrigatoriedade de preenchimento conjunto.  
Etapas:
- O usuário acessa a aba `Perfil e Classificacao`.
- O sistema exibe um painel com 3 colunas iguais: DISC, Personalidade e Nine Box.
- Cada coluna possui campos editáveis para data e valor, histórico somente leitura e botão "Salvar".
- O usuário preenchelinha editável de qualquer coluna (data + valor).
- O usuário clica em "Salvar" da coluna desejada.
- O sistema persiste apenas os dados daquela coluna, independentemente das outras.
- Os campos são limpos após o salvamento e o foco retorna ao campo de data.

**Fluxo 8: Analisar Radar Cultural por data**  
Atores: Gerente / Sistema  
Intenção: Comparar visualmente avaliações culturais ao longo do tempo.  
Etapas:
- O usuário acessa o Radar Cultural no resumo do liderado.
- O sistema lista no dropdown todas as datas disponíveis de avaliação cultural.
- O usuário troca a data pelo dropdown ou scroll do mouse.
- O sistema atualiza o gráfico com animação para refletir a avaliação selecionada.

**Fluxo 9: Conduzir conversa com perguntas exploratórias**  
Atores: Gerente / Sistema  
Intenção: Apoiar 1:1 e coleta de dados com perguntas orientadoras.  
Etapas:
- O usuário navega em qualquer aba com campos de informação.
- O sistema exibe ícone de informação ao lado dos labels.
- Ao passar o mouse, o sistema mostra tooltip textual com o conteúdo configurado para o tipo de informação.

**Fluxo 10: Editar texto do tooltip por campo**  
Atores: Gerente / Sistema  
Intenção: Ajustar rapidamente orientações exibidas no tooltip durante o uso da ferramenta.  
Etapas:
- O usuário dá duplo clique no ícone de informação de um campo.
- O sistema abre modal de edição de tooltip para o campo selecionado.
- O modal carrega automaticamente o texto atual exibido no tooltip.
- O usuário inclui/edita o texto e salva.
- O sistema persiste o novo conteúdo no tipo de informação e o tooltip passa a exibir o texto atualizado.

## 8. Critérios de Aceitação (Macro / de alto nível)

- O dashboard inicial exibe todos os liderados cadastrados em cards de resumo organizados horizontalmente, com rolagem quando necessário.
- Cada card do dashboard exibe o nome do liderado, indicadores principais e um Radar Cultural estático baseado na última avaliação cultural disponível.
- Ao clicar em um card do dashboard, o sistema abre a visão individual do liderado correspondente.
- A visão individual exibe no topo da área de conteúdo um breadcrumb `Dashboard | [combobox]`; clicar em "Dashboard" retorna ao dashboard e o combobox permite alternar de liderado sem sair da visão individual.
- O painel lateral de resumo da visão individual exibe o nome do liderado como título do painel.
- A tela individual apresenta as seções em abas e permite alternância de contexto sem recarregar a página.
- A seção `1:1` exibe histórico tabular, mantém uma linha editável para novo registro e não utiliza coluna lateral de histórico.
- A seção `Feedbacks` segue o mesmo padrão de histórico tabular com linha editável para novo registro, incluindo o campo `Polaridade` como combobox com opções `Positivo` e `Negativo`.
- A seção `Cultura` substitui `Avaliações Gerais` e exibe os sete pilares culturais como colunas numéricas por data.
- A aba `Perfil e Classificacao` (renomeada) exibe um painel com 3 colunas iguais (DISC, Personalidade, Nine Box), cada coluna contendo: campos editáveis para data e valor, tabela de histórico (somente leitura) e botão "Salvar" individual. Cada coluna opera de forma 100% independente, sem validação cruzada ou obrigatoriedade de preenchimento conjunto.
- As seções `CHAVE`, `GROW / PDI` e `SWOT` exibem abas internas por Value Object individual, cada uma com tabela `Data | Propriedade`, com primeira linha editável e histórico somente leitura. Cada Value Object individual (Conhecimentos, Habilidades, Atitudes, Valores, Expectativas, Metas, Situação Atual, Opções, Próximos Passos, Fortalezas, Oportunidades, Fraquezas, Ameaças) possui sua própria tabela no banco de dados.
- A seção `Informacoes Pessoais` é exibida em três colunas com distribuição fixa: coluna 1 (dados cadastrais e carreira), coluna 2 (`Gostos pessoais` e `Red Flags`) e coluna 3 (`BIO`).
- O Radar Cultural da visão individual exibe as sete dimensões culturais, lista todas as datas disponíveis em dropdown e atualiza ao trocar a data via seleção ou scroll do mouse.
- A atualização do Radar Cultural da visão individual ocorre com animação de transição entre valores.
- Em qualquer tela com informação tratada, há ícone de informação por label e tooltip textual no hover, sem título fixo e sem bullets.
- Duplo clique no ícone de informação abre modal para incluir/editar o texto do tooltip, já preenchido com o conteúdo atual daquele campo.
- Toda alteração realizada em qualquer informação do liderado é registrada no histórico, permitindo consulta de evolução.
- Todas as operações funcionam localmente, sem dependência de conexão externa.

## 9. Métricas de Sucesso

- **Métrica Primária:** % de liderados com histórico completo e atualizado nas principais seções (Informações Pessoais, CHAVE, GROW/PDI, SWOT, Classificação de Perfil, Feedbacks, 1:1).
  - Meta inicial: 90% dos liderados com todas as seções preenchidas após 3 meses de uso.
- **Métrica Secundária:** Tempo médio entre o registro de um novo dado e sua consulta posterior (indicador de fluidez e utilidade do sistema).
  - Meta: < 10 segundos em 95% dos casos.
- **Métrica Secundária:** % de reuniões 1:1 registradas com uso de perguntas exploratórias.
  - Meta: 80% dos 1:1 com pelo menos uma pergunta exploratória utilizada.
- **Métrica Saudável:** Nenhum caso de perda de dados ou inconsistência de histórico reportado pelo usuário.
- **Métrica Saudável:** Satisfação do usuário (autoavaliação periódica, escala 1–5).
  - Meta: ≥ 4,0 após 6 meses de uso.

## 10. Pressupostos & Dependências
- O usuário terá acesso contínuo ao ambiente local onde a ferramenta está instalada (sem restrições de permissão de leitura/gravação em disco).
- O sistema operacional do usuário suporta execução de aplicações web locais (ex: navegador moderno, permissões para rodar localmente).
- Não há necessidade de integração com sistemas externos, autenticação ou multiusuário nesta versão.
- O modelo de dados e as perguntas exploratórias poderão evoluir sem exigir migração manual de registros antigos.
- O usuário é responsável por realizar backups periódicos dos dados, caso deseje proteção adicional contra falhas locais.

## 11. Riscos & Mitigações
- **Risco 1: Perda de dados por falha local (máquina, disco, exclusão acidental).**
  - **Impacto:** Alto (perda de histórico e rastreabilidade).
  - **Mitigação:** Implementar rotina de backup local/exportação simples (ex.: arquivo único versionado por data), com lembrete periódico no sistema e instrução clara de restauração.

- **Risco 2: Corrupção da base local após interrupção inesperada (queda de energia, fechamento abrupto).**
  - **Impacto:** Alto (inconsistência de dados e histórico incompleto).
  - **Mitigação:** Usar persistência transacional/atômica, validação de integridade na inicialização e mecanismo de recuperação a partir do último backup válido.

- **Risco 3: Alterações indevidas sem trilha confiável de auditoria.**
  - **Impacto:** Alto (compromete confiança nas informações).
  - **Mitigação:** Garantir RF-23 em 100% das operações de criação/edição/exclusão lógica, com registro de data/hora, valor anterior, novo valor e origem da alteração.

- **Risco 4: Exposição indevida de dados sensíveis no ambiente local.**
  - **Impacto:** Alto (dados pessoais e avaliações qualitativas).
  - **Mitigação:** Restringir armazenamento a diretório local controlado, evitar logs com dados sensíveis em texto aberto e orientar protecao da estação (senha do SO, disco criptografado, bloqueio de tela).

- **Risco 5: Crescimento do modelo de dados quebrar compatibilidade com registros antigos.**
  - **Impacto:** Médio/Alto (retrabalho e perda de continuidade histórica).
  - **Mitigação:** Adotar versionamento de esquema, migrações incrementais e testes de regressão de leitura/escrita para dados legados antes de cada evolução.

- **Risco 6: Queda de usabilidade por excesso de campos e seções na tela individual.**
  - **Impacto:** Médio (reduz adoção e qualidade dos registros).
  - **Mitigação:** Organização por seções colapsáveis, priorização visual por contexto de uso (1:1, PDI, feedback), e revisão periódica de UX com base no uso real.

- **Risco 7: Evolução dos pilares/dimensões culturais gerar retrabalho na visualização do Radar Cultural.**
  - **Impacto:** Médio.
  - **Mitigação:** Manter componente de radar parametrizável por dimensões e versionar configuração de pilares para preservar comparabilidade histórica.

- **Risco 8: Dependência de operação por uma única pessoa (fator de continuidade).**
  - **Impacto:** Médio.
  - **Mitigação:** Documentar procedimentos mínimos de uso, backup e restauração no repositório, reduzindo risco operacional em caso de troca de contexto ou indisponibilidade temporária.

- **Risco 9: Degradação de desempenho com aumento de histórico e registros por liderado.**
  - **Impacto:** Médio.
  - **Mitigação:** Indexação dos principais campos de consulta, paginação de histórico por seção e testes de desempenho com massa de dados representativa.

## 12. Anexos / Observações

- **Fonte de referência do modelo:** planilha `docs/Diretrizes de gestão/Nome - XRay.xlsx` e estrutura validada ao longo deste PRD.
- **Técnicas de gestão de apoio:** materiais da pasta `docs/Diretrizes de gestão/` (CHAVE, GROW/STAR, Nine Box, conflitos, empoderamento, etc.) como referência conceitual, sem obrigação de implementar todos os frameworks na V1.
- **Alinhamento da prototipação validada:** dashboard com cards de resumo por liderado em lista horizontal, visão individual com abas, histórico tabular em `1:1`, `Feedbacks` e `Cultura`, histórico por propriedade em abas internas para `Classificação de Perfil`/`CHAVE`/`GROW / PDI`/`SWOT`, seção `Informacoes Pessoais` em 3 colunas e Radar Cultural com seletor de data.
- **Comportamento de tooltip validado:** tooltip textual sem título fixo e sem bullets; duplo clique no ícone de informação abre modal para editar o conteúdo, com preload do texto atual.
- **Comportamento do Radar Cultural:** no dashboard, cada card mostra a última avaliação de forma estática; na visão individual, há seleção de data por dropdown, troca por scroll do mouse e transição animada ao atualizar os dados.
- **Diretriz de operação local:** esta versão não contempla autenticação, multiusuário ou integração externa; qualquer evolução nesses temas deve gerar revisão deste PRD.
- **Diretriz de proteção de dados:** apesar do uso individual/local, o conteúdo pode incluir dados pessoais e avaliações sensíveis; recomenda-se rotina formal de backup e boas práticas de segurança da estação.
- **Critério de evolução:** novas seções/campos devem preservar compatibilidade com dados existentes e manter histórico completo de alterações (RF-23).
