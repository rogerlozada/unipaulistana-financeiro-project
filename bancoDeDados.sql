
-- SCRIPT DE CRIAÇÃO DO BANCO DE DADOS

IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'cliente'))
BEGIN
    CREATE TABLE cliente (
    ClienteID int IDENTITY(1,1) PRIMARY KEY,
    Nome varchar(255) NOT NULL
);
END
GO


IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'departamento'))
BEGIN
    CREATE TABLE departamento (
    departamentoID int IDENTITY(1,1) PRIMARY KEY,
    Nome varchar(255) NOT NULL
);
END
GO

IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'grupoDeSeguranca'))
BEGIN
    CREATE TABLE grupoDeSeguranca (
    grupoDeSegurancaID int IDENTITY(1,1) PRIMARY KEY,
    Nome varchar(255) NOT NULL
);
END
GO

IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'diretivaSeguranca'))
BEGIN
    CREATE TABLE diretivaSeguranca (
    diretivaSegurancaID int IDENTITY(1,1) PRIMARY KEY,
    Nome varchar(255) NOT NULL
);
END
GO


IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'grupoDiretivaSeguranca'))
BEGIN
    CREATE TABLE grupoDiretivaSeguranca (
    grupoDiretivaSegurancaID int IDENTITY(1,1) PRIMARY KEY,
    DiretivaSegurancaID INT NOT NULL,
	GrupoDeSegurancaID INT NOT NULL,
    FOREIGN KEY(DiretivaSegurancaID) REFERENCES diretivaSeguranca(diretivaSegurancaID),
	FOREIGN KEY(GrupoDeSegurancaID) REFERENCES grupoDeSeguranca(GrupoDeSegurancaID)
);
END
GO


IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'usuario'))
BEGIN
    CREATE TABLE usuario (
    usuarioID int IDENTITY(1,1) PRIMARY KEY,
    Nome varchar(255) NOT NULL,
    Email varchar(255) NOT NULL,
    Senha varchar(10) NOT NULL,
    Foto varchar(50) NULL,
    DepartamentoID INT NOT NULL,
	GrupoDeSegurancaID INT NOT NULL,
    FOREIGN KEY(DepartamentoID) REFERENCES departamento(departamentoID),
	FOREIGN KEY(GrupoDeSegurancaID) REFERENCES grupoDeSeguranca(GrupoDeSegurancaID)
);
END
GO


IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'solicitacao'))
BEGIN
    CREATE TABLE solicitacao (
    SolicitacaoID int IDENTITY(1,1) PRIMARY KEY,
    DataDeCriacao DATETIME NOT NULL,
    DataDeConclusao DATETIME NULL,
    Descricao TEXT,
	ClienteID INT NOT NULL,
    DepartamentoID INT NOT NULL,
    SolicitanteID INT NOT NULL,
    UsuarioID INT NOT NULL,
    Status INT NOT NULL,
    Concluido BIT NOT NULL DEFAULT 0,
    FOREIGN KEY(ClienteID) REFERENCES cliente(ClienteID),
	FOREIGN KEY(departamentoID) REFERENCES departamento(departamentoID),
    FOREIGN KEY(usuarioID) REFERENCES usuario(usuarioID),
    FOREIGN KEY(SolicitanteID) REFERENCES usuario(usuarioID)
);
END
GO


IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'solicitacaoItem'))
BEGIN
    CREATE TABLE solicitacaoItem (
    solicitacaoItemID int IDENTITY(1,1) PRIMARY KEY,
    SolicitacaoID INT NOT NULL,
    Data DATETIME NOT NULL,
    Descricao TEXT,
    usuarioID INT NOT NULL,
    FOREIGN KEY(SolicitacaoID) REFERENCES solicitacao(SolicitacaoID),
	FOREIGN KEY(usuarioID) REFERENCES usuario(usuarioID)
);
END
GO


IF (NOT EXISTS (SELECT * FROM grupoDeSeguranca WHERE nome = 'admin'))
BEGIN
    INSERT INTO grupoDeSeguranca (nome) values ('admin')
END
GO

IF (NOT EXISTS (SELECT * FROM departamento WHERE nome = 'admin'))
BEGIN
    INSERT INTO departamento (nome) values ('admin')
END
GO

IF (NOT EXISTS (SELECT * FROM usuario WHERE Email = 'admin@uni.com.br'))
BEGIN
    INSERT INTO usuario (nome, email, senha, foto, DepartamentoID, GrupoDeSegurancaID) 
			values 
	('admin', 'admin@uni.com.br', '123456', 'user.png', (SELECT TOP 1 DepartamentoID FROM departamento WHERE nome = 'admin'),
			(SELECT TOP 1 grupoDeSegurancaID FROM grupoDeSeguranca WHERE nome = 'admin'))
END
GO
