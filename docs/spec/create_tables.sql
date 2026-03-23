-- Script de criação da base e tabelas para People Management (SQLite)
-- Alinhado ao schema atualmente implementado no backend

CREATE TABLE Liderados (
    Id TEXT PRIMARY KEY,
    Nome TEXT NOT NULL,
    DataCriacaoUtc TEXT NOT NULL
);

CREATE INDEX IX_Liderados_Nome ON Liderados (Nome);

CREATE TABLE InformacoesPessoais (
    LideradoId TEXT PRIMARY KEY,
    Nome TEXT NOT NULL,
    DataNascimento DATE,
    EstadoCivil TEXT,
    QuantidadeFilhos INTEGER,
    DataContratacao DATE,
    Cargo TEXT,
    DataInicioCargo DATE,
    AspiracaoCarreira TEXT,
    GostosPessoais TEXT,
    RedFlags TEXT,
    Bio TEXT,
    FOREIGN KEY (LideradoId) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE TABLE Feedbacks (
    Id TEXT PRIMARY KEY,
    LideradoId TEXT NOT NULL,
    Data DATE NOT NULL,
    Conteudo TEXT NOT NULL,
    Receptividade TEXT NOT NULL,
    Polaridade TEXT NOT NULL,
    FOREIGN KEY (LideradoId) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE INDEX IX_Feedbacks_LideradoId_Data ON Feedbacks (LideradoId, Data);

CREATE TABLE OneOnOnes (
    Id TEXT PRIMARY KEY,
    LideradoId TEXT NOT NULL,
    Data DATE NOT NULL,
    Resumo TEXT NOT NULL,
    TarefasAcordadas TEXT NOT NULL,
    ProximosAssuntos TEXT NOT NULL,
    FOREIGN KEY (LideradoId) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE INDEX IX_OneOnOnes_LideradoId_Data ON OneOnOnes (LideradoId, Data);

CREATE TABLE CulturaAvaliacoes (
    Id TEXT PRIMARY KEY,
    LideradoId TEXT NOT NULL,
    Data DATE NOT NULL,
    AprenderEMelhorarSempre INTEGER NOT NULL,
    AtitudeDeDono INTEGER NOT NULL,
    BuscarMelhoresResultadosParaClientes INTEGER NOT NULL,
    EspiritoDeEquipe INTEGER NOT NULL,
    Excelencia INTEGER NOT NULL,
    FazerAcontecer INTEGER NOT NULL,
    InovarParaInspirar INTEGER NOT NULL,
    FOREIGN KEY (LideradoId) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE UNIQUE INDEX IX_CulturaAvaliacoes_LideradoId_Data ON CulturaAvaliacoes (LideradoId, Data);

CREATE TABLE Tooltips (
    ChaveCampo TEXT PRIMARY KEY,
    Texto TEXT NOT NULL
);

CREATE TABLE Disc (
    IdLiderado TEXT NOT NULL,
    Data TEXT NOT NULL,
    Valor TEXT NOT NULL,
    PRIMARY KEY (IdLiderado, Data),
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE TABLE Personalidade (
    IdLiderado TEXT NOT NULL,
    Data TEXT NOT NULL,
    Valor TEXT NOT NULL,
    PRIMARY KEY (IdLiderado, Data),
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE TABLE NineBox (
    IdLiderado TEXT NOT NULL,
    Data TEXT NOT NULL,
    Valor TEXT NOT NULL,
    PRIMARY KEY (IdLiderado, Data),
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);

-- Classificacao de Perfil e apenas agrupador de UI (nao existe tabela dedicada).

CREATE TABLE PropriedadesHistoricas (
    IdLiderado TEXT NOT NULL,
    Tipo TEXT NOT NULL,
    Data TEXT NOT NULL,
    Valor TEXT NOT NULL,
    PRIMARY KEY (IdLiderado, Tipo, Data),
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE INDEX IX_PropriedadesHistoricas_IdLiderado_Tipo ON PropriedadesHistoricas (IdLiderado, Tipo);

