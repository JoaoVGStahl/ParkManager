CREATE DATABASE db_estacionamento
USE db_estacionamento

CREATE DATABASE db_estacionamento_bkp
USE db_estacionamento_bkp
Set Language Português
SELECT @@language



CREATE TABLE tb_estacionamento(
    id INT PRIMARY KEY IDENTITY,
    valor_hr DECIMAL(10,2),
	tolerancia TIME(0),
	qtd_vagas INT NOT NULL,
	vagas_atuais INT NOT NULL,
	caminho_log varchar(100) NOT NULL,
	status SMALLINT NOT NULL
)

CREATE TABLE tb_cliente(
    id_cliente INT PRIMARY KEY IDENTITY,
	nome VARCHAR(80) not null,
	telefone VARCHAR(14),
	status SMALLINT NOT NULL
)

CREATE TABLE tb_automovel(
    id_automovel INT PRIMARY KEY NOT NULL,
	automovel VARCHAR(20) 
)

CREATE TABLE tb_marca(
    id_marca INT PRIMARY KEY IDENTITY NOT NULL,
	marca VARCHAR(25),
	id_automovel INT NOT NULL,
	FOREIGN KEY(id_automovel) REFERENCES tb_automovel
)

CREATE TABLE tb_carro(
    id_carro INT PRIMARY KEY IDENTITY,
	cliente_id INT NOT NULL,
	marca VARCHAR(25),
	tipo VARCHAR(20),
	placa VARCHAR(8) NOT NULL,
	status SMALLINT NOT NULL,
	FOREIGN KEY(cliente_id) REFERENCES tb_cliente,
)

CREATE TABLE tb_ticket(
   id_ticket INT PRIMARY KEY IDENTITY NOT NULL,
   carro_id INT,
   status SMALLINT NOT NULL
)

CREATE TABLE tb_usuario(
    id_usuario INT PRIMARY KEY IDENTITY,
    login VARCHAR(20) UNIQUE NOT NULL,
	senha VARCHAR(18) NOT NULL,
	nivel SMALLINT NOT NULL, --1 para cliente, 2 para funcionario, 3 para adm
	status SMALLINT NOT NULL
)

CREATE TABLE tb_entrada(
   id_entrada INT PRIMARY KEY IDENTITY,
   ticket_id INT NOT NULL,
   usuario_id INT NOT NULL,
   hr_entrada TIME NOT NULL,
   data_entrada DATE,
   status SMALLINT NOT NULL,
   FOREIGN KEY(ticket_id) REFERENCES tb_ticket,
   FOREIGN KEY (usuario_id) REFERENCES tb_usuario
)

CREATE TABLE tb_forma_pgt(
   id_pgt INT PRIMARY KEY IDENTITY,
   descricao VARCHAR(18),
   status SMALLINT
)

CREATE TABLE tb_saida(
   id_saida INT PRIMARY KEY IDENTITY NOT NULL,
   ticket_id INT NOT NULL,
   usuario_id INT NOT NULL,
   hr_saida TIME NOT NULL,
   data_saida DATE NOT NULL,
   forma_pgt_id INT NOT NULL,
   total DECIMAL(10,2) NOT NULL,
   troco decimal(10,2) NOT NULL,
   status SMALLINT NOT NULL,
   FOREIGN KEY (ticket_id) REFERENCES tb_ticket,
   FOREIGN KEY (usuario_id) REFERENCES tb_usuario,
   FOREIGN KEY (forma_pgt_id) REFERENCES tb_forma_pgt
)

CREATE TABLE tb_fotos(
   id_fotos INT PRIMARY KEY IDENTITY,
   ticket_id INT,
   foto_caminho VARCHAR(100)
   FOREIGN KEY (ticket_id) REFERENCES tb_ticket
) 


INSERT INTO tb_estacionamento (valor_hr,tolerancia, qtd_vagas, vagas_atuais,caminho_log, status) VALUES
('6', '00:15:00', '20', '0','log.dat', '1');

INSERT INTO tb_cliente(nome,telefone,status) VALUES('Convidado','00 12345-6789', 1);

INSERT INTO tb_automovel (id_automovel, automovel) VALUES
(1,'Carro'),
(2,'Moto'),
(3,'Caminhonete'),
(4,'Caminhão');

INSERT INTO tb_marca (marca, id_automovel) VALUES
('Audi', '1'),
('Avelloz', '2'),
('BRP', '2'),
('Bravax', '2'),
('Bull', '2'),
('BMW','1'),
('BMW','2'),
('CFMoto', '2'),
('Citroën', '1'),
('Dafra', '2'),
('DAF', '3'),
('Dayang', '2'),
('Dodge', '1'),
('Ducati', '2'),
('Ferrari', '1'),
('Ford', '1'),
('Foton', '3'),
('Haojue', '2'),
('Harley-Davidson', '2'),
('Iros', '2'),
('Jaguar', '1'),
('Jeep', '1'),
('KTM', '2'),
('Kawasaki', '2'),
('Kymco', '2'),
('Lamborghini', '1'),
('Land Rover', '1'),
('MAN', '3'),
('Maserati', '1'),
('McLaren', '1'),
('Mitsubishi', '1'),
('Motocar', '2'),
('Nissan', '1'),
('Peugeot', '1'),
('Porshe', '1'),
('Royal Enfield', '2'),
('Scania', '4'),
('Shineray', '2'),
('Sousa', '2'),
('Subaru', '1'),
('Toller', '1'),
('Traxx', '2'),
('Triumph', '2'),
('Vespa', '2'),
('Volvo','4'),
('Voltz', '2'),
('Wuyang', '2'),
('Yamaha', '2');

INSERT INTO tb_carro(cliente_id,marca,tipo,placa,status) VALUES(1, 'Audi','Carro', 'GOD3492', 1)

INSERT INTO tb_ticket(carro_id,status) VALUES(1,0)

INSERT INTO tb_usuario(login,senha,nivel,status) VALUES('admin','admin', 3, 0)

INSERT INTO tb_usuario(login,senha,nivel,status) VALUES('joao.girardi','admin', 1, 1)

INSERT INTO tb_entrada(ticket_id, usuario_id, hr_entrada, data_entrada, status) VALUES (1, 1, '07:20:00', '06-09-2021', 1)

INSERT INTO tb_forma_pgt (descricao,status) VALUES('PIX', 1)

INSERT INTO tb_forma_pgt (descricao,status) VALUES('Crédito', 1)

INSERT INTO tb_forma_pgt (descricao,status) VALUES('Débito', 1)

INSERT INTO tb_forma_pgt (descricao,status) VALUES('Dinheiro', 1)

INSERT INTO tb_saida (ticket_id, usuario_id, hr_saida, data_saida, forma_pgt_id, total,troco, status) VALUES (1, 1, '07:25:00', '06-09-2021', 1, 12,0, 0)

INSERT INTO tb_fotos (ticket_id, foto_caminho) VALUES (1, 'C:\ParkManager\Fotos\');

SELECT * FROM tb_ticket

SELECT * FROM tb_entrada

>>>>>>> tela-encerrar-ticket
Instancia: db-park-manager.ch2qj4cvcflx.us-east-1.rds.amazonaws.com,1433
User: sa
Senha: adminparkmanager

--StrConnection
Server=db-park-manager.ch2qj4cvcflx.us-east-1.rds.amazonaws.com,1433;Database=db_estacionamento;User Id=sa;Password=adminparkmanager;
--Procedure
USE [db_estacionamento]
GO
/****** Object:  StoredProcedure [dbo].[InsertTicket]    Script Date: 10/09/2021 09:31:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertTicket]
(
@idUsuario int,
@NomeCliente varchar(80),
@Telefone varchar(14),
@placa varchar(7),
@marca varchar(25),
@tipo varchar(20),
@hr_entrada time(7),
@data_entrada date,
@caminhoFoto varchar(100)
) 
AS 
DECLARE @idCarro int,
@idTicket int,
@idCliente int,
@NomeClienteTB varchar(80)

SET @idCarro = (SELECT id_carro from tb_carro WHERE placa=@placa AND status=1)
			IF(@idCarro IS NULL)
			BEGIN
				IF(@NomeCliente ='Convidado')
				BEGIN
					INSERT INTO tb_carro(cliente_id,marca,tipo,placa,status) VALUES(1,@marca,@tipo,@placa, 1)
					SET @idCarro = @@IDENTITY
				END
				ELSE
				BEGIN
					INSERT INTO tb_cliente(nome,telefone,status) VALUES(@NomeCliente,@Telefone,1)
					SET @idCliente = @@IDENTITY

					INSERT INTO tb_carro(cliente_id,marca,tipo,placa,status) VALUES(@idCliente,@marca,@tipo,@placa, 1)
					SET @idCarro = @@IDENTITY
				END
			END	
			ELSE
			BEGIN
			SET @NomeClienteTB = (SELECT Cli.nome[Nome] FROM tb_cliente as Cli INNER JOIN tb_carro as Car ON  Cli.id_cliente = Car.cliente_id AND Car.id_carro=@idCarro)
				IF(@NomeClienteTB != @NomeCliente)
				BEGIN
					UPDATE Cli SET nome=@NomeCliente,telefone=@Telefone FROM tb_cliente as Cli INNER JOIN tb_carro as Car ON Cli.id_cliente = Car.cliente_id AND Car.id_Carro = @idCarro
				END
			END
INSERT INTO tb_ticket(carro_id,status) VALUES(@idCarro,1)
SET @idTicket = @@IDENTITY
INSERT INTO tb_fotos (ticket_id, foto_caminho) VALUES (@idTicket,@caminhoFoto);
INSERT INTO tb_entrada(ticket_id, usuario_id, hr_entrada, data_entrada, status) VALUES (@idTicket, @idUsuario,@hr_entrada, @data_entrada, 1)
return @idTicket

--SELECT ComboBox Tipo
SELECT id_automovel, automovel from tb_automovel ORDER BY  automovel desc

--SELECT ComboBox Marca
SELECT A.automovel[Tipo], M.marca [Marca] FROM tb_automovel as A INNER JOIN tb_marca as M ON A.id_automovel = M.id_automovel AND A.automovel = 'Carro'

--SELECT Ticket count ticket aberto
SELECT COUNT(id_ticket) FROM tb_ticket WHERE status=1


--Select Ticket para tela de operação
SELECT Car.tipo[Tipo], Car.marca[Marca], Car.placa[Placa], Cli.nome[Nome], Cli.telefone[Telefone], Entrada.hr_entrada[Hora Entrada],FORMAT(Entrada.data_entrada,'dd/MM/yyyy') AS[Data Entrada] FROM tb_ticket AS Ticket INNER JOIN tb_carro AS Car ON Ticket.carro_id = Car.id_Carro INNER JOIN tb_cliente AS Cli ON Car.cliente_id = Cli.id_cliente INNER JOIN tb_entrada AS Entrada ON Entrada.ticket_id = Ticket.id_ticket AND Placa='ABC1234' AND Ticket.status=1


SELECT 
	Ticket.id_ticket[#Ticket], Entrada.hr_entrada[Hora Entrada],Entrada.data_entrada[Data Entrada], Usuario.login[Usuario Entrada], Car.placa[Placa], Car.tipo[Tipo], Car.marca[Marca], Cli.nome[Nome], Cli.Telefone[Telefone], Saida.hr_saida[Hora Saida], Saida.data_saida[Data Saida], Usuario.login[Usuario Saida], Saida.Total[Total] 
FROM 
	tb_ticket AS Ticket
INNER JOIN tb_entrada AS Entrada 
ON  
	Entrada.ticket_id = Ticket.id_ticket
INNER JOIN tb_usuario AS Usuario
ON
	Entrada.usuario_id = Usuario.id_usuario
INNER JOIN tb_carro AS Car
ON 
	Ticket.carro_id = Car.id_carro
INNER JOIN tb_cliente AS Cli
ON 
	Car.cliente_id = Cli.id_cliente 
INNER JOIN tb_saida AS Saida
ON 
	Saida.ticket_id = Ticket.id_ticket
INNER JOIN tb_forma_pgt AS Forma
ON 
	Saida.forma_pgt_id = Forma.id_pgt
WHERE Ticket.id_ticket=0 AND  Car.placa='GOD3492'AND Entrada.data_entrada='06-09-2021' AND Saida.data_saida='06-09-2021' AND Ticket.status=0

SELECT 
	Ticket.id_ticket[#Ticket], Entrada.hr_entrada[Hora Entrada],Entrada.data_entrada[Data Entrada], Usuario.login[Usuario Entrada], Car.placa[Placa], Car.tipo[Tipo], Car.marca[Marca], Cli.nome[Nome], Cli.Telefone[Telefone]
FROM
	tb_ticket AS Ticket
INNER JOIN tb_entrada AS Entrada 
ON  
	Entrada.ticket_id = Ticket.id_ticket
INNER JOIN tb_usuario AS Usuario
ON
	Entrada.usuario_id = Usuario.id_usuario
INNER JOIN tb_carro AS Car
ON 
	Ticket.carro_id = Car.id_carro
INNER JOIN tb_cliente AS Cli
ON 
	Car.cliente_id = Cli.id_cliente 
WHERE Ticket.status=1

SELECT id_pgt, descricao FROM tb_forma_pgt WHERE status=1
>>>>>>> tela-encerrar-ticket
