using System;
using System.Collections.Generic;
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
        public DataTable ExecuteProcedureReturnDataTable(string NameProcedure, List<SqlParameter> sp = null)
        {
            SqlDataAdapter da;
            SqlCommand cmd;
            DataTable dt = new DataTable();
            try
            {
                var connection = ConexaoBanco();
                cmd = new SqlCommand(NameProcedure, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (sp != null)
                {
                    cmd.Parameters.AddRange(sp.ToArray());
                }
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexao.Close();
                dt.Dispose();
            }
        }
        public int ExecuteProcedureReturnInt(string NameProcedure, List<SqlParameter> sp = null)
        {
            SqlCommand cmd = null;
            int LinesAffected = 0;
            try
            {
                var connection = ConexaoBanco();
                cmd = new SqlCommand(NameProcedure, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (sp != null)
                {
                    cmd.Parameters.AddRange(sp.ToArray());
                    LinesAffected = cmd.ExecuteNonQuery();
                }
                return LinesAffected;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexao.Close();
                cmd.Dispose();
            }
        }
        public int ExecuteProcedureWithReturnValue(string NameProcedure, List<SqlParameter> sp = null)
        {
            SqlCommand cmd = null;
            try
            {
                var connection = ConexaoBanco();
                cmd = new SqlCommand(NameProcedure, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (sp != null)
                {
                    cmd.Parameters.AddRange(sp.ToArray());
                }
                SqlParameter ReturnValue = cmd.Parameters.Add("@return_value", SqlDbType.Int);
                ReturnValue.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(ReturnValue.Value);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexao.Close();
                cmd.Dispose();
            }
        }
    }
}
