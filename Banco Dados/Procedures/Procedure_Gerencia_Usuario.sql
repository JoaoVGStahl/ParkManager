/****** Object:  StoredProcedure [dbo].[Gerencia_Usuario]    Script Date: 24/10/2021 14:09:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[Gerencia_Usuario]
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
-- Flag 4 = SELECT Usuario login
	IF(@Flag = 4)
	BEGIN
		SELECT id_usuario[ID], login[Login], nivel[Nivel], status[Status] FROM tb_usuario WHERE login=@Login AND senha=@Senha
	END
