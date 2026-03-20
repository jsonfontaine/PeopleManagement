# Frontend - People Management

Frontend React/Vite organizado em `frontend`, seguindo PRD/TSD e comportamento do prototipo de `docs/spec/prototype`:

- Dashboard com cards horizontais e radar cultural por liderado
- Visao individual com breadcrumb, combobox e abas por secao
- Secoes com tabela historica e primeira linha editavel
- Radar cultural com seletor de data, scroll do mouse e transicao animada
- Tooltip no hover e edicao por duplo clique com persistencia em `PUT /api/tooltips/{chaveCampo}`

## Requisitos

- Node.js 20+
- Backend em execucao (padrao: `http://localhost:5252`)

## Desenvolvimento

```bash
npm install
npm run dev
```

## Build

```bash
npm run build
npm run preview
```

## Configuracao opcional

Se o backend estiver em outra URL, defina `VITE_BACKEND_URL` antes de rodar:

```bash
set VITE_BACKEND_URL=http://localhost:5252
npm run dev
```

