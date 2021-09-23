USE [db_estacionamento]
GO
/****** Object:  StoredProcedure [dbo].[InsertTicket]    Script Date: 10/09/2021 09:31:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertTicket]
(
@idUsuario int,
@Nome_Cliente varchar(50),
@Telefone varchar(14),
@Placa varchar(7),
@Marca varchar(25),
@Tipo varchar(20),
@Hr_Entrada time(0),
@Data_Entrada date,
@Caminho_Foto varchar(100)
) 
AS 
DECLARE @idCarro int,
@idTicket int,
@idCliente int,
@idMarca int,
@idTipo int

	IF(@Nome_Cliente = 'Convidado')
	BEGIN
		SET @idCliente = 1
	END	
	ELSE
	BEGIN
		SET @idCliente = (SELECT id_cliente FROM tb_cliente WHERE nome=@Nome_Cliente AND telefone=@Telefone)
		IF(@idCliente IS NULL)
		BEGIN
			INSERT INTO tb_cliente (nome,telefone,status) VALUES (@Nome_Cliente,@Telefone,1)
			SET @idCliente = @@IDENTITY
		END
	END	
	SET @idCarro = (SELECT id_carro FROM tb_carro WHERE placa=@Placa)
		IF(@idCarro IS NULL)
		BEGIN
			SET @idTipo = (SELECT id_automovel FROM tb_automovel WHERE automovel=@Tipo)
			SET @idMarca = (SELECT id_marca FROM tb_marca AS M INNER JOIN tb_automovel AS A ON M.automovel_id = A.id_automovel AND A.id_automovel=@idTipo AND marca=@Marca)
			INSERT INTO tb_carro (placa,marca_id,tipo_id,status) VALUES(@Placa,@idMarca,@idTipo,1)
			SET @idCarro = @@IDENTITY
		END
	INSERT INTO tb_ticket (cliente_id,carro_id,status) VALUES(@idCliente,@idCarro,1)
	SET @idTicket = @@IDENTITY
	INSERT INTO tb_entrada(ticket_id,usuario_id,hr_entrada,data_entrada,status) VALUES(@idTicket,@idUsuario,@Hr_Entrada,@Data_Entrada,1)
	INSERT INTO tb_fotos(ticket_id,foto_caminho) VALUES (@idTicket,@Caminho_Foto)
RETURN @idTicket