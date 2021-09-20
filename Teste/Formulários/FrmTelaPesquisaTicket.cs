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
            try
            {
                dt = banco.ProcedureSemParametros(6);
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
            Frm.ShowDialog();
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
