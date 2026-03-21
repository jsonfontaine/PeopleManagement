-- Script de criação da base e tabelas para People Management (SQLite)
-- Gerado conforme modelagem de dados do TSD.md

-- Tabela: Liderado
CREATE TABLE Liderado (
    Id TEXT PRIMARY KEY,
    Nome TEXT NOT NULL,
    DataNascimento DATE,
    EstadoCivil TEXT,
    QuantidadeFilhos INTEGER,
    DataContratacao DATE,
    Cargo TEXT,
    DataInicioCargo DATE,
    Aspiracao TEXT,
    GostosPessoais TEXT,
    Bio TEXT,
    RedFlags TEXT
);

-- Tabela: Propriedade
CREATE TABLE Propriedade (
    Id TEXT PRIMARY KEY,
    Nome TEXT NOT NULL,
    Secao TEXT,
    Tooltip TEXT
);

-- Tabela: Conhecimento
CREATE TABLE Conhecimento (
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE,
    PRIMARY KEY (IdLiderado, Data)
);

-- Tabela: Habilidade
CREATE TABLE Habilidade (
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE,
    PRIMARY KEY (IdLiderado, Data)
);

-- Tabela: Atitude
CREATE TABLE Atitude (
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE,
    PRIMARY KEY (IdLiderado, Data)
);

-- Tabela: Valor
CREATE TABLE Valor (
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE,
    PRIMARY KEY (IdLiderado, Data)
);

-- Tabela: Expectativa
CREATE TABLE Expectativa (
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE,
    PRIMARY KEY (IdLiderado, Data)
);

-- Tabela: Metas
CREATE TABLE Metas (
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE,
    PRIMARY KEY (IdLiderado, Data)
);

-- Tabela: SituacaoAtual
CREATE TABLE SituacaoAtual (
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE,
    PRIMARY KEY (IdLiderado, Data)
);

-- Tabela: Opcoes
CREATE TABLE Opcoes (
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE,
    PRIMARY KEY (IdLiderado, Data)
);

-- Tabela: ProximosPassos
CREATE TABLE ProximosPassos (
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE,
    PRIMARY KEY (IdLiderado, Data)
);

-- Tabela: DISC
CREATE TABLE DISC (
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE,
    PRIMARY KEY (IdLiderado, Data)
);

-- Tabela: Personalidade
CREATE TABLE Personalidade (
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE,
    PRIMARY KEY (IdLiderado, Data)
);

-- Tabela: NineBox
CREATE TABLE NineBox (
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE,
    PRIMARY KEY (IdLiderado, Data)
);

-- Tabela: CulturaGenial
CREATE TABLE CulturaGenial (
    IdLiderado TEXT NOT NULL,
    Protagonismo INTEGER,
    Colaboracao INTEGER,
    Inovacao INTEGER,
    OrientacaoParaResultado INTEGER,
    FocoNoCliente INTEGER,
    Etica INTEGER,
    Transparencia INTEGER,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE,
    PRIMARY KEY (IdLiderado, Data)
);

-- Tabela: Feedback
CREATE TABLE Feedback (
    IdLiderado TEXT NOT NULL,
    Conteudo TEXT,
    Polaridade TEXT,
    Receptividade TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE,
    PRIMARY KEY (IdLiderado, Data)
);

-- Tabela: OneOnOne
CREATE TABLE OneOnOne (
    IdLiderado TEXT NOT NULL,
    Resumo TEXT,
    Tarefas TEXT,
    ProximosAssuntos TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE,
    PRIMARY KEY (IdLiderado, Data)
);

-- Tabela: Fortaleza
CREATE TABLE Fortaleza (
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE,
    PRIMARY KEY (IdLiderado, Data)
);

-- Tabela: Oportunidade
CREATE TABLE Oportunidade (
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE,
    PRIMARY KEY (IdLiderado, Data)
);

-- Tabela: Fraqueza
CREATE TABLE Fraqueza (
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE,
    PRIMARY KEY (IdLiderado, Data)
);

-- Tabela: Ameaca
CREATE TABLE Ameaca (
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE,
    PRIMARY KEY (IdLiderado, Data)
);

-- Tabela: FatoObservacao
CREATE TABLE FatoObservacao (
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE,
    PRIMARY KEY (IdLiderado, Data)
);

-- Tabela: HistoricoAlteracoes
CREATE TABLE HistoricoAlteracoes (
    Id TEXT PRIMARY KEY,
    LideradoId TEXT NOT NULL,
    Secao TEXT NOT NULL,
    Campo TEXT NOT NULL,
    ValorAnterior TEXT,
    ValorNovo TEXT NOT NULL,
    DataAlteracaoUtc TEXT NOT NULL,
    UsuarioResponsavel TEXT NOT NULL,
    FOREIGN KEY (LideradoId) REFERENCES Liderado(Id) ON DELETE CASCADE
);
