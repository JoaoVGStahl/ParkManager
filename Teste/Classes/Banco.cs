using System;
using System.Data;
using System.Collections.Generic;
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
                //Define o tipo de comando
                cmd.CommandText = sql;
                //Executa o comando
                da = new SqlDataAdapter(cmd.CommandText, connection);
                //preenche o DataTable com o retorno da consulta
                da.Fill(dt);
                //Retorna o DataTable preenchido
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //Fecha a conexao e limpa a variavel sql
                da.Dispose();
                dt.Dispose();
                conexao.Close();
                sql = "";
            }
        }
        #region Procedure para inserir Dados utilizando Procedures
        public DataTable InsertData(string NameProcedure, List<SqlParameter> sp = null)
        {
            SqlDataAdapter da = null;
            SqlCommand cmd =null;
            DataTable dt = new DataTable();


            try
            {
                var connection = ConexaoBanco();
                cmd = new SqlCommand(NameProcedure, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if(sp != null)
                {
                    cmd.Parameters.AddRange(sp.ToArray());
                    da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    
                }
                return dt;



            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexao.Close();
                da.Dispose();
                cmd.Dispose();
                dt.Dispose();
                
                
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
                cmd.Parameters.AddWithValue("@Nome_Cliente", nome);
                cmd.Parameters.AddWithValue("@Telefone", telefone);
                cmd.Parameters.AddWithValue("@Placa", placa);
                cmd.Parameters.AddWithValue("@Marca", marca);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                cmd.Parameters.AddWithValue("@Hr_Entrada", HoraAtual);
                cmd.Parameters.AddWithValue("@Data_Entrada", DataAtual);
                cmd.Parameters.AddWithValue("@Caminho_Foto", @"ParkManager\Fotos\008.png");
                //Retorna o IDTicket que acabou de ser criado
                var returnParameter = cmd.Parameters.Add("@Return_value", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                //Executa o comando
                cmd.ExecuteNonQuery();
                //Retorna o ID no formato int
                int idTicket = Convert.ToInt32(returnParameter.Value);
                return idTicket;
            }
            catch (Exception)
            {

                throw ;
            }
            finally
            {
                cmd.Dispose();
                //Fecha a conexao
                conexao.Close();
            }
        }
        #endregion

        #region Procedure com Flags para realizar consultas sem parametros especificos, ou seja, carregas todas as informações de uma tabela
        public DataTable ProcedureSemParametros(int flag)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da;
            SqlCommand cmd;
            try
            {
                // Cria e Abre a conexao
                var connection = ConexaoBanco();
                //Qual será a consulta no Banco de Dados
                cmd = new SqlCommand("[dbo].[Funcoes_Pesquisa]",connection);
                //Tipo de Consulta
                cmd.CommandType = CommandType.StoredProcedure;
                //Parametros
                cmd.Parameters.AddWithValue("@Flag", flag);
                //Preencher DataTable
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                // Destrói os recursos usados da memoria
                da.Dispose();
                cmd.Dispose();
                //Retorna DataTable
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexao.Close();
                
            }
            
            //Abre a conexao e obtem paramentros restantes
            
        }
        #endregion
        #region Procedure que carrega o ComboBox Marca da tela de operação, de acordo com o Tipo Selecionado
        public DataTable ProcedureMarca(int flag, string tipo,string placa)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da;
            SqlCommand cmd;
            try
            {
                // Cria e Abre a conexao
                var connection = ConexaoBanco();
                //Qual será a consulta no Banco de Dados
                cmd = new SqlCommand("[dbo].[Funcoes_Pesquisa]", connection);
                //Tipo de Consulta
                cmd.CommandType = CommandType.StoredProcedure;
                //Parametros
                cmd.Parameters.AddWithValue("@Flag", flag);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                
                //Preencher DataTable
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                // Destrói os recursos usados da memoria
                da.Dispose();
                cmd.Dispose();
                //Retorna DataTable
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexao.Close();

            }

            //Abre a conexao e obtem paramentros restantes

        }
        #endregion
        #region Procedure que trás as informações de um ticket sobre um veiculo especifico
        public DataTable ProcedurePesquisaTicketVeiculo(int flag,string placa)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da;
            SqlCommand cmd;
            try
            {
                // Cria e Abre a conexao
                var connection = ConexaoBanco();
                //Qual será a consulta no Banco de Dados
                cmd = new SqlCommand("[dbo].[Funcoes_Pesquisa]", connection);
                //Tipo de Consulta
                cmd.CommandType = CommandType.StoredProcedure;
                //Parametros
                cmd.Parameters.AddWithValue("@Flag", flag);
                cmd.Parameters.AddWithValue("@Placa",placa);
                //Preencher DataTable
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                // Destrói os recursos usados da memoria
                da.Dispose();
                cmd.Dispose();
                //Retorna DataTable
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexao.Close();

            }

            //Abre a conexao e obtem paramentros restantes

        }
        #endregion
        public DataTable ProcedureCarregarTicket(int flag,int idTicket)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da;
            SqlCommand cmd;
            try
            {
                // Cria e Abre a conexao
                var connection = ConexaoBanco();
                //Qual será a consulta no Banco de Dados
                cmd = new SqlCommand("[dbo].[Funcoes_Pesquisa]", connection);
                //Tipo de Consulta
                cmd.CommandType = CommandType.StoredProcedure;
                //Parametros
                cmd.Parameters.AddWithValue("@Flag", flag);
                cmd.Parameters.AddWithValue("@idTicket", idTicket);
                //Preencher DataTable
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                // Destrói os recursos usados da memoria
                da.Dispose();
                cmd.Dispose();
                //Retorna DataTable
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexao.Close();

            }

            //Abre a conexao e obtem paramentros restantes

        }
        public int DmlBancoSql(string query)
        {
            int result;
            SqlCommand cmd = new SqlCommand();
            try
            {
                //Abre a conexao
                var connection = ConexaoBanco();
                //Cria o comando
                cmd = new SqlCommand(query, connection);
                result = cmd.ExecuteNonQuery();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                cmd.Dispose();
                conexao.Close();
            }
        }
    }
}
