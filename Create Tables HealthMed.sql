CREATE TABLE Perfil (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PerfilDescricao NVARCHAR(100) NOT NULL
);

CREATE TABLE Usuario (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UsuarioNome NVARCHAR(255) NOT NULL,
    UsuarioEmail NVARCHAR(255) UNIQUE NOT NULL,
    UsuarioCPF NVARCHAR(14) UNIQUE NOT NULL,
    UsuarioSenha NVARCHAR(255) NOT NULL,
    PerfilId INT NOT NULL,
    CriadoEm DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (PerfilId) REFERENCES Perfil(Id)
);

CREATE TABLE Medico (
    Id INT PRIMARY KEY,
    MedicoCRM NVARCHAR(50) NOT NULL,
	MedicoValorConsulta DECIMAL(18,2) NOT NULL,
	CriadoEm DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (Id) REFERENCES Usuario(Id)
);

CREATE TABLE Paciente (
    Id INT PRIMARY KEY,
	PacienteDataNascimento DATETIME2 NOT NULL,
	PacienteTelefone NVARCHAR(11),
	PacienteObservacao NVARCHAR(255),
	CriadoEm DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (Id) REFERENCES Usuario(Id)
);

CREATE TABLE Especialidade (
    Id INT PRIMARY KEY IDENTITY(1,1),
    EspecialidadeDescricao NVARCHAR(100) NOT NULL
);

CREATE TABLE MedicoEspecialidade (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MedicoId INT NOT NULL,
    EspecialidadeId INT NOT NULL,
    FOREIGN KEY (MedicoId) REFERENCES Medico(Id),
    FOREIGN KEY (EspecialidadeId) REFERENCES Especialidade(Id)
);

CREATE TABLE HorarioDisponivel (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MedicoId INT NOT NULL,
    HorarioDisponivelDiaSemana INT NOT NULL,
    HorarioDisponivelHoraInicio TIME NOT NULL,
    HorarioDisponivelHoraFim TIME NOT NULL,

    CONSTRAINT FK_HorarioDisponivel_Medico FOREIGN KEY (MedicoId)
        REFERENCES Medico(Id)
        ON DELETE CASCADE
);

CREATE TABLE AgendamentoStatus (
    Id INT PRIMARY KEY IDENTITY(1,1),
    AgendamentoStatusDescricao NVARCHAR(100) NOT NULL
);

CREATE TABLE MotivoCancelamento (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MotivoCancelamentoDescricao NVARCHAR(255) NOT NULL
);

CREATE TABLE Agendamento (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PacienteId INT NOT NULL,
    MedicoId INT NOT NULL,
    AgendamentoDataHora DATETIME NOT NULL,
	EspecialidadeId INT NOT NULL,
    AgendamentoStatusId INT NOT NULL,
    MotivoCancelamentoId INT NULL,
	AgendamentoValor DECIMAL (18,2) NOT NULL,
    FOREIGN KEY (PacienteId) REFERENCES Paciente(Id),
    FOREIGN KEY (MedicoId) REFERENCES Medico(Id),
    FOREIGN KEY (AgendamentoStatusId) REFERENCES AgendamentoStatus(Id),
    FOREIGN KEY (MotivoCancelamentoId) REFERENCES MotivoCancelamento(Id),
	FOREIGN KEY (EspecialidadeId) REFERENCES Especialidade(Id)
);
