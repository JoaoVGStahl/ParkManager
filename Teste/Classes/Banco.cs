using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Teste
{
    class Banco
    {
        static SqlConnection conexao;

        static SqlCommand cmd;

        static SqlDataReader reader;

        static SqlDataAdapter adapter;
    }
}
