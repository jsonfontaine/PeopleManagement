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

const TAB_LABELS = {
  "Classificacao de Perfil": "Perfil e Classificacao"
};

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
  situacaoAtual: ["Como voce descreveria o cenario atual com fatos e contexto?"],
  opcoes: ["Que caminhos viaveis existem para avancar a partir daqui?"],
  proximosPassos: ["Qual sera seu proximo passo concreto ate o proximo 1:1?"],
  fortalezas: ["Quais pontos fortes mais ajudam sua performance hoje?"],
  oportunidades: ["Que oportunidades podem acelerar seu desenvolvimento ou impacto?"],
  fraquezas: ["Que fragilidades estao limitando seus resultados neste momento?"],
  ameacas: ["Que riscos externos ou internos podem prejudicar sua evolucao?"],
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

function buildDiscDateErrorMessage(value) {
  const typedValue = String(value || "").trim();
  if (!typedValue) {
    return "Informe a data do DISC no formato dd/MM/aaaa (ex.: 27/11/2025).";
  }

  return `Nao consegui entender a data "${typedValue}". Use o formato dd/MM/aaaa (ex.: 27/11/2025).`;
}

const RICH_TEXT_ALLOWED_TAGS = new Set([
  "B",
  "STRONG",
  "I",
  "EM",
  "U",
  "S",
  "BR",
  "P",
  "DIV",
  "UL",
  "OL",
  "LI",
  "BLOCKQUOTE",
  "A"
]);

function escapeHtml(value) {
  return String(value || "")
    .replace(/&/g, "&amp;")
    .replace(/</g, "&lt;")
    .replace(/>/g, "&gt;")
    .replace(/"/g, "&quot;")
    .replace(/'/g, "&#39;");
}

function convertPlainTextToHtml(value) {
  return escapeHtml(value).replace(/\r?\n/g, "<br>");
}

function sanitizeRichText(value) {
  const rawHtml = String(value || "");
  if (!rawHtml.trim()) {
    return "";
  }

  const parser = new DOMParser();
  const doc = parser.parseFromString(`<div>${rawHtml}</div>`, "text/html");
  const root = doc.body.firstElementChild;
  if (!root) {
    return "";
  }

  const sanitizeNode = (node) => {
    if (node.nodeType === Node.TEXT_NODE) {
      return node.textContent || "";
    }

    if (node.nodeType !== Node.ELEMENT_NODE) {
      return "";
    }

    const element = node;
    const tag = element.tagName.toUpperCase();
    const sanitizedChildren = Array.from(element.childNodes).map(sanitizeNode).join("");

    if (!RICH_TEXT_ALLOWED_TAGS.has(tag)) {
      return sanitizedChildren;
    }

    if (tag === "BR") {
      return "<br>";
    }

    if (tag === "A") {
      const href = element.getAttribute("href") || "";
      const isSafeHref = /^(https?:\/\/|mailto:|#)/i.test(href);
      const safeHref = isSafeHref ? href : "#";
      const escapedHref = escapeHtml(safeHref);
      return `<a href="${escapedHref}" target="_blank" rel="noopener noreferrer">${sanitizedChildren}</a>`;
    }

    return `<${tag.toLowerCase()}>${sanitizedChildren}</${tag.toLowerCase()}>`;
  };

  return Array.from(root.childNodes).map(sanitizeNode).join("");
}

function normalizeRichTextValue(value) {
  const text = String(value || "");
  if (!text.trim()) {
    return "";
  }

  const hasHtml = /<\/?[a-z][\s\S]*>/i.test(text);
  return hasHtml ? sanitizeRichText(text) : convertPlainTextToHtml(text);
}

function richTextToPlainText(value) {
  const html = normalizeRichTextValue(value);
  if (!html) {
    return "";
  }

  const parser = new DOMParser();
  const doc = parser.parseFromString(`<div>${html}</div>`, "text/html");
  return (doc.body.textContent || "").replace(/\u00A0/g, " ").trim();
}

function isRichTextEmpty(value) {
  return !richTextToPlainText(value);
}

function RichTextEditor({ value, onChange, placeholder = "", className = "", minHeight = 95 }) {
  const editorRef = useRef(null);
  const normalizedValue = useMemo(() => normalizeRichTextValue(value), [value]);
  const effectiveMinHeight = Math.max(95, Number(minHeight) || 95);

  function adjustEditorHeight() {
    if (!editorRef.current) {
      return;
    }

    editorRef.current.style.minHeight = `${effectiveMinHeight}px`;
    editorRef.current.style.height = "auto";
    editorRef.current.style.height = `${Math.max(effectiveMinHeight, editorRef.current.scrollHeight)}px`;
  }

  useEffect(() => {
    if (!editorRef.current) {
      return;
    }

    if (editorRef.current.innerHTML !== normalizedValue) {
      editorRef.current.innerHTML = normalizedValue;
    }

    adjustEditorHeight();
  }, [normalizedValue, effectiveMinHeight]);

  function updateValue() {
    adjustEditorHeight();
    const html = sanitizeRichText(editorRef.current?.innerHTML || "");
    onChange?.(html);
  }

  function execute(command) {
    editorRef.current?.focus();
    document.execCommand(command, false, null);
    updateValue();
  }

  function handleCreateLink() {
    editorRef.current?.focus();
    const url = window.prompt("Informe a URL do link (https://...):", "https://");
    if (!url) {
      return;
    }

    document.execCommand("createLink", false, url);
    updateValue();
  }

  return (
    <div className={`rich-text-editor ${className}`}>
      <div className="rich-text-toolbar" aria-label="Formatacao de texto">
        <button type="button" className="btn ghost small" onClick={() => execute("bold")}>B</button>
        <button type="button" className="btn ghost small" onClick={() => execute("italic")}>I</button>
        <button type="button" className="btn ghost small" onClick={() => execute("underline")}>U</button>
        <button type="button" className="btn ghost small" onClick={() => execute("insertUnorderedList")}>Lista</button>
        <button type="button" className="btn ghost small" onClick={() => execute("insertOrderedList")}>1.</button>
        <button type="button" className="btn ghost small" onClick={handleCreateLink}>Link</button>
        <button type="button" className="btn ghost small" onClick={() => execute("removeFormat")}>Limpar</button>
      </div>
      <div
        ref={editorRef}
        className="rich-text-input"
        contentEditable
        suppressContentEditableWarning
        data-placeholder={placeholder}
        style={{ minHeight: effectiveMinHeight }}
        onInput={updateValue}
        onBlur={updateValue}
      />
    </div>
  );
}

function RichTextView({ value, className = "" }) {
  const html = useMemo(() => normalizeRichTextValue(value), [value]);
  if (!html) {
    return <span>-</span>;
  }

  return <div className={`rich-text-view ${className}`} dangerouslySetInnerHTML={{ __html: html }} />;
}

async function requestJson(path, options) {
  const response = await fetch(path, {
    headers: {
      "Content-Type": "application/json"
    },
    cache: "no-store",
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
    return labels
      .map((_, index) => {
        const angle = (Math.PI * 2 * index) / labels.length - Math.PI / 2;
        return `${cx + r * Math.cos(angle)},${cy + r * Math.sin(angle)}`;
      })
      .join(" ");
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

function MaskedDateInput({ value, onChange, className = "", placeholder = "dd/MM/yyyy", ariaLabel, inputRef }) {
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
      ref={inputRef}
      type="text"
      inputMode="numeric"
      maxLength={10}
      className={className}
      placeholder={placeholder}
      aria-label={ariaLabel}
      value={currentValue}
      onChange={(event) => {
        const masked = formatDateDigitsToMask(event.target.value);
        if (isControlled) {
          onChange?.(masked);
          return;
        }
        setInternalValue(masked);
      }}
    />
  );
}

function ClassificacaoPerfilColumnsSection({ groups, renderInfoIcon, classificacaoPerfilDraft, onDraftChange, onSaveColumn, onDeleteColumn, dateInputRefs }) {
  return (
    <div className="classification-columns">
      {groups.map((group) => {
        const draft = classificacaoPerfilDraft?.[group.tooltipKey] || { data: "", valor: "" };
        return (
          <article key={group.tooltipKey} className="classification-column">
            <div className="classification-column-header">
              <h4>
                {group.label} {renderInfoIcon(group.label, group.tooltipKey)}
              </h4>
            </div>

            <table className="history-table classification-table" style={{ tableLayout: "fixed", width: "100%" }}>
              <thead>
                <tr>
                  <th className="col-date" style={{ width: "150px", minWidth: "150px", maxWidth: "150px" }}>Data</th>
                  <th className="col-value">Registro</th>
                  <th style={{ width: "96px", minWidth: "96px", maxWidth: "96px" }}>Acoes</th>
                </tr>
              </thead>
              <tbody>
                <tr className="history-edit">
                  <td className="date-cell">
                    <MaskedDateInput
                      className="date-input"
                      ariaLabel={`Data do registro de ${group.label}`}
                      value={draft.data || ""}
                      inputRef={(element) => {
                        if (element) {
                          dateInputRefs.current[group.tooltipKey] = element;
                        }
                      }}
                      onChange={(nextValue) => onDraftChange(group.tooltipKey, "data", nextValue)}
                    />
                  </td>
                  <td>
                    <textarea
                      rows="2"
                      className="plain-textarea"
                      placeholder={`Registrar ${group.label}`}
                      value={draft.valor || ""}
                      onChange={(event) => onDraftChange(group.tooltipKey, "valor", event.target.value)}
                    />
                  </td>
                  <td className="col-actions">
                    <button type="button" className="btn ghost small" onClick={() => onSaveColumn(group.tooltipKey)}>
                      Salvar
                    </button>
                  </td>
                </tr>
                {group.rows.map((row) => (
                  <tr key={`${row.data}-${row.valor}`}>
                    <td>{row.data}</td>
                    <td>{richTextToPlainText(row.valor) || row.valor}</td>
                    <td>
                      <button
                        type="button"
                        className="btn ghost small danger-outline"
                        onClick={() => onDeleteColumn?.(group.tooltipKey, row.data)}
                      >
                        Excluir
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>

            {/* Salvar movido para a coluna Acoes da linha editavel */}
          </article>
        );
      })}
    </div>
  );
}

function HistoricalPropertyTabsSection({ sections, renderInfoIcon, initialActiveKey }) {
  const [activeProperty, setActiveProperty] = useState(initialActiveKey || sections[0]?.key || "");

  useEffect(() => {
    if (!sections.length) {
      setActiveProperty("");
      return;
    }

    const hasCurrentSection = sections.some((section) => section.key === activeProperty);
    if (hasCurrentSection) {
      return;
    }

    const fallbackKey = sections.some((section) => section.key === initialActiveKey)
      ? initialActiveKey
      : sections[0]?.key || "";

    setActiveProperty(fallbackKey);
  }, [sections, activeProperty, initialActiveKey]);

  return (
    <div className="prop-tabs">
      <div className="prop-tabs-bar">
        {sections.map((section) => (
          <button
            key={section.key}
            type="button"
            className={`prop-tab-btn ${activeProperty === section.key ? "active" : ""}`}
            onClick={() => setActiveProperty(section.key)}
          >
            {section.label}
          </button>
        ))}
      </div>

      {sections.map((section) => (
        <div key={section.key} className={`prop-tab-panel ${activeProperty === section.key ? "active" : ""}`}>
          <table className="history-table classification-table" style={{ tableLayout: "fixed", width: "100%" }}>
            <thead>
              <tr>
                <th className="col-date" style={{ width: "150px", minWidth: "150px", maxWidth: "150px" }}>Data</th>
                <th className="col-value">
                  {section.label} {renderInfoIcon(section.label, section.tooltipKey)}
                </th>
                <th style={{ width: "96px", minWidth: "96px", maxWidth: "96px" }}>Acoes</th>
              </tr>
            </thead>
            <tbody>
              <tr className="history-edit">
                <td className="date-cell">
                  <MaskedDateInput
                    className="date-input"
                    ariaLabel={`Data do registro de ${section.label}`}
                    value={section.draft?.data || ""}
                    inputRef={activeProperty === section.key ? section.dateInputRef : undefined}
                    onChange={(nextValue) => section.onDraftChange("data", nextValue)}
                  />
                </td>
                <td>
                  <RichTextEditor
                    minHeight={62}
                    placeholder={`Registrar ${section.label}`}
                    value={section.draft?.valor || ""}
                    onChange={(nextValue) => section.onDraftChange("valor", nextValue)}
                  />
                </td>
                <td className="col-actions">
                  <button type="button" className="btn ghost small" onClick={section.onSave}>
                    Salvar
                  </button>
                </td>
              </tr>
              {(section.historico || []).map((row) => (
                <tr key={`${row.data}-${row.valor}`}>
                  <td>{row.data}</td>
                  <td><RichTextView value={row.valor} /></td>
                  <td>
                    <button
                      type="button"
                      className="btn ghost small danger-outline"
                      onClick={() => section.onDelete?.(row.data)}
                    >
                      Excluir
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>

          {/* Salvar movido para a coluna Acoes da linha editavel */}
        </div>
      ))}
    </div>
  );
}

function ChaveSection({
  conhecimentosHistorico,
  conhecimentosDraft,
  onConhecimentosDraftChange,
  onSaveConhecimentos,
  onDeleteConhecimentos,
  conhecimentosDateInputRef,
  habilidadesHistorico,
  habilidadesDraft,
  onHabilidadesDraftChange,
  onSaveHabilidades,
  onDeleteHabilidades,
  habilidadesDateInputRef,
  atitudesHistorico,
  atitudesDraft,
  onAtitudesDraftChange,
  onSaveAtitudes,
  onDeleteAtitudes,
  atitudesDateInputRef,
  valoresHistorico,
  valoresDraft,
  onValoresDraftChange,
  onSaveValores,
  onDeleteValores,
  valoresDateInputRef,
  expectativasHistorico,
  expectativasDraft,
  onExpectativasDraftChange,
  onSaveExpectativas,
  onDeleteExpectativas,
  expectativasDateInputRef,
  renderInfoIcon
}) {
  const sections = [
    {
      key: "conhecimentos",
      label: "Conhecimentos",
      tooltipKey: "conhecimentos",
      historico: conhecimentosHistorico,
      draft: conhecimentosDraft,
      onDraftChange: onConhecimentosDraftChange,
      onSave: onSaveConhecimentos,
      onDelete: onDeleteConhecimentos,
      dateInputRef: conhecimentosDateInputRef
    },
    {
      key: "habilidades",
      label: "Habilidades",
      tooltipKey: "habilidades",
      historico: habilidadesHistorico,
      draft: habilidadesDraft,
      onDraftChange: onHabilidadesDraftChange,
      onSave: onSaveHabilidades,
      onDelete: onDeleteHabilidades,
      dateInputRef: habilidadesDateInputRef
    },
    {
      key: "atitudes",
      label: "Atitudes",
      tooltipKey: "atitudes",
      historico: atitudesHistorico,
      draft: atitudesDraft,
      onDraftChange: onAtitudesDraftChange,
      onSave: onSaveAtitudes,
      onDelete: onDeleteAtitudes,
      dateInputRef: atitudesDateInputRef
    },
    {
      key: "valores",
      label: "Valores",
      tooltipKey: "valores",
      historico: valoresHistorico,
      draft: valoresDraft,
      onDraftChange: onValoresDraftChange,
      onSave: onSaveValores,
      onDelete: onDeleteValores,
      dateInputRef: valoresDateInputRef
    },
    {
      key: "expectativas",
      label: "Expectativas",
      tooltipKey: "expectativas",
      historico: expectativasHistorico,
      draft: expectativasDraft,
      onDraftChange: onExpectativasDraftChange,
      onSave: onSaveExpectativas,
      onDelete: onDeleteExpectativas,
      dateInputRef: expectativasDateInputRef
    }
  ];

  return <HistoricalPropertyTabsSection sections={sections} renderInfoIcon={renderInfoIcon} initialActiveKey="conhecimentos" />;
}

function GrowPdiSection({
  metasHistorico,
  metasDraft,
  onMetasDraftChange,
  onSaveMetas,
  onDeleteMetas,
  metasDateInputRef,
  situacaoAtualHistorico,
  situacaoAtualDraft,
  onSituacaoAtualDraftChange,
  onSaveSituacaoAtual,
  onDeleteSituacaoAtual,
  situacaoAtualDateInputRef,
  opcoesHistorico,
  opcoesDraft,
  onOpcoesDraftChange,
  onSaveOpcoes,
  onDeleteOpcoes,
  opcoesDateInputRef,
  proximosPassosHistorico,
  proximosPassosDraft,
  onProximosPassosDraftChange,
  onSaveProximosPassos,
  onDeleteProximosPassos,
  proximosPassosDateInputRef,
  renderInfoIcon
}) {
  const sections = [
    {
      key: "metas",
      label: "Metas",
      tooltipKey: "metas",
      historico: metasHistorico,
      draft: metasDraft,
      onDraftChange: onMetasDraftChange,
      onSave: onSaveMetas,
      onDelete: onDeleteMetas,
      dateInputRef: metasDateInputRef
    },
    {
      key: "situacaoAtual",
      label: "Situacao Atual",
      tooltipKey: "situacaoAtual",
      historico: situacaoAtualHistorico,
      draft: situacaoAtualDraft,
      onDraftChange: onSituacaoAtualDraftChange,
      onSave: onSaveSituacaoAtual,
      onDelete: onDeleteSituacaoAtual,
      dateInputRef: situacaoAtualDateInputRef
    },
    {
      key: "opcoes",
      label: "Opcoes",
      tooltipKey: "opcoes",
      historico: opcoesHistorico,
      draft: opcoesDraft,
      onDraftChange: onOpcoesDraftChange,
      onSave: onSaveOpcoes,
      onDelete: onDeleteOpcoes,
      dateInputRef: opcoesDateInputRef
    },
    {
      key: "proximosPassos",
      label: "Proximos Passos",
      tooltipKey: "proximosPassos",
      historico: proximosPassosHistorico,
      draft: proximosPassosDraft,
      onDraftChange: onProximosPassosDraftChange,
      onSave: onSaveProximosPassos,
      onDelete: onDeleteProximosPassos,
      dateInputRef: proximosPassosDateInputRef
    }
  ];

  return <HistoricalPropertyTabsSection sections={sections} renderInfoIcon={renderInfoIcon} initialActiveKey="metas" />;
}

function SwotSection({
  fortalezasHistorico,
  fortalezasDraft,
  onFortalezasDraftChange,
  onSaveFortalezas,
  onDeleteFortalezas,
  fortalezasDateInputRef,
  oportunidadesHistorico,
  oportunidadesDraft,
  onOportunidadesDraftChange,
  onSaveOportunidades,
  onDeleteOportunidades,
  oportunidadesDateInputRef,
  fraquezasHistorico,
  fraquezasDraft,
  onFraquezasDraftChange,
  onSaveFraquezas,
  onDeleteFraquezas,
  fraquezasDateInputRef,
  ameacasHistorico,
  ameacasDraft,
  onAmeacasDraftChange,
  onSaveAmeacas,
  onDeleteAmeacas,
  ameacasDateInputRef,
  renderInfoIcon
}) {
  const sections = [
    {
      key: "fortalezas",
      label: "Fortalezas",
      tooltipKey: "fortalezas",
      historico: fortalezasHistorico,
      draft: fortalezasDraft,
      onDraftChange: onFortalezasDraftChange,
      onSave: onSaveFortalezas,
      onDelete: onDeleteFortalezas,
      dateInputRef: fortalezasDateInputRef
    },
    {
      key: "oportunidades",
      label: "Oportunidades",
      tooltipKey: "oportunidades",
      historico: oportunidadesHistorico,
      draft: oportunidadesDraft,
      onDraftChange: onOportunidadesDraftChange,
      onSave: onSaveOportunidades,
      onDelete: onDeleteOportunidades,
      dateInputRef: oportunidadesDateInputRef
    },
    {
      key: "fraquezas",
      label: "Fraquezas",
      tooltipKey: "fraquezas",
      historico: fraquezasHistorico,
      draft: fraquezasDraft,
      onDraftChange: onFraquezasDraftChange,
      onSave: onSaveFraquezas,
      onDelete: onDeleteFraquezas,
      dateInputRef: fraquezasDateInputRef
    },
    {
      key: "ameacas",
      label: "Ameacas",
      tooltipKey: "ameacas",
      historico: ameacasHistorico,
      draft: ameacasDraft,
      onDraftChange: onAmeacasDraftChange,
      onSave: onSaveAmeacas,
      onDelete: onDeleteAmeacas,
      dateInputRef: ameacasDateInputRef
    }
  ];

  return <HistoricalPropertyTabsSection sections={sections} renderInfoIcon={renderInfoIcon} initialActiveKey="fortalezas" />;
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
    disc: { data: "", valor: "" },
    personalidade: { data: "", valor: "" },
    nineBox: { data: "", valor: "" }
  });

  const [discHistorico, setDiscHistorico] = useState([]);
  const [personalidadeHistorico, setPersonalidadeHistorico] = useState([]);
  const [nineBoxHistorico, setNineBoxHistorico] = useState([]);
  const [conhecimentosHistorico, setConhecimentosHistorico] = useState([]);
  const [conhecimentosDraft, setConhecimentosDraft] = useState({ data: "", valor: "" });
  const [habilidadesHistorico, setHabilidadesHistorico] = useState([]);
  const [habilidadesDraft, setHabilidadesDraft] = useState({ data: "", valor: "" });
  const [atitudesHistorico, setAtitudesHistorico] = useState([]);
  const [atitudesDraft, setAtitudesDraft] = useState({ data: "", valor: "" });
  const [valoresHistorico, setValoresHistorico] = useState([]);
  const [valoresDraft, setValoresDraft] = useState({ data: "", valor: "" });
  const [expectativasHistorico, setExpectativasHistorico] = useState([]);
  const [expectativasDraft, setExpectativasDraft] = useState({ data: "", valor: "" });
  const [metasHistorico, setMetasHistorico] = useState([]);
  const [metasDraft, setMetasDraft] = useState({ data: "", valor: "" });
  const [situacaoAtualHistorico, setSituacaoAtualHistorico] = useState([]);
  const [situacaoAtualDraft, setSituacaoAtualDraft] = useState({ data: "", valor: "" });
  const [opcoesHistorico, setOpcoesHistorico] = useState([]);
  const [opcoesDraft, setOpcoesDraft] = useState({ data: "", valor: "" });
  const [proximosPassosHistorico, setProximosPassosHistorico] = useState([]);
  const [proximosPassosDraft, setProximosPassosDraft] = useState({ data: "", valor: "" });
  const [fortalezasHistorico, setFortalezasHistorico] = useState([]);
  const [fortalezasDraft, setFortalezasDraft] = useState({ data: "", valor: "" });
  const [oportunidadesHistorico, setOportunidadesHistorico] = useState([]);
  const [oportunidadesDraft, setOportunidadesDraft] = useState({ data: "", valor: "" });
  const [fraquezasHistorico, setFraquezasHistorico] = useState([]);
  const [fraquezasDraft, setFraquezasDraft] = useState({ data: "", valor: "" });
  const [ameacasHistorico, setAmeacasHistorico] = useState([]);
  const [ameacasDraft, setAmeacasDraft] = useState({ data: "", valor: "" });
  
  const prevLideradoIdRef = useRef(null);
  const classificacaoDataInputRefs = useRef({});
  const conhecimentosDateInputRef = useRef(null);
  const habilidadesDateInputRef = useRef(null);
  const atitudesDateInputRef = useRef(null);
  const valoresDateInputRef = useRef(null);
  const expectativasDateInputRef = useRef(null);
  const metasDateInputRef = useRef(null);
  const situacaoAtualDateInputRef = useRef(null);
  const opcoesDateInputRef = useRef(null);
  const proximosPassosDateInputRef = useRef(null);
  const fortalezasDateInputRef = useRef(null);
  const oportunidadesDateInputRef = useRef(null);
  const fraquezasDateInputRef = useRef(null);
  const ameacasDateInputRef = useRef(null);
  const feedbackDateInputRef = useRef(null);
  const oneOnOneDateInputRef = useRef(null);
  const culturaDateInputRef = useRef(null);

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
    if (view !== "dashboard") {
      return;
    }

    let active = true;
    async function loadDashboardFresh() {
      try {
        const [dashboard, lideradosList] = await Promise.all([
          requestJson("/api/dashboard/"),
          requestJson("/api/liderados/")
        ]);

        if (!active) {
          return;
        }

        setDashboardCards(dashboard?.cards || []);
        setLiderados(lideradosList || []);
      } catch {
        // noop: keeps current data and avoids noisy errors when toggling views quickly
      }
    }

    loadDashboardFresh();
    return () => {
      active = false;
    };
  }, [view]);

  useEffect(() => {
    if (view !== "leader" || !selectedLideradoId) {
      return;
    }

    // Always refresh when entering the leader page to avoid stale historical tables.
    setLeaderReloadKey((value) => value + 1);
  }, [view, selectedLideradoId]);

  useEffect(() => {
    if (!selectedLideradoId) {
      return;
    }

    const isNewLiderado = selectedLideradoId !== prevLideradoIdRef.current;
    prevLideradoIdRef.current = selectedLideradoId;

    let active = true;

    async function loadLeader() {
      setLoadingLeader(true);
      setError("");

      try {
        const [visao, feedbackResponse, oneOnOneResponse, culturaResponse, discResponse, personalidadeResponse, nineBoxResponse, conhecimentosResponse, habilidadesResponse, atitudesResponse, valoresResponse, expectativasResponse, metasResponse, situacaoAtualResponse, opcoesResponse, proximosPassosResponse, fortalezasResponse, oportunidadesResponse, fraquezasResponse, ameacasResponse] = await Promise.all([
          requestJson(`/api/liderados/${selectedLideradoId}/visao-individual`),
          requestJson(`/api/feedbacks/${selectedLideradoId}`),
          requestJson(`/api/one-on-ones/${selectedLideradoId}`),
          requestJson(`/api/cultura/${selectedLideradoId}`),
          requestJson(`/api/disc/${selectedLideradoId}`),
          requestJson(`/api/personalidade/${selectedLideradoId}`),
          requestJson(`/api/nine-box/${selectedLideradoId}`),
          requestJson(`/api/conhecimentos/${selectedLideradoId}`),
          requestJson(`/api/habilidades/${selectedLideradoId}`),
          requestJson(`/api/atitudes/${selectedLideradoId}`),
          requestJson(`/api/valores/${selectedLideradoId}`),
          requestJson(`/api/expectativas/${selectedLideradoId}`),
          requestJson(`/api/metas/${selectedLideradoId}`),
          requestJson(`/api/situacao-atual/${selectedLideradoId}`),
          requestJson(`/api/opcoes/${selectedLideradoId}`),
          requestJson(`/api/proximos-passos/${selectedLideradoId}`),
          requestJson(`/api/fortalezas/${selectedLideradoId}`),
          requestJson(`/api/oportunidades/${selectedLideradoId}`),
          requestJson(`/api/fraquezas/${selectedLideradoId}`),
          requestJson(`/api/ameacas/${selectedLideradoId}`)
        ]);

        if (!active) {
          return;
        }

        setLeaderView(visao?.conteudo || null);
        setFeedbacks(feedbackResponse?.registros || []);
        setOneOnOnes(oneOnOneResponse?.registros || []);
        setCultureEntries(culturaResponse?.registros || []);
        setDiscHistorico(discResponse?.registros || []);
        setPersonalidadeHistorico(personalidadeResponse?.registros || []);
        setNineBoxHistorico(nineBoxResponse?.registros || []);
        setConhecimentosHistorico(conhecimentosResponse?.registros || []);
        setHabilidadesHistorico(habilidadesResponse?.registros || []);
        setAtitudesHistorico(atitudesResponse?.registros || []);
        setValoresHistorico(valoresResponse?.registros || []);
        setExpectativasHistorico(expectativasResponse?.registros || []);
        setMetasHistorico(metasResponse?.registros || []);
        setSituacaoAtualHistorico(situacaoAtualResponse?.registros || []);
        setOpcoesHistorico(opcoesResponse?.registros || []);
        setProximosPassosHistorico(proximosPassosResponse?.registros || []);
        setFortalezasHistorico(fortalezasResponse?.registros || []);
        setOportunidadesHistorico(oportunidadesResponse?.registros || []);
        setFraquezasHistorico(fraquezasResponse?.registros || []);
        setAmeacasHistorico(ameacasResponse?.registros || []);

        if (isNewLiderado) {
          setCultureIndex(0);
          setActiveTab(TAB_ORDER[0]);
          setConhecimentosDraft({ data: "", valor: "" });
          setHabilidadesDraft({ data: "", valor: "" });
          setAtitudesDraft({ data: "", valor: "" });
          setValoresDraft({ data: "", valor: "" });
          setExpectativasDraft({ data: "", valor: "" });
          setMetasDraft({ data: "", valor: "" });
          setSituacaoAtualDraft({ data: "", valor: "" });
          setOpcoesDraft({ data: "", valor: "" });
          setProximosPassosDraft({ data: "", valor: "" });
          setFortalezasDraft({ data: "", valor: "" });
          setOportunidadesDraft({ data: "", valor: "" });
          setFraquezasDraft({ data: "", valor: "" });
          setAmeacasDraft({ data: "", valor: "" });
        }

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
        const initialDate = toDisplayDate(classificacao.dataDisc || classificacao.Data || classificacao.dataAtualizacaoUtc || "");
        setClassificacaoPerfilDraft({
          disc: { data: initialDate, valor: "" },
          personalidade: { data: initialDate, valor: "" },
          nineBox: { data: initialDate, valor: "" }
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
    if (view !== "leader" || !selectedLideradoId) {
      return;
    }

    let active = true;

    async function loadActiveTabData() {
      try {
        if (activeTab === "Informacoes Pessoais") {
          const visao = await requestJson(`/api/liderados/${selectedLideradoId}/visao-individual`);
          if (!active) return;

          setLeaderView(visao?.conteudo || null);
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
          return;
        }

        if (activeTab === "Classificacao de Perfil") {
          const [discResponse, personalidadeResponse, nineBoxResponse] = await Promise.all([
            requestJson(`/api/disc/${selectedLideradoId}`),
            requestJson(`/api/personalidade/${selectedLideradoId}`),
            requestJson(`/api/nine-box/${selectedLideradoId}`)
          ]);
          if (!active) return;

          setDiscHistorico(discResponse?.registros || []);
          setPersonalidadeHistorico(personalidadeResponse?.registros || []);
          setNineBoxHistorico(nineBoxResponse?.registros || []);
          return;
        }

        if (activeTab === "CHAVE") {
          const [conhecimentosResponse, habilidadesResponse, atitudesResponse, valoresResponse, expectativasResponse] = await Promise.all([
            requestJson(`/api/conhecimentos/${selectedLideradoId}`),
            requestJson(`/api/habilidades/${selectedLideradoId}`),
            requestJson(`/api/atitudes/${selectedLideradoId}`),
            requestJson(`/api/valores/${selectedLideradoId}`),
            requestJson(`/api/expectativas/${selectedLideradoId}`)
          ]);
          if (!active) return;

          setConhecimentosHistorico(conhecimentosResponse?.registros || []);
          setHabilidadesHistorico(habilidadesResponse?.registros || []);
          setAtitudesHistorico(atitudesResponse?.registros || []);
          setValoresHistorico(valoresResponse?.registros || []);
          setExpectativasHistorico(expectativasResponse?.registros || []);
          return;
        }

        if (activeTab === "GROW / PDI") {
          const [metasResponse, situacaoAtualResponse, opcoesResponse, proximosPassosResponse] = await Promise.all([
            requestJson(`/api/metas/${selectedLideradoId}`),
            requestJson(`/api/situacao-atual/${selectedLideradoId}`),
            requestJson(`/api/opcoes/${selectedLideradoId}`),
            requestJson(`/api/proximos-passos/${selectedLideradoId}`)
          ]);
          if (!active) return;

          setMetasHistorico(metasResponse?.registros || []);
          setSituacaoAtualHistorico(situacaoAtualResponse?.registros || []);
          setOpcoesHistorico(opcoesResponse?.registros || []);
          setProximosPassosHistorico(proximosPassosResponse?.registros || []);
          return;
        }

        if (activeTab === "SWOT") {
          const [fortalezasResponse, oportunidadesResponse, fraquezasResponse, ameacasResponse] = await Promise.all([
            requestJson(`/api/fortalezas/${selectedLideradoId}`),
            requestJson(`/api/oportunidades/${selectedLideradoId}`),
            requestJson(`/api/fraquezas/${selectedLideradoId}`),
            requestJson(`/api/ameacas/${selectedLideradoId}`)
          ]);
          if (!active) return;

          setFortalezasHistorico(fortalezasResponse?.registros || []);
          setOportunidadesHistorico(oportunidadesResponse?.registros || []);
          setFraquezasHistorico(fraquezasResponse?.registros || []);
          setAmeacasHistorico(ameacasResponse?.registros || []);
          return;
        }

        if (activeTab === "Feedbacks") {
          const feedbackResponse = await requestJson(`/api/feedbacks/${selectedLideradoId}`);
          if (!active) return;
          setFeedbacks(feedbackResponse?.registros || []);
          return;
        }

        if (activeTab === "1:1") {
          const oneOnOneResponse = await requestJson(`/api/one-on-ones/${selectedLideradoId}`);
          if (!active) return;
          setOneOnOnes(oneOnOneResponse?.registros || []);
          return;
        }

        if (activeTab === "Cultura") {
          const culturaResponse = await requestJson(`/api/cultura/${selectedLideradoId}`);
          if (!active) return;

          setCultureEntries(culturaResponse?.registros || []);
        }

      } catch (loadError) {
        if (active) {
          setError(loadError.message);
        }
      }
    }

    loadActiveTabData();
    return () => {
      active = false;
    };
  }, [view, activeTab, selectedLideradoId]);

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
    const latestDisc = discHistorico[0]?.valor || "-";
    const latestCultureEntry = (cultureEntries || []).reduce((latest, current) => {
      if (!latest) {
        return current;
      }

      const currentDate = Date.parse(`${toIsoDate(current?.data) || current?.data || ""}T00:00:00Z`);
      const latestDate = Date.parse(`${toIsoDate(latest?.data) || latest?.data || ""}T00:00:00Z`);

      if (Number.isNaN(currentDate)) {
        return latest;
      }
      if (Number.isNaN(latestDate)) {
        return current;
      }

      return currentDate > latestDate ? current : latest;
    }, null);

    const cultureScores = latestCultureEntry
      ? [
          latestCultureEntry.aprenderEMelhorarSempre,
          latestCultureEntry.atitudeDeDono,
          latestCultureEntry.buscarMelhoresResultadosParaClientes,
          latestCultureEntry.espiritoDeEquipe,
          latestCultureEntry.excelencia,
          latestCultureEntry.fazerAcontecer,
          latestCultureEntry.inovarParaInspirar
        ].map((value) => Number(value))
      : [];

    const hasValidCultureScores = cultureScores.length === 7 && cultureScores.every((value) => Number.isFinite(value));
    const notaGeralFromCulture = hasValidCultureScores
      ? Number((cultureScores.reduce((sum, value) => sum + value, 0) / cultureScores.length).toFixed(1))
      : null;

    return {
      disc: leaderView?.disc || card?.disc || latestDisc,
      perfil: leaderView?.perfil || card?.perfil || "-",
      nineBox: leaderView?.nineBox || card?.nineBox || "-",
      feedbacks: Number.isInteger(leaderView?.quantidadeFeedbacks)
        ? leaderView.quantidadeFeedbacks
        : card?.quantidadeFeedbacks || 0,
      oneOnOnes: Number.isInteger(leaderView?.quantidadeOneOnOnes)
        ? leaderView.quantidadeOneOnOnes
        : card?.quantidadeOneOnOnes || 0,
      notaGeral: notaGeralFromCulture ?? card?.notaGeral ?? "-"
    };
  }, [cultureEntries, dashboardCards, discHistorico, leaderView, selectedLideradoId]);

  const propertySectionData = useMemo(() => {
    return {
      "Classificacao de Perfil": PROPERTY_SECTION_CONFIG["Classificacao de Perfil"].properties.map((property) => ({
        ...property,
        rows:
          property.tooltipKey === "disc"
            ? discHistorico.map((r) => ({ data: toDisplayDate(r.data), valor: r.valor }))
            : property.tooltipKey === "personalidade"
              ? personalidadeHistorico.map((r) => ({ data: toDisplayDate(r.data), valor: r.valor }))
              : nineBoxHistorico.map((r) => ({ data: toDisplayDate(r.data), valor: r.valor }))
      }))
    };
  }, [discHistorico, personalidadeHistorico, nineBoxHistorico]);

  const dashboardCardsWithFallbackRadar = useMemo(() => {
    return dashboardCards.map((card) => {
      const cultura = card.ultimaAvaliacaoCultura || null;
      const radar = cultura
        ? [
            Number(cultura.aprenderEMelhorarSempre),
            Number(cultura.atitudeDeDono),
            Number(cultura.buscarMelhoresResultadosParaClientes),
            Number(cultura.espiritoDeEquipe),
            Number(cultura.excelencia),
            Number(cultura.fazerAcontecer),
            Number(cultura.inovarParaInspirar)
          ]
        : [5, 5, 5, 5, 5, 5, 5];

      const hasValidRadar = radar.length === 7 && radar.every((value) => Number.isFinite(value));
      const notaGeralFromRadar = hasValidRadar
        ? Number((radar.reduce((sum, value) => sum + value, 0) / radar.length).toFixed(1))
        : null;

      return {
        ...card,
        radar,
        notaGeralCalculada: cultura ? notaGeralFromRadar : card.notaGeral ?? null
      };
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
      setError("Nenhum liderado selecionado.");
      return;
    }

    const isoDate = toIsoDate(feedbackDraft.data);
    if (!isoDate) {
      setError("Informe a data de Feedbacks no formato dd/MM/aaaa (ex.: 27/11/2025).");
      return;
    }
    if (isRichTextEmpty(feedbackDraft.conteudo)) {
      setError("O conteudo de Feedbacks e obrigatorio.");
      return;
    }
    if (isRichTextEmpty(feedbackDraft.receptividade)) {
      setError("A receptividade de Feedbacks e obrigatoria.");
      return;
    }

    try {
      await requestJson(`/api/feedbacks`, {
        method: "POST",
        body: JSON.stringify({
          lideradoId: selectedLideradoId,
          conteudo: sanitizeRichText(feedbackDraft.conteudo),
          receptividade: sanitizeRichText(feedbackDraft.receptividade),
          polaridade: feedbackDraft.polaridade,
          data: isoDate
        })
      });

      const response = await requestJson(`/api/feedbacks/${selectedLideradoId}`);
      setFeedbacks(response?.registros || []);
      setFeedbackDraft({ data: "", conteudo: "", receptividade: "", polaridade: "Positivo" });

      requestAnimationFrame(() => {
        feedbackDateInputRef.current?.focus();
      });

      setError("");
      await refreshCurrentLeader();
    } catch (saveError) {
      setError(saveError.message);
    }
  }

  async function handleSaveOneOnOne() {
    if (!selectedLideradoId) {
      setError("Nenhum liderado selecionado.");
      return;
    }

    const isoDate = toIsoDate(oneOnOneDraft.data);
    if (!isoDate) {
      setError("Informe a data de 1:1 no formato dd/MM/aaaa (ex.: 27/11/2025).");
      return;
    }
    if (isRichTextEmpty(oneOnOneDraft.resumo)) {
      setError("O resumo de 1:1 e obrigatorio.");
      return;
    }
    if (isRichTextEmpty(oneOnOneDraft.tarefasAcordadas)) {
      setError("As tarefas acordadas de 1:1 sao obrigatorias.");
      return;
    }
    if (isRichTextEmpty(oneOnOneDraft.proximosAssuntos)) {
      setError("Os proximos assuntos de 1:1 sao obrigatorios.");
      return;
    }

    try {
      await requestJson(`/api/one-on-ones`, {
        method: "POST",
        body: JSON.stringify({
          lideradoId: selectedLideradoId,
          resumo: sanitizeRichText(oneOnOneDraft.resumo),
          tarefasAcordadas: sanitizeRichText(oneOnOneDraft.tarefasAcordadas),
          proximosAssuntos: sanitizeRichText(oneOnOneDraft.proximosAssuntos),
          data: isoDate
        })
      });

      const response = await requestJson(`/api/one-on-ones/${selectedLideradoId}`);
      setOneOnOnes(response?.registros || []);
      setOneOnOneDraft({ data: "", resumo: "", tarefasAcordadas: "", proximosAssuntos: "" });

      requestAnimationFrame(() => {
        oneOnOneDateInputRef.current?.focus();
      });

      setError("");
      await refreshCurrentLeader();
    } catch (saveError) {
      setError(saveError.message);
    }
  }

  async function handleSaveCultura() {
    if (!selectedLideradoId) {
      setError("Nenhum liderado selecionado.");
      return;
    }

    const isoDate = toIsoDate(cultureDraft.data);
    if (!isoDate) {
      setError("Informe a data de Cultura no formato dd/MM/aaaa (ex.: 27/11/2025).");
      return;
    }

    try {
      await requestJson(`/api/cultura`, {
        method: "POST",
        body: JSON.stringify({
          lideradoId: selectedLideradoId,
          data: isoDate,
          aprenderEMelhorarSempre: Number(cultureDraft.aprenderEMelhorarSempre || 0),
          atitudeDeDono: Number(cultureDraft.atitudeDeDono || 0),
          buscarMelhoresResultadosParaClientes: Number(cultureDraft.buscarMelhoresResultadosParaClientes || 0),
          espiritoDeEquipe: Number(cultureDraft.espiritoDeEquipe || 0),
          excelencia: Number(cultureDraft.excelencia || 0),
          fazerAcontecer: Number(cultureDraft.fazerAcontecer || 0),
          inovarParaInspirar: Number(cultureDraft.inovarParaInspirar || 0)
        })
      });

      const response = await requestJson(`/api/cultura/${selectedLideradoId}`);
      setCultureEntries(response?.registros || []);

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

      requestAnimationFrame(() => {
        culturaDateInputRef.current?.focus();
      });

      setError("");
      await refreshCurrentLeader();
    } catch (saveError) {
      setError(saveError.message);
    }
  }

  async function handleSaveClassificacaoPerfilByTipo(tipo) {
    setError("");
    if (!selectedLideradoId) {
      setError("Nenhum liderado selecionado.");
      return;
    }
    const draft = classificacaoPerfilDraft?.[tipo] || { data: "", valor: "" };
    const dataTexto = String(draft.data || "").trim();
    if (!toIsoDate(dataTexto)) {
      setError(buildDiscDateErrorMessage(dataTexto));
      return;
    }

    const valueToSave = String(draft.valor || "").trim();
    if (!valueToSave) {
      const labelByKey = {
        disc: "DISC",
        personalidade: "Personalidade",
        nineBox: "Nine Box"
      };
      setError(`Preencha o campo ${labelByKey[tipo]} antes de salvar.`);
      return;
    }

    try {
      if (tipo === "disc") {
        await requestJson(`/api/disc`, {
          method: "POST",
          body: JSON.stringify({
            lideradoId: selectedLideradoId,
            disc: valueToSave,
            data: dataTexto
          })
        });
        const discResponse = await requestJson(`/api/disc/${selectedLideradoId}`);
        setDiscHistorico(discResponse?.registros || []);
      } else if (tipo === "personalidade") {
        await requestJson(`/api/personalidade`, {
          method: "POST",
          body: JSON.stringify({
            lideradoId: selectedLideradoId,
            valor: valueToSave,
            data: dataTexto
          })
        });
        const personalidadeResponse = await requestJson(`/api/personalidade/${selectedLideradoId}`);
        setPersonalidadeHistorico(personalidadeResponse?.registros || []);
      } else if (tipo === "nineBox") {
        await requestJson(`/api/nine-box`, {
          method: "POST",
          body: JSON.stringify({
            lideradoId: selectedLideradoId,
            valor: valueToSave,
            data: dataTexto
          })
        });
        const nineBoxResponse = await requestJson(`/api/nine-box/${selectedLideradoId}`);
        setNineBoxHistorico(nineBoxResponse?.registros || []);
      }

      setClassificacaoPerfilDraft((prev) => ({
        ...prev,
        [tipo]: {
          ...(prev[tipo] || { data: "", valor: "" }),
          valor: ""
        }
      }));

      requestAnimationFrame(() => {
        classificacaoDataInputRefs.current[tipo]?.focus();
      });

      setLeaderReloadKey((value) => value + 1);
      await refreshCurrentLeader();

      setError("");
    } catch (e) {
      setError(e.message);
    }
  }

  async function handleDeleteClassificacaoPerfilByTipo(tipo, data) {
    if (!selectedLideradoId) {
      setError("Nenhum liderado selecionado.");
      return;
    }

    const isoDate = toIsoDate(data);
    if (!isoDate) {
      setError("Nao consegui entender a data para exclusao.");
      return;
    }

    const labelByKey = {
      disc: "DISC",
      personalidade: "Personalidade",
      nineBox: "Nine Box"
    };

    const confirmed = window.confirm(`Excluir registro de ${labelByKey[tipo]} da data ${toDisplayDate(isoDate)}?`);
    if (!confirmed) {
      return;
    }

    try {
      const endpointByKey = {
        disc: "/api/disc",
        personalidade: "/api/personalidade",
        nineBox: "/api/nine-box"
      };

      await requestJson(endpointByKey[tipo], {
        method: "DELETE",
        body: JSON.stringify({
          lideradoId: selectedLideradoId,
          data: isoDate
        })
      });

      if (tipo === "disc") {
        const response = await requestJson(`/api/disc/${selectedLideradoId}`);
        setDiscHistorico(response?.registros || []);
      } else if (tipo === "personalidade") {
        const response = await requestJson(`/api/personalidade/${selectedLideradoId}`);
        setPersonalidadeHistorico(response?.registros || []);
      } else if (tipo === "nineBox") {
        const response = await requestJson(`/api/nine-box/${selectedLideradoId}`);
        setNineBoxHistorico(response?.registros || []);
      }

      setLeaderReloadKey((value) => value + 1);
      await refreshCurrentLeader();
      setError("");
    } catch (e) {
      setError(e.message);
    }
  }

  async function saveHistoricalEntry({ draft, endpoint, label, setHistorico, setDraft, dateInputRef }) {
    if (!selectedLideradoId) {
      setError("Nenhum liderado selecionado.");
      return;
    }

    const currentDraft = draft || { data: "", valor: "" };
    const isoDate = toIsoDate(currentDraft.data);
    if (!isoDate) {
      setError(`Informe a data de ${label} no formato dd/MM/aaaa (ex.: 27/11/2025).`);
      return;
    }

    if (isRichTextEmpty(currentDraft.valor)) {
      setError("O valor e obrigatorio.");
      return;
    }

    try {
      await requestJson(endpoint, {
        method: "POST",
        body: JSON.stringify({
          lideradoId: selectedLideradoId,
          valor: sanitizeRichText(currentDraft.valor),
          data: isoDate
        })
      });

      const response = await requestJson(`${endpoint}/${selectedLideradoId}`);
      setHistorico(response?.registros || []);
      setDraft({ data: "", valor: "" });

      requestAnimationFrame(() => {
        dateInputRef.current?.focus();
      });

      setError("");
      await refreshCurrentLeader();
    } catch (e) {
      setError(e.message);
    }
  }

  async function deleteHistoricalEntry({ endpoint, label, data, setHistorico }) {
    if (!selectedLideradoId) {
      setError("Nenhum liderado selecionado.");
      return;
    }

    const isoDate = toIsoDate(data);
    if (!isoDate) {
      setError("Nao consegui entender a data para exclusao.");
      return;
    }

    const confirmed = window.confirm(`Excluir registro de ${label} da data ${toDisplayDate(isoDate)}?`);
    if (!confirmed) {
      return;
    }

    try {
      await requestJson(endpoint, {
        method: "DELETE",
        body: JSON.stringify({
          lideradoId: selectedLideradoId,
          data: isoDate
        })
      });

      const response = await requestJson(`${endpoint}/${selectedLideradoId}`);
      setHistorico(response?.registros || []);
      setLeaderReloadKey((value) => value + 1);
      await refreshCurrentLeader();
      setError("");
    } catch (e) {
      setError(e.message);
    }
  }

  async function handleDeleteFeedback(data) {
    await deleteHistoricalEntry({ endpoint: "/api/feedbacks", label: "Feedbacks", data, setHistorico: setFeedbacks });
  }

  async function handleDeleteOneOnOne(data) {
    await deleteHistoricalEntry({ endpoint: "/api/one-on-ones", label: "1:1", data, setHistorico: setOneOnOnes });
  }

  async function handleDeleteCultura(data) {
    await deleteHistoricalEntry({ endpoint: "/api/cultura", label: "Cultura", data, setHistorico: setCultureEntries });
  }

  async function handleDeleteConhecimentos(data) {
    await deleteHistoricalEntry({ endpoint: "/api/conhecimentos", label: "Conhecimentos", data, setHistorico: setConhecimentosHistorico });
  }

  async function handleDeleteHabilidades(data) {
    await deleteHistoricalEntry({ endpoint: "/api/habilidades", label: "Habilidades", data, setHistorico: setHabilidadesHistorico });
  }

  async function handleDeleteAtitudes(data) {
    await deleteHistoricalEntry({ endpoint: "/api/atitudes", label: "Atitudes", data, setHistorico: setAtitudesHistorico });
  }

  async function handleDeleteValores(data) {
    await deleteHistoricalEntry({ endpoint: "/api/valores", label: "Valores", data, setHistorico: setValoresHistorico });
  }

  async function handleDeleteExpectativas(data) {
    await deleteHistoricalEntry({ endpoint: "/api/expectativas", label: "Expectativas", data, setHistorico: setExpectativasHistorico });
  }

  async function handleDeleteMetas(data) {
    await deleteHistoricalEntry({ endpoint: "/api/metas", label: "Metas", data, setHistorico: setMetasHistorico });
  }

  async function handleDeleteSituacaoAtual(data) {
    await deleteHistoricalEntry({ endpoint: "/api/situacao-atual", label: "Situacao Atual", data, setHistorico: setSituacaoAtualHistorico });
  }

  async function handleDeleteOpcoes(data) {
    await deleteHistoricalEntry({ endpoint: "/api/opcoes", label: "Opcoes", data, setHistorico: setOpcoesHistorico });
  }

  async function handleDeleteProximosPassos(data) {
    await deleteHistoricalEntry({ endpoint: "/api/proximos-passos", label: "Proximos Passos", data, setHistorico: setProximosPassosHistorico });
  }

  async function handleDeleteFortalezas(data) {
    await deleteHistoricalEntry({ endpoint: "/api/fortalezas", label: "Fortalezas", data, setHistorico: setFortalezasHistorico });
  }

  async function handleDeleteOportunidades(data) {
    await deleteHistoricalEntry({ endpoint: "/api/oportunidades", label: "Oportunidades", data, setHistorico: setOportunidadesHistorico });
  }

  async function handleDeleteFraquezas(data) {
    await deleteHistoricalEntry({ endpoint: "/api/fraquezas", label: "Fraquezas", data, setHistorico: setFraquezasHistorico });
  }

  async function handleDeleteAmeacas(data) {
    await deleteHistoricalEntry({ endpoint: "/api/ameacas", label: "Ameacas", data, setHistorico: setAmeacasHistorico });
  }

  async function handleSaveConhecimentos() {
    await saveHistoricalEntry({
      draft: conhecimentosDraft,
      endpoint: "/api/conhecimentos",
      label: "Conhecimentos",
      setHistorico: setConhecimentosHistorico,
      setDraft: setConhecimentosDraft,
      dateInputRef: conhecimentosDateInputRef
    });
  }

  async function handleSaveHabilidades() {
    await saveHistoricalEntry({
      draft: habilidadesDraft,
      endpoint: "/api/habilidades",
      label: "Habilidades",
      setHistorico: setHabilidadesHistorico,
      setDraft: setHabilidadesDraft,
      dateInputRef: habilidadesDateInputRef
    });
  }

  async function handleSaveAtitudes() {
    await saveHistoricalEntry({
      draft: atitudesDraft,
      endpoint: "/api/atitudes",
      label: "Atitudes",
      setHistorico: setAtitudesHistorico,
      setDraft: setAtitudesDraft,
      dateInputRef: atitudesDateInputRef
    });
  }

  async function handleSaveValores() {
    await saveHistoricalEntry({
      draft: valoresDraft,
      endpoint: "/api/valores",
      label: "Valores",
      setHistorico: setValoresHistorico,
      setDraft: setValoresDraft,
      dateInputRef: valoresDateInputRef
    });
  }

  async function handleSaveExpectativas() {
    await saveHistoricalEntry({
      draft: expectativasDraft,
      endpoint: "/api/expectativas",
      label: "Expectativas",
      setHistorico: setExpectativasHistorico,
      setDraft: setExpectativasDraft,
      dateInputRef: expectativasDateInputRef
    });
  }

  async function handleSaveMetas() {
    await saveHistoricalEntry({
      draft: metasDraft,
      endpoint: "/api/metas",
      label: "Metas",
      setHistorico: setMetasHistorico,
      setDraft: setMetasDraft,
      dateInputRef: metasDateInputRef
    });
  }

  async function handleSaveSituacaoAtual() {
    await saveHistoricalEntry({
      draft: situacaoAtualDraft,
      endpoint: "/api/situacao-atual",
      label: "Situacao Atual",
      setHistorico: setSituacaoAtualHistorico,
      setDraft: setSituacaoAtualDraft,
      dateInputRef: situacaoAtualDateInputRef
    });
  }

  async function handleSaveOpcoes() {
    await saveHistoricalEntry({
      draft: opcoesDraft,
      endpoint: "/api/opcoes",
      label: "Opcoes",
      setHistorico: setOpcoesHistorico,
      setDraft: setOpcoesDraft,
      dateInputRef: opcoesDateInputRef
    });
  }

  async function handleSaveProximosPassos() {
    await saveHistoricalEntry({
      draft: proximosPassosDraft,
      endpoint: "/api/proximos-passos",
      label: "Proximos Passos",
      setHistorico: setProximosPassosHistorico,
      setDraft: setProximosPassosDraft,
      dateInputRef: proximosPassosDateInputRef
    });
  }

  async function handleSaveFortalezas() {
    await saveHistoricalEntry({
      draft: fortalezasDraft,
      endpoint: "/api/fortalezas",
      label: "Fortalezas",
      setHistorico: setFortalezasHistorico,
      setDraft: setFortalezasDraft,
      dateInputRef: fortalezasDateInputRef
    });
  }

  async function handleSaveOportunidades() {
    await saveHistoricalEntry({
      draft: oportunidadesDraft,
      endpoint: "/api/oportunidades",
      label: "Oportunidades",
      setHistorico: setOportunidadesHistorico,
      setDraft: setOportunidadesDraft,
      dateInputRef: oportunidadesDateInputRef
    });
  }

  async function handleSaveFraquezas() {
    await saveHistoricalEntry({
      draft: fraquezasDraft,
      endpoint: "/api/fraquezas",
      label: "Fraquezas",
      setHistorico: setFraquezasHistorico,
      setDraft: setFraquezasDraft,
      dateInputRef: fraquezasDateInputRef
    });
  }

  async function handleSaveAmeacas() {
    await saveHistoricalEntry({
      draft: ameacasDraft,
      endpoint: "/api/ameacas",
      label: "Ameacas",
      setHistorico: setAmeacasHistorico,
      setDraft: setAmeacasDraft,
      dateInputRef: ameacasDateInputRef
    });
  }

  function getTabLabel(tabName) {
    return TAB_LABELS[tabName] || tabName;
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
                      <div className="kpi-label">DISC</div>
                      <div className="kpi-value">{card.disc || "-"}</div>
                    </div>
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
                      <div className="kpi-value">{card.notaGeralCalculada ?? "-"}</div>
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
                  <div className="kpi-label">DISC</div>
                  <div className="kpi-value">{summaryMetrics.disc}</div>
                </div>
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
                      {toDisplayDate(item.data)}
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
                  {getTabLabel(tab)}
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
                        <RichTextEditor
                          className="field-rich-text"
                          minHeight={120}
                          placeholder="Registrar gostos pessoais"
                          value={personalForm.gostosPessoais}
                          onChange={(nextValue) => setPersonalForm((prev) => ({ ...prev, gostosPessoais: nextValue }))}
                        />
                      </div>

                      <div className="field">
                        <label>
                          Red Flags {renderInfoIcon("Red Flags", "redFlags")}
                        </label>
                        <RichTextEditor
                          className="field-rich-text"
                          minHeight={120}
                          placeholder="Registrar red flags"
                          value={personalForm.redFlags}
                          onChange={(nextValue) => setPersonalForm((prev) => ({ ...prev, redFlags: nextValue }))}
                        />
                      </div>
                    </div>

                    <div className="personal-info-column personal-info-column--stack personal-info-bio">
                      <div className="field field-bio">
                        <label>
                          BIO {renderInfoIcon("BIO", "bio")}
                        </label>
                        <RichTextEditor
                          className="field-rich-text"
                          minHeight={260}
                          placeholder="Registrar bio"
                          value={personalForm.bio}
                          onChange={(nextValue) => setPersonalForm((prev) => ({ ...prev, bio: nextValue }))}
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
                  </div>
                  <div className="fields fields--table">
                    <table className="history-table" style={{ tableLayout: 'fixed', width: '100%' }}>
                      <thead>
                        <tr>
                          <th style={{ width: '150px', minWidth: '150px', maxWidth: '150px' }}>Data</th>
                          <th>Resumo</th>
                          <th>Tarefas acordadas</th>
                          <th>Proximos assuntos</th>
                          <th style={{ width: '96px', minWidth: '96px', maxWidth: '96px' }}>Acoes</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr className="history-edit">
                          <td className="date-cell">
                            <MaskedDateInput
                              className="date-input"
                              value={oneOnOneDraft.data}
                              inputRef={oneOnOneDateInputRef}
                              onChange={(nextValue) => setOneOnOneDraft((prev) => ({ ...prev, data: nextValue }))}
                              ariaLabel="Data do 1:1"
                            />
                          </td>
                          <td>
                            <RichTextEditor
                              minHeight={62}
                              value={oneOnOneDraft.resumo}
                              onChange={(nextValue) => setOneOnOneDraft((prev) => ({ ...prev, resumo: nextValue }))}
                            />
                          </td>
                          <td>
                            <RichTextEditor
                              minHeight={62}
                              value={oneOnOneDraft.tarefasAcordadas}
                              onChange={(nextValue) => setOneOnOneDraft((prev) => ({ ...prev, tarefasAcordadas: nextValue }))}
                            />
                          </td>
                          <td>
                            <RichTextEditor
                              minHeight={62}
                              value={oneOnOneDraft.proximosAssuntos}
                              onChange={(nextValue) => setOneOnOneDraft((prev) => ({ ...prev, proximosAssuntos: nextValue }))}
                            />
                          </td>
                          <td className="col-actions">
                            <button type="button" className="btn ghost small" onClick={handleSaveOneOnOne}>
                              Salvar
                            </button>
                          </td>
                        </tr>
                        {oneOnOnes.map((item) => (
                          <tr key={`${item.data}-${item.resumo}`}>
                            <td>{toDisplayDate(item.data)}</td>
                            <td><RichTextView value={item.resumo} /></td>
                            <td><RichTextView value={item.tarefasAcordadas} /></td>
                            <td><RichTextView value={item.proximosAssuntos} /></td>
                            <td>
                              <button type="button" className="btn ghost small danger-outline" onClick={() => handleDeleteOneOnOne(item.data)}>
                                Excluir
                              </button>
                            </td>
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
                  </div>

                  <div className="fields fields--table">
                    <table className="history-table feedback-table" style={{ tableLayout: 'fixed', width: '100%' }}>
                      <thead>
                        <tr>
                          <th style={{ width: '150px', minWidth: '150px', maxWidth: '150px' }}>Data</th>
                          <th>Conteudo do feedback</th>
                          <th>Receptividade</th>
                          <th>Polaridade</th>
                          <th style={{ width: '96px', minWidth: '96px', maxWidth: '96px' }}>Acoes</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr className="history-edit">
                          <td className="date-cell">
                            <MaskedDateInput
                              className="date-input"
                              value={feedbackDraft.data}
                              inputRef={feedbackDateInputRef}
                              onChange={(nextValue) => setFeedbackDraft((prev) => ({ ...prev, data: nextValue }))}
                              ariaLabel="Data do feedback"
                            />
                          </td>
                          <td>
                            <RichTextEditor
                              className="feedback-rich-editor"
                              minHeight={62}
                              value={feedbackDraft.conteudo}
                              onChange={(nextValue) => setFeedbackDraft((prev) => ({ ...prev, conteudo: nextValue }))}
                            />
                          </td>
                          <td>
                            <RichTextEditor
                              className="feedback-rich-editor"
                              minHeight={62}
                              value={feedbackDraft.receptividade}
                              onChange={(nextValue) => setFeedbackDraft((prev) => ({ ...prev, receptividade: nextValue }))}
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
                          <td className="col-actions">
                            <button type="button" className="btn ghost small" onClick={handleSaveFeedback}>
                              Salvar
                            </button>
                          </td>
                        </tr>
                        {feedbacks.map((item) => (
                          <tr key={`${item.data}-${item.conteudo}`}>
                            <td>{toDisplayDate(item.data)}</td>
                            <td><RichTextView value={item.conteudo} /></td>
                            <td><RichTextView value={item.receptividade} /></td>
                            <td>{item.polaridade}</td>
                            <td>
                              <button type="button" className="btn ghost small danger-outline" onClick={() => handleDeleteFeedback(item.data)}>
                                Excluir
                              </button>
                            </td>
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
                  </div>

                  <div className="fields fields--table table-scroll">
                    <table className="history-table classification-table culture-table" style={{ tableLayout: 'fixed', width: '100%' }}>
                      <thead>
                        <tr>
                          <th className="col-date" style={{ width: '150px', minWidth: '150px', maxWidth: '150px' }}>Data</th>
                          <th className="col-score">Aprender e Melhorar Sempre</th>
                          <th className="col-score">Atitude de Dono</th>
                          <th className="col-score">Buscar os melhores resultados para os clientes</th>
                          <th className="col-score">Espirito de Equipe</th>
                          <th className="col-score">Excelencia</th>
                          <th className="col-score">Fazer Acontecer</th>
                          <th className="col-score">Inovar para Inspirar</th>
                          <th className="col-actions" style={{ width: '96px', minWidth: '96px', maxWidth: '96px' }}>Acoes</th>
                        </tr>
                      </thead>
                      <tbody>
                        <tr className="history-edit">
                          <td className="date-cell">
                            <MaskedDateInput
                              className="date-input"
                              value={cultureDraft.data}
                              inputRef={culturaDateInputRef}
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
                          <td className="col-actions">
                            <button type="button" className="btn ghost small" onClick={handleSaveCultura}>
                              Salvar
                            </button>
                          </td>
                        </tr>
                        {cultureEntries.map((item) => (
                          <tr key={item.data}>
                            <td>{toDisplayDate(item.data)}</td>
                            <td>{item.aprenderEMelhorarSempre}</td>
                            <td>{item.atitudeDeDono}</td>
                            <td>{item.buscarMelhoresResultadosParaClientes}</td>
                            <td>{item.espiritoDeEquipe}</td>
                            <td>{item.excelencia}</td>
                            <td>{item.fazerAcontecer}</td>
                            <td>{item.inovarParaInspirar}</td>
                            <td className="col-actions">
                              <button type="button" className="btn ghost small danger-outline" onClick={() => handleDeleteCultura(item.data)}>
                                Excluir
                              </button>
                            </td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  </div>
                </section>
              ) : null}

              {/* Fatos e Observacoes tab removed */}

              {activeTab === "Classificacao de Perfil" ? (
                <section className="panel section single-column">
                  <div className="panel-header">
                    <h3 className="section-title">{getTabLabel(activeTab)}</h3>
                  </div>
                  <div className="fields fields--prop-tabs">
                    <ClassificacaoPerfilColumnsSection
                      groups={propertySectionData[activeTab] || []}
                      renderInfoIcon={renderInfoIcon}
                      classificacaoPerfilDraft={classificacaoPerfilDraft}
                      onDraftChange={(tipo, field, value) =>
                        setClassificacaoPerfilDraft((prev) => ({
                          ...prev,
                          [tipo]: { ...(prev[tipo] || { data: "", valor: "" }), [field]: value }
                        }))
                      }
                      onSaveColumn={handleSaveClassificacaoPerfilByTipo}
                      onDeleteColumn={handleDeleteClassificacaoPerfilByTipo}
                      dateInputRefs={classificacaoDataInputRefs}
                    />
                  </div>
                </section>
              ) : null}

              {activeTab === "CHAVE" ? (
                <section className="panel section single-column">
                  <div className="panel-header">
                    <h3 className="section-title">CHAVE</h3>
                  </div>
                  <div className="fields fields--prop-tabs">
                    <ChaveSection
                      conhecimentosHistorico={(conhecimentosHistorico || []).map((r) => ({
                        data: toDisplayDate(r.data),
                        valor: r.valor
                      }))}
                      conhecimentosDraft={conhecimentosDraft}
                      onConhecimentosDraftChange={(field, value) =>
                        setConhecimentosDraft(prev => ({
                          ...prev,
                          [field]: value
                        }))
                      }
                      onSaveConhecimentos={handleSaveConhecimentos}
                      onDeleteConhecimentos={handleDeleteConhecimentos}
                      conhecimentosDateInputRef={conhecimentosDateInputRef}
                      habilidadesHistorico={(habilidadesHistorico || []).map((r) => ({
                        data: toDisplayDate(r.data),
                        valor: r.valor
                      }))}
                      habilidadesDraft={habilidadesDraft}
                      onHabilidadesDraftChange={(field, value) =>
                        setHabilidadesDraft((prev) => ({
                          ...prev,
                          [field]: value
                        }))
                      }
                      onSaveHabilidades={handleSaveHabilidades}
                      onDeleteHabilidades={handleDeleteHabilidades}
                      habilidadesDateInputRef={habilidadesDateInputRef}
                      atitudesHistorico={(atitudesHistorico || []).map((r) => ({
                        data: toDisplayDate(r.data),
                        valor: r.valor
                      }))}
                      atitudesDraft={atitudesDraft}
                      onAtitudesDraftChange={(field, value) =>
                        setAtitudesDraft((prev) => ({
                          ...prev,
                          [field]: value
                        }))
                      }
                      onSaveAtitudes={handleSaveAtitudes}
                      onDeleteAtitudes={handleDeleteAtitudes}
                      atitudesDateInputRef={atitudesDateInputRef}
                      valoresHistorico={(valoresHistorico || []).map((r) => ({
                        data: toDisplayDate(r.data),
                        valor: r.valor
                      }))}
                      valoresDraft={valoresDraft}
                      onValoresDraftChange={(field, value) =>
                        setValoresDraft((prev) => ({
                          ...prev,
                          [field]: value
                        }))
                      }
                      onSaveValores={handleSaveValores}
                      onDeleteValores={handleDeleteValores}
                      valoresDateInputRef={valoresDateInputRef}
                      expectativasHistorico={(expectativasHistorico || []).map((r) => ({
                        data: toDisplayDate(r.data),
                        valor: r.valor
                      }))}
                      expectativasDraft={expectativasDraft}
                      onExpectativasDraftChange={(field, value) =>
                        setExpectativasDraft((prev) => ({
                          ...prev,
                          [field]: value
                        }))
                      }
                      onSaveExpectativas={handleSaveExpectativas}
                      onDeleteExpectativas={handleDeleteExpectativas}
                      expectativasDateInputRef={expectativasDateInputRef}
                      renderInfoIcon={renderInfoIcon}
                    />
                  </div>
                </section>
              ) : null}

              {activeTab === "GROW / PDI" ? (
                <section className="panel section single-column">
                  <div className="panel-header">
                    <h3 className="section-title">{activeTab}</h3>
                  </div>
                  <div className="fields fields--prop-tabs">
                    <GrowPdiSection
                      metasHistorico={(metasHistorico || []).map((r) => ({
                        data: toDisplayDate(r.data),
                        valor: r.valor
                      }))}
                      metasDraft={metasDraft}
                      onMetasDraftChange={(field, value) =>
                        setMetasDraft((prev) => ({
                          ...prev,
                          [field]: value
                        }))
                      }
                      onSaveMetas={handleSaveMetas}
                      onDeleteMetas={handleDeleteMetas}
                      metasDateInputRef={metasDateInputRef}
                      situacaoAtualHistorico={(situacaoAtualHistorico || []).map((r) => ({
                        data: toDisplayDate(r.data),
                        valor: r.valor
                      }))}
                      situacaoAtualDraft={situacaoAtualDraft}
                      onSituacaoAtualDraftChange={(field, value) =>
                        setSituacaoAtualDraft((prev) => ({
                          ...prev,
                          [field]: value
                        }))
                      }
                      onSaveSituacaoAtual={handleSaveSituacaoAtual}
                      onDeleteSituacaoAtual={handleDeleteSituacaoAtual}
                      situacaoAtualDateInputRef={situacaoAtualDateInputRef}
                      opcoesHistorico={(opcoesHistorico || []).map((r) => ({
                        data: toDisplayDate(r.data),
                        valor: r.valor
                      }))}
                      opcoesDraft={opcoesDraft}
                      onOpcoesDraftChange={(field, value) =>
                        setOpcoesDraft((prev) => ({
                          ...prev,
                          [field]: value
                        }))
                      }
                      onSaveOpcoes={handleSaveOpcoes}
                      onDeleteOpcoes={handleDeleteOpcoes}
                      opcoesDateInputRef={opcoesDateInputRef}
                      proximosPassosHistorico={(proximosPassosHistorico || []).map((r) => ({
                        data: toDisplayDate(r.data),
                        valor: r.valor
                      }))}
                      proximosPassosDraft={proximosPassosDraft}
                      onProximosPassosDraftChange={(field, value) =>
                        setProximosPassosDraft((prev) => ({
                          ...prev,
                          [field]: value
                        }))
                      }
                      onSaveProximosPassos={handleSaveProximosPassos}
                      onDeleteProximosPassos={handleDeleteProximosPassos}
                      proximosPassosDateInputRef={proximosPassosDateInputRef}
                      renderInfoIcon={renderInfoIcon}
                    />
                  </div>
                </section>
              ) : null}

              {activeTab === "SWOT" ? (
                <section className="panel section single-column">
                  <div className="panel-header">
                    <h3 className="section-title">{activeTab}</h3>
                  </div>
                  <div className="fields fields--prop-tabs">
                    <SwotSection
                      fortalezasHistorico={(fortalezasHistorico || []).map((r) => ({
                        data: toDisplayDate(r.data),
                        valor: r.valor
                      }))}
                      fortalezasDraft={fortalezasDraft}
                      onFortalezasDraftChange={(field, value) =>
                        setFortalezasDraft((prev) => ({
                          ...prev,
                          [field]: value
                        }))
                      }
                      onSaveFortalezas={handleSaveFortalezas}
                      onDeleteFortalezas={handleDeleteFortalezas}
                      fortalezasDateInputRef={fortalezasDateInputRef}
                      oportunidadesHistorico={(oportunidadesHistorico || []).map((r) => ({
                        data: toDisplayDate(r.data),
                        valor: r.valor
                      }))}
                      oportunidadesDraft={oportunidadesDraft}
                      onOportunidadesDraftChange={(field, value) =>
                        setOportunidadesDraft((prev) => ({
                          ...prev,
                          [field]: value
                        }))
                      }
                      onSaveOportunidades={handleSaveOportunidades}
                      onDeleteOportunidades={handleDeleteOportunidades}
                      oportunidadesDateInputRef={oportunidadesDateInputRef}
                      fraquezasHistorico={(fraquezasHistorico || []).map((r) => ({
                        data: toDisplayDate(r.data),
                        valor: r.valor
                      }))}
                      fraquezasDraft={fraquezasDraft}
                      onFraquezasDraftChange={(field, value) =>
                        setFraquezasDraft((prev) => ({
                          ...prev,
                          [field]: value
                        }))
                      }
                      onSaveFraquezas={handleSaveFraquezas}
                      onDeleteFraquezas={handleDeleteFraquezas}
                      fraquezasDateInputRef={fraquezasDateInputRef}
                      ameacasHistorico={(ameacasHistorico || []).map((r) => ({
                        data: toDisplayDate(r.data),
                        valor: r.valor
                      }))}
                      ameacasDraft={ameacasDraft}
                      onAmeacasDraftChange={(field, value) =>
                        setAmeacasDraft((prev) => ({
                          ...prev,
                          [field]: value
                        }))
                      }
                      onSaveAmeacas={handleSaveAmeacas}
                      onDeleteAmeacas={handleDeleteAmeacas}
                      ameacasDateInputRef={ameacasDateInputRef}
                      renderInfoIcon={renderInfoIcon}
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
        <RichTextView value={hoverTooltip.text} className="tooltip-rich-text" />
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
            <RichTextEditor
              className="tooltip-rich-editor"
              minHeight={220}
              placeholder="Escreva o tooltip formatado"
              value={tooltipModal.text}
              onChange={(nextValue) => setTooltipModal((prev) => ({ ...prev, text: nextValue }))}
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

