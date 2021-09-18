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
    public partial class FrmTelaPesquisaTicket : Form
    {
        Banco banco = new Banco();
        public FrmTelaPesquisaTicket()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmTelaTicket_Load(object sender, EventArgs e)
        {
            PreencherGrid();

        }
        private void PreencherGrid()
        {
            DataTable dt = new DataTable();
            string query = @"
            SELECT 
                Ticket.id_ticket[#Ticket],CONVERT(varchar,Entrada.data_entrada,103) AS[Data Entrada],CONVERT(varchar, Entrada.hr_entrada,8)AS [Hora Entrada], Usuario.login[Usuario Entrada], Car.placa[Placa], Car.tipo[Tipo], Car.marca[Marca], Cli.nome[Nome], Cli.Telefone[Telefone],Ticket.status[Status Ticket]
            FROM
                tb_ticket AS Ticket
            INNER JOIN 
                tb_entrada AS Entrada 
            ON  
	            Entrada.ticket_id = Ticket.id_ticket
            INNER JOIN 
                tb_usuario AS Usuario
            ON
	            Entrada.usuario_id = Usuario.id_usuario
            INNER JOIN 
                tb_carro AS Car
            ON 
	            Ticket.carro_id = Car.id_carro
            INNER JOIN 
                tb_cliente AS Cli
            ON 
	            Car.cliente_id = Cli.id_cliente 
            WHERE Ticket.status=1";
            try
            {
                dt = banco.QueryBancoSql(query);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Falha ao carregar as informações!",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }


        }
        private void button3_Click(object sender, EventArgs e)
        {
            FrmTelaEncerrarTicket Frm = new FrmTelaEncerrarTicket();
            Frm.Show();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
