using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Teste
{
    class Banco
    {
        static string StrConn = @"Server=db-park-manager.ch2qj4cvcflx.us-east-1.rds.amazonaws.com,1433; Database=db_estacionamento; User Id=sa; Password=adminparkmanager";

        static SqlConnection conexao = new SqlConnection(StrConn);

        static SqlCommand cmd;

        static SqlDataAdapter adapter = new SqlDataAdapter();

        static SqlDataReader reader;

    }
}
