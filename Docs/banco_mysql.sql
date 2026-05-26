CREATE DATABASE IF NOT EXISTS medcontrol_db;
USE medcontrol_db;

CREATE TABLE SETOR (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(45) NOT NULL,
    Andar VARCHAR(45) NOT NULL,
    Descricao VARCHAR(100) NOT NULL
);

CREATE TABLE MEDICO (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(80) NOT NULL,
    Crm VARCHAR(10) NOT NULL,
    Especialidade VARCHAR(45) NOT NULL,
    Telefone VARCHAR(11) NOT NULL
);

CREATE TABLE MEDICAMENTO (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(60) NOT NULL,
    Dosagem VARCHAR(45) NOT NULL,
    ViaAdministracao VARCHAR(45) NOT NULL,
    Estoque INT NOT NULL
);

CREATE TABLE PACIENTE (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(80) NOT NULL,
    Cpf VARCHAR(14) NOT NULL,
    DataNascimento DATETIME NOT NULL,
    Leito VARCHAR(45) NOT NULL,
    Telefone VARCHAR(11) NOT NULL,
    SetorId INT NOT NULL,
    FOREIGN KEY (SetorId) REFERENCES SETOR(Id)
);

CREATE TABLE PRESCRICAO (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    DataPrescricao DATETIME NOT NULL,
    Observacao VARCHAR(100) NOT NULL,
    MedicoId INT NOT NULL,
    PacienteId INT NOT NULL,
    FOREIGN KEY (MedicoId) REFERENCES MEDICO(Id),
    FOREIGN KEY (PacienteId) REFERENCES PACIENTE(Id)
);

CREATE TABLE PRESCRICAO_MEDICAMENTO (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Quantidade INT NOT NULL,
    Frequencia VARCHAR(45) NOT NULL,
    Horario TIME NOT NULL,
    PrescricaoId INT NOT NULL,
    MedicamentoId INT NOT NULL,
    FOREIGN KEY (PrescricaoId) REFERENCES PRESCRICAO(Id),
    FOREIGN KEY (MedicamentoId) REFERENCES MEDICAMENTO(Id)
);

INSERT INTO SETOR (Nome, Andar, Descricao) VALUES
('UTI', '1', 'Unidade de terapia intensiva'),
('Enfermaria', '2', 'Setor de internacao');

INSERT INTO MEDICO (Nome, Crm, Especialidade, Telefone) VALUES
('Joao Pereira', '123456', 'Clinico Geral', '14999990000'),
('Ana Souza', '654321', 'Cardiologia', '14999991111');

INSERT INTO MEDICAMENTO (Nome, Dosagem, ViaAdministracao, Estoque) VALUES
('Dipirona', '500mg', 'Oral', 100),
('Amoxicilina', '875mg', 'Oral', 50);

INSERT INTO PACIENTE (Nome, Cpf, DataNascimento, Leito, Telefone, SetorId) VALUES
('Maria Oliveira', '11122233344', '1985-05-10', 'A101', '14988880000', 1),
('Carlos Santos', '55566677788', '1978-08-20', 'B202', '14988881111', 2);

INSERT INTO PRESCRICAO (DataPrescricao, Observacao, MedicoId, PacienteId) VALUES
('2026-05-26', 'Paciente em observacao', 1, 1);

INSERT INTO PRESCRICAO_MEDICAMENTO (Quantidade, Frequencia, Horario, PrescricaoId, MedicamentoId) VALUES
(1, '8 em 8 horas', '08:00:00', 1, 1),
(1, '12 em 12 horas', '10:00:00', 1, 2);
