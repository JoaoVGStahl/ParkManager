CREATE FUNCTION Function_Pesquisa_Tela_Operacao(
@Placa varchar(7)
)
RETURNS TABLE 
AS 
RETURN

SELECT 
	Ticket.id_ticket[#Ticket], 
	A.automovel[Tipo],
	M.marca[Marca],
	Car.placa[Placa],
	Cli.nome[Nome Cliente], 
	Cli.telefone[Telefone],
	CONVERT(varchar, Entrada.hr_entrada,8) AS [Hora Entrada],
	CONVERT(varchar,Entrada.data_entrada,103) AS [Data Entrada] 
FROM tb_ticket AS Ticket 
INNER JOIN tb_carro as Car 
ON Ticket.carro_id = Car.id_carro 
INNER JOIN tb_cliente AS Cli 
ON Ticket.cliente_id = Cli.id_cliente 
INNER JOIN tb_automovel AS A 
ON Car.tipo_id = A.id_automovel 
INNER JOIN tb_marca AS M 
ON Car.marca_id = M.id_marca 
INNER JOIN tb_entrada AS Entrada 
ON Entrada.ticket_id = Ticket.id_ticket
WHERE Car.placa=@Placa
GO
