CREATE OR ALTER PROCEDURE Salvar_Valor(
@Valor_Hora decimal(10,2) = null,
@Valor_Minimo decimal(10,2) = null,
@Valor_Unico decimal(10,2) = null
)
AS
	IF(@Valor_Hora != null)
	BEGIN
		UPDATE tb_estacionamento SET valor_hr = @Valor_Hora WHERE id=1 AND status=1
	END
	IF(@Valor_Minimo != null)
	BEGIN
		UPDATE tb_estacionamento SET valor_min = @Valor_Minimo WHERE id=1 AND status=1
	END
	IF(@Valor_Unico != null)
	BEGIN
		UPDATE tb_estacionamento SET valor_unico = @Valor_Unico WHERE id=1 AND status=1
	END