
-- SCRIPT DE INSERÇÃO DAS DIRETIVAS DE SEGURANÇA


-- direitvas de usuário

IF (NOT EXISTS (SELECT * FROM diretivaSeguranca WHERE nome = 'PermiteListarUsuario'))
BEGIN
    INSERT INTO diretivaSeguranca (nome) values ('PermiteListarUsuario')
END
GO

IF (NOT EXISTS (SELECT * FROM diretivaSeguranca WHERE nome = 'PermiteCriarUsuario'))
BEGIN
    INSERT INTO diretivaSeguranca (nome) values ('PermiteCriarUsuario')
END
GO

IF (NOT EXISTS (SELECT * FROM diretivaSeguranca WHERE nome = 'PermiteAlterarUsuario'))
BEGIN
    INSERT INTO diretivaSeguranca (nome) values ('PermiteAlterarUsuario')
END
GO

IF (NOT EXISTS (SELECT * FROM diretivaSeguranca WHERE nome = 'PermiteExcluirUsuario'))
BEGIN
    INSERT INTO diretivaSeguranca (nome) values ('PermiteExcluirUsuario')
END
GO

IF (NOT EXISTS (SELECT * FROM diretivaSeguranca WHERE nome = 'PermiteAlterarSenhaViaAdminUsuario'))
BEGIN
    INSERT INTO diretivaSeguranca (nome) values ('PermiteAlterarSenhaViaAdminUsuario')
END
GO


-- direitvas de cliente

IF (NOT EXISTS (SELECT * FROM diretivaSeguranca WHERE nome = 'PermiteListarCliente'))
BEGIN
    INSERT INTO diretivaSeguranca (nome) values ('PermiteListarCliente')
END
GO

IF (NOT EXISTS (SELECT * FROM diretivaSeguranca WHERE nome = 'PermiteCriarCliente'))
BEGIN
    INSERT INTO diretivaSeguranca (nome) values ('PermiteCriarCliente')
END
GO

IF (NOT EXISTS (SELECT * FROM diretivaSeguranca WHERE nome = 'PermiteAlterarCliente'))
BEGIN
    INSERT INTO diretivaSeguranca (nome) values ('PermiteAlterarCliente')
END
GO

IF (NOT EXISTS (SELECT * FROM diretivaSeguranca WHERE nome = 'PermiteExcluirCliente'))
BEGIN
    INSERT INTO diretivaSeguranca (nome) values ('PermiteExcluirCliente')
END
GO


-- direitvas de grupo de segurança

IF (NOT EXISTS (SELECT * FROM diretivaSeguranca WHERE nome = 'PermiteListarGrupoDeSeguranca'))
BEGIN
    INSERT INTO diretivaSeguranca (nome) values ('PermiteListarGrupoDeSeguranca')
END
GO

IF (NOT EXISTS (SELECT * FROM diretivaSeguranca WHERE nome = 'PermiteCriarGrupoDeSeguranca'))
BEGIN
    INSERT INTO diretivaSeguranca (nome) values ('PermiteCriarGrupoDeSeguranca')
END
GO

IF (NOT EXISTS (SELECT * FROM diretivaSeguranca WHERE nome = 'PermiteAlterarGrupoDeSeguranca'))
BEGIN
    INSERT INTO diretivaSeguranca (nome) values ('PermiteAlterarGrupoDeSeguranca')
END
GO

IF (NOT EXISTS (SELECT * FROM diretivaSeguranca WHERE nome = 'PermiteExcluirGrupoDeSeguranca'))
BEGIN
    INSERT INTO diretivaSeguranca (nome) values ('PermiteExcluirGrupoDeSeguranca')
END
GO

IF (NOT EXISTS (SELECT * FROM diretivaSeguranca WHERE nome = 'PermiteAssociarDiretivaGrupoDeSeguranca'))
BEGIN
    INSERT INTO diretivaSeguranca (nome) values ('PermiteAssociarDiretivaGrupoDeSeguranca')
END
GO


-- diretivas de financeiro

IF (NOT EXISTS (SELECT * FROM diretivaSeguranca WHERE nome = 'PermiteListarFinanceiro'))
BEGIN
    INSERT INTO diretivaSeguranca (nome) values ('PermiteListarFinanceiro')
END
GO

IF (NOT EXISTS (SELECT * FROM diretivaSeguranca WHERE nome = 'PermiteCriarFinanceiro'))
BEGIN
    INSERT INTO diretivaSeguranca (nome) values ('PermiteCriarFinanceiro')
END
GO

IF (NOT EXISTS (SELECT * FROM diretivaSeguranca WHERE nome = 'PermiteAlterarFinanceiro'))
BEGIN
    INSERT INTO diretivaSeguranca (nome) values ('PermiteAlterarFinanceiro')
END
GO

IF (NOT EXISTS (SELECT * FROM diretivaSeguranca WHERE nome = 'PermiteExcluirFinanceiro'))
BEGIN
    INSERT INTO diretivaSeguranca (nome) values ('PermiteExcluirFinanceiro')
END
GO
