using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Teste
{
    class Banco
    {
        private SqlConnection conexao = new SqlConnection(Properties.Settings.Default.StringBanco);

        public void TestConn(string ConnString)
        {
            try
            {
                conexao = new SqlConnection(ConnString);
                conexao.Open();
            }
            catch (Exception)
            {
                conexao = new SqlConnection(Properties.Settings.Default.StringBanco);
                throw;
            }
            finally
            {
                conexao.Close();
            }
        }
        public DataTable ExecuteProcedureReturnDataTable(string NameProcedure, List<SqlParameter> sp = null)
        {
            SqlDataAdapter da;
            SqlCommand cmd;
            DataTable dt = new DataTable();
            try
            {

                conexao.Open();
                cmd = new SqlCommand(NameProcedure, conexao);
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
                conexao.Open();
                cmd = new SqlCommand(NameProcedure, conexao);
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
                conexao.Open();
                cmd = new SqlCommand(NameProcedure, conexao);
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
        public DataSet ExecuteProcedureWithReturnMultipleTables(string NameProcedure, List<SqlParameter> sp = null)
        {
            SqlDataAdapter da;
            SqlCommand cmd;
            var result = new DataSet();
            try
            {
                conexao.Open();
                cmd = new SqlCommand(NameProcedure, conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                if (sp != null)
                {
                    cmd.Parameters.AddRange(sp.ToArray());
                }
                da = new SqlDataAdapter(cmd);
                da.Fill(result);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}
