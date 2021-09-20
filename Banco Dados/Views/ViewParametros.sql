CREATE VIEW parametros AS
SELECT 
	valor_hr[Valor Hora],
	tolerancia[Tempo Tolêrancia],
	cnpj[CNPJ],
	razao_social[Razão Social],
	endereco[Endereço],
	bairro[Bairro],
	estado[Estado],
	cep[CEP],
	telefone[Telefone],
	incricao_estadual[Inscrição Estadual],
	caminho_log[Caminho Arquivo Log],
	caminho_foto_padrao[Caminho Padrão Fotos],
	porta_arduino[Porta Arduino],
	string_conn[String Conexão]	 
FROM tb_estacionamento WHERE status=1