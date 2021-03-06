/****** Object:  Database [db_estacionamento]    Script Date: 06/11/2021 11:48:47 ******/
CREATE DATABASE [db_estacionamento]  (EDITION = 'Standard', SERVICE_OBJECTIVE = 'S0', MAXSIZE = 250 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS;
GO
ALTER DATABASE [db_estacionamento] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [db_estacionamento] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [db_estacionamento] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [db_estacionamento] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [db_estacionamento] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [db_estacionamento] SET ARITHABORT OFF 
GO
ALTER DATABASE [db_estacionamento] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [db_estacionamento] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [db_estacionamento] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [db_estacionamento] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [db_estacionamento] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [db_estacionamento] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [db_estacionamento] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [db_estacionamento] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [db_estacionamento] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [db_estacionamento] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [db_estacionamento] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [db_estacionamento] SET  MULTI_USER 
GO
ALTER DATABASE [db_estacionamento] SET ENCRYPTION ON
GO
ALTER DATABASE [db_estacionamento] SET QUERY_STORE = ON
GO
ALTER DATABASE [db_estacionamento] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  User [ParkManagerSystem]    Script Date: 06/11/2021 11:48:48 ******/
CREATE USER [ParkManagerSystem] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [lucasmendes]    Script Date: 06/11/2021 11:48:48 ******/
CREATE USER [lucasmendes] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [JoaoGirardi]    Script Date: 06/11/2021 11:48:48 ******/
CREATE USER [JoaoGirardi] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [carlosbarbosa]    Script Date: 06/11/2021 11:48:48 ******/
CREATE USER [carlosbarbosa] WITH DEFAULT_SCHEMA=[dbo]
GO
sys.sp_addrolemember @rolename = N'db_datareader', @membername = N'ParkManagerSystem'
GO
sys.sp_addrolemember @rolename = N'db_datawriter', @membername = N'ParkManagerSystem'
GO
sys.sp_addrolemember @rolename = N'db_datareader', @membername = N'lucasmendes'
GO
sys.sp_addrolemember @rolename = N'db_datawriter', @membername = N'lucasmendes'
GO
sys.sp_addrolemember @rolename = N'db_owner', @membername = N'JoaoGirardi'
GO
sys.sp_addrolemember @rolename = N'db_accessadmin', @membername = N'JoaoGirardi'
GO
sys.sp_addrolemember @rolename = N'db_datareader', @membername = N'JoaoGirardi'
GO
sys.sp_addrolemember @rolename = N'db_owner', @membername = N'carlosbarbosa'
GO
sys.sp_addrolemember @rolename = N'db_securityadmin', @membername = N'carlosbarbosa'
GO
/****** Object:  Table [dbo].[tb_automovel]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_automovel](
	[id_automovel] [int] NOT NULL,
	[automovel] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_automovel] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_carro]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_carro](
	[id_carro] [int] IDENTITY(1,1) NOT NULL,
	[placa] [varchar](7) NOT NULL,
	[marca_id] [int] NULL,
	[tipo_id] [int] NULL,
	[status] [smallint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_carro] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_cliente]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_cliente](
	[id_cliente] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NOT NULL,
	[telefone] [varchar](14) NULL,
	[status] [smallint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_cliente] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_entrada]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_entrada](
	[id_entrada] [int] IDENTITY(1,1) NOT NULL,
	[ticket_id] [int] NOT NULL,
	[usuario_id] [int] NOT NULL,
	[hr_entrada] [time](7) NOT NULL,
	[data_entrada] [date] NOT NULL,
	[status] [smallint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_entrada] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ticket_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_estacionamento]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_estacionamento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[valor_hr] [decimal](10, 2) NULL,
	[tolerancia] [time](0) NULL,
	[cnpj] [varchar](18) NULL,
	[razao_social] [varchar](32) NULL,
	[endereco] [varchar](50) NULL,
	[bairro] [varchar](32) NULL,
	[numero] [int] NULL,
	[cidade] [varchar](32) NULL,
	[estado] [char](2) NULL,
	[cep] [char](9) NULL,
	[inscricao_estadual] [varchar](15) NULL,
	[telefone] [varchar](14) NULL,
	[caminho_log] [varchar](100) NULL,
	[caminho_foto_padrao] [varchar](50) NULL,
	[porta_arduino] [char](4) NULL,
	[string_conn] [varchar](250) NULL,
	[status] [smallint] NOT NULL,
	[valor_min] [decimal](10, 2) NULL,
	[valor_unico] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[cnpj] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_forma_pgt]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_forma_pgt](
	[id_pgt] [int] IDENTITY(1,1) NOT NULL,
	[descricao] [varchar](32) NULL,
	[status] [smallint] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_pgt] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_fotos]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_fotos](
	[id_fotos] [int] IDENTITY(1,1) NOT NULL,
	[ticket_id] [int] NULL,
	[foto_caminho] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_fotos] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ticket_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_marca]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_marca](
	[id_marca] [int] IDENTITY(1,1) NOT NULL,
	[marca] [varchar](25) NULL,
	[automovel_id] [int] NOT NULL,
	[status] [smallint] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_marca] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_saida]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_saida](
	[id_saida] [int] IDENTITY(1,1) NOT NULL,
	[entrada_id] [int] NOT NULL,
	[usuario_id] [int] NOT NULL,
	[hr_saida] [time](7) NOT NULL,
	[data_saida] [date] NOT NULL,
	[forma_pgt_id] [int] NOT NULL,
	[permanencia] [time](7) NOT NULL,
	[total] [decimal](10, 2) NOT NULL,
	[troco] [decimal](10, 2) NOT NULL,
	[status] [smallint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_saida] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[entrada_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_ticket]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_ticket](
	[id_ticket] [int] IDENTITY(1,1) NOT NULL,
	[cliente_id] [int] NOT NULL,
	[carro_id] [int] NOT NULL,
	[status] [smallint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_ticket] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_usuario]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_usuario](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[login] [varchar](32) NOT NULL,
	[senha] [varchar](32) NOT NULL,
	[nivel] [smallint] NOT NULL,
	[status] [smallint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[login] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tb_carro]  WITH CHECK ADD FOREIGN KEY([marca_id])
REFERENCES [dbo].[tb_marca] ([id_marca])
GO
ALTER TABLE [dbo].[tb_carro]  WITH CHECK ADD FOREIGN KEY([tipo_id])
REFERENCES [dbo].[tb_automovel] ([id_automovel])
GO
ALTER TABLE [dbo].[tb_entrada]  WITH CHECK ADD FOREIGN KEY([ticket_id])
REFERENCES [dbo].[tb_ticket] ([id_ticket])
GO
ALTER TABLE [dbo].[tb_entrada]  WITH CHECK ADD FOREIGN KEY([usuario_id])
REFERENCES [dbo].[tb_usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[tb_fotos]  WITH CHECK ADD FOREIGN KEY([ticket_id])
REFERENCES [dbo].[tb_ticket] ([id_ticket])
GO
ALTER TABLE [dbo].[tb_marca]  WITH CHECK ADD FOREIGN KEY([automovel_id])
REFERENCES [dbo].[tb_automovel] ([id_automovel])
GO
ALTER TABLE [dbo].[tb_saida]  WITH CHECK ADD FOREIGN KEY([entrada_id])
REFERENCES [dbo].[tb_entrada] ([id_entrada])
GO
ALTER TABLE [dbo].[tb_saida]  WITH CHECK ADD FOREIGN KEY([forma_pgt_id])
REFERENCES [dbo].[tb_forma_pgt] ([id_pgt])
GO
ALTER TABLE [dbo].[tb_saida]  WITH CHECK ADD FOREIGN KEY([usuario_id])
REFERENCES [dbo].[tb_usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[tb_ticket]  WITH CHECK ADD FOREIGN KEY([carro_id])
REFERENCES [dbo].[tb_carro] ([id_carro])
GO
ALTER TABLE [dbo].[tb_ticket]  WITH CHECK ADD FOREIGN KEY([cliente_id])
REFERENCES [dbo].[tb_cliente] ([id_cliente])
GO
/****** Object:  StoredProcedure [dbo].[Adicionar_Marcas]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Adicionar_Marcas]
@Marca VARCHAR(25) = NULL,
@idTipo INT = NULL,
@Tipo VARCHAR(20) = NULL
AS
	BEGIN
		SET @idTipo = (SELECT id_automovel FROM tb_automovel WHERE automovel=@Tipo);
		INSERT INTO tb_marca(marca,automovel_id,status) VALUES (@Marca,@idTipo,1);
	END
		
	
GO
/****** Object:  StoredProcedure [dbo].[Alterar_Marca]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Alterar_Marca](
@idMarca int = null,
@idTipo int = null,
@Marca  varchar(25)= null,
@Tipo varchar(20)= null
)
AS
	BEGIN
	   SET @idTipo = (SELECT id_automovel FROM tb_automovel WHERE automovel=@Tipo);
	   UPDATE tb_marca SET marca = @Marca, automovel_id = @idTipo WHERE id_marca = @idMarca; 
	END
GO
/****** Object:  StoredProcedure [dbo].[Carregar_Tela_Encerrar]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Carregar_Tela_Encerrar]
(
@idTicket int = null
)
AS
BEGIN
	SELECT Ticket.id_ticket[#Ticket], CONVERT(varchar, Entrada.hr_entrada,8) AS [Hora Entrada],CONVERT(varchar,Entrada.data_entrada,103) AS[Data Entrada] FROM tb_ticket AS Ticket  INNER JOIN tb_entrada AS Entrada ON Entrada.ticket_id = Ticket.id_ticket AND Ticket.id_ticket=@idTicket
END
GO
/****** Object:  StoredProcedure [dbo].[ComboBox_Marca]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ComboBox_Marca]
(
  @Tipo varchar(20) = null
)
AS
BEGIN
	SELECT A.automovel[Tipo], M.marca [Marca] FROM tb_automovel as A INNER JOIN tb_marca as M ON A.id_automovel = M.automovel_id AND M.status =1 AND A.automovel =@Tipo ORDER BY M.marca
END
GO
/****** Object:  StoredProcedure [dbo].[ComboBox_Tipo]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ComboBox_Tipo]
AS
BEGIN
	 SELECT id_automovel, automovel from tb_automovel
END
GO
/****** Object:  StoredProcedure [dbo].[Deletar_Marca]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Deletar_Marca](
@idMarca int
)
AS
	BEGIN
	   UPDATE tb_marca SET status = 0 WHERE id_marca = @idMarca;
	END
GO
/****** Object:  StoredProcedure [dbo].[Encerrar_Ticket]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Encerrar_Ticket](
@IdEntrada int = null,
@IdUsuario int = null,
@idTicket int = null,
@IdFormaPgt int = null,
@DataSaida datetime = null,
@Permanencia time(7) = null,
@HoraSaida time(7) = null,
@Total decimal(10,2) = null,
@Troco decimal(10,2) = null,
@Status smallint = null,
@Forma_Pgt varchar(32) = null
)
AS
BEGIN
	BEGIN TRY
		SAVE TRANSACTION IfError
		BEGIN TRAN
			SET @IdEntrada = (SELECT Entrada.id_entrada FROM tb_entrada AS Entrada INNER JOIN tb_ticket Ticket ON Ticket.id_ticket = Entrada.ticket_id WHERE Ticket.id_ticket = @idTicket)
			SET @IdFormaPgt = (SELECT id_pgt FROM tb_forma_pgt WHERE descricao = @Forma_Pgt)
			INSERT INTO tb_saida VALUES(@IdEntrada, @IdUsuario, @HoraSaida, @DataSaida, @IdFormaPgt, @Permanencia, @Total, @Troco, 1)
			UPDATE tb_entrada SET status = 0 WHERE id_entrada = @IdEntrada
			UPDATE tb_ticket SET status = 0 WHERE id_ticket = @idTicket
			COMMIT TRANSACTION
			return 1
	END TRY
	BEGIN CATCH
			ROLLBACK TRANSACTION IfError
			return 0
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Filtrar_Marcas_Veiculo]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Filtrar_Marcas_Veiculo](
@Tipo varchar(20)
)
AS 
	BEGIN
		SELECT M.id_marca[ID], A.automovel[Tipo], M.marca[Marca] FROM tb_marca AS M INNER JOIN tb_automovel A ON M.automovel_id = A.id_automovel WHERE M.status = 1 AND A.automovel = @Tipo
	END
GO
/****** Object:  StoredProcedure [dbo].[Funcoes_Pesquisa]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE    PROCEDURE [dbo].[Funcoes_Pesquisa](
@Flag int,
@Tipo varchar(20) = null,
@Placa varchar(7) = null,
@idTicket int = null,
@IdUsuario int =null,
@idCarro int = null,
@idCliente int = null,
@Login varchar(32) = null,
@DataEntrada datetime = null,
@DataSaida datetime = null,
@Status int = null,
@Nome varchar(45) = null,
@Telefone varchar (14) = null
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
		SELECT COUNT(id_ticket)[QTD] FROM tb_ticket AS Ticket INNER JOIN tb_carro AS Car ON Ticket.carro_id = Car.id_carro WHERE Ticket.status=1 AND Car.placa=@Placa AND Car.status=1
	END
-- 6 =  Ticket's Aberto Load Tela Pesquisa Ticket
	IF(@Flag = 6)
	BEGIN
		SELECT Ticket.id_ticket[#Ticket],CONVERT(varchar, Entrada.hr_entrada,8) AS [Hora Entrada],CONVERT(varchar,Entrada.data_entrada,103) AS[Data Entrada],Usuario.login[Usuario Entrada],Car.placa[Placa],A.automovel[Tipo],M.marca[Marca],Cli.nome[Nome Cliente],Cli.telefone[Telefone] FROM tb_ticket AS Ticket INNER JOIN tb_entrada AS Entrada ON Entrada.ticket_id = Ticket.id_ticket INNER JOIN tb_usuario AS Usuario ON Entrada.usuario_id = Usuario.id_usuario INNER JOIN tb_carro AS Car ON Car.id_carro = Ticket.carro_id INNER JOIN tb_automovel AS A ON Car.tipo_id = A.id_automovel INNER JOIN tb_marca AS M ON Car.marca_id = M.id_marca INNER JOIN tb_cliente AS Cli ON Cli.id_cliente = Ticket.cliente_id WHERE Ticket.status = 1
	END
-- 7 = Pesquisa Ticket Especifico P/ Tela Operação
	IF(@Flag = 7)
	BEGIN
		SELECT Ticket.id_ticket[#Ticket], A.automovel[Tipo],M.marca[Marca],Car.placa[Placa],Cli.nome[Nome Cliente], Cli.telefone[Telefone],CONVERT(varchar, Entrada.hr_entrada,8) AS [Hora Entrada],CONVERT(varchar,Entrada.data_entrada,103) AS [Data Entrada], foto_caminho[Caminho Foto]  FROM tb_ticket AS Ticket INNER JOIN tb_carro as Car ON Ticket.carro_id = Car.id_carro INNER JOIN tb_cliente AS Cli ON Ticket.cliente_id = Cli.id_cliente INNER JOIN tb_automovel AS A ON Car.tipo_id = A.id_automovel INNER JOIN tb_marca AS M ON Car.marca_id = M.id_marca INNER JOIN tb_entrada AS Entrada ON Entrada.ticket_id = Ticket.id_ticket INNER JOIN tb_fotos AS Fotos ON Fotos.ticket_id = Ticket.id_ticket WHERE Ticket.Status=1 AND Car.placa=@Placa  
	END
-- 8 = SELECT Identificação do estacionamento
	IF(@Flag = 8)
	BEGIN	
		SELECT id[ID],cnpj[CNPJ], razao_social[Razão Social], endereco[Endereço],bairro[Bairro], numero[Número], cidade[Cidade],estado[Estado] , cep[CEP], inscricao_estadual[Inscrição Estadual], telefone[Telefone] FROM tb_estacionamento WHERE status=1
	END
-- 9 = SELECT de usuario
	IF(@Flag = 9)
    BEGIN
	   	SELECT id_usuario[ID], login[Login],nivel[Nivel],CASE WHEN status = 1 THEN 'Ativo' ELSE 'Inativo' END [Status] FROM tb_usuario WHERE status=1
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
-- 12 = SELECT Tela Desenvolvedor
	IF(@Flag = 12)
	BEGIN
		SELECT id,caminho_log[Caminho Log], porta_arduino[Porta Arduino],string_conn[String Conexão] FROM tb_estacionamento WHERE status =1
	END
-- 13 = Pesquisa informações d eum veiculo pela placa
	IF(@Flag = 13)
	BEGIN
		SELECT Car.placa[Placa], A.automovel[Tipo], M.marca FROM tb_carro AS Car INNER JOIN tb_automovel AS A ON Car.tipo_id = A.id_automovel INNER JOIN tb_marca AS M ON Car.marca_id = M.id_marca WHERE Car.placa = @Placa AND Car.status=1
	END
--14 = SELECT Parametros do Sistema
	IF(@Flag = 14)
	BEGIN
		SELECT id[ID],valor_hr[Valor Hora],valor_min[Valor Minimo], valor_unico[Valor Unico], tolerancia[Tolerancia], caminho_log[Caminho Log] FROM tb_estacionamento WHERE status=1
	END
--15 = SELECT Tela de Cadastro Veiculos
	IF(@Flag = 15)
	BEGIN
		SELECT Car.id_carro [ID],Car.placa[Placa], A.automovel[Tipo], M.marca FROM tb_carro AS Car INNER JOIN tb_automovel AS A ON Car.tipo_id = A.id_automovel INNER JOIN tb_marca AS M ON Car.marca_id = M.id_marca WHERE Car.status=1
	END
--16 = SELECT VEICULO ESPECIFICO
	IF(@Flag = 16)
	BEGIN
		SELECT Car.id_carro [ID],Car.placa[Placa], A.automovel[Tipo], M.marca[Marca],Car.status[Status] FROM tb_carro AS Car INNER JOIN tb_automovel AS A ON Car.tipo_id = A.id_automovel INNER JOIN tb_marca AS M ON Car.marca_id = M.id_marca WHERE Car.id_carro = @idCarro
	END
--17 = SELECT TICKETS ABERTO PARA UM VEICULO
	IF(@Flag = 17)
	BEGIN
		SELECT Ticket.id_ticket FROM tb_ticket AS Ticket INNER JOIN tb_carro AS Car ON Ticket.carro_id = Car.id_carro WHERE Ticket.status=1 AND Car.id_carro=@idCarro AND Car.status=1
	END
-- 18 = SELECT Tela pesquisa 
	IF(@Flag = 18)
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
-- 19 = SELECT Cliente grid
	IF(@Flag = 19)
	BEGIN
		SELECT id_cliente[ID],nome[Nome],telefone[Telefone] FROM tb_cliente WHERE status=@Status AND id_cliente > 1
	END
-- 20 = SELECT Cliente 
	IF(@Flag = 20)
	BEGIN
		SELECT id_cliente[ID],nome[Nome],telefone[Telefone],status[Status] FROM tb_cliente WHERE id_cliente = @idCliente
	END
-- 21 = SELECT CLIENTE PELO NOME E TELEFONE
	IF(@Flag = 21)
	BEGIN
		SELECT id_cliente[ID] FROM tb_cliente WHERE nome = @Nome AND telefone = @Telefone
	END
-- 22 = SELECT QTD TICKET PELO CLIENTE
	IF(@Flag = 22)
	BEGIN
		SELECT COUNT(id_ticket)[QTD] FROM tb_ticket AS Ticket INNER JOIN tb_cliente AS Cli ON Ticket.cliente_id = Cli.id_Cliente AND Ticket.status=1 AND Cli.id_cliente = @idCliente
	END
GO
/****** Object:  StoredProcedure [dbo].[Gerencia_Cliente]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Gerencia_Cliente](
@Flag int,
@idCliente int = null,
@Nome varchar(45) = null,
@Telefone varchar(14) = null,
@Status int = null
)
AS
-- 0 == SALVAR/EDITAR
	IF(@Flag =0)
	BEGIN
		IF(@idCliente IS NOT NULL)
		BEGIN
			UPDATE tb_cliente SET status =0 WHERE id_cliente = @idCliente
		END
		INSERT INTO tb_cliente(nome, telefone, status) VALUES (@Nome,@Telefone,@Status)
	END
-- 1 = Excluir cliente
	IF(@Flag = 1)
	BEGIN
		UPDATE tb_cliente SET status=0 WHERE id_cliente=@idCliente
	END
GO
/****** Object:  StoredProcedure [dbo].[Gerencia_Usuario]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Gerencia_Usuario]
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
GO
/****** Object:  StoredProcedure [dbo].[Gerencia_Veiculo]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[Gerencia_Veiculo]
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
/****** Object:  StoredProcedure [dbo].[Info_Estacionamento]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Info_Estacionamento]
AS
BEGIN	
	SELECT id[ID],cnpj[CNPJ], razao_social[Razão Social], endereco[Endereço],bairro[Bairro], numero[Número], cidade[Cidade],estado[Estado] , cep[CEP], inscricao_estadual[Inscrição Estadual], telefone[Telefone] FROM tb_estacionamento WHERE status=1
END
GO
/****** Object:  StoredProcedure [dbo].[InsertTicket]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[InsertTicket]
(
@idCarro int = null,
@idTicket int = null,
@idCliente int = null,
@idMarca int = null,
@idTipo int = null,
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
BEGIN
	BEGIN TRY
	BEGIN TRAN
		SAVE TRANSACTION IfError
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
					SET @idMarca = (SELECT id_marca FROM tb_marca AS M INNER JOIN tb_automovel AS A ON M.automovel_id = A.id_automovel AND A.id_automovel=@idTipo AND M.marca=@Marca)
					INSERT INTO tb_carro (placa,marca_id,tipo_id,status) VALUES(@Placa,@idMarca,@idTipo,1)
					SET @idCarro = @@IDENTITY
				END
			INSERT INTO tb_ticket (cliente_id,carro_id,status) VALUES(@idCliente,@idCarro,1)
			SET @idTicket = @@IDENTITY
			INSERT INTO tb_entrada(ticket_id,usuario_id,hr_entrada,data_entrada,status) VALUES(@idTicket,@idUsuario,@Hr_Entrada,@Data_Entrada,1)
			INSERT INTO tb_fotos(ticket_id,foto_caminho) VALUES (@idTicket,@Caminho_Foto)
			COMMIT TRANSACTION
			return @idTicket
	END TRY
	BEGIN CATCH
			ROLLBACK TRANSACTION IfError
			return 0
	END CATCH
END
			
GO
/****** Object:  StoredProcedure [dbo].[Metodos_Pagamento]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Metodos_Pagamento]
AS
BEGIN
	SELECT id_pgt,descricao FROM tb_forma_pgt WHERE status=1
END
GO
/****** Object:  StoredProcedure [dbo].[Parametros]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Parametros](
@Flag int,
@Id_Estacionamento INT =null,
@Caminho varchar(100) = null,
@Porta_Arduino varchar(4) = null,
@String_Conn varchar(250) = null,
@Cnpj varchar(18) = null,
@Razao_Social varchar(32) = null,
@Endereco varchar(50) = null,
@Bairro varchar(32) = null,
@Numero int = null,
@Cidade varchar(32) = null,
@Estado char(2) = null,
@Cep char(9) = null,
@Inscricao_Estadual varchar(15) = null,
@Telefone varchar(14) = null
)
AS
-- 1 = Update parametros do Desenvolvedor
	IF(@Flag = 1)
	BEGIN
		IF(@Id_Estacionamento IS NULL)
		BEGIN
			INSERT INTO tb_estacionamento(caminho_log,porta_arduino, string_conn,status) VALUES(@Caminho, @Porta_Arduino, @String_Conn, 1)
		END
		ELSE
		BEGIN
			UPDATE tb_estacionamento SET caminho_log=@Caminho, porta_arduino=@Porta_Arduino, string_conn=@String_Conn WHERE id = @Id_Estacionamento
		END
	END
-- 2 = INSERT/UPDATE Identificacao do Estacionamento
	IF(@Flag = 2)
	BEGIN
		IF(@Id_Estacionamento IS NULL)
		BEGIN
			INSERT INTO tb_estacionamento(cnpj,razao_social,endereco,bairro,numero,cidade,estado,cep,inscricao_estadual,telefone, status) VALUES(@Cnpj,@Razao_Social,@Endereco,@Bairro,@Numero,@Cidade,@Estado,@Cep,@Inscricao_Estadual,@Telefone,1)
		END
		ELSE
		BEGIN
			UPDATE tb_estacionamento SET cnpj=@Cnpj, razao_social=@Razao_Social,endereco=@Endereco,bairro=@Bairro,numero=@Numero,cidade=@Cidade,estado=@Estado,cep=@Cep,inscricao_estadual=@Inscricao_Estadual, telefone=@Telefone WHERE id = @Id_Estacionamento
		END
	END
GO
/****** Object:  StoredProcedure [dbo].[Parametros_Sistema]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Parametros_Sistema]
AS
BEGIN
	SELECT id[ID],valor_hr[Valor Hora],valor_min[Valor Minimo], valor_unico[Valor Unico], tolerancia[Tolerancia], caminho_log[Caminho Log] FROM tb_estacionamento WHERE status=1
END
GO
/****** Object:  StoredProcedure [dbo].[Pesquisa_Info_Placa]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Pesquisa_Info_Placa]
(
@Placa varchar(7) = null
)
AS
BEGIN
   SELECT Car.placa[Placa], A.automovel[Tipo], M.marca[Marca] FROM tb_carro AS Car INNER JOIN tb_automovel AS A ON Car.tipo_id = A.id_automovel INNER JOIN tb_marca AS M ON Car.marca_id = M.id_marca WHERE Car.placa = @Placa AND Car.status=1
END
GO
/****** Object:  StoredProcedure [dbo].[Pesquisa_Marca_Esp]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Pesquisa_Marca_Esp](
@Marca varchar(25) = null,
@Tipo varchar(20) = null
)
AS 
	BEGIN
		SELECT M.marca[Marca] FROM tb_marca AS M INNER JOIN tb_automovel A ON M.automovel_id = A.id_automovel WHERE M.status = 1 AND M.marca= @Marca AND A.automovel = @Tipo
	END
GO
/****** Object:  StoredProcedure [dbo].[Pesquisa_TelaDesenvolvedor]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Pesquisa_TelaDesenvolvedor]
AS
BEGIN
	SELECT id,caminho_log[Caminho Log], porta_arduino[Porta Arduino],string_conn[String Conexão] FROM tb_estacionamento WHERE status =1
END
GO
/****** Object:  StoredProcedure [dbo].[Pesquisa_Ticket_TelaOperacao]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Pesquisa_Ticket_TelaOperacao]
(
@Placa varchar(7) = null
)
AS
BEGIN
	SELECT Ticket.id_ticket[#Ticket], A.automovel[Tipo],M.marca[Marca],Car.placa[Placa],Cli.nome[Nome Cliente], Cli.telefone[Telefone],CONVERT(varchar, Entrada.hr_entrada,8) AS [Hora Entrada],CONVERT(varchar,Entrada.data_entrada,103) AS [Data Entrada], foto_caminho[Caminho Foto]  FROM tb_ticket AS Ticket INNER JOIN tb_carro as Car ON Ticket.carro_id = Car.id_carro INNER JOIN tb_cliente AS Cli ON Ticket.cliente_id = Cli.id_cliente INNER JOIN tb_automovel AS A ON Car.tipo_id = A.id_automovel INNER JOIN tb_marca AS M ON Car.marca_id = M.id_marca INNER JOIN tb_entrada AS Entrada ON Entrada.ticket_id = Ticket.id_ticket INNER JOIN tb_fotos AS Fotos ON Fotos.ticket_id = Ticket.id_ticket WHERE Ticket.Status=1 AND Car.placa=@Placa  
END
GO
/****** Object:  StoredProcedure [dbo].[Pesquisa_TicketAberto_Placa]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Pesquisa_TicketAberto_Placa]
(
@Placa varchar(7) = null
)
AS
BEGIN
	SELECT COUNT(id_ticket)[QTD] FROM tb_ticket AS Ticket INNER JOIN tb_carro AS Car ON Ticket.carro_id = Car.id_carro WHERE Ticket.status=1 AND Car.placa=@Placa AND Car.status=1
END
GO
/****** Object:  StoredProcedure [dbo].[Pesquisa_Usuario_Id]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Pesquisa_Usuario_Id]
(
@IdUsuario int =null
)
AS
BEGIN
	SELECT id_usuario[ID], login[Login],nivel[Nivel],status[Status] FROM tb_usuario WHERE id_usuario = @IdUsuario
END
GO
/****** Object:  StoredProcedure [dbo].[Pesquisa_Usuario_Login]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Pesquisa_Usuario_Login]
(
@Login varchar(32) = null
)
AS
BEGIN
	SELECT login[Login] FROM tb_usuario WHERE login=@Login
END
GO
/****** Object:  StoredProcedure [dbo].[Pesquisa_Usuarios]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Pesquisa_Usuarios]
AS
BEGIN
	SELECT id_usuario[ID], login[Login],nivel[Nivel],CASE WHEN status = 1 THEN 'Ativo' ELSE 'Inativo' END [Status] FROM tb_usuario WHERE status=1
END
GO
/****** Object:  StoredProcedure [dbo].[Salvar_Valor]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Salvar_Valor](
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
GO
/****** Object:  StoredProcedure [dbo].[Select_Cliente]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Select_Cliente]
(
@idCliente int = null
)
AS
BEGIN
	SELECT id_cliente[ID],nome[Nome],telefone[Telefone],status[Status] FROM tb_cliente WHERE id_cliente = @idCliente
END
GO
/****** Object:  StoredProcedure [dbo].[Select_Cliente_Grid]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Select_Cliente_Grid]
(
@Status int = null
)
AS
BEGIN
	SELECT id_cliente[ID],nome[Nome],telefone[Telefone] FROM tb_cliente WHERE status=@Status AND id_cliente > 1
END
GO
/****** Object:  StoredProcedure [dbo].[Select_Cliente_Nome_Telefone]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Select_Cliente_Nome_Telefone]
(
@Nome varchar(45) = null,
@Telefone varchar (14) = null
)
AS
BEGIN
	SELECT id_cliente[ID] FROM tb_cliente WHERE nome = @Nome AND telefone = @Telefone
END
GO
/****** Object:  StoredProcedure [dbo].[Select_Marca_Id]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Select_Marca_Id](
@idMarca int
)
AS
	BEGIN
		SELECT M.id_marca[ID], A.automovel[Tipo], M.marca[Marca] FROM tb_marca AS M INNER JOIN tb_automovel A ON M.automovel_id = A.id_automovel AND M.status = 1 AND M.id_marca =@idMarca
	END
GO
/****** Object:  StoredProcedure [dbo].[Select_QtdTicket_Cliente]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Select_QtdTicket_Cliente]
(
@idCliente int = null
)
AS
BEGIN
	SELECT COUNT(id_ticket)[QTD] FROM tb_ticket AS Ticket INNER JOIN tb_cliente AS Cli ON Ticket.cliente_id = Cli.id_Cliente AND Ticket.status=1 AND Cli.id_cliente = @idCliente
END
GO
/****** Object:  StoredProcedure [dbo].[Select_Tela_Pesquisa]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Select_Tela_Pesquisa]
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
GO
/****** Object:  StoredProcedure [dbo].[Select_TelaCadastro_Veiculos]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Select_TelaCadastro_Veiculos]
AS
BEGIN
	SELECT Car.id_carro [ID],Car.placa[Placa], A.automovel[Tipo], M.marca FROM tb_carro AS Car INNER JOIN tb_automovel AS A ON Car.tipo_id = A.id_automovel INNER JOIN tb_marca AS M ON Car.marca_id = M.id_marca WHERE Car.status=1
END
GO
/****** Object:  StoredProcedure [dbo].[Select_TicketAberto_Veiculo]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Select_TicketAberto_Veiculo]
(
@idCarro int = null
)
AS
BEGIN
	SELECT Ticket.id_ticket FROM tb_ticket AS Ticket INNER JOIN tb_carro AS Car ON Ticket.carro_id = Car.id_carro WHERE Ticket.status=1 AND Car.id_carro=@idCarro AND Car.status=1
END
GO
/****** Object:  StoredProcedure [dbo].[Select_Veiculo_Especifico]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Select_Veiculo_Especifico]
@idCarro int = null
AS
BEGIN
	SELECT Car.id_carro [ID],Car.placa[Placa], A.automovel[Tipo], M.marca[Marca],Car.status[Status] FROM tb_carro AS Car INNER JOIN tb_automovel AS A ON Car.tipo_id = A.id_automovel INNER JOIN tb_marca AS M ON Car.marca_id = M.id_marca WHERE Car.id_carro = @idCarro
END
GO
/****** Object:  StoredProcedure [dbo].[Tickets_Abertos]    Script Date: 06/11/2021 11:48:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Tickets_Abertos]
AS
BEGIN
	SELECT COUNT(id_ticket)AS [Ticket's Abertos] FROM tb_ticket WHERE status=1
END
GO
ALTER DATABASE [db_estacionamento] SET  READ_WRITE 
GO
