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
- Seções `1:1`, `Feedbacks` e `Cultura` com histórico em formato de tabela + linha editável para novo registro
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

- Problema: Atualização manual e trabalhosa de múltiplas planilhas individuais para cada liderado, dificultando a padronização e aumentando o risco de inconsistências e perda de informações.
- Oportunidade: Centralizar todo o histórico e dados estruturados em uma ferramenta web, facilitando o acompanhamento, a evolução dos liderados e a tomada de decisão baseada em dados consolidados.

## 5. Requisitos Funcionais
- RF-01: O usuário pode cadastrar um novo liderado informando somente o nome.
- RF-02: O usuário pode visualizar um dashboard inicial com visão consolidada de todos os liderados por meio de cards de resumo, um para cada liderado.
- RF-03: Ao clicar em um card do dashboard, o usuário pode abrir a visão individual e editar o histórico do liderado selecionado.
- RF-04: A tela individual do liderado deve organizar as seções em abas navegáveis.
- RF-05: O usuário pode registrar e atualizar informações pessoais do liderado (ex.: gostos pessoais, bio, red flags, estado civil, filhos, cargo e aspiração de carreira).
- RF-06: O usuário pode registrar e consultar avaliações no modelo C.H.A.V.E (conhecimentos, habilidades, atitudes, valores e expectativas).
- RF-07: O usuário pode registrar e acompanhar informações de desenvolvimento no formato GROW/PDI (metas, situação atual, opções e próximos passos).
- RF-08: O usuário pode registrar análises SWOT do liderado.
- RF-09: O usuário pode registrar e consultar avaliações de perfil (DISC, personalidade e posicionamento em nine box).
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
- RF-20: O usuário pode cadastrar, editar e excluir múltiplas perguntas exploratórias para cada tipo de informação acompanhada pelo sistema (ex.: conhecimentos, habilidades, atitudes, valores, expectativas, metas e próximos passos), sem associação a um liderado específico.
- RF-21: Em qualquer tela que exiba uma informação tratada pelo sistema, o sistema deve exibir um ícone de informação ao lado do respectivo label.
- RF-22: Ao passar o mouse sobre o ícone de informação, o sistema deve exibir um popup com todas as perguntas exploratórias cadastradas para aquele tipo de informação, independentemente do liderado selecionado.
- RF-23: O sistema deve manter um histórico completo de todas as alterações realizadas em qualquer informação do liderado, incluindo data/hora da alteração, valor anterior, novo valor e usuário responsável. O histórico deve ser consultável pelo usuário em cada seção, permitindo acompanhar a evolução das informações ao longo do tempo.

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
- O sistema abre a visão individual com abas por seção.
- O usuário troca entre abas para consultar/editar os dados de cada contexto.

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

**Fluxo 7: Analisar Radar Cultural por data**  
Atores: Gerente / Sistema  
Intenção: Comparar visualmente avaliações culturais ao longo do tempo.  
Etapas:
- O usuário acessa o Radar Cultural no resumo do liderado.
- O sistema lista no dropdown todas as datas disponíveis de avaliação cultural.
- O usuário troca a data pelo dropdown ou scroll do mouse.
- O sistema atualiza o gráfico com animação para refletir a avaliação selecionada.

**Fluxo 8: Conduzir conversa com perguntas exploratórias**  
Atores: Gerente / Sistema  
Intenção: Apoiar 1:1 e coleta de dados com perguntas orientadoras.  
Etapas:
- O usuário navega em qualquer aba com campos de informação.
- O sistema exibe ícone de informação ao lado dos labels.
- Ao passar o mouse, o sistema mostra popup com perguntas exploratórias do tipo de informação.

## 8. Critérios de Aceitação (Macro / de alto nível)

- O dashboard inicial exibe todos os liderados cadastrados em cards de resumo organizados horizontalmente, com rolagem quando necessário.
- Cada card do dashboard exibe o nome do liderado, indicadores principais e um Radar Cultural estático baseado na última avaliação cultural disponível.
- Ao clicar em um card do dashboard, o sistema abre a visão individual do liderado correspondente.
- A tela individual apresenta as seções em abas e permite alternância de contexto sem recarregar a página.
- A seção `1:1` exibe histórico tabular, mantém uma linha editável para novo registro e não utiliza coluna lateral de histórico.
- A seção `Feedbacks` segue o mesmo padrão de histórico tabular com linha editável para novo registro, incluindo o campo `Polaridade` como combobox com opções `Positivo` e `Negativo`.
- A seção `Cultura` substitui `Avaliações Gerais` e exibe os sete pilares culturais como colunas numéricas por data.
- O Radar Cultural da visão individual exibe as sete dimensões culturais, lista todas as datas disponíveis em dropdown e atualiza ao trocar a data via seleção ou scroll do mouse.
- A atualização do Radar Cultural da visão individual ocorre com animação de transição entre valores.
- Em qualquer tela com informação tratada, há ícone de informação por label e popup de perguntas exploratórias no hover.
- Toda alteração realizada em qualquer informação do liderado é registrada no histórico, permitindo consulta de evolução.
- Todas as operações funcionam localmente, sem dependência de conexão externa.

## 9. Métricas de Sucesso

- **Métrica Primária:** % de liderados com histórico completo e atualizado nas principais seções (Informações Pessoais, CHAVE, GROW/PDI, SWOT, Classificação, Feedbacks, 1:1).
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
- **Alinhamento da prototipação validada:** dashboard com cards de resumo por liderado em lista horizontal, visão individual com abas, histórico tabular em `1:1`, `Feedbacks` e `Cultura`, e Radar Cultural com seletor de data.
- **Comportamento do Radar Cultural:** no dashboard, cada card mostra a última avaliação de forma estática; na visão individual, há seleção de data por dropdown, troca por scroll do mouse e transição animada ao atualizar os dados.
- **Diretriz de operação local:** esta versão não contempla autenticação, multiusuário ou integração externa; qualquer evolução nesses temas deve gerar revisão deste PRD.
- **Diretriz de proteção de dados:** apesar do uso individual/local, o conteúdo pode incluir dados pessoais e avaliações sensíveis; recomenda-se rotina formal de backup e boas práticas de segurança da estação.
- **Critério de evolução:** novas seções/campos devem preservar compatibilidade com dados existentes e manter histórico completo de alterações (RF-23).
