
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

IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'financeiro'))
BEGIN
    CREATE TABLE financeiro (
    financeiroID int IDENTITY(1,1) PRIMARY KEY,
    DataDeVencimento DateTime NOT NULL,
    Valor Float NOT NULL,
    ClienteID INT NOT NULL,
    Status INT NOT NULL,
    FOREIGN KEY(ClienteID) REFERENCES cliente(ClienteID)
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
