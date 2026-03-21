// Converte yyyy-MM-dd para dd/MM/yyyy
function toPtBrDate(value) {
  if (!value) return "";
  if (/^\d{2}\/\d{2}\/\d{4}$/.test(value)) return value;
  if (/^\d{4}-\d{2}-\d{2}$/.test(value)) {
    const [year, month, day] = value.split("-");
    return `${day}/${month}/${year}`;
  }
  return value;
}

// Converte dd/MM/yyyy para yyyy-MM-dd
function ptBrToIsoDate(value) {
  if (!value) return "";
  const match = value.match(/^(\d{2})\/(\d{2})\/(\d{4})$/);
  if (match) {
    const [, day, month, year] = match;
    return `${year}-${month}-${day}`;
  }
  return value;
}
import { useEffect, useMemo, useRef, useState } from "react";

const TAB_ORDER = [
  "Informacoes Pessoais",
  "Classificacao de Perfil",
  "CHAVE",
  "GROW / PDI",
  "SWOT",
  "Cultura",
  "Feedbacks",
  "1:1"
];

const PROPERTY_SECTION_CONFIG = {
  "Classificacao de Perfil": {
    secaoAliases: ["ClassificacaoDePerfil", "Classificacao", "Perfil"],
    properties: [
      { label: "DISC", tooltipKey: "disc" },
      { label: "Personalidade", tooltipKey: "personalidade" },
      { label: "Nine Box", tooltipKey: "nineBox" }
    ]
  },
  CHAVE: {
    secaoAliases: ["CHAVE"],
    properties: [
      { label: "Conhecimentos", tooltipKey: "conhecimentos" },
      { label: "Habilidades", tooltipKey: "habilidades" },
      { label: "Atitudes", tooltipKey: "atitudes" },
      { label: "Valores", tooltipKey: "valores" },
      { label: "Expectativas", tooltipKey: "expectativas" }
    ]
  },
  "GROW / PDI": {
    secaoAliases: ["GROW", "PDI", "GrowPdi"],
    properties: [
      { label: "Metas", tooltipKey: "metas" },
      { label: "Situacao Atual", tooltipKey: "situacaoAtual" },
      { label: "Opcoes", tooltipKey: "opcoes" },
      { label: "Proximos Passos", tooltipKey: "proximosPassos" }
    ]
  },
  SWOT: {
    secaoAliases: ["SWOT"],
    properties: [
      { label: "Fortalezas", tooltipKey: "fortalezas" },
      { label: "Oportunidades", tooltipKey: "oportunidades" },
      { label: "Fraquezas", tooltipKey: "fraquezas" },
      { label: "Ameacas", tooltipKey: "ameacas" }
    ]
  }
};

const DEFAULT_TOOLTIPS = {
  default: [
    "O que mudou desde o ultimo registro?",
    "Que evidencia concreta sustenta esta percepcao?"
  ],
  conhecimentos: [
    "Em quais temas tecnicos voce se sente mais confiante hoje?",
    "Quais lacunas tecnicas voce quer fechar no proximo trimestre?"
  ],
  habilidades: [
    "Qual habilidade comportamental mais impactou seu trabalho recente?",
    "Em que situacao voce sentiu que faltou repertorio?"
  ],
  atitudes: ["Como voce reage quando recebe feedback dificil?"],
  valores: ["Que tipo de ambiente de trabalho potencializa seu desempenho?"],
  expectativas: ["Que evolucao de carreira voce espera nos proximos 12 meses?"],
  metas: ["Quais metas pessoais/profissionais sao mais importantes agora?"],
  proximosPassos: ["Qual sera seu proximo passo concreto ate o proximo 1:1?"],
  redFlags: ["Existe algum fator de risco comportamental que merece atencao?"]
};

function formatDateDigitsToMask(rawValue) {
  const digits = String(rawValue || "").replace(/\D/g, "").slice(0, 8);
  if (digits.length <= 2) {
    return digits;
  }
  if (digits.length <= 4) {
    return `${digits.slice(0, 2)}/${digits.slice(2)}`;
  }
  return `${digits.slice(0, 2)}/${digits.slice(2, 4)}/${digits.slice(4)}`;
}

function toDisplayDate(value) {
  if (!value) {
    return "";
  }

  const text = String(value).trim();
  if (/^\d{4}-\d{2}-\d{2}$/.test(text)) {
    const [year, month, day] = text.split("-");
    return `${day}/${month}/${year}`;
  }

  return formatDateDigitsToMask(text);
}

function toIsoDate(value) {
  if (!value) {
    return null;
  }

  const text = String(value).trim();
  if (/^\d{4}-\d{2}-\d{2}$/.test(text)) {
    return text;
  }

  const match = text.match(/^(\d{2})\/(\d{2})\/(\d{4})$/);
  if (!match) {
    return null;
  }

  const [, day, month, year] = match;
  return `${year}-${month}-${day}`;
}

async function requestJson(path, options) {
  const response = await fetch(path, {
    headers: {
      "Content-Type": "application/json"
    },
    ...options
  });

  if (!response.ok) {
    let errorMessage = `Erro ${response.status}`;
    try {
      const payload = await response.json();
      if (payload?.erro) {
        errorMessage = payload.erro;
      }
    } catch {
      // noop
    }
    throw new Error(errorMessage);
  }

  if (response.status === 204) {
    return null;
  }

  return response.json();
}

function RadarChart({ values, id, className }) {
  const labels = ["Aprender", "Dono", "Cliente", "Equipe", "Excelencia", "Acontecer", "Inovar"];
  const cx = 130;
  const cy = 110;
  const radius = 84;

  const layers = [1, 2, 3, 4].map((layer) => {
    const r = (radius / 4) * layer;
    const points = labels
      .map((_, index) => {
        const angle = (Math.PI * 2 * index) / labels.length - Math.PI / 2;
        return `${cx + r * Math.cos(angle)},${cy + r * Math.sin(angle)}`;
      })
      .join(" ");
    return points;
  });

  const spokes = labels.map((label, index) => {
    const angle = (Math.PI * 2 * index) / labels.length - Math.PI / 2;
    const x = cx + radius * Math.cos(angle);
    const y = cy + radius * Math.sin(angle);
    const tx = cx + (radius + 14) * Math.cos(angle);
    const ty = cy + (radius + 14) * Math.sin(angle);
    return { label, x, y, tx, ty };
  });

  const polygonPoints = labels
    .map((_, index) => {
      const angle = (Math.PI * 2 * index) / labels.length - Math.PI / 2;
      const score = Math.max(0, Math.min(values[index] || 0, 10));
      const r = (radius * score) / 10;
      return `${cx + r * Math.cos(angle)},${cy + r * Math.sin(angle)}`;
    })
    .join(" ");

  return (
    <svg id={id} className={className} viewBox="0 0 260 220" aria-label="Grafico radar cultural">
      {layers.map((points) => (
        <polygon key={points} points={points} fill="none" stroke="#334155" strokeOpacity="0.5" />
      ))}

      {spokes.map((spoke) => (
        <g key={spoke.label}>
          <line x1={cx} y1={cy} x2={spoke.x} y2={spoke.y} stroke="#334155" strokeOpacity="0.7" />
          <text x={spoke.tx} y={spoke.ty} fill="#cbd5e1" fontSize="10" textAnchor="middle">
            {spoke.label}
          </text>
        </g>
      ))}

      <polygon points={polygonPoints} fill="rgba(59, 130, 246, 0.35)" stroke="#60a5fa" strokeWidth="2" />
    </svg>
  );
}

function MaskedDateInput({ value, onChange, className = "", placeholder = "dd/MM/yyyy", ariaLabel }) {
  const [internalValue, setInternalValue] = useState("");
  const isControlled = value !== undefined;
  const currentValue = isControlled ? toDisplayDate(value) : internalValue;

  useEffect(() => {
    if (!isControlled) {
      return;
    }
    setInternalValue(toDisplayDate(value));
  }, [isControlled, value]);

  return (
    <input
      type="text"
      inputMode="numeric"
      maxLength={10}
      className={className}
      placeholder={placeholder}
      aria-label={ariaLabel}
      value={currentValue}
      onChange={(event) => {
        const masked = formatDateDigitsToMask(event.target.value);
        console.log('[DEBUG] MaskedDateInput onChange:', event.target.value, 'masked:', masked);
        if (isControlled) {
          onChange?.(masked);
          return;
        }
        setInternalValue(masked);
      }}
    />
  );
}

function PropertyTabsSection({ groups, renderInfoIcon, classificacaoPerfilDraft, setClassificacaoPerfilDraft, isClassificacaoPerfil }) {
  const [activePropertyIndex, setActivePropertyIndex] = useState(0);

  useEffect(() => {
    setActivePropertyIndex(0);
  }, [groups]);

  return (
    <div className="prop-tabs">
      <div className="prop-tabs-bar">
        {groups.map((group, index) => (
          <button
            key={group.label}
            type="button"
            className={`prop-tab-btn ${index === activePropertyIndex ? "active" : ""}`}
            onClick={() => setActivePropertyIndex(index)}
          >
            {group.label}
          </button>
        ))}
      </div>

      {groups.map((group, index) => (
        <div key={group.label} className={`prop-tab-panel ${index === activePropertyIndex ? "active" : ""}`}>
          <table className="history-table classification-table" style={{ tableLayout: 'fixed', width: '100%' }}>
            <thead>
              <tr>
                <th className="col-date" style={{ width: '150px', minWidth: '150px', maxWidth: '150px' }}>Data</th>
                <th className="col-value">
                  {group.label} {renderInfoIcon(group.label, group.tooltipKey)}
                </th>
              </tr>
            </thead>
            <tbody>
              <tr className="history-edit">
                <td className="date-cell">
                  {group.tooltipKey === "dataDisc" ? (
                    <input
                      type="text"
                      className="date-input"
                      aria-label={`Data do registro de ${group.label}`}
                      value={classificacaoPerfilDraft.dataDisc || ""}
                      onChange={e => {
                        const nextValue = e.target.value;
                        setClassificacaoPerfilDraft(draft => ({ ...draft, dataDisc: nextValue }));
                      }}
                      placeholder="dd/MM/yyyy"
                    />
                  ) : (
                    <MaskedDateInput className="date-input" ariaLabel={`Data do registro de ${group.label}`} />
                  )}
                </td>
                <td>
                  {isClassificacaoPerfil && group.tooltipKey !== "dataDisc" ? (
                    <textarea
                      rows="2"
                      placeholder={`Registrar ${group.label}`}
                      value={classificacaoPerfilDraft[group.tooltipKey] || ""}
                      onChange={e => setClassificacaoPerfilDraft(draft => ({ ...draft, [group.tooltipKey]: e.target.value }))}
                    />
                  ) : (
                    <textarea rows="2" placeholder={`Registrar ${group.label}`} disabled={group.tooltipKey === "dataDisc"} />
                  )}
                </td>
              </tr>
              {group.rows.map((row) => (
                <tr key={`${row.data}-${row.valor}`}>
                  <td>{row.data}</td>
                  <td>{row.valor}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      ))}
    </div>
  );
}

function App() {
  const [view, setView] = useState("dashboard");
  const [loading, setLoading] = useState(true);
  const [loadingLeader, setLoadingLeader] = useState(false);
  const [error, setError] = useState("");

  const [dashboardCards, setDashboardCards] = useState([]);
  const [liderados, setLiderados] = useState([]);
  const [selectedLideradoId, setSelectedLideradoId] = useState("");
  const [activeTab, setActiveTab] = useState(TAB_ORDER[0]);

  const [leaderView, setLeaderView] = useState(null);
  const [leaderReloadKey, setLeaderReloadKey] = useState(0);
  const [feedbacks, setFeedbacks] = useState([]);
  const [oneOnOnes, setOneOnOnes] = useState([]);
  const [cultureEntries, setCultureEntries] = useState([]);
  const [cultureIndex, setCultureIndex] = useState(0);
  const [radarValues, setRadarValues] = useState([0, 0, 0, 0, 0, 0, 0]);

  const [personalForm, setPersonalForm] = useState({
    nome: "",
    dataNascimento: "",
    estadoCivil: "",
    quantidadeFilhos: "",
    dataContratacao: "",
    cargo: "",
    dataInicioCargo: "",
    aspiracaoCarreira: "",
    gostosPessoais: "",
    redFlags: "",
    bio: ""
  });

  const [feedbackDraft, setFeedbackDraft] = useState({ data: "", conteudo: "", receptividade: "", polaridade: "Positivo" });
  const [oneOnOneDraft, setOneOnOneDraft] = useState({ data: "", resumo: "", tarefasAcordadas: "", proximosAssuntos: "" });
  const [cultureDraft, setCultureDraft] = useState({
    data: "",
    aprenderEMelhorarSempre: "",
    atitudeDeDono: "",
    buscarMelhoresResultadosParaClientes: "",
    espiritoDeEquipe: "",
    excelencia: "",
    fazerAcontecer: "",
    inovarParaInspirar: ""
  });

  const [tooltipMap, setTooltipMap] = useState({});
  const [hoverTooltip, setHoverTooltip] = useState({ visible: false, x: 0, y: 0, text: "" });
  const [tooltipModal, setTooltipModal] = useState({ open: false, key: "", label: "", text: "" });

  const animationFrameRef = useRef(null);

  const [classificacaoPerfilDraft, setClassificacaoPerfilDraft] = useState({
    disc: "",
    personalidade: "",
    nineBox: "",
    dataDisc: ""
  });

  useEffect(() => {
    let active = true;

    async function load() {
      setLoading(true);
      setError("");

      try {
        const [dashboard, lideradosList] = await Promise.all([
          requestJson("/api/dashboard/"),
          requestJson("/api/liderados/")
        ]);

        if (!active) {
          return;
        }

        const cards = dashboard?.cards || [];
        setDashboardCards(cards);
        setLiderados(lideradosList || []);

        const firstId = cards[0]?.lideradoId || lideradosList?.[0]?.id;
        if (firstId) {
          setSelectedLideradoId(String(firstId));
        }
      } catch (loadError) {
        if (active) {
          setError(loadError.message);
        }
      } finally {
        if (active) {
          setLoading(false);
        }
      }
    }

    load();
    return () => {
      active = false;
    };
  }, []);

  useEffect(() => {
    if (!selectedLideradoId) {
      return;
    }

    let active = true;

    async function loadLeader() {
      setLoadingLeader(true);
      setError("");

      try {
        const [visao, feedbackResponse, oneOnOneResponse] = await Promise.all([
          requestJson(`/api/liderados/${selectedLideradoId}/visao-individual`),
          requestJson(`/api/liderados/${selectedLideradoId}/feedbacks/`),
          requestJson(`/api/liderados/${selectedLideradoId}/one-on-ones/`)
        ]);

        const datas = visao?.conteudo?.datasAvaliacaoCultura || [];
        const radarResponses = await Promise.all(
          datas.map((data) => requestJson(`/api/liderados/${selectedLideradoId}/cultura/radar?data=${data}`))
        );

        if (!active) {
          return;
        }

        const nextCultureEntries = radarResponses.map((item) => item.radar).filter(Boolean);

        setLeaderView(visao?.conteudo || null);
        setFeedbacks(feedbackResponse?.registros || []);
        setOneOnOnes(oneOnOneResponse?.registros || []);
        setCultureEntries(nextCultureEntries);
        setCultureIndex(0);
        setActiveTab(TAB_ORDER[0]);

        const informacoes = visao?.conteudo?.informacoesPessoais;
        setPersonalForm({
          nome: informacoes?.nome || "",
          dataNascimento: toDisplayDate(informacoes?.dataNascimento),
          estadoCivil: informacoes?.estadoCivil || "",
          quantidadeFilhos: informacoes?.quantidadeFilhos ?? "",
          dataContratacao: toDisplayDate(informacoes?.dataContratacao),
          cargo: informacoes?.cargo || "",
          dataInicioCargo: toDisplayDate(informacoes?.dataInicioCargo),
          aspiracaoCarreira: informacoes?.aspiracaoCarreira || "",
          gostosPessoais: informacoes?.gostosPessoais || "",
          redFlags: informacoes?.redFlags || "",
          bio: informacoes?.bio || ""
        });

        // Initialize classificacaoPerfilDraft from leaderView/classificacaoPerfil
        const classificacao = visao?.conteudo?.classificacaoPerfil || visao?.conteudo || {};
        setClassificacaoPerfilDraft({
          disc: classificacao.disc || "",
          personalidade: classificacao.perfil || classificacao.personalidade || "",
          nineBox: classificacao.nineBox || "",
          dataDisc: toDisplayDate(classificacao.dataDisc || classificacao.Data || classificacao.dataAtualizacaoUtc || "")
        });
      } catch (loadError) {
        if (active) {
          setError(loadError.message);
        }
      } finally {
        if (active) {
          setLoadingLeader(false);
        }
      }
    }

    loadLeader();
    return () => {
      active = false;
    };
  }, [selectedLideradoId, leaderReloadKey]);

  useEffect(() => {
    const target = cultureEntries[cultureIndex] || null;
    const nextValues = target
      ? [
          target.aprenderEMelhorarSempre,
          target.atitudeDeDono,
          target.buscarMelhoresResultadosParaClientes,
          target.espiritoDeEquipe,
          target.excelencia,
          target.fazerAcontecer,
          target.inovarParaInspirar
        ]
      : [0, 0, 0, 0, 0, 0, 0];

    if (!radarValues || radarValues.length === 0) {
      setRadarValues(nextValues);
      return;
    }

    if (animationFrameRef.current) {
      cancelAnimationFrame(animationFrameRef.current);
      animationFrameRef.current = null;
    }

    const startValues = [...radarValues];
    const duration = 320;
    const start = performance.now();

    const step = (now) => {
      const progress = Math.min(1, (now - start) / duration);
      const eased = 1 - Math.pow(1 - progress, 2);
      const values = startValues.map((value, index) => value + (nextValues[index] - value) * eased);
      setRadarValues(values);

      if (progress < 1) {
        animationFrameRef.current = requestAnimationFrame(step);
      }
    };

    animationFrameRef.current = requestAnimationFrame(step);
    return () => {
      if (animationFrameRef.current) {
        cancelAnimationFrame(animationFrameRef.current);
        animationFrameRef.current = null;
      }
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [cultureEntries, cultureIndex]);

  const selectedLideradoNome = useMemo(() => {
    const byCard = dashboardCards.find((card) => String(card.lideradoId) === selectedLideradoId)?.nome;
    if (byCard) {
      return byCard;
    }
    return liderados.find((item) => String(item.id) === selectedLideradoId)?.nome || "Liderado selecionado";
  }, [dashboardCards, liderados, selectedLideradoId]);

  const summaryMetrics = useMemo(() => {
    const card = dashboardCards.find((item) => String(item.lideradoId) === selectedLideradoId);
    return {
      perfil: leaderView?.perfil || card?.perfil || "-",
      nineBox: leaderView?.nineBox || card?.nineBox || "-",
      feedbacks: Number.isInteger(leaderView?.quantidadeFeedbacks)
        ? leaderView.quantidadeFeedbacks
        : card?.quantidadeFeedbacks || 0,
      oneOnOnes: Number.isInteger(leaderView?.quantidadeOneOnOnes)
        ? leaderView.quantidadeOneOnOnes
        : card?.quantidadeOneOnOnes || 0,
      notaGeral: card?.notaGeral ?? "-"
    };
  }, [dashboardCards, leaderView, selectedLideradoId]);

  const propertySectionData = useMemo(() => {
    const buildRows = (sectionName) => {
      const config = PROPERTY_SECTION_CONFIG[sectionName];
      if (!config) {
        return [];
      }
      return [];
    };
    return Object.fromEntries(
      Object.keys(PROPERTY_SECTION_CONFIG).map((sectionName) => [
        sectionName,
        PROPERTY_SECTION_CONFIG[sectionName].properties.map((property) => ({
          ...property,
          rows: buildRows(sectionName)
        }))
      ])
    );
  }, []);

  const dashboardCardsWithFallbackRadar = useMemo(() => {
    return dashboardCards.map((card) => {
      const radar = card.ultimaAvaliacaoCultura
        ? [
            card.ultimaAvaliacaoCultura.aprenderEMelhorarSempre,
            card.ultimaAvaliacaoCultura.atitudeDeDono,
            card.ultimaAvaliacaoCultura.buscarMelhoresResultadosParaClientes,
            card.ultimaAvaliacaoCultura.espiritoDeEquipe,
            card.ultimaAvaliacaoCultura.excelencia,
            card.ultimaAvaliacaoCultura.fazerAcontecer,
            card.ultimaAvaliacaoCultura.inovarParaInspirar
          ]
        : [5, 5, 5, 5, 5, 5, 5];

      return { ...card, radar };
    });
  }, [dashboardCards]);

  async function refreshCurrentLeader() {
    if (!selectedLideradoId) {
      return;
    }

    const [dashboard] = await Promise.all([requestJson("/api/dashboard/")]);
    setDashboardCards(dashboard?.cards || []);
    setSelectedLideradoId((current) => current || selectedLideradoId);
  }

  async function handleCreateLiderado() {
    const nome = window.prompt("Nome do novo liderado:");
    if (!nome) {
      return;
    }

    try {
      await requestJson("/api/liderados/", {
        method: "POST",
        body: JSON.stringify({ nome })
      });

      const [dashboard, lideradosList] = await Promise.all([
        requestJson("/api/dashboard/"),
        requestJson("/api/liderados/")
      ]);
      setDashboardCards(dashboard?.cards || []);
      setLiderados(lideradosList || []);
    } catch (saveError) {
      setError(saveError.message);
    }
  }

  async function handleDeleteLiderado() {
    if (!selectedLideradoId) {
      return;
    }

    const confirmado = window.confirm(
      `Tem certeza que deseja excluir o liderado "${selectedLideradoNome}"? Esta acao nao pode ser desfeita.`
    );

    if (!confirmado) {
      return;
    }

    try {
      await requestJson(`/api/liderados/${selectedLideradoId}`, {
        method: "DELETE"
      });

      const [dashboard, lideradosList] = await Promise.all([
        requestJson("/api/dashboard/"),
        requestJson("/api/liderados/")
      ]);

      const cards = dashboard?.cards || [];
      const nextLiderados = lideradosList || [];
      const nextId = cards[0]?.lideradoId || nextLiderados[0]?.id || "";

      setDashboardCards(cards);
      setLiderados(nextLiderados);
      setSelectedLideradoId(nextId ? String(nextId) : "");
      setView("dashboard");
      setLeaderReloadKey((value) => value + 1);
    } catch (deleteError) {
      setError(deleteError.message);
    }
  }

  async function handleSaveInformacoesPessoais() {
    if (!selectedLideradoId) {
      return;
    }

    try {
      await requestJson(`/api/liderados/${selectedLideradoId}/informacoes-pessoais`, {
        method: "PUT",
        body: JSON.stringify({
          nome: personalForm.nome,
          dataNascimento: toIsoDate(personalForm.dataNascimento),
          estadoCivil: personalForm.estadoCivil || null,
          quantidadeFilhos: personalForm.quantidadeFilhos === "" ? null : Number(personalForm.quantidadeFilhos),
          dataContratacao: toIsoDate(personalForm.dataContratacao),
          cargo: personalForm.cargo || null,
          dataInicioCargo: toIsoDate(personalForm.dataInicioCargo),
          aspiracaoCarreira: personalForm.aspiracaoCarreira || null,
          gostosPessoais: personalForm.gostosPessoais || null,
          redFlags: personalForm.redFlags || null,
          bio: personalForm.bio || null
        })
      });

      await refreshCurrentLeader();
      window.alert("Informacoes pessoais salvas.");
    } catch (saveError) {
      setError(saveError.message);
    }
  }

  async function handleSaveFeedback() {
    if (!selectedLideradoId) {
      return;
    }

    try {
      await requestJson(`/api/liderados/${selectedLideradoId}/feedbacks/`, {
        method: "POST",
        body: JSON.stringify({
          ...feedbackDraft,
          data: toIsoDate(feedbackDraft.data)
        })
      });

      setFeedbackDraft({ data: "", conteudo: "", receptividade: "", polaridade: "Positivo" });
      setLeaderReloadKey((value) => value + 1);
      await refreshCurrentLeader();
    } catch (saveError) {
      setError(saveError.message);
    }
  }

  async function handleSaveOneOnOne() {
    if (!selectedLideradoId) {
      return;
    }

    try {
      await requestJson(`/api/liderados/${selectedLideradoId}/one-on-ones/`, {
        method: "POST",
        body: JSON.stringify({
          ...oneOnOneDraft,
          data: toIsoDate(oneOnOneDraft.data)
        })
      });

      setOneOnOneDraft({ data: "", resumo: "", tarefasAcordadas: "", proximosAssuntos: "" });
      setLeaderReloadKey((value) => value + 1);
      await refreshCurrentLeader();
    } catch (saveError) {
      setError(saveError.message);
    }
  }

  async function handleSaveCultura() {
    if (!selectedLideradoId) {
      return;
    }

    try {
      await requestJson(`/api/liderados/${selectedLideradoId}/cultura/`, {
        method: "POST",
        body: JSON.stringify({
          data: toIsoDate(cultureDraft.data),
          aprenderEMelhorarSempre: Number(cultureDraft.aprenderEMelhorarSempre || 0),
          atitudeDeDono: Number(cultureDraft.atitudeDeDono || 0),
          buscarMelhoresResultadosParaClientes: Number(cultureDraft.buscarMelhoresResultadosParaClientes || 0),
          espiritoDeEquipe: Number(cultureDraft.espiritoDeEquipe || 0),
          excelencia: Number(cultureDraft.excelencia || 0),
          fazerAcontecer: Number(cultureDraft.fazerAcontecer || 0),
          inovarParaInspirar: Number(cultureDraft.inovarParaInspirar || 0)
        })
      });

      setCultureDraft({
        data: "",
        aprenderEMelhorarSempre: "",
        atitudeDeDono: "",
        buscarMelhoresResultadosParaClientes: "",
        espiritoDeEquipe: "",
        excelencia: "",
        fazerAcontecer: "",
        inovarParaInspirar: ""
      });
      setLeaderReloadKey((value) => value + 1);
      await refreshCurrentLeader();
    } catch (saveError) {
      setError(saveError.message);
    }
  }

  async function handleSaveClassificacaoPerfil() {
            console.log('[DEBUG] Draft completo:', classificacaoPerfilDraft);
            console.log('[DEBUG] Valor de dataDisc no envio:', classificacaoPerfilDraft.dataDisc, 'Tipo:', typeof classificacaoPerfilDraft.dataDisc);
    setError("");
    if (!selectedLideradoId) {
      setError("Nenhum liderado selecionado.");
      return;
    }
    if (!classificacaoPerfilDraft.dataDisc || !/^\d{2}\/\d{2}\/\d{4}$/.test(classificacaoPerfilDraft.dataDisc)) {
      setError("Por favor, preencha a data do DISC no formato dd/MM/aaaa.");
      return;
    }
    try {
      await requestJson(`/api/liderados/${selectedLideradoId}/classificacao-perfil`, {
        method: "PUT",
        body: JSON.stringify({
          perfil: classificacaoPerfilDraft.personalidade, // Fix: send as 'perfil' not 'personalidade'
          nineBox: classificacaoPerfilDraft.nineBox,
          disc: classificacaoPerfilDraft.disc,
          Data: ptBrToIsoDate(classificacaoPerfilDraft.dataDisc)
        })
      });
      setError("");
      setLeaderReloadKey((k) => k + 1);
    } catch (e) {
      setError(e.message);
    }
  }

  async function handleSaveDisc() {
    setError("");
    if (!selectedLideradoId) {
      setError("Nenhum liderado selecionado.");
      return;
    }
    if (!classificacaoPerfilDraft.dataDisc || !/^[0-9]{2}\/[0-9]{2}\/[0-9]{4}$/.test(classificacaoPerfilDraft.dataDisc)) {
      setError("Por favor, preencha a data do DISC no formato dd/MM/aaaa.");
      return;
    }
    try {
      await requestJson(`/api/disc`, {
        method: "POST",
        body: JSON.stringify({
          lideradoId: selectedLideradoId,
          disc: classificacaoPerfilDraft.disc,
          data: classificacaoPerfilDraft.dataDisc
        })
      });
      setError("");
      setLeaderReloadKey((k) => k + 1);
    } catch (e) {
      setError(e.message);
    }
  }

  function getTooltipText(key) {
    const value = tooltipMap[key];
    if (value && value.trim()) {
      return value;
    }
    const fallback = DEFAULT_TOOLTIPS[key];
    if (Array.isArray(fallback)) {
      return fallback.join("\n");
    }
    return DEFAULT_TOOLTIPS.default.join("\n");
  }

  async function ensureTooltip(key) {
    if (!key || tooltipMap[key]) {
      return;
    }

    try {
      const response = await requestJson(`/api/tooltips/${encodeURIComponent(key)}`);
      setTooltipMap((prev) => ({ ...prev, [key]: response?.texto || "" }));
    } catch {
      // Not found keeps default value.
    }
  }

  async function openTooltipModal(key, label) {
    await ensureTooltip(key);
    setTooltipModal({ open: true, key, label, text: getTooltipText(key) });
  }

  async function saveTooltipModal() {
    try {
      await requestJson(`/api/tooltips/${encodeURIComponent(tooltipModal.key)}`, {
        method: "PUT",
        body: JSON.stringify({ texto: tooltipModal.text })
      });

      setTooltipMap((prev) => ({ ...prev, [tooltipModal.key]: tooltipModal.text }));
      setTooltipModal({ open: false, key: "", label: "", text: "" });
    } catch (saveError) {
      setError(saveError.message);
    }
  }

  function handleSaveSectionWithoutEndpoint(sectionName) {
    setError(
      `A secao ${sectionName} ainda nao possui endpoint de gravacao no backend. ` +
        "O botao foi habilitado para manter o comportamento do prototipo."
    );
  }

  function renderInfoIcon(label, key) {
    return (
      <span
        className="info-icon"
        onMouseEnter={async (event) => {
          await ensureTooltip(key);
          const rect = event.currentTarget.getBoundingClientRect();
          setHoverTooltip({
            visible: true,
            x: rect.left + 18,
            y: rect.bottom + 8,
            text: getTooltipText(key)
          });
        }}
        onMouseLeave={() => setHoverTooltip((prev) => ({ ...prev, visible: false }))}
        onDoubleClick={(event) => {
          event.preventDefault();
          openTooltipModal(key, label);
        }}
      >
        i
      </span>
    );
  }

  if (loading) {
    return (
      <main className="layout">
        <p className="muted">Carregando frontend...</p>
      </main>
    );
  }

  return (
    <>
      <header className="topbar" id="app-header">
        <div id="app-header-title">
          <h1 id="app-header-h1">People Management</h1>
          <p className="subtitle" id="app-header-subtitle">Dashboard + visao individual</p>
        </div>
      </header>

      <main className="layout" id="app-main">
        {error ? (
          <div className="panel error-panel" id="app-error-panel">
            <div className="error-panel-header">
              <span>{error}</span>
              <button
                type="button"
                className="btn ghost small"
                aria-label="Fechar mensagem de erro"
                onClick={() => setError("")}
              >
                Fechar
              </button>
            </div>
          </div>
        ) : null}

        <nav className={`breadcrumb ${view === "leader" ? "" : "hidden"}`} aria-label="Navegacao" id="app-breadcrumb">
          <button type="button" className="breadcrumb-link" id="app-breadcrumb-dashboard" onClick={() => setView("dashboard")}>Dashboard</button>
          <span className="breadcrumb-sep" id="app-breadcrumb-sep">|</span>
          <select
            className="radar-date"
            id="app-breadcrumb-liderado-select"
            value={selectedLideradoId}
            onChange={(event) => setSelectedLideradoId(event.target.value)}
          >
            {liderados.map((liderado) => (
              <option key={liderado.id} value={liderado.id} id={`app-breadcrumb-liderado-option-${liderado.id}`}>
                {liderado.nome}
              </option>
            ))}
          </select>
        </nav>

        <section className={`view ${view === "dashboard" ? "" : "hidden"}`}>
          <div className="panel">
            <div className="panel-header">
              <h2>Dashboard</h2>
              <button className="btn" type="button" onClick={handleCreateLiderado}>
                + Novo liderado
              </button>
            </div>
            <p className="muted">Visao consolidada com indicadores principais.</p>
            <div className="dashboard-summary-cards">
              {dashboardCardsWithFallbackRadar.map((card) => (
                <article
                  key={card.lideradoId}
                  className="dashboard-summary-card"
                  onClick={() => {
                    setSelectedLideradoId(String(card.lideradoId));
                    setView("leader");
                  }}
                >
                  <h3>{card.nome}</h3>
                  <div className="summary">
                    <div className="kpi">
                      <div className="kpi-label">Perfil</div>
                      <div className="kpi-value">{card.perfil || "-"}</div>
                    </div>
                    <div className="kpi">
                      <div className="kpi-label">Nine Box</div>
                      <div className="kpi-value">{card.nineBox || "-"}</div>
                    </div>
                    <div className="kpi">
                      <div className="kpi-label">Feedbacks</div>
                      <div className="kpi-value">{card.quantidadeFeedbacks}</div>
                    </div>
                    <div className="kpi">
                      <div className="kpi-label">1:1</div>
                      <div className="kpi-value">{card.quantidadeOneOnOnes}</div>
                    </div>
                    <div className="kpi">
                      <div className="kpi-label">Nota geral</div>
                      <div className="kpi-value">{card.notaGeral ?? "-"}</div>
                    </div>
                  </div>
                  <h4>Radar Cultural</h4>
                  <RadarChart id={`dashboardRadar-${card.lideradoId}`} className="dashboard-radar" values={card.radar} />
                </article>
              ))}
            </div>
          </div>
        </section>

        <section id="leaderView" className={`view ${view === "leader" ? "" : "hidden"}`}>
          <aside className="left-column">
            <div className="panel sticky">
              <div className="panel-header">
                <h2>{selectedLideradoNome}</h2>
                <button type="button" className="btn danger small" onClick={handleDeleteLiderado}>
                  Excluir
                </button>
              </div>
              <div className="summary">
                <div className="kpi">
                  <div className="kpi-label">Perfil</div>
                  <div className="kpi-value">{summaryMetrics.perfil}</div>
                </div>
                <div className="kpi">
                  <div className="kpi-label">Nine Box</div>
                  <div className="kpi-value">{summaryMetrics.nineBox}</div>
                </div>
                <div className="kpi">
                  <div className="kpi-label">Feedbacks</div>
                  <div className="kpi-value">{summaryMetrics.feedbacks}</div>
                </div>
                <div className="kpi">
                  <div className="kpi-label">1:1</div>
                  <div className="kpi-value">{summaryMetrics.oneOnOnes}</div>
                </div>
                <div className="kpi">
                  <div className="kpi-label">Nota geral</div>
                  <div className="kpi-value">{summaryMetrics.notaGeral}</div>
                </div>
              </div>

              <h3 className="radar-title">
                Radar Cultural
                <select
                  className="radar-date"
                  value={String(cultureIndex)}
                  onChange={(event) => setCultureIndex(Number(event.target.value))}
                  onWheel={(event) => {
                    if (!cultureEntries.length) {
                      return;
                    }
                    event.preventDefault();
                    const direction = event.deltaY > 0 ? 1 : -1;
                    const next = Math.max(0, Math.min(cultureIndex + direction, cultureEntries.length - 1));
                    setCultureIndex(next);
                  }}
                  disabled={!cultureEntries.length}
                >
                  {cultureEntries.length === 0 ? <option value="0">Sem avaliacoes</option> : null}
                  {cultureEntries.map((item, index) => (
                    <option key={item.data} value={index}>
                      {item.data}
                    </option>
                  ))}
                </select>
              </h3>

              <RadarChart id="radar" className="radar" values={radarValues} />
              <p className="muted small">
                Dimensoes: Aprender e Melhorar Sempre, Atitude de Dono, Cliente, Espirito de Equipe, Excelencia,
                Fazer Acontecer e Inovar para Inspirar.
              </p>
            </div>
          </aside>

          <section className="right-column">
            <div className="tabs-bar">
              {TAB_ORDER.map((tab) => (
                <button
                  key={tab}
                  type="button"
                  className={`tab-btn ${tab === activeTab ? "active" : ""}`}
                  onClick={() => setActiveTab(tab)}
                >
                  {tab}
                </button>
              ))}
            </div>

            <div className="tabs-content">
              {loadingLeader ? <div className="panel muted">Carregando liderado...</div> : null}

              {activeTab === "Informacoes Pessoais" ? (
                <section className="panel section single-column">
                  <div className="panel-header">
                    <h3 className="section-title">Informacoes Pessoais</h3>
                    <button type="button" className="btn ghost small" onClick={handleSaveInformacoesPessoais}>
                      Salvar
                    </button>
                  </div>

                  <div className="fields personal-info-grid">
                    <div className="personal-info-column personal-info-column--stack">
                      {[
                        ["Nome", "nome", "nome", "text"],
                        ["Data de nascimento", "dataNascimento", "dataNascimento", "date"],
                        ["Estado civil", "estadoCivil", "estadoCivil", "text"],
                        ["Quantidade de filhos", "quantidadeFilhos", "quantidadeFilhos", "number"],
                        ["Data de contratacao", "dataContratacao", "dataContratacao", "date"],
                        ["Cargo", "cargo", "cargo", "text"],
                        ["Data de inicio do cargo", "dataInicioCargo", "dataInicioCargo", "date"],
                        ["Aspiracao (Carreira Y)", "aspiracaoCarreira", "aspiracaoCarreira", "text"]
                      ].map(([label, key, tooltipKey, type]) => (
                        <div className="field" key={key}>
                          <label>
                            {label} {renderInfoIcon(label, tooltipKey)}
                          </label>
                          {type === "date" ? (
                            <MaskedDateInput
                              className="field-date-input"
                              id={`input-${key}`}
                              name={key}
                              value={personalForm[key]}
                              onChange={(nextValue) => setPersonalForm((prev) => ({ ...prev, [key]: nextValue }))}
                              ariaLabel={label}
                            />
                          ) : (
                            <input
                              type={type}
                              id={`input-${key}`}
                              name={key}
                              value={personalForm[key]}
                              onChange={(event) => setPersonalForm((prev) => ({ ...prev, [key]: event.target.value }))}
                            />
                          )}
                        </div>
                      ))}
                    </div>

                    <div className="personal-info-column personal-info-column--stack personal-info-secondary">
                      <div className="field">
                        <label>
                          Gostos pessoais {renderInfoIcon("Gostos pessoais", "gostosPessoais")}
                        </label>
                        <textarea
                          id="textarea-gostosPessoais"
                          name="gostosPessoais"
                          value={personalForm.gostosPessoais}
                          onChange={(event) => setPersonalForm((prev) => ({ ...prev, gostosPessoais: event.target.value }))}
                        />
                      </div>

                      <div className="field">
                        <label>
                          Red Flags {renderInfoIcon("Red Flags", "redFlags")}
                        </label>
                        <textarea
                          id="textarea-redFlags"
                          name="redFlags"
                          value={personalForm.redFlags}
                          onChange={(event) => setPersonalForm((prev) => ({ ...prev, redFlags: event.target.value }))}
                        />
                      </div>
                    </div>

                    <div className="personal-info-column personal-info-column--stack personal-info-bio">
                      <div className="field field-bio">
                        <label>
                          BIO {renderInfoIcon("BIO", "bio")}
                        </label>
                        <textarea
                          rows="15"
                          id="textarea-bio"
                          name="bio"
                          value={personalForm.bio}
                          onChange={(event) => setPersonalForm((prev) => ({ ...prev, bio: event.target.value }))}
                        />
                      </div>
                    </div>
                  </div>
                </section>
              ) : null}

              {activeTab === "1:1" ? (
                <section className="panel section single-column">
                  <div className="panel-header">
                    <h3 className="section-title">1:1</h3>
                    <button type="button" className="btn ghost small" onClick={handleSaveOneOnOne}>
                      Salvar
                    </button>
                  </div>
                  <div className="fields">
                    <table className="history-table" style={{ tableLayout: 'fixed', width: '100%' }}>
                      <thead>
                        <tr>
                          <th style={{ width: '150px', minWidth: '150px', maxWidth: '150px' }}>Data</th>
                          <th>Resumo</th>
                          <th>Tarefas acordadas</th>
                          <th>Proximos assuntos</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr className="history-edit">
                          <td className="date-cell">
                            <MaskedDateInput
                              className="date-input"
                              value={oneOnOneDraft.data}
                              onChange={(nextValue) => setOneOnOneDraft((prev) => ({ ...prev, data: nextValue }))}
                              ariaLabel="Data do 1:1"
                            />
                          </td>
                          <td>
                            <textarea
                              rows="2"
                              value={oneOnOneDraft.resumo}
                              onChange={(event) => setOneOnOneDraft((prev) => ({ ...prev, resumo: event.target.value }))}
                            />
                          </td>
                          <td>
                            <textarea
                              rows="2"
                              value={oneOnOneDraft.tarefasAcordadas}
                              onChange={(event) => setOneOnOneDraft((prev) => ({ ...prev, tarefasAcordadas: event.target.value }))}
                            />
                          </td>
                          <td>
                            <textarea
                              rows="2"
                              value={oneOnOneDraft.proximosAssuntos}
                              onChange={(event) => setOneOnOneDraft((prev) => ({ ...prev, proximosAssuntos: event.target.value }))}
                            />
                          </td>
                        </tr>
                        {oneOnOnes.map((item) => (
                          <tr key={`${item.data}-${item.resumo}`}>
                            <td>{item.data}</td>
                            <td>{item.resumo}</td>
                            <td>{item.tarefasAcordadas}</td>
                            <td>{item.proximosAssuntos}</td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  </div>
                </section>
              ) : null}

              {activeTab === "Feedbacks" ? (
                <section className="panel section single-column">
                  <div className="panel-header">
                    <h3 className="section-title">Feedbacks</h3>
                    <button type="button" className="btn ghost small" onClick={handleSaveFeedback}>
                      Salvar
                    </button>
                  </div>

                  <div className="fields">
                    <table className="history-table feedback-table" style={{ tableLayout: 'fixed', width: '100%' }}>
                      <thead>
                        <tr>
                          <th style={{ width: '150px', minWidth: '150px', maxWidth: '150px' }}>Data</th>
                          <th>Conteudo do feedback</th>
                          <th>Receptividade</th>
                          <th>Polaridade</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr className="history-edit">
                          <td className="date-cell">
                            <MaskedDateInput
                              className="date-input"
                              value={feedbackDraft.data}
                              onChange={(nextValue) => setFeedbackDraft((prev) => ({ ...prev, data: nextValue }))}
                              ariaLabel="Data do feedback"
                            />
                          </td>
                          <td>
                            <textarea
                              rows="2"
                              value={feedbackDraft.conteudo}
                              onChange={(event) => setFeedbackDraft((prev) => ({ ...prev, conteudo: event.target.value }))}
                            />
                          </td>
                          <td>
                            <textarea
                              rows="2"
                              value={feedbackDraft.receptividade}
                              onChange={(event) => setFeedbackDraft((prev) => ({ ...prev, receptividade: event.target.value }))}
                            />
                          </td>
                          <td>
                            <select
                              value={feedbackDraft.polaridade}
                              onChange={(event) => setFeedbackDraft((prev) => ({ ...prev, polaridade: event.target.value }))}
                            >
                              <option value="Positivo">Positivo</option>
                              <option value="Negativo">Negativo</option>
                            </select>
                          </td>
                        </tr>
                        {feedbacks.map((item) => (
                          <tr key={`${item.data}-${item.conteudo}`}>
                            <td>{item.data}</td>
                            <td>{item.conteudo}</td>
                            <td>{item.receptividade}</td>
                            <td>{item.polaridade}</td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  </div>
                </section>
              ) : null}

              {activeTab === "Cultura" ? (
                <section className="panel section single-column">
                  <div className="panel-header">
                    <h3 className="section-title">Cultura</h3>
                    <button type="button" className="btn ghost small" onClick={handleSaveCultura}>
                      Salvar
                    </button>
                  </div>

                  <div className="fields table-scroll">
                    <table className="history-table culture-table" style={{ tableLayout: 'fixed', width: '100%' }}>
                      <thead>
                        <tr>
                          <th style={{ width: '150px', minWidth: '150px', maxWidth: '150px' }}>Data</th>
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
                        <tr className="history-edit">
                          <td className="date-cell">
                            <MaskedDateInput
                              className="date-input"
                              value={cultureDraft.data}
                              onChange={(nextValue) => setCultureDraft((prev) => ({ ...prev, data: nextValue }))}
                              ariaLabel="Data da avaliacao de cultura"
                            />
                          </td>
                          {[
                            "aprenderEMelhorarSempre",
                            "atitudeDeDono",
                            "buscarMelhoresResultadosParaClientes",
                            "espiritoDeEquipe",
                            "excelencia",
                            "fazerAcontecer",
                            "inovarParaInspirar"
                          ].map((key) => (
                            <td key={key}>
                              <input
                                className="score-input"
                                type="number"
                                min="0"
                                max="10"
                                value={cultureDraft[key]}
                                onChange={(event) =>
                                  setCultureDraft((prev) => ({
                                    ...prev,
                                    [key]: event.target.value
                                  }))
                                }
                              />
                            </td>
                          ))}
                        </tr>
                        {cultureEntries.map((item) => (
                          <tr key={item.data}>
                            <td>{item.data}</td>
                            <td>{item.aprenderEMelhorarSempre}</td>
                            <td>{item.atitudeDeDono}</td>
                            <td>{item.buscarMelhoresResultadosParaClientes}</td>
                            <td>{item.espiritoDeEquipe}</td>
                            <td>{item.excelencia}</td>
                            <td>{item.fazerAcontecer}</td>
                            <td>{item.inovarParaInspirar}</td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  </div>
                </section>
              ) : null}

              {/* Fatos e Observacoes tab removed */}

              {["Classificacao de Perfil", "CHAVE", "GROW / PDI", "SWOT"].includes(activeTab) ? (
                <section className="panel section single-column">
                  <div className="panel-header">
                    <h3 className="section-title">{activeTab}</h3>
                    {activeTab === "Classificacao de Perfil" ? (
                      <button
                        type="button"
                        className="btn ghost small"
                        onClick={handleSaveClassificacaoPerfil}
                      >
                        Salvar
                      </button>
                    ) : (
                      <button
                        type="button"
                        className="btn ghost small"
                        onClick={() => handleSaveSectionWithoutEndpoint(activeTab)}
                      >
                        Salvar
                      </button>
                    )}
                  </div>
                  <div className="fields fields--prop-tabs">
                    <PropertyTabsSection
                      groups={propertySectionData[activeTab] || []}
                      renderInfoIcon={renderInfoIcon}
                      {...(activeTab === "Classificacao de Perfil" ? { classificacaoPerfilDraft, setClassificacaoPerfilDraft, isClassificacaoPerfil: true } : {})}
                    />
                  </div>
                </section>
              ) : null}
            </div>
          </section>
        </section>
      </main>

      <div
        id="tooltip"
        className={`tooltip ${hoverTooltip.visible ? "" : "hidden"}`}
        style={{ left: `${hoverTooltip.x}px`, top: `${hoverTooltip.y}px` }}
      >
        {hoverTooltip.text}
      </div>

      <dialog className={`modal-backdrop ${tooltipModal.open ? "" : "hidden"}`} open={tooltipModal.open}>
        <div className="modal-panel" role="document">
          <div className="modal-header">
            <h3>Editar tooltip - {tooltipModal.label}</h3>
          </div>
          <div className="modal-body">
            <label className="modal-label" htmlFor="tooltipInput">
              Texto do tooltip
            </label>
            <textarea
              id="tooltipInput"
              rows="8"
              value={tooltipModal.text}
              onChange={(event) => setTooltipModal((prev) => ({ ...prev, text: event.target.value }))}
            />
          </div>
          <div className="modal-actions">
            <button
              className="btn ghost"
              type="button"
              onClick={() => setTooltipModal({ open: false, key: "", label: "", text: "" })}
            >
              Cancelar
            </button>
            <button className="btn" type="button" onClick={saveTooltipModal}>
              Salvar
            </button>
          </div>
        </div>
      </dialog>
    </>
  );
}

export default App;

