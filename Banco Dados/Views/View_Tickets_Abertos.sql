CREATE VIEW Tickets_Abertos AS 
SELECT 
	Ticket.id_ticket[#Ticket], 
	CONVERT(varchar, Entrada.hr_entrada,8) AS [Hora Entrada],
	CONVERT(varchar,Entrada.data_entrada,103) AS[Data Entrada],
	Usuario.login[Usuario Entrada],
	Car.placa[Placa],
	A.automovel[Tipo],
	M.marca[Marca],
	Cli.nome[Nome Cliente],
	Cli.telefone[Telefone]

FROM tb_ticket AS Ticket

INNER JOIN tb_entrada AS Entrada ON 
	Entrada.ticket_id = Ticket.id_ticket
INNER JOIN tb_usuario AS Usuario ON
	Entrada.usuario_id = Usuario.id_usuario
INNER JOIN tb_carro AS Car ON 
	Car.id_carro = Ticket.carro_id
INNER JOIN tb_automovel AS A ON 
	Car.tipo_id = A.id_automovel
INNER JOIN tb_marca AS M ON 
	Car.marca_id = M.id_marca 
INNER JOIN tb_cliente AS Cli ON 
	Cli.id_cliente = Ticket.cliente_id 
WHERE Ticket.status = 1