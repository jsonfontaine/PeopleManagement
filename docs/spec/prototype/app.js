const leaders = [
  {
    id: 1,
    nome: "Ana Costa",
    perfil: "D/I",
    nineBox: "Alto potencial",
    sections: {
      "Informacoes Pessoais": {
        fields: [
          ["Nome", "Ana Costa", "nome"],
          ["Cargo", "Coordenadora de Engenharia", "cargo"],
          ["Aspiracao de carreira", "Gestao senior", "aspiracao"],
          ["Red flags", "Centraliza decisoes em picos de pressao", "redFlags", true]
        ],
        history: [
          "2026-02-10 10:15 - Cargo: Especialista -> Coordenadora",
          "2026-03-01 09:20 - Aspiracao: Arquitetura -> Gestao senior"
        ]
      },
      CHAVE: {
        fields: [
          ["Conhecimentos", "Backend, Cloud, Observabilidade", "conhecimentos", true],
          ["Habilidades", "Comunicacao com negocio", "habilidades", true],
          ["Atitudes", "Proativa", "atitudes", true],
          ["Valores", "Autonomia e responsabilidade", "valores", true],
          ["Expectativas", "Maior exposicao estrategica", "expectativas", true]
        ],
        propertyHistory: {
          conhecimentos: [
            { data: "2026-03-10", valor: "Backend, Cloud, Observabilidade" },
            { data: "2026-01-15", valor: "Backend e APIs" }
          ],
          habilidades: [
            { data: "2026-03-01", valor: "Comunicacao com negocio" }
          ],
          atitudes: [
            { data: "2026-02-22", valor: "Proativa" }
          ],
          valores: [
            { data: "2026-02-05", valor: "Autonomia e responsabilidade" }
          ],
          expectativas: [
            { data: "2026-03-05", valor: "Maior exposicao estrategica" },
            { data: "2025-12-10", valor: "Assumir projetos criticos" }
          ]
        }
      },
      "GROW / PDI": {
        fields: [
          ["Metas de vida", "Liderar uma area de plataforma", "metas", true],
          ["Situacao atual", "Em transicao para coordenacao", "situacaoAtual", true],
          ["O que pode ajudar", "Mentoria e shadowing de reunioes", "opcoes", true],
          ["Proximos passos", "Plano de 90 dias", "proximosPassos", true]
        ],
        propertyHistory: {
          metas: [
            { data: "2026-03-07", valor: "Liderar uma area de plataforma" },
            { data: "2025-11-18", valor: "Ser referencia tecnica" }
          ],
          situacaoAtual: [
            { data: "2026-02-28", valor: "Em transicao para coordenacao" }
          ],
          opcoes: [
            { data: "2026-03-02", valor: "Mentoria e shadowing de reunioes" }
          ],
          proximosPassos: [
            { data: "2026-03-07", valor: "Plano de 90 dias" },
            { data: "2026-02-10", valor: "Definir marcos mensais" }
          ]
        }
      },
      SWOT: {
        fields: [
          ["Fortalezas e oportunidades", "Visao sistemica", "fortalezas", true],
          ["Fraquezas e ameacas", "Gestao de conflitos", "fraquezas", true]
        ],
        propertyHistory: {
          fortalezas: [
            { data: "2026-02-27", valor: "Visao sistemica" },
            { data: "2025-12-22", valor: "Facilidade de alinhamento tecnico" }
          ],
          fraquezas: [
            { data: "2026-02-27", valor: "Gestao de conflitos" },
            { data: "2025-12-22", valor: "Delegacao em picos" }
          ]
        }
      },
      Classificacao: {
        fields: [
          ["DISC", "DI", "disc"],
          ["Personalidade", "ENTJ", "personalidade"],
          ["Nine Box", "Alto potencial", "nineBox"]
        ],
        classificationHistory: {
          disc: [
            { data: "2026-03-14", valor: "DI" },
            { data: "2026-01-23", valor: "ID" }
          ],
          personalidade: [
            { data: "2026-02-20", valor: "ENTJ" },
            { data: "2025-11-05", valor: "ENFJ" }
          ],
          nineBox: [
            { data: "2026-02-01", valor: "Alto potencial" },
            { data: "2025-12-01", valor: "Eficaz" }
          ]
        }
      },
      "Fatos e Observacoes": {
        factHistory: [
          {
            data: "2026-03-09",
            observacao: "Conduziu incidente critico com boa comunicacao e calma na coordenacao do time."
          },
          {
            data: "2026-02-18",
            observacao: "Facilitou alinhamento entre squads em contexto de dependencia critica."
          }
        ]
      },
      Cultura: {
        cultureHistory: [
          {
            data: "2026-03-12",
            aprender: 4,
            dono: 5,
            cliente: 4,
            equipe: 5,
            excelencia: 4,
            acontecer: 4,
            inovar: 3
          },
          {
            data: "2026-02-12",
            aprender: 3,
            dono: 4,
            cliente: 4,
            equipe: 4,
            excelencia: 3,
            acontecer: 4,
            inovar: 3
          }
        ]
      },
      Feedbacks: {
        feedbackHistory: [
          {
            data: "2026-03-10",
            conteudo: "Excelente colaboracao cross-team",
            receptividade: "Alta",
            polaridade: "Positivo"
          },
          {
            data: "2026-02-20",
            conteudo: "Boa evolucao em comunicacao com stakeholders",
            receptividade: "Boa",
            polaridade: "Positivo"
          },
          {
            data: "2026-02-05",
            conteudo: "Precisa melhorar delegacao em semanas criticas",
            receptividade: "Media",
            polaridade: "Negativo"
          }
        ]
      },
      "1:1": {
        oneOnOneHistory: [
          {
            data: "2026-03-11",
            consideracoes: "Foco em delegacao",
            tarefas: "Delegar ritual de planejamento",
            proximo: "2026-03-24"
          },
          {
            data: "2026-02-25",
            consideracoes: "Alinhar prioridades do trimestre",
            tarefas: "Preparar plano 30-60-90",
            proximo: "2026-03-11"
          },
          {
            data: "2026-02-11",
            consideracoes: "Revisar nivel de autonomia",
            tarefas: "Mapear decisoes criticas",
            proximo: "2026-02-25"
          }
        ]
      }
    }
  },
  {
    id: 2,
    nome: "Bruno Lima",
    perfil: "S/C",
    nineBox: "Eficaz",
    sections: {}
  },
  {
    id: 3,
    nome: "Carla Nunes",
    perfil: "I/D",
    nineBox: "Mantenedora",
    sections: {}
  }
];

const exploratoryQuestions = {
  conhecimentos: [
    "Em quais temas tecnicos voce se sente mais confiante hoje?",
    "Quais lacunas tecnicas voce quer fechar no proximo trimestre?"
  ],
  habilidades: [
    "Qual habilidade comportamental mais impactou seu trabalho recente?",
    "Em que situacao voce sentiu que faltou repertorio?"
  ],
  atitudes: [
    "Como voce reage quando recebe feedback dificil?"
  ],
  valores: [
    "Que tipo de ambiente de trabalho potencializa seu desempenho?"
  ],
  expectativas: [
    "Que evolucao de carreira voce espera nos proximos 12 meses?"
  ],
  metas: [
    "Quais metas pessoais/profissionais sao mais importantes agora?"
  ],
  proximosPassos: [
    "Qual sera seu proximo passo concreto ate o proximo 1:1?"
  ],
  redFlags: [
    "Existe algum fator de risco comportamental que merece atencao?"
  ],
  default: [
    "O que mudou desde o ultimo registro?",
    "Que evidencia concreta sustenta esta percepcao?"
  ]
};

let currentLeader = leaders[0];

const dashboardView = document.getElementById("dashboardView");
const leaderView = document.getElementById("leaderView");
const leaderBreadcrumb = document.getElementById("leaderBreadcrumb");
const breadcrumbDashboard = document.getElementById("breadcrumbDashboard");
const leaderSelect = document.getElementById("leaderSelect");
const summaryLeaderName = document.getElementById("summaryLeaderName");
const summaryBlock = document.getElementById("summaryBlock");
const dashboardSummaryCards = document.getElementById("dashboardSummaryCards");
const tabsBar = document.getElementById("tabsBar");
const sectionsContainer = document.getElementById("sectionsContainer");
const sectionTemplate = document.getElementById("sectionTemplate");
const tooltip = document.getElementById("tooltip");
const radarDateSelect = document.getElementById("radarDateSelect");

let currentRadarValues = null;
let radarAnimationId = null;
let cultureEntries = [];
let cultureIndex = 0;

function init() {
  renderDashboard();
  renderLeaderOptions();
  renderLeader(currentLeader.id);

  if (breadcrumbDashboard) {
    breadcrumbDashboard.addEventListener("click", () => setView("dashboard"));
  }
  leaderSelect.addEventListener("change", (event) => renderLeader(Number(event.target.value)));
  tabsBar.addEventListener("click", onTabClick);

  if (radarDateSelect) {
    radarDateSelect.addEventListener("change", onRadarDateChange);
    radarDateSelect.addEventListener("wheel", onRadarDateWheel, { passive: false });
  }

  document.addEventListener("mouseover", onInfoHover);
  document.addEventListener("mouseout", onInfoOut);
}

function setView(viewName) {
  const isDashboard = viewName === "dashboard";
  dashboardView.classList.toggle("hidden", !isDashboard);
  leaderView.classList.toggle("hidden", isDashboard);
  if (leaderBreadcrumb) {
    leaderBreadcrumb.classList.toggle("hidden", isDashboard);
  }
}

function renderDashboard() {

  if (!dashboardSummaryCards) {
    return;
  }

  dashboardSummaryCards.innerHTML = "";
  leaders.forEach((leader) => {
    const card = document.createElement("article");
    card.className = "dashboard-summary-card";
    card.innerHTML = `
      <h3>${leader.nome}</h3>
      <div class="summary"></div>
      <h4>Radar Cultural</h4>
      <svg id="dashboardRadar-${leader.id}" class="dashboard-radar" viewBox="0 0 260 220" aria-label="Radar cultural do liderado ${leader.nome}"></svg>
    `;

    const summaryNode = card.querySelector(".summary");
    if (summaryNode) {
      renderSummary(leader, summaryNode);
    }

    const cultureHistory = getCultureEntries(leader);
    const latest = cultureHistory[0];
    const values = latest
      ? [
          latest.aprender,
          latest.dono,
          latest.cliente,
          latest.equipe,
          latest.excelencia,
          latest.acontecer,
          latest.inovar
        ].map((value) => Math.max(0, Math.min(100, Number(value) * 20)))
      : [65, 65, 65, 65, 65, 65, 65];

    card.addEventListener("click", () => {
      renderLeader(leader.id);
      setView("leader");
    });

    // Append first so renderRadar can find the SVG element by id.
    dashboardSummaryCards.appendChild(card);
    renderRadar(values, `dashboardRadar-${leader.id}`);
  });
}

function renderLeaderOptions() {
  leaderSelect.innerHTML = "";

  leaders.forEach((leader) => {
    const option = document.createElement("option");
    option.value = String(leader.id);
    option.textContent = leader.nome;
    leaderSelect.appendChild(option);
  });
}

function renderLeader(leaderId) {
  currentLeader = leaders.find((item) => item.id === leaderId) || leaders[0];
  leaderSelect.value = String(currentLeader.id);
  if (summaryLeaderName) {
    summaryLeaderName.textContent = currentLeader.nome;
  }

  renderSummary(currentLeader);
  renderSections(currentLeader);

  cultureEntries = getCultureEntries(currentLeader);
  cultureIndex = 0;
  setupRadarDateOptions(cultureEntries, cultureIndex);
  updateRadarForCultureIndex(cultureIndex);

  renderDashboard();
}

function renderSummary(leader, targetBlock = summaryBlock) {
  if (!targetBlock) {
    return;
  }

  targetBlock.innerHTML = "";
  const summaryMetrics = getLeaderSummaryMetrics(leader);
  const kpis = [
    ["Perfil", summaryMetrics.perfil],
    ["Nine Box", summaryMetrics.nineBox],
    ["Feedbacks", summaryMetrics.feedbacks],
    ["1:1", summaryMetrics.oneOnOnes],
    ["Nota geral", summaryMetrics.notaGeral]
  ];

  kpis.forEach(([label, value]) => {
    const box = document.createElement("div");
    box.className = "kpi";
    box.innerHTML = `<div class="kpi-label">${label}</div><div class="kpi-value">${value}</div>`;
    targetBlock.appendChild(box);
  });
}


function getCultureEntries(leader) {
  const history = leader.sections?.Cultura?.cultureHistory || [];
  return [...history].sort((a, b) => String(b.data).localeCompare(String(a.data)));
}

function getLatestCultureEntry(leader) {
  return getCultureEntries(leader)[0] || null;
}

function getCultureAverage(entry) {
  if (!entry) {
    return null;
  }

  const scores = [
    entry.aprender,
    entry.dono,
    entry.cliente,
    entry.equipe,
    entry.excelencia,
    entry.acontecer,
    entry.inovar
  ].map(Number);

  if (scores.some((value) => Number.isNaN(value))) {
    return null;
  }

  const total = scores.reduce((sum, value) => sum + value, 0);
  return total / scores.length;
}

function getLeaderSummaryMetrics(leader) {
  const feedbacks = leader.sections?.Feedbacks?.feedbackHistory || [];
  const oneOnOnes = leader.sections?.["1:1"]?.oneOnOneHistory || [];
  const latestCultureEntry = getLatestCultureEntry(leader);
  const cultureAverage = getCultureAverage(latestCultureEntry);

  return {
    perfil: leader.perfil || "—",
    nineBox: leader.nineBox || "—",
    feedbacks: feedbacks.length,
    oneOnOnes: oneOnOnes.length,
    notaGeral: cultureAverage === null ? "—" : cultureAverage.toFixed(1)
  };
}

function getFactEntries(sectionData) {
  return Array.isArray(sectionData.factHistory) ? sectionData.factHistory : [];
}

function getPropertyHistoryRows(field, sectionData, historyKey = "propertyHistory") {
  const [, value, key] = field;
  const historyByProperty = sectionData[historyKey] || {};
  const propertyHistory = Array.isArray(historyByProperty[key]) ? historyByProperty[key] : [];

  if (propertyHistory.length > 0) {
    return propertyHistory.map((entry) => ({
      data: entry.data || "",
      valor: entry.valor || entry.value || ""
    }));
  }

  return value
    ? [{ data: "", valor: value }]
    : [];
}

function setupRadarDateOptions(entries, selectedIndex) {
  if (!radarDateSelect) {
    return;
  }

  radarDateSelect.innerHTML = "";

  if (entries.length === 0) {
    const option = document.createElement("option");
    option.value = "";
    option.textContent = "Sem avaliacoes";
    radarDateSelect.appendChild(option);
    radarDateSelect.disabled = true;
    return;
  }

  entries.forEach((entry, index) => {
    const option = document.createElement("option");
    option.value = String(index);
    option.textContent = entry.data;
    radarDateSelect.appendChild(option);
  });

  radarDateSelect.disabled = false;
  radarDateSelect.value = String(selectedIndex);
}

function updateRadarForCultureIndex(index) {
  if (cultureEntries.length === 0) {
    renderRadarAnimated([65, 65, 65, 65, 65, 65, 65]);
    return;
  }

  const entry = cultureEntries[index];
  if (!entry) {
    return;
  }

  const values = [
    entry.aprender,
    entry.dono,
    entry.cliente,
    entry.equipe,
    entry.excelencia,
    entry.acontecer,
    entry.inovar
  ].map((value) => Math.max(0, Math.min(100, Number(value) * 20)));

  renderRadarAnimated(values);
}

function onRadarDateChange(event) {
  const selectedIndex = Number(event.target.value);
  if (Number.isNaN(selectedIndex)) {
    return;
  }

  cultureIndex = Math.max(0, Math.min(selectedIndex, cultureEntries.length - 1));
  updateRadarForCultureIndex(cultureIndex);
}

function onRadarDateWheel(event) {
  if (!radarDateSelect || cultureEntries.length === 0) {
    return;
  }

  event.preventDefault();
  const direction = event.deltaY > 0 ? 1 : -1;
  const nextIndex = Math.max(0, Math.min(cultureIndex + direction, cultureEntries.length - 1));

  if (nextIndex === cultureIndex) {
    return;
  }

  cultureIndex = nextIndex;
  radarDateSelect.value = String(cultureIndex);
  updateRadarForCultureIndex(cultureIndex);
}

function renderRadarAnimated(targetValues) {
  if (!Array.isArray(targetValues) || targetValues.length === 0) {
    return;
  }

  if (!currentRadarValues) {
    currentRadarValues = [...targetValues];
    renderRadar(currentRadarValues, "radar");
    return;
  }

  if (radarAnimationId) {
    cancelAnimationFrame(radarAnimationId);
    radarAnimationId = null;
  }

  const startValues = [...currentRadarValues];
  const duration = 320;
  const startTime = performance.now();

  const tick = (now) => {
    const progress = Math.min(1, (now - startTime) / duration);
    const eased = 1 - Math.pow(1 - progress, 2);

    currentRadarValues = startValues.map((start, i) => {
      const end = targetValues[i] ?? start;
      return start + (end - start) * eased;
    });

    renderRadar(currentRadarValues, "radar");

    if (progress < 1) {
      radarAnimationId = requestAnimationFrame(tick);
    }
  };

  radarAnimationId = requestAnimationFrame(tick);
}

function renderRadar(values, svgId = "radar") {
  const svg = document.getElementById(svgId);
  if (!svg) {
    return;
  }

  svg.innerHTML = "";

  const cx = 130;
  const cy = 110;
  const radius = 84;
  const labels = [
    "Aprender",
    "Dono",
    "Cliente",
    "Equipe",
    "Excelencia",
    "Acontecer",
    "Inovar"
  ];

  for (let layer = 1; layer <= 4; layer += 1) {
    const r = (radius / 4) * layer;
    svg.appendChild(makePolygon(cx, cy, r, labels.length, "none", "#334155", 0.5));
  }

  labels.forEach((label, index) => {
    const angle = (Math.PI * 2 * index) / labels.length - Math.PI / 2;
    const x = cx + radius * Math.cos(angle);
    const y = cy + radius * Math.sin(angle);
    svg.appendChild(makeLine(cx, cy, x, y, "#334155", 0.7));

    const tx = cx + (radius + 14) * Math.cos(angle);
    const ty = cy + (radius + 14) * Math.sin(angle);
    svg.appendChild(makeText(tx, ty, label));
  });

  const points = values.map((val, index) => {
    const angle = (Math.PI * 2 * index) / labels.length - Math.PI / 2;
    const r = (radius * Math.max(0, Math.min(val, 100))) / 100;
    const x = cx + r * Math.cos(angle);
    const y = cy + r * Math.sin(angle);
    return `${x},${y}`;
  });

  const dataShape = document.createElementNS("http://www.w3.org/2000/svg", "polygon");
  dataShape.setAttribute("points", points.join(" "));
  dataShape.setAttribute("fill", "rgba(59, 130, 246, 0.35)");
  dataShape.setAttribute("stroke", "#60a5fa");
  dataShape.setAttribute("stroke-width", "2");
  svg.appendChild(dataShape);
}

function makePolygon(cx, cy, radius, sides, fill, stroke, strokeOpacity) {
  const poly = document.createElementNS("http://www.w3.org/2000/svg", "polygon");
  const points = [];
  for (let i = 0; i < sides; i += 1) {
    const angle = (Math.PI * 2 * i) / sides - Math.PI / 2;
    points.push(`${cx + radius * Math.cos(angle)},${cy + radius * Math.sin(angle)}`);
  }
  poly.setAttribute("points", points.join(" "));
  poly.setAttribute("fill", fill);
  poly.setAttribute("stroke", stroke);
  poly.setAttribute("stroke-opacity", String(strokeOpacity));
  return poly;
}

function makeLine(x1, y1, x2, y2, stroke, strokeOpacity) {
  const line = document.createElementNS("http://www.w3.org/2000/svg", "line");
  line.setAttribute("x1", String(x1));
  line.setAttribute("y1", String(y1));
  line.setAttribute("x2", String(x2));
  line.setAttribute("y2", String(y2));
  line.setAttribute("stroke", stroke);
  line.setAttribute("stroke-opacity", String(strokeOpacity));
  return line;
}

function makeText(x, y, text) {
  const node = document.createElementNS("http://www.w3.org/2000/svg", "text");
  node.setAttribute("x", String(x));
  node.setAttribute("y", String(y));
  node.setAttribute("fill", "#cbd5e1");
  node.setAttribute("font-size", "10");
  node.setAttribute("text-anchor", "middle");
  node.textContent = text;
  return node;
}

function renderSections(leader) {
  sectionsContainer.innerHTML = "";
  tabsBar.innerHTML = "";
  const sections = leader.sections;

  if (!sections || Object.keys(sections).length === 0) {
    const panel = document.createElement("section");
    panel.className = "panel";
    panel.innerHTML = "<p class='muted'>Este liderado esta sem detalhes no mock. Selecione Ana Costa para ver o prototipo completo.</p>";
    sectionsContainer.appendChild(panel);
    return;
  }

  const entries = Object.entries(sections);
  entries.forEach(([sectionName, sectionData], index) => {
    const tab = document.createElement("button");
    tab.className = "tab-btn";
    tab.type = "button";
    tab.dataset.tab = sectionName;
    tab.textContent = sectionName;
    tabsBar.appendChild(tab);

    const sectionNode = sectionTemplate.content.firstElementChild.cloneNode(true);
    sectionNode.classList.add("tab-section");
    sectionNode.dataset.tab = sectionName;
    sectionNode.querySelector(".section-title").textContent = sectionName;

    const fieldsContainer = sectionNode.querySelector(".fields");

    if (sectionName === "1:1") {
      sectionNode.classList.add("single-column");
      const history = sectionData.oneOnOneHistory || [];
      fieldsContainer.innerHTML = `
        <table class="history-table">
          <colgroup>
            <col class="col-date" />
            <col class="col-resumo" />
            <col class="col-tarefas" />
            <col class="col-proximo" />
          </colgroup>
          <thead>
            <tr>
              <th>Data</th>
              <th>Resumo</th>
              <th>Tarefas acordadas</th>
              <th>Proximo encontro (assuntos)</th>
            </tr>
          </thead>
          <tbody>
            <tr class="history-edit">
              <td><input class="date-input" type="text" placeholder="2026-03-18" aria-label="Data do 1:1" /></td>
              <td><textarea placeholder="Resumo do 1:1" aria-label="Resumo do 1:1" rows="2"></textarea></td>
              <td><textarea placeholder="Tarefas acordadas" aria-label="Tarefas acordadas" rows="2"></textarea></td>
              <td><textarea placeholder="Assuntos para o proximo encontro" aria-label="Assuntos para o proximo encontro" rows="2"></textarea></td>
            </tr>
            ${history.map((row) => `
              <tr>
                <td>${row.data}</td>
                <td>${row.consideracoes}</td>
                <td>${row.tarefas}</td>
                <td>${row.proximo}</td>
              </tr>
            `).join("")}
          </tbody>
        </table>
      `;

      const historyBlock = sectionNode.querySelector(".history");
      if (historyBlock) {
        historyBlock.remove();
      }
    } else if (sectionName === "Fatos e Observacoes") {
      sectionNode.classList.add("single-column");
      const history = getFactEntries(sectionData);
      fieldsContainer.innerHTML = `
        <table class="history-table facts-table">
          <colgroup>
            <col class="col-date" />
            <col class="col-observacao" />
          </colgroup>
          <thead>
            <tr>
              <th>Data</th>
              <th>Fato ou observacao</th>
            </tr>
          </thead>
          <tbody>
            <tr class="history-edit">
              <td><input class="date-input" type="text" placeholder="2026-03-18" aria-label="Data do fato ou observacao" /></td>
              <td><textarea placeholder="Registrar fato ou observacao relevante" aria-label="Fato ou observacao relevante" rows="2"></textarea></td>
            </tr>
            ${history.map((row) => `
              <tr>
                <td>${row.data || ""}</td>
                <td>${row.observacao || ""}</td>
              </tr>
            `).join("")}
          </tbody>
        </table>
      `;

      const historyBlock = sectionNode.querySelector(".history");
      if (historyBlock) {
        historyBlock.remove();
      }
    } else if (["Classificacao", "CHAVE", "GROW / PDI", "SWOT"].includes(sectionName)) {
      sectionNode.classList.add("single-column");
      fieldsContainer.classList.add("fields--prop-tabs");
      const historyKey = sectionName === "Classificacao" ? "classificationHistory" : "propertyHistory";
      const fields = Array.isArray(sectionData.fields) ? sectionData.fields : [];

      const wrapper = document.createElement("div");
      wrapper.className = "prop-tabs";

      // Tab bar
      const propTabsBar = document.createElement("div");
      propTabsBar.className = "prop-tabs-bar";
      fields.forEach((field, i) => {
        const [label] = field;
        const btn = document.createElement("button");
        btn.type = "button";
        btn.className = "prop-tab-btn" + (i === 0 ? " active" : "");
        btn.textContent = label;
        propTabsBar.appendChild(btn);
      });

      // Panels
      const propTabsContent = document.createElement("div");
      propTabsContent.className = "prop-tabs-content";
      fields.forEach((field, i) => {
        const [label] = field;
        const history = getPropertyHistoryRows(field, sectionData, historyKey);
        const panel = document.createElement("div");
        panel.className = "prop-tab-panel" + (i === 0 ? " active" : "");
        panel.innerHTML = `
          <table class="history-table classification-table">
            <colgroup>
              <col class="col-date" />
              <col class="col-value" />
            </colgroup>
            <thead>
              <tr>
                <th>Data</th>
                <th>${label}</th>
              </tr>
            </thead>
            <tbody>
              <tr class="history-edit">
                <td><input class="date-input" type="text" placeholder="2026-03-18" aria-label="Data do registro de ${label}" /></td>
                <td><textarea placeholder="Registrar ${label}" aria-label="Registro de ${label}" rows="2"></textarea></td>
              </tr>
              ${history.map((row) => `
                <tr>
                  <td>${row.data || ""}</td>
                  <td>${row.valor || ""}</td>
                </tr>
              `).join("")}
            </tbody>
          </table>
        `;
        propTabsContent.appendChild(panel);
      });

      wrapper.appendChild(propTabsBar);
      wrapper.appendChild(propTabsContent);

      // Tab navigation
      const propTabBtns = propTabsBar.querySelectorAll(".prop-tab-btn");
      const propPanels = propTabsContent.querySelectorAll(".prop-tab-panel");
      propTabsBar.addEventListener("click", (e) => {
        const btn = e.target.closest(".prop-tab-btn");
        if (!btn) { return; }
        const idx = [...propTabBtns].indexOf(btn);
        if (idx === -1) { return; }
        propTabBtns.forEach((b) => b.classList.remove("active"));
        propPanels.forEach((p) => p.classList.remove("active"));
        btn.classList.add("active");
        propPanels[idx].classList.add("active");
      });

      fieldsContainer.innerHTML = "";
      fieldsContainer.appendChild(wrapper);

      const historyBlock = sectionNode.querySelector(".history");
      if (historyBlock) {
        historyBlock.remove();
      }
    } else if (sectionName === "Feedbacks") {
      sectionNode.classList.add("single-column");
      const history = sectionData.feedbackHistory || [];
      fieldsContainer.innerHTML = `
        <table class="history-table feedback-table">
          <colgroup>
            <col class="col-date" />
            <col class="col-resumo" />
            <col class="col-tarefas" />
            <col class="col-polaridade" />
          </colgroup>
          <thead>
            <tr>
              <th>Data</th>
              <th>Conteudo do feedback</th>
              <th>Receptividade</th>
              <th>Polaridade</th>
            </tr>
          </thead>
          <tbody>
            <tr class="history-edit">
              <td><input class="date-input" type="text" placeholder="2026-03-18" aria-label="Data do feedback" /></td>
              <td><textarea placeholder="Conteudo do feedback" aria-label="Conteudo do feedback" rows="2"></textarea></td>
              <td><textarea placeholder="Receptividade" aria-label="Receptividade" rows="2"></textarea></td>
              <td>
                <select aria-label="Polaridade do feedback">
                  <option value="Positivo">Positivo</option>
                  <option value="Negativo">Negativo</option>
                </select>
              </td>
            </tr>
            ${history.map((row) => `
              <tr>
                <td>${row.data}</td>
                <td>${row.conteudo}</td>
                <td>${row.receptividade}</td>
                <td>${row.polaridade || ""}</td>
              </tr>
            `).join("")}
          </tbody>
        </table>
      `;

      const historyBlock = sectionNode.querySelector(".history");
      if (historyBlock) {
        historyBlock.remove();
      }
    } else if (sectionName === "Cultura") {
      sectionNode.classList.add("single-column");
      const history = sectionData.cultureHistory || [];
      fieldsContainer.innerHTML = `
        <div class="table-scroll">
          <table class="history-table culture-table">
            <colgroup>
              <col class="col-date" />
              <col class="col-pillar" />
              <col class="col-pillar" />
              <col class="col-pillar" />
              <col class="col-pillar" />
              <col class="col-pillar" />
              <col class="col-pillar" />
              <col class="col-pillar" />
            </colgroup>
            <thead>
              <tr>
                <th>Data</th>
                <th>Aprender e Melhorar Sempre</th>
                <th>Atitude de Dono</th>
                <th>Buscar os melhores resultados para os clientes</th>
                <th>Espirito de Equipe</th>
                <th>Excelencia</th>
                <th>Fazer Acontecer</th>
                <th>Inovar para Inspirar</th>
              </tr>
            </thead>
            <tbody>
              <tr class="history-edit">
                <td><input class="date-input" type="text" placeholder="2026-03-18" aria-label="Data da avaliacao" /></td>
                <td><input class="score-input" type="number" aria-label="Aprender e Melhorar Sempre" /></td>
                <td><input class="score-input" type="number" aria-label="Atitude de Dono" /></td>
                <td><input class="score-input" type="number" aria-label="Buscar os melhores resultados para os clientes" /></td>
                <td><input class="score-input" type="number" aria-label="Espirito de Equipe" /></td>
                <td><input class="score-input" type="number" aria-label="Excelencia" /></td>
                <td><input class="score-input" type="number" aria-label="Fazer Acontecer" /></td>
                <td><input class="score-input" type="number" aria-label="Inovar para Inspirar" /></td>
              </tr>
              ${history.map((row) => `
                <tr>
                  <td>${row.data}</td>
                  <td>${row.aprender}</td>
                  <td>${row.dono}</td>
                  <td>${row.cliente}</td>
                  <td>${row.equipe}</td>
                  <td>${row.excelencia}</td>
                  <td>${row.acontecer}</td>
                  <td>${row.inovar}</td>
                </tr>
              `).join("")}
            </tbody>
          </table>
        </div>
      `;

      const historyBlock = sectionNode.querySelector(".history");
      if (historyBlock) {
        historyBlock.remove();
      }
    } else {
      const fields = Array.isArray(sectionData.fields) ? sectionData.fields : [];
      fields.forEach((field) => {
        const [label, value, questionKey, isTextarea] = field;
        const wrapper = document.createElement("div");
        wrapper.className = "field";
        const inputTag = isTextarea ? "textarea" : "input";

        wrapper.innerHTML = `
          <label>
            ${label}
            <span class="info-icon" data-qkey="${questionKey || "default"}">i</span>
          </label>
          <${inputTag}>${isTextarea ? value : ""}</${inputTag}>
        `;

        if (!isTextarea) {
          wrapper.querySelector("input").value = value ?? "";
        }

        fieldsContainer.appendChild(wrapper);
      });

      const historyList = sectionNode.querySelector(".history-list");
      const historyItems = Array.isArray(sectionData.history) ? sectionData.history : [];
      historyItems.forEach((item) => {
        const li = document.createElement("li");
        li.textContent = item;
        historyList.appendChild(li);
      });
    }

    sectionsContainer.appendChild(sectionNode);

    if (index === 0) {
      tab.classList.add("active");
      sectionNode.classList.remove("hidden");
    } else {
      sectionNode.classList.add("hidden");
    }
  });
}

function onTabClick(event) {
  const tab = event.target.closest(".tab-btn");
  if (!tab) {
    return;
  }

  const tabKey = tab.dataset.tab;
  if (!tabKey) {
    return;
  }

  Array.from(tabsBar.querySelectorAll(".tab-btn")).forEach((btn) => {
    btn.classList.toggle("active", btn.dataset.tab === tabKey);
  });

  Array.from(sectionsContainer.querySelectorAll(".tab-section")).forEach((section) => {
    section.classList.toggle("hidden", section.dataset.tab !== tabKey);
  });
}

function onInfoHover(event) {
  const icon = event.target.closest(".info-icon");
  if (!icon) {
    return;
  }

  const qKey = icon.dataset.qkey || "default";
  const questions = exploratoryQuestions[qKey] || exploratoryQuestions.default;
  tooltip.innerHTML = `<strong>Perguntas exploratorias:</strong><br>${questions.map((q) => `- ${q}`).join("<br>")}`;
  tooltip.classList.remove("hidden");

  const rect = icon.getBoundingClientRect();
  tooltip.style.left = `${rect.left + 18}px`;
  tooltip.style.top = `${rect.bottom + 8}px`;
}

function onInfoOut(event) {
  if (!event.target.closest(".info-icon")) {
    return;
  }
  tooltip.classList.add("hidden");
}

init();

