CREATE OR ALTER PROCEDURE Parametros(
@Flag int,
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
		UPDATE tb_estacionamento SET caminho_log=@Caminho, porta_arduino=@Porta_Arduino, string_conn=@String_Conn WHERE id=1
	END
-- 2 = Update Identificacao do Estacionamento
	IF(@Flag = 2)
	BEGIN
		UPDATE tb_estacionamento SET cnpj=@Cnpj, razao_social=@Razao_Social,endereco=@Endereco,bairro=@Bairro,numero=@Numero,cidade=@Cidade,estado=@Estado,cep=@Cep,inscricao_estadual=@Inscricao_Estadual, telefone=@Telefone WHERE cnpj=@Cnpj
	END

	SELECT * FROM tb_estacionamento