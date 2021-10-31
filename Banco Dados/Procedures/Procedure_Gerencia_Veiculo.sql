/****** Object:  StoredProcedure [dbo].[Gerencia_Veiculo]    Script Date: 24/10/2021 13:58:26 ******/
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
-- 0 SALVAR/EDITAR 
	IF(@Flag = 0)
	BEGIN
		IF(@idCarro IS NOT NULL)
		BEGIN
			UPDATE tb_carro SET status=0 WHERE id_carro=@idCarro
		END			
		SET @idTipo = (SELECT id_automovel FROM tb_automovel WHERE automovel=@Tipo)
		SET @idMarca = (SELECT M.id_marca FROM tb_marca AS M INNER JOIN tb_automovel AS A ON M.automovel_id = A.id_automovel AND A.id_automovel=@idTipo AND M.marca=@Marca)
		INSERT INTO tb_carro (placa,marca_id,tipo_id,status) VALUES(@Placa,@idMarca,@idTipo,@Status)
	END	
-- 1 == EXCLUIR VEICULO
	IF(@Flag = 1)
	BEGIN
		UPDATE tb_carro SET status=0 WHERE id_carro=@idCarro
	END
GO


/****** Object:  StoredProcedure [dbo].[Gerencia_Usuario]    Script Date: 24/10/2021 13:58:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE [dbo].[Gerencia_Usuario]
@Flag INT,
@Id INT = null,
@Login VARCHAR(32) = NULL,
@Senha VARCHAR(32) = NULL,
@Nivel INT = null,
@Status INT = null
AS
--Flag 1 = Criar Usuario
   IF(@Flag = 1)
   BEGIN
      INSERT INTO tb_usuario(login,senha,nivel,status) VALUES (@Login,@Senha,@Nivel,@Status);
   END
-- Flag 2 = Alterar Usuario
    IF(@Flag = 2)
	BEGIN
	   UPDATE tb_usuario SET login = @Login, senha = @Senha, nivel = @Nivel, status = @Status WHERE id_usuario = @Id; 
	END
-- Flag 3 = Deletar usuario
	IF(@Flag = 3)
	BEGIN
	   UPDATE tb_usuario SET status = 0 WHERE id_usuario = @Id;
	END
GO


