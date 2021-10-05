USE master
CREATE LOGIN SEULOGIN WITH password='SUA SENHA';

CREATE USER SEULOGIN FROM LOGIN SEULOGIN

USE db_estacionamento

CREATE USER SEULOGIN FROM LOGIN SEULOGIN;

--Dono do Banco
EXEC sp_addrolemember 'db_owner', 'SEU LOGIN';

-- Permissão para ler os dados do banco
EXEC sp_addrolemember 'db_datareader', 'SEU LOGIN';

-- Permissão para escrever no banco
EXEC sp_addrolemember 'db_datawriter', 'SEU LOGIN';

-- Admin segurança
EXEC sp_addrolemember 'db_securityadmin', 'SEU LOGIN';

-- Permissão Public (Nenhuma)
EXEC sp_addrolemember 'db_public', 'SEU LOGIN';

USE db_estacionamento;   
GRANT EXECUTE ON OBJECT::dbo.Parametros 
    TO ParkManagerSystem;  
GO  

USE db_estacionamento;   
GRANT EXECUTE ON OBJECT::dbo.Funcoes_Pesquisa 
    TO ParkManagerSystem;  
GO  

USE db_estacionamento;   
GRANT EXECUTE ON OBJECT::dbo.Gerencia_Usuario
    TO ParkManagerSystem; 
GO  

USE db_estacionamento;   
GRANT EXECUTE ON OBJECT::dbo.Gerencia_Veiculo 
    TO ParkManagerSystem;  
GO  

USE db_estacionamento;   
GRANT EXECUTE ON OBJECT::dbo.InsertTicket 
    TO ParkManagerSystem;  
GO  