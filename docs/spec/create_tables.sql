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
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE
);

-- Tabela: Habilidade
CREATE TABLE Habilidade (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE
);

-- Tabela: Atitude
CREATE TABLE Atitude (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE
);

-- Tabela: Valor
CREATE TABLE Valor (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE
);

-- Tabela: Expectativa
CREATE TABLE Expectativa (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE
);

-- Tabela: Metas
CREATE TABLE Metas (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE
);

-- Tabela: SituacaoAtual
CREATE TABLE SituacaoAtual (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE
);

-- Tabela: Opcoes
CREATE TABLE Opcoes (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE
);

-- Tabela: ProximosPassos
CREATE TABLE ProximosPassos (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE
);

-- Tabela: DISC
CREATE TABLE DISC (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE
);

-- Tabela: Personalidade
CREATE TABLE Personalidade (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE
);

-- Tabela: NineBox
CREATE TABLE NineBox (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE
);

-- Tabela: CulturaGenial
CREATE TABLE CulturaGenial (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    IdLiderado TEXT NOT NULL,
    Protagonismo INTEGER,
    Colaboracao INTEGER,
    Inovacao INTEGER,
    OrientacaoParaResultado INTEGER,
    FocoNoCliente INTEGER,
    Etica INTEGER,
    Transparencia INTEGER,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE
);

-- Tabela: Feedback
CREATE TABLE Feedback (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    IdLiderado TEXT NOT NULL,
    Conteudo TEXT,
    Polaridade TEXT,
    Receptividade TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE
);

-- Tabela: OneOnOne
CREATE TABLE OneOnOne (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    IdLiderado TEXT NOT NULL,
    Resumo TEXT,
    Tarefas TEXT,
    ProximosAssuntos TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE
);

-- Tabela: Fortaleza
CREATE TABLE Fortaleza (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE
);

-- Tabela: Oportunidade
CREATE TABLE Oportunidade (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE
);

-- Tabela: Fraqueza
CREATE TABLE Fraqueza (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE
);

-- Tabela: Ameaca
CREATE TABLE Ameaca (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE
);

-- Tabela: FatoObservacao
CREATE TABLE FatoObservacao (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    IdLiderado TEXT NOT NULL,
    Valor TEXT,
    Data DATE,
    FOREIGN KEY (IdLiderado) REFERENCES Liderado(Id) ON DELETE CASCADE
);

