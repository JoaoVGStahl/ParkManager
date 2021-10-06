using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Teste
{
    public partial class FrmTelaCliente : Form
    {
        Banco banco = new Banco();
        public FrmTelaCliente()
        {
            InitializeComponent();
        }

        private void FrmTelaCliente_Load(object sender, EventArgs e)
        {
            cmbStatus.SelectedIndex = 1;
            CarregarGrid();
        }
        private void CarregarGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@Flag", SqlDbType = SqlDbType.Int, Value = 19},
                    new SqlParameter(){ParameterName="@Status", SqlDbType = SqlDbType.Int, Value = cmbStatus.SelectedIndex}
                };
                dt = banco.InsertData("dbo.Funcoes_Pesquisa", sp);
                if(dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Falha ao carregar a lista de cliente!", "Falha ao carregar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Falha ao carregar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
