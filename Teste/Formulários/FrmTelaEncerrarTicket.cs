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
    public partial class FrmTelaEncerrarTicket : Form
    {
        Banco banco = new Banco();
        public FrmTelaEncerrarTicket()
        {
            InitializeComponent();
        }

        private void FrmTelaEncerrarTicket_Load(object sender, EventArgs e)
        {
            CarregarComboFormaPagamento();
        }
        private void CarregarComboFormaPagamento()
        {
            DataTable dt = new DataTable();
            string query = @"
            SELECT 
                id_pgt, descricao 
            FROM 
                tb_forma_pgt 
            WHERE 
                status=1";
            dt.Clear();
            dt = banco.QueryBancoSql(query);
            cmbFormaPagamento.DataSource = null;
            cmbFormaPagamento.DataSource = dt;
            cmbFormaPagamento.ValueMember = "id_pgt";
            cmbFormaPagamento.DisplayMember = "descricao";
            cmbFormaPagamento.SelectedItem = null;

        }


        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
