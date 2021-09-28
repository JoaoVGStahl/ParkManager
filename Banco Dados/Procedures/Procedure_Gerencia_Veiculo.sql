USE [db_estacionamento]
GO

/****** Object:  StoredProcedure [dbo].[Gerencia_Veiculo]    Script Date: 28/09/2021 15:14:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE OR ALTER PROCEDURE [dbo].[Gerencia_Veiculo]
(
@Flag int,
@idCarro int = null,
@Placa varchar(7) = null,
@Tipo varchar(20) = null,
@Marca varchar(25) = null,
@Status int = null
)
AS 
DECLARE
@idTipo int,
@idMarca int

-- 0 = Insert Tb_Carro
	IF(@Flag = 0)
	BEGIN
		SET @idTipo = (SELECT id_automovel FROM tb_automovel WHERE automovel=@Tipo)
		SET @idMarca = (SELECT M.id_marca FROM tb_marca AS M INNER JOIN tb_automovel AS A ON M.automovel_id = A.id_automovel AND A.id_automovel=1 AND M.marca='Porshe')
		INSERT INTO tb_carro (placa,marca_id,tipo_id,status) VALUES(@Placa,@idMarca,@idTipo,1)
	END
-- 1 = UPDATE tb_Carro e cria novo registro.
	IF(@Flag = 1)
	BEGIN
		UPDATE tb_carro SET  status=0 WHERE id_carro=@idCarro
		SET @idTipo = (SELECT id_automovel FROM tb_automovel WHERE automovel=@Tipo)
		SET @idMarca = (SELECT M.id_marca FROM tb_marca AS M INNER JOIN tb_automovel AS A ON M.automovel_id = A.id_automovel AND A.id_automovel=1 AND M.marca='Porshe')
		INSERT INTO tb_carro (placa,marca_id,tipo_id,status) VALUES(@Placa,@idMarca,@idTipo,1)
	END

GO


