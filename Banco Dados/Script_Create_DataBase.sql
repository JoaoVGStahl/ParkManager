CREATE DATABASE db_estacionamento
USE db_estacionamento

Set Language Português

CREATE TABLE tb_estacionamento(
    id INT PRIMARY KEY IDENTITY,
    valor_hr DECIMAL(10,2),
	tolerancia TIME(0),
	cnpj varchar(18)NOT NULL,
	razao_social varchar (32) NOT NULL,
	endereco varchar(100) NOT NULL,
	bairro varchar(32) NOT NULL,
	cidade varchar (32) NOT NULL,
	estado char(2) NOT NULL,
	cep char(9) NOT NULL,
	incricao_estadual varchar(15) NOT NULL,
	telefone varchar(14) NOT NULL,
	caminho_log varchar(100) NOT NULL,
	caminho_foto_padrao varchar(50) NOT NULL,
	porta_arduino char(4) NULL,
	string_conn varchar(250) NULL,
	status SMALLINT NOT NULL
)

CREATE TABLE tb_cliente(
    id_cliente INT PRIMARY KEY IDENTITY,
	nome VARCHAR(50) not null,
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
	automovel_id INT NOT NULL,
	FOREIGN KEY(automovel_id) REFERENCES tb_automovel
)

CREATE TABLE tb_carro(
    id_carro INT PRIMARY KEY IDENTITY,
	placa VARCHAR(7) UNIQUE NOT NULL,
	marca_id INT,
	tipo_id INT,
	status SMALLINT NOT NULL,
	FOREIGN KEY(tipo_id) REFERENCES tb_automovel,
	FOREIGN KEY (marca_id) REFERENCES tb_marca
)

CREATE TABLE tb_ticket(
   id_ticket INT PRIMARY KEY IDENTITY NOT NULL,
   cliente_id INT NOT NULL,
   carro_id INT,
   status SMALLINT NOT NULL,
   FOREIGN KEY(cliente_id) REFERENCES tb_cliente,
   FOREIGN KEY(carro_id) REFERENCES tb_carro
)

CREATE TABLE tb_usuario(
    id_usuario INT PRIMARY KEY IDENTITY,
    login VARCHAR(32) UNIQUE NOT NULL,
	senha VARCHAR(32) NOT NULL,
	nivel SMALLINT NOT NULL, 
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
   descricao VARCHAR(32),
   status SMALLINT
)

CREATE TABLE tb_saida(
   id_saida INT PRIMARY KEY IDENTITY NOT NULL,
   entrada_id INT NOT NULL,
   usuario_id INT NOT NULL,
   hr_saida TIME NOT NULL,
   data_saida DATE NOT NULL,
   forma_pgt_id INT NOT NULL,
   permanencia TIME NOT NULL,
   total DECIMAL(10,2) NOT NULL,
   troco decimal(10,2) NOT NULL,
   status SMALLINT NOT NULL,
   FOREIGN KEY (entrada_id) REFERENCES tb_entrada,
   FOREIGN KEY (usuario_id) REFERENCES tb_usuario,
   FOREIGN KEY (forma_pgt_id) REFERENCES tb_forma_pgt
)

CREATE TABLE tb_fotos(
   id_fotos INT PRIMARY KEY IDENTITY,
   ticket_id INT,
   foto_caminho VARCHAR(100)
   FOREIGN KEY (ticket_id) REFERENCES tb_ticket
) 
INSERT INTO tb_estacionamento(valor_hr,tolerancia,cnpj,razao_social,endereco,bairro,cidade,estado,cep,incricao_estadual,telefone,caminho_log,caminho_foto_padrao,porta_arduino,string_conn,status) VALUES
(6.00,'00:15:00','23.934.793/0001-11','Estacionamento São José Ltda','Avenida Raul Furquim','Centro','Bebedouro','SP','14700-000','123.908.064.656','(17)3343-1234','C:\ParkManager\log\log.dat','C:\ParkManager\Fotos\','COM3','Server=db-park-manager.ch2qj4cvcflx.us-east-1.rds.amazonaws.com,1433;Database=db_estacionamento;User Id=sa;Password=adminparkmanager;',1)

INSERT INTO tb_forma_pgt (descricao,status) VALUES
('PIX',1),
('Crédito',1),
('Débito',1),
('Dinheiro',1)

INSERT INTO tb_automovel (id_automovel, automovel) VALUES
(1,'Carro'),
(2,'Moto'),
(3,'Caminhonete'),
(4,'Caminhão');

INSERT INTO tb_marca (marca, automovel_id) VALUES
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
('Volkswagen','1'),
('Wuyang', '2'),
('Yamaha', '2');

INSERT INTO tb_usuario(login,senha,nivel,status) VALUEs('admin','admin',2,1)

INSERT INTO tb_cliente(nome,telefone,status) VALUES('Convidado','(00)00000-0000',1)

--Informações conexao
Instancia: db-park-manager.ch2qj4cvcflx.us-east-1.rds.amazonaws.com,1433
User: sa
Senha: adminparkmanager

--StrConnection
Server=db-park-manager.ch2qj4cvcflx.us-east-1.rds.amazonaws.com,1433;Database=db_estacionamento;User Id=sa;Password=adminparkmanager;

