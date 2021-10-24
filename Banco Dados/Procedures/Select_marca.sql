/****** Object:  StoredProcedure [dbo].[Adicionar_Marcas]    Script Date: 24/10/2021 13:56:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[Adicionar_Marcas]
@Flag INT,
@idMarca int,
@Marca VARCHAR(25) = NULL,
@idTipo INT = NULL,
@Tipo VARCHAR(20) = NULL,
@Status SMALLINT = null
AS
--Flag 1 = Adicionar Marca
   IF(@Flag = 1)
   BEGIN
      SET @idTipo = (SELECT id_automovel FROM tb_automovel WHERE automovel=@Tipo);
      INSERT INTO tb_marca(marca,automovel_id,status) VALUES (@Marca,@idTipo,1);
   END
-- Flag 2 = Alterar Marca
    IF(@Flag = 2)
	BEGIN
	   SET @idMarca = (SELECT id_marca FROM tb_marca INNER JOIN tb_automovel ON tb_marca.automovel_id = tb_automovel.id_automovel AND tb_automovel.id_automovel=@idTipo AND tb_marca.marca=@Marca)
	   UPDATE tb_marca SET marca = @Marca, automovel_id = @idTipo, status = @Status WHERE id_marca = @idMarca; 
	END
-- Flag 3 = Deletar marca
	IF(@Flag = 3)
	BEGIN
	   SET @idMarca = (SELECT id_marca FROM tb_marca INNER JOIN tb_automovel ON tb_marca.automovel_id = tb_automovel.id_automovel AND tb_automovel.id_automovel=@idTipo AND tb_marca.marca=@Marca)
	   UPDATE tb_marca SET status = 0 WHERE id_marca = @idMarca;
	END
GO


