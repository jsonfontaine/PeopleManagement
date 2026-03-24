-- Script unico de criacao da base e tabelas para People Management (SQLite)
-- Padrao adotado:
-- 1) Somente a tabela Liderados possui coluna Id
-- 2) Coluna de referencia do liderado sempre se chama IdLiderado
-- 3) Demais tabelas usam nome no singular

CREATE TABLE Liderados (
    Id TEXT PRIMARY KEY,
    Nome TEXT NOT NULL,
    DataCriacaoUtc TEXT NOT NULL
);

CREATE INDEX IX_Liderados_Nome ON Liderados (Nome);

CREATE TABLE InformacaoPessoal (
    IdLiderado TEXT PRIMARY KEY,
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
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE TABLE Feedback (
    IdLiderado TEXT NOT NULL,
    Data DATE NOT NULL,
    Conteudo TEXT NOT NULL,
    Receptividade TEXT NOT NULL,
    Polaridade TEXT NOT NULL,
    PRIMARY KEY (IdLiderado, Data),
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE INDEX IX_Feedback_IdLiderado_Data ON Feedback (IdLiderado, Data);

CREATE TABLE OneOnOne (
    IdLiderado TEXT NOT NULL,
    Data DATE NOT NULL,
    Resumo TEXT NOT NULL,
    TarefasAcordadas TEXT NOT NULL,
    ProximosAssuntos TEXT NOT NULL,
    PRIMARY KEY (IdLiderado, Data),
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE INDEX IX_OneOnOne_IdLiderado_Data ON OneOnOne (IdLiderado, Data);

CREATE TABLE CulturaAvaliacao (
    IdLiderado TEXT NOT NULL,
    Data DATE NOT NULL,
    AprenderEMelhorarSempre INTEGER NOT NULL,
    AtitudeDeDono INTEGER NOT NULL,
    BuscarMelhoresResultadosParaClientes INTEGER NOT NULL,
    EspiritoDeEquipe INTEGER NOT NULL,
    Excelencia INTEGER NOT NULL,
    FazerAcontecer INTEGER NOT NULL,
    InovarParaInspirar INTEGER NOT NULL,
    PRIMARY KEY (IdLiderado, Data),
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE TABLE Tooltip (
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

CREATE TABLE Conhecimento (
    IdLiderado TEXT NOT NULL,
    Data TEXT NOT NULL,
    Valor TEXT NOT NULL,
    PRIMARY KEY (IdLiderado, Data),
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE TABLE Habilidade (
    IdLiderado TEXT NOT NULL,
    Data TEXT NOT NULL,
    Valor TEXT NOT NULL,
    PRIMARY KEY (IdLiderado, Data),
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE TABLE Atitude (
    IdLiderado TEXT NOT NULL,
    Data TEXT NOT NULL,
    Valor TEXT NOT NULL,
    PRIMARY KEY (IdLiderado, Data),
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE TABLE Valor (
    IdLiderado TEXT NOT NULL,
    Data TEXT NOT NULL,
    Valor TEXT NOT NULL,
    PRIMARY KEY (IdLiderado, Data),
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE TABLE Expectativa (
    IdLiderado TEXT NOT NULL,
    Data TEXT NOT NULL,
    Valor TEXT NOT NULL,
    PRIMARY KEY (IdLiderado, Data),
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE TABLE Meta (
    IdLiderado TEXT NOT NULL,
    Data TEXT NOT NULL,
    Valor TEXT NOT NULL,
    PRIMARY KEY (IdLiderado, Data),
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE TABLE SituacaoAtual (
    IdLiderado TEXT NOT NULL,
    Data TEXT NOT NULL,
    Valor TEXT NOT NULL,
    PRIMARY KEY (IdLiderado, Data),
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE TABLE Opcao (
    IdLiderado TEXT NOT NULL,
    Data TEXT NOT NULL,
    Valor TEXT NOT NULL,
    PRIMARY KEY (IdLiderado, Data),
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE TABLE ProximoPasso (
    IdLiderado TEXT NOT NULL,
    Data TEXT NOT NULL,
    Valor TEXT NOT NULL,
    PRIMARY KEY (IdLiderado, Data),
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE TABLE Fortaleza (
    IdLiderado TEXT NOT NULL,
    Data TEXT NOT NULL,
    Valor TEXT NOT NULL,
    PRIMARY KEY (IdLiderado, Data),
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE TABLE Oportunidade (
    IdLiderado TEXT NOT NULL,
    Data TEXT NOT NULL,
    Valor TEXT NOT NULL,
    PRIMARY KEY (IdLiderado, Data),
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE TABLE Fraqueza (
    IdLiderado TEXT NOT NULL,
    Data TEXT NOT NULL,
    Valor TEXT NOT NULL,
    PRIMARY KEY (IdLiderado, Data),
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);

CREATE TABLE Ameaca (
    IdLiderado TEXT NOT NULL,
    Data TEXT NOT NULL,
    Valor TEXT NOT NULL,
    PRIMARY KEY (IdLiderado, Data),
    FOREIGN KEY (IdLiderado) REFERENCES Liderados(Id) ON DELETE CASCADE
);


