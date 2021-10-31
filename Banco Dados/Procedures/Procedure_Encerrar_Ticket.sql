CREATE OR ALTER PROCEDURE Encerrar_Ticket(
@IdEntrada int = null,
@IdUsuario int = null,
@idTicket int = null,
@IdFormaPgt int = null,
@DataSaida datetime = null,
@Permanencia time(7) = null,
@HoraSaida time(7) = null,
@Total decimal(10,2) = null,
@Troco decimal(10,2) = null,
@Status smallint = null,
@Forma_Pgt varchar(32) = null
)
AS
BEGIN
	BEGIN TRAN
		SAVE TRANSACTION IfError
		BEGIN TRY
			SET @IdEntrada = (SELECT Entrada.id_entrada FROM tb_entrada AS Entrada INNER JOIN tb_ticket Ticket ON Ticket.id_ticket = Entrada.ticket_id WHERE Ticket.id_ticket = @idTicket)
			SET @IdFormaPgt = (SELECT id_pgt FROM tb_forma_pgt WHERE descricao = @Forma_Pgt)
			INSERT INTO tb_saida VALUES(@IdEntrada, @IdUsuario, @HoraSaida, @DataSaida, @IdFormaPgt, @Permanencia, @Total, @Troco, 1)
			UPDATE tb_entrada SET status = 0 WHERE id_entrada = @IdEntrada
			UPDATE tb_ticket SET status = 0 WHERE id_ticket = @idTicket
			COMMIT TRANSACTION
			return 1
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION IfError
			return 0
		END CATCH
END

