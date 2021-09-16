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
            CarregarTicket();
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
        private void CarregarTicket()
        {
            lblHoraSaida.Text = DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToShortDateString();
            DataTable dt = new DataTable();
            string query = @"
             SELECT 
                Ticket.id_ticket[#Ticket], CONVERT(varchar, Entrada.hr_entrada,8) AS [Hora Entrada],CONVERT(varchar,Entrada.data_entrada,103) AS[Data Entrada] 
            FROM 
                tb_ticket AS Ticket  
            INNER JOIN 
                tb_entrada AS Entrada 
            ON 
                Entrada.ticket_id = Ticket.id_ticket 
            AND 
                Ticket.id_ticket="+ Globais.IdTicket;
            dt = banco.QueryBancoSql(query);
            if(dt.Rows.Count > 0)
            {
                lblIdTicket.Text ="#" + Convert.ToString(dt.Rows[0].ItemArray[0]);
                lblHoraEntrada.Text = Convert.ToString(dt.Rows[0].ItemArray[1]) + " " + Convert.ToString(dt.Rows[0].ItemArray[2]);
                
            }
            else
            {
                MessageBox.Show("Houve uma falha ao carregar o ticket! \nRealize a pesquisa e tente novamente!","Falha ao carregar Ticket!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
