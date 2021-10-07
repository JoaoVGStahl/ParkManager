CREATE OR ALTER PROCEDURE Gerencia_Cliente(
@Flag int,
@idCliente int = null,
@Nome varchar(45) = null,
@Telefone varchar(14) = null,
@Status int = null
)
AS
-- 0 == SALVAR/EDITAR
	IF(@Flag =0)
	BEGIN
		IF(@idCliente IS NOT NULL)
		BEGIN
			UPDATE tb_cliente SET status =0 WHERE id_cliente = @idCliente
		END
		INSERT INTO tb_cliente(nome, telefone, status) VALUES (@Nome,@Telefone,@Status)
	END
-- 1 = Excluir cliente
	IF(@Flag = 1)
	BEGIN
		UPDATE tb_cliente SET status=0 WHERE id_cliente=@idCliente
	END