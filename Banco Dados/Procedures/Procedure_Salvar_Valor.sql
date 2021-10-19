/****** Object:  StoredProcedure [dbo].[Salvar_Valor]    Script Date: 19/10/2021 13:59:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[Salvar_Valor](
@Id_Estacionamento INT,
@Valor_Hora decimal(10,2) = null,
@Valor_Minimo decimal(10,2) = null,
@Valor_Unico decimal(10,2) = null,
@Tolerancia time = null
)
AS
--1 = Update dos valores
	IF(@Valor_Hora != -1)
	BEGIN
		UPDATE tb_estacionamento SET valor_hr = @Valor_Hora WHERE id=@Id_Estacionamento AND status=1
	END
	IF(@Valor_Minimo != -1)
	BEGIN
		UPDATE tb_estacionamento SET valor_min = @Valor_Minimo WHERE id=@Id_Estacionamento AND status=1
	END
	IF(@Valor_Unico != -1)
	BEGIN
		UPDATE tb_estacionamento SET valor_unico = @Valor_Unico WHERE id=@Id_Estacionamento AND status=1
	END
	UPDATE tb_estacionamento SET tolerancia=@Tolerancia WHERE id=@Id_Estacionamento AND status=1