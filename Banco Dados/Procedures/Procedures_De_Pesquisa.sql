--ComboBox Tipo
CREATE OR ALTER PROCEDURE ComboBox_Tipo
AS
BEGIN
	SELECT id_automovel, automovel from tb_automovel
END

--ComboBox Marca
CREATE OR ALTER PROCEDURE ComboBox_Marca
(
  @Tipo varchar(20) = null
)
AS
BEGIN
	SELECT A.automovel[Tipo], M.marca [Marca] FROM tb_automovel as A INNER JOIN tb_marca as M ON A.id_automovel = M.automovel_id AND A.automovel =@Tipo
END

--Quantidade Ticket's Abertos
CREATE OR ALTER PROCEDURE Tickets_Abertos
AS
BEGIN
	SELECT COUNT(id_ticket)AS [Ticket's Abertos] FROM tb_ticket WHERE status=1
END

--Carregar Tela Encerrar Ticket
CREATE OR ALTER PROCEDURE Carregar_Tela_Encerrar
(
@idTicket int = null
)
AS
BEGIN
	SELECT Ticket.id_ticket[#Ticket], CONVERT(varchar, Entrada.hr_entrada,8) AS [Hora Entrada],CONVERT(varchar,Entrada.data_entrada,103) AS[Data Entrada] FROM tb_ticket AS Ticket  INNER JOIN tb_entrada AS Entrada ON Entrada.ticket_id = Ticket.id_ticket AND Ticket.id_ticket=@idTicket
END

--Métodos de Pagamento
CREATE OR ALTER PROCEDURE Metodos_Pagamento
AS
BEGIN
	SELECT id_pgt,descricao FROM tb_forma_pgt WHERE status=1
END

--Verificar se há algum ticket em Aberto para Determinado Veiculo
CREATE OR ALTER PROCEDURE Pesquisa_TicketAberto_Placa
(
@Placa varchar(7) = null
)
AS
BEGIN
	SELECT COUNT(id_ticket)[QTD] FROM tb_ticket AS Ticket INNER JOIN tb_carro AS Car ON Ticket.carro_id = Car.id_carro WHERE Ticket.status=1 AND Car.placa=@Placa AND Car.status=1
END

--Ticket's Aberto Load Tela Pesquisa Ticket
CREATE OR ALTER PROCEDURE Pesquisa_TicketsAbertos_TelaPesquisa
AS
BEGIN
	SELECT Ticket.id_ticket[#Ticket],CONVERT(varchar, Entrada.hr_entrada,8) AS [Hora Entrada],CONVERT(varchar,Entrada.data_entrada,103) AS[Data Entrada],Usuario.login[Usuario Entrada],Car.placa[Placa],A.automovel[Tipo],M.marca[Marca],Cli.nome[Nome Cliente],Cli.telefone[Telefone] FROM tb_ticket AS Ticket INNER JOIN tb_entrada AS Entrada ON Entrada.ticket_id = Ticket.id_ticket INNER JOIN tb_usuario AS Usuario ON Entrada.usuario_id = Usuario.id_usuario INNER JOIN tb_carro AS Car ON Car.id_carro = Ticket.carro_id INNER JOIN tb_automovel AS A ON Car.tipo_id = A.id_automovel INNER JOIN tb_marca AS M ON Car.marca_id = M.id_marca INNER JOIN tb_cliente AS Cli ON Cli.id_cliente = Ticket.cliente_id WHERE Ticket.status = 1
END

--Pesquisa Ticket Especifico P/ Tela Operação
CREATE OR ALTER PROCEDURE Pesquisa_Ticket_TelaOperacao
(
@Placa varchar(7) = null
)
AS
BEGIN
	SELECT Ticket.id_ticket[#Ticket], A.automovel[Tipo],M.marca[Marca],Car.placa[Placa],Cli.nome[Nome Cliente], Cli.telefone[Telefone],CONVERT(varchar, Entrada.hr_entrada,8) AS [Hora Entrada],CONVERT(varchar,Entrada.data_entrada,103) AS [Data Entrada], foto_caminho[Caminho Foto]  FROM tb_ticket AS Ticket INNER JOIN tb_carro as Car ON Ticket.carro_id = Car.id_carro INNER JOIN tb_cliente AS Cli ON Ticket.cliente_id = Cli.id_cliente INNER JOIN tb_automovel AS A ON Car.tipo_id = A.id_automovel INNER JOIN tb_marca AS M ON Car.marca_id = M.id_marca INNER JOIN tb_entrada AS Entrada ON Entrada.ticket_id = Ticket.id_ticket INNER JOIN tb_fotos AS Fotos ON Fotos.ticket_id = Ticket.id_ticket WHERE Ticket.Status=1 AND Car.placa=@Placa  
END

--SELECT Identificação do estacionamento
CREATE OR ALTER PROCEDURE Info_Estacionamento
AS
BEGIN	
	SELECT id[ID],cnpj[CNPJ], razao_social[Razão Social], endereco[Endereço],bairro[Bairro], numero[Número], cidade[Cidade],estado[Estado] , cep[CEP], inscricao_estadual[Inscrição Estadual], telefone[Telefone] FROM tb_estacionamento WHERE status=1
END

--SELECT de usuario
CREATE OR ALTER PROCEDURE Pesquisa_Usuarios
AS
BEGIN
	SELECT id_usuario[ID], login[Login],nivel[Nivel],CASE WHEN status = 1 THEN 'Ativo' ELSE 'Inativo' END [Status] FROM tb_usuario WHERE status=1
END

--SELECT Usuario Especifico
CREATE OR ALTER PROCEDURE Pesquisa_Usuario_Id
(
@IdUsuario int =null
)
AS
BEGIN
	SELECT id_usuario[ID], login[Login],nivel[Nivel],status[Status] FROM tb_usuario WHERE id_usuario = @IdUsuario
END

--SELECT Usuario pelo Login
CREATE OR ALTER PROCEDURE Pesquisa_Usuario_Login
(
@Login varchar(32) = null
)
AS
BEGIN
	SELECT login[Login] FROM tb_usuario WHERE login=@Login
END

--SELECT Tela Desenvolvedor
CREATE OR ALTER PROCEDURE Pesquisa_TelaDesenvolvedor
AS
BEGIN
	SELECT id,caminho_log[Caminho Log], porta_arduino[Porta Arduino],string_conn[String Conexão] FROM tb_estacionamento WHERE status =1
END

--Pesquisa informações de um veiculo pela placa
CREATE OR ALTER PROCEDURE Pesquisa_Info_Placa
(
@Placa varchar(7) = null
)
AS
BEGIN
   SELECT Car.placa[Placa], A.automovel[Tipo], M.marca FROM tb_carro AS Car INNER JOIN tb_automovel AS A ON Car.tipo_id = A.id_automovel INNER JOIN tb_marca AS M ON Car.marca_id = M.id_marca WHERE Car.placa = @Placa AND Car.status=1
END

--SELECT Parametros do Sistema
CREATE OR ALTER PROCEDURE Parametros_Sistema
AS
BEGIN
	SELECT id[ID],valor_hr[Valor Hora],valor_min[Valor Minimo], valor_unico[Valor Unico], tolerancia[Tolerancia], caminho_log[Caminho Log] FROM tb_estacionamento WHERE status=1
END

--SELECT Tela de Cadastro Veiculos
CREATE OR ALTER PROCEDURE Select_TelaCadastro_Veiculos
AS
BEGIN
	SELECT Car.id_carro [ID],Car.placa[Placa], A.automovel[Tipo], M.marca FROM tb_carro AS Car INNER JOIN tb_automovel AS A ON Car.tipo_id = A.id_automovel INNER JOIN tb_marca AS M ON Car.marca_id = M.id_marca WHERE Car.status=1
END

--SELECT VEICULO ESPECIFICO
CREATE OR ALTER PROCEDURE Select_Veiculo_Especifico
@idCarro int = null
AS
BEGIN
	SELECT Car.id_carro [ID],Car.placa[Placa], A.automovel[Tipo], M.marca[Marca],Car.status[Status] FROM tb_carro AS Car INNER JOIN tb_automovel AS A ON Car.tipo_id = A.id_automovel INNER JOIN tb_marca AS M ON Car.marca_id = M.id_marca WHERE Car.id_carro = @idCarro
END

--SELECT TICKETS ABERTO PARA UM VEICULO
CREATE OR ALTER PROCEDURE Select_TicketAberto_Veiculo
(
@idCarro int = null
)
AS
BEGIN
	SELECT Ticket.id_ticket FROM tb_ticket AS Ticket INNER JOIN tb_carro AS Car ON Ticket.carro_id = Car.id_carro WHERE Ticket.status=1 AND Car.id_carro=@idCarro AND Car.status=1
END

--SELECT Tela pesquisa 
CREATE OR ALTER PROCEDURE Select_Tela_Pesquisa
(
@Placa varchar(7) = null,
@Status int = null,
@DataEntrada datetime = null,
@DataSaida datetime = null,
@idTicket int = null
)
AS
	BEGIN
		IF(@Status = 0) -- ENCERRADO
		BEGIN
			SELECT Ticket.id_ticket[#Ticket],CONVERT(varchar, Entrada.hr_entrada,8) AS [Hora Entrada],CONVERT(varchar,Entrada.data_entrada,103) AS[Data Entrada],UsuarioEntrada.login[UsuarioEntrada],Car.placa[Placa],A.automovel[Tipo],M.marca[Marca],Cli.nome[Nome Cliente],Cli.telefone[Telefone],CONVERT(varchar, Saida.hr_saida,8)[Hora Saida],CONVERT(varchar,Saida.data_saida,103)[Data Saida], Saida.permanencia[Permanência], Saida.total[Total], Saida.troco[Troco], Forma.descricao[Forma Pagamento], UsuarioSaida.login[Usuario Saida], CASE WHEN Ticket.status = 1 THEN 'Ativo' ELSE 'Encerrado' END[Status]  FROM tb_ticket AS Ticket INNER JOIN tb_entrada AS Entrada ON Entrada.ticket_id = Ticket.id_ticket INNER JOIN tb_saida AS Saida ON Saida.entrada_id = Entrada.id_entrada INNER JOIN tb_usuario AS UsuarioSaida ON Saida.usuario_id = UsuarioSaida.id_usuario INNER JOIN tb_usuario AS UsuarioEntrada ON UsuarioEntrada.id_usuario = Entrada.usuario_id  INNER JOIN tb_carro AS Car ON Car.id_carro = Ticket.carro_id INNER JOIN tb_automovel AS A ON Car.tipo_id = A.id_automovel INNER JOIN tb_marca AS M ON Car.marca_id = M.id_marca INNER JOIN tb_cliente AS Cli ON Cli.id_cliente = Ticket.cliente_id INNER JOIN tb_forma_pgt AS Forma ON Saida.forma_pgt_id = Forma.id_pgt WHERE (Car.placa LIKE '%' + @Placa + '%' OR @Placa IS NULL) AND (Entrada.data_entrada >= @DataEntrada OR @DataEntrada IS NULL) AND (Saida.data_saida <= @DataSaida OR @DataSaida IS NULL) AND (Ticket.status=@Status OR @Status IS NULL) AND (Ticket.id_ticket = @IdTicket OR @IdTicket IS NULL)
		END
		IF(@Status = 1) -- ATIVO
		BEGIN
			SELECT Ticket.id_ticket[#Ticket],CONVERT(varchar, Entrada.hr_entrada,8) AS [Hora Entrada],CONVERT(varchar,Entrada.data_entrada,103) AS[Data Entrada],UsuarioEntrada.login[UsuarioEntrada],Car.placa[Placa],A.automovel[Tipo],M.marca[Marca],Cli.nome[Nome Cliente],Cli.telefone[Telefone], CASE WHEN ticket.status=1 THEN  'Ativo' ELSE 'Encerrado'  END [Status]  FROM tb_ticket AS Ticket INNER JOIN tb_entrada AS Entrada ON Entrada.ticket_id = Ticket.id_ticket   INNER JOIN tb_usuario AS UsuarioEntrada ON UsuarioEntrada.id_usuario = Entrada.usuario_id  INNER JOIN tb_carro AS Car ON Car.id_carro = Ticket.carro_id INNER JOIN tb_automovel AS A ON Car.tipo_id = A.id_automovel INNER JOIN tb_marca AS M ON Car.marca_id = M.id_marca INNER JOIN tb_cliente AS Cli ON Cli.id_cliente = Ticket.cliente_id WHERE (Car.placa LIKE '%' + @Placa + '%' OR @Placa IS NULL) AND (Entrada.data_entrada >= @DataEntrada OR @DataEntrada IS NULL) AND (Ticket.status=@Status OR @Status IS NULL) AND (Ticket.id_ticket = @IdTicket OR @IdTicket IS NULL)
		END
		IF(@Status = 2) -- AMBOS
		BEGIN
			SELECT Ticket.id_ticket[#Ticket],CONVERT(varchar, Entrada.hr_entrada,8) AS [Hora Entrada],CONVERT(varchar,Entrada.data_entrada,103) AS[Data Entrada],UsuarioEntrada.login[UsuarioEntrada],Car.placa[Placa],A.automovel[Tipo],M.marca[Marca],Cli.nome[Nome Cliente],Cli.telefone[Telefone], CASE WHEN ticket.status=1 THEN  'Ativo' ELSE 'Encerrado'  END [Status]  FROM tb_ticket AS Ticket INNER JOIN tb_entrada AS Entrada ON Entrada.ticket_id = Ticket.id_ticket   INNER JOIN tb_usuario AS UsuarioEntrada ON UsuarioEntrada.id_usuario = Entrada.usuario_id  INNER JOIN tb_carro AS Car ON Car.id_carro = Ticket.carro_id INNER JOIN tb_automovel AS A ON Car.tipo_id = A.id_automovel INNER JOIN tb_marca AS M ON Car.marca_id = M.id_marca INNER JOIN tb_cliente AS Cli ON Cli.id_cliente = Ticket.cliente_id
		END
	END

--SELECT Cliente grid
CREATE OR ALTER PROCEDURE Select_Cliente_Grid
(
@Status int = null
)
AS
BEGIN
	SELECT id_cliente[ID],nome[Nome],telefone[Telefone] FROM tb_cliente WHERE status=@Status AND id_cliente > 1
END

--SELECT Cliente 
CREATE OR ALTER PROCEDURE Select_Cliente
(
@idCliente int = null
)
AS
BEGIN
	SELECT id_cliente[ID],nome[Nome],telefone[Telefone],status[Status] FROM tb_cliente WHERE id_cliente = @idCliente
END

--SELECT CLIENTE PELO NOME E TELEFONE
CREATE OR ALTER PROCEDURE Select_Cliente_Nome_Telefone
(
@Nome varchar(45) = null,
@Telefone varchar (14) = null
)
AS
BEGIN
	SELECT id_cliente[ID] FROM tb_cliente WHERE nome = @Nome AND telefone = @Telefone
END

--SELECT QTD TICKET PELO CLIENTE
CREATE OR ALTER PROCEDURE Select_QtdTicket_Cliente
(
@idCliente int = null
)
AS
BEGIN
	SELECT COUNT(id_ticket)[QTD] FROM tb_ticket AS Ticket INNER JOIN tb_cliente AS Cli ON Ticket.cliente_id = Cli.id_Cliente AND Ticket.status=1 AND Cli.id_cliente = @idCliente
END
