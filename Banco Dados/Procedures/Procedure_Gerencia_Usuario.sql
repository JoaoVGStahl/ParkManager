CREATE PROCEDURE Gerencia_Usuario
@Flag INT,
@Id INT = null,
@Login VARCHAR(32) = NULL,
@Senha VARCHAR(32) = NULL,
@Nivel SMALLINT = null,
@Status SMALLINT = null
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
GO;