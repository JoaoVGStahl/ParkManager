using System;
using System.Data;
using System.Data.SqlClient;

namespace Teste
{
    class Banco
    {
        static SqlConnection conexao;

        private static SqlConnection ConexaoBanco()
        {
            //Função que Instancia, abre a conexão e retorna;
            conexao = new SqlConnection(Properties.Settings.Default.StringBanco);
            conexao.Open();
            return conexao;
        }
        public DataTable QueryBancoSql(string sql)
        {
            //Instanciando e limpando o DataAdapter e DataTable
            SqlDataAdapter da;
            DataTable dt = new DataTable();
            dt.Clear();
            da = null;
            try
            {
                //Abre a conexão
                var connection = ConexaoBanco();
                //Cria o comando
                var cmd = connection.CreateCommand();
                //Defini o tipo de comando
                cmd.CommandText = sql;
                //Executa o comando
                da = new SqlDataAdapter(cmd.CommandText, connection);
                //preenche o DataTable com o retorno da consulta
                da.Fill(dt);
                //Retorna o DataTable preenchido
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;


            }
            finally
            {
                //Fecha a conexao e limpa a variavel sql
                conexao.Close();
                sql = "";
            }
        }

        public int ProcedureInserirTicket(string placa, string tipo, string marca, string nome, string telefone)
        {
            //Abre a conexao e obtem paramentros restantes
            var connection = ConexaoBanco();
            DateTime DataAtual = DateTime.Now;
            DateTime HoraAtual = DateTime.Now;
            //Define qual procedimento será realizado
            SqlCommand cmd = new SqlCommand("[dbo].[InsertTicket]", connection);
            //Define o tipo do comando
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                //Envia os paramentros para o Banco de Dados
                cmd.Parameters.AddWithValue("@idUsuario", Globais.IdUsuario);
                cmd.Parameters.AddWithValue("@NomeCliente", nome);
                cmd.Parameters.AddWithValue("@Telefone", telefone);
                cmd.Parameters.AddWithValue("@placa", placa);
                cmd.Parameters.AddWithValue("@marca", marca);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@hr_entrada", HoraAtual);
                cmd.Parameters.AddWithValue("@data_entrada", DataAtual);
                cmd.Parameters.AddWithValue("@caminhoFoto", @"ParkManager\Fotos\008.png");
                //Retorna o IDTicket que acabou de ser criado
                var returnParameter = cmd.Parameters.Add("@Return_value", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                //Executa o comando
                cmd.ExecuteNonQuery();
                //Retorna o ID no formato int
                int idTicket = Convert.ToInt32(returnParameter.Value);
                return idTicket;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                //Fecha a conexao
                conexao.Close();
            }
        }
    }
}
