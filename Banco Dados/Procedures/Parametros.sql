ALTER PROCEDURE Parametros(
@Flag int,
@Caminho varchar(100) = null,
@Porta_Arduino varchar(4) = null,
@String_Conn varchar(250) = null
)
AS
-- 1 = Update parametros do Desenvolvedor
	IF(@Flag = 1)
	BEGIN
		UPDATE tb_estacionamento SET caminho_log=@Caminho, porta_arduino=@Porta_Arduino, string_conn=@String_Conn WHERE id=1
	END