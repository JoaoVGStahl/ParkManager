using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teste
{
    public partial class FrmTelaUsuario : Form
    {
        Banco banco = new Banco();

        public FrmTelaUsuario()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmTelaUsuario_Load(object sender, EventArgs e)
        {
            PreencherGrid();
        }
        private void PreencherGrid()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT id_usuario[ID], login[Login],nivel[Nivel] FROM tb_usuario WHERE status=1";

            dt = banco.QueryBancoSql(query);

            dataGridView1.DataSource = dt;
        }
    }
}
