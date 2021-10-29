/****** Object:  StoredProcedure [dbo].[Parametros]    Script Date: 24/10/2021 13:59:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[Parametros](
@Flag int,
@Id_Estacionamento INT =null,
@Caminho varchar(100) = null,
@Porta_Arduino varchar(4) = null,
@String_Conn varchar(250) = null,
@Cnpj varchar(18) = null,
@Razao_Social varchar(32) = null,
@Endereco varchar(50) = null,
@Bairro varchar(32) = null,
@Numero int = null,
@Cidade varchar(32) = null,
@Estado char(2) = null,
@Cep char(9) = null,
@Inscricao_Estadual varchar(15) = null,
@Telefone varchar(14) = null
)
AS
-- 1 = Update parametros do Desenvolvedor
	IF(@Flag = 1)
	BEGIN
		IF(@Id_Estacionamento IS NULL)
		BEGIN
			INSERT INTO tb_estacionamento(caminho_log,porta_arduino, string_conn,status) VALUES(@Caminho, @Porta_Arduino, @String_Conn, 1)
		END
		ELSE
		BEGIN
			UPDATE tb_estacionamento SET caminho_log=@Caminho, porta_arduino=@Porta_Arduino, string_conn=@String_Conn WHERE id = @Id_Estacionamento
		END
	END
-- 2 = INSERT/UPDATE Identificacao do Estacionamento
	IF(@Flag = 2)
	BEGIN
		IF(@Id_Estacionamento IS NULL)
		BEGIN
			INSERT INTO tb_estacionamento(cnpj,razao_social,endereco,bairro,numero,cidade,estado,cep,inscricao_estadual,telefone, status) VALUES(@Cnpj,@Razao_Social,@Endereco,@Bairro,@Numero,@Cidade,@Estado,@Cep,@Inscricao_Estadual,@Telefone,1)
		END
		ELSE
		BEGIN
			UPDATE tb_estacionamento SET cnpj=@Cnpj, razao_social=@Razao_Social,endereco=@Endereco,bairro=@Bairro,numero=@Numero,cidade=@Cidade,estado=@Estado,cep=@Cep,inscricao_estadual=@Inscricao_Estadual, telefone=@Telefone WHERE id = @Id_Estacionamento
		END
	END
GO


