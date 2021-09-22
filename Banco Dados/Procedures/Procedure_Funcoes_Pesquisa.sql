ALTER PROCEDURE Funcoes_Pesquisa(
@Flag int,
@Tipo varchar(20) = null,
@Placa varchar(7) = null,
@idTicket int = null,
@IdUsuario int =null,
@Login varchar(32) = null
)
AS
-- 0 = ComboBox Tipo
	IF(@Flag = 0)
	BEGIN
		SELECT id_automovel, automovel from tb_automovel
	END
-- 1 = ComboBox Marca
	IF(@Flag = 1)
	BEGIN
		SELECT A.automovel[Tipo], M.marca [Marca] FROM tb_automovel as A INNER JOIN tb_marca as M ON A.id_automovel = M.automovel_id AND A.automovel =@Tipo
	END
-- 2 = Quantidade Ticket's Abertos
	IF(@Flag = 2)
	BEGIN
		SELECT COUNT(id_ticket)AS [Ticket's Abertos] FROM tb_ticket WHERE status=1
	END
-- 3 = Carregar Tela Encerrar Ticket
	IF(@Flag = 3)
	BEGIN
		SELECT Ticket.id_ticket[#Ticket], CONVERT(varchar, Entrada.hr_entrada,8) AS [Hora Entrada],CONVERT(varchar,Entrada.data_entrada,103) AS[Data Entrada] FROM tb_ticket AS Ticket  INNER JOIN tb_entrada AS Entrada ON Entrada.ticket_id = Ticket.id_ticket AND Ticket.id_ticket=@idTicket
	END
-- 4 = Métodos de Pagamento
	IF(@Flag = 4)
	BEGIN
		SELECT id_pgt,descricao FROM tb_forma_pgt WHERE status=1
	END
-- 5 = Verificar se há algum ticket em Aberto para Determinado Veiculo
	IF(@Flag = 5)
	BEGIN
		SELECT COUNT(id_ticket) FROM tb_ticket AS Ticket INNER JOIN tb_carro AS Car ON Ticket.carro_id = Car.id_carro WHERE Ticket.status=1 AND Car.placa=@Placa
	END
-- 6 =  Ticket's Aberto Load Tela Pesquisa Ticket
	IF(@Flag = 6)
	BEGIN
		SELECT Ticket.id_ticket[#Ticket],CONVERT(varchar, Entrada.hr_entrada,8) AS [Hora Entrada],CONVERT(varchar,Entrada.data_entrada,103) AS[Data Entrada],Usuario.login[Usuario Entrada],Car.placa[Placa],A.automovel[Tipo],M.marca[Marca],Cli.nome[Nome Cliente],Cli.telefone[Telefone] FROM tb_ticket AS Ticket INNER JOIN tb_entrada AS Entrada ON Entrada.ticket_id = Ticket.id_ticket INNER JOIN tb_usuario AS Usuario ON Entrada.usuario_id = Usuario.id_usuario INNER JOIN tb_carro AS Car ON Car.id_carro = Ticket.carro_id INNER JOIN tb_automovel AS A ON Car.tipo_id = A.id_automovel INNER JOIN tb_marca AS M ON Car.marca_id = M.id_marca INNER JOIN tb_cliente AS Cli ON Cli.id_cliente = Ticket.cliente_id WHERE Ticket.status = 1
	END
-- 7 = Pesquisa Ticket Especifico P/ Tela Operação
	IF(@Flag = 7)
	BEGIN
		SELECT Ticket.id_ticket[#Ticket], A.automovel[Tipo],M.marca[Marca],Car.placa[Placa],Cli.nome[Nome Cliente], Cli.telefone[Telefone],CONVERT(varchar, Entrada.hr_entrada,8) AS [Hora Entrada],CONVERT(varchar,Entrada.data_entrada,103) AS [Data Entrada] FROM tb_ticket AS Ticket INNER JOIN tb_carro as Car ON Ticket.carro_id = Car.id_carro INNER JOIN tb_cliente AS Cli ON Ticket.cliente_id = Cli.id_cliente INNER JOIN tb_automovel AS A ON Car.tipo_id = A.id_automovel INNER JOIN tb_marca AS M ON Car.marca_id = M.id_marca INNER JOIN tb_entrada AS Entrada ON Entrada.ticket_id = Ticket.id_ticket WHERE Ticket.Status=1 AND Car.placa=@Placa  
	END
-- 8 = SELECT Parametros do Sistema
	IF(@Flag = 8)
	BEGIN	
		SELECT * FROM tb_estacionamento WHERE status=1
	END
-- 9 = SELECT de usuario
	IF(@Flag = 9)
    BEGIN
	   	SELECT id_usuario[ID], login[Login],nivel[Nivel],status[Status] FROM tb_usuario WHERE status=1
    END
-- 10 = SELECT Usuario Especifico
	IF(@Flag = 10)
	BEGIN
		SELECT id_usuario[ID], login[Login],nivel[Nivel],status[Status] FROM tb_usuario WHERE id_usuario = @IdUsuario
	END
-- 11 = SELECT Usuario pelo Login
	IF(@Flag = 11)
	BEGIN
		SELECT login[Login] FROM tb_usuario WHERE login=@Login
	END

