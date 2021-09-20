CREATE VIEW parametros AS
SELECT 
	valor_hr[Valor Hora],
	tolerancia[Tempo Tol�rancia],
	cnpj[CNPJ],
	razao_social[Raz�o Social],
	endereco[Endere�o],
	bairro[Bairro],
	estado[Estado],
	cep[CEP],
	telefone[Telefone],
	incricao_estadual[Inscri��o Estadual],
	caminho_log[Caminho Arquivo Log],
	caminho_foto_padrao[Caminho Padr�o Fotos],
	porta_arduino[Porta Arduino],
	string_conn[String Conex�o]	 
FROM tb_estacionamento WHERE status=1