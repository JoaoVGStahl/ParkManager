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
            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;
            cmbStatus.SelectedIndex = 1;
            dtpEntrada.Value = DateTime.Today.AddDays(-7);
            btnPesquisa.PerformClick();
            //PreencherGrid();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            

        }
        private void button3_Click(object sender, EventArgs e)
        {
            Globais.IdTicket = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
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
            if(cmbStatus.SelectedIndex == 1)
            {
                dtpSaida.Enabled = false;
            }
            else
            {
                dtpSaida.Enabled = true;
                btnEncerrar.Enabled = false;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Caracteres permitidos
            string caracterespermitidos = "ABCDEFGHIJ0123456789";
            //Apenas Letras E BackSpace nos 3 primeiros digitos
            if (txtPlaca.TextLength < 3)
            {
                if (!char.IsLetter(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
                {
                    e.Handled = true;
                }
            }
            //Apenas Números E BackSpace no 4º Digito
            if (txtPlaca.TextLength == 3)
            {
                if (!char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
                {
                    e.Handled = true;
                }
            }
            // Apenas letras de A-J e 0-9 e BackSpace no 5º Digito
            if (txtPlaca.TextLength == 4)
            {
                if (!(caracterespermitidos.Contains(e.KeyChar.ToString().ToUpper())) && !(e.KeyChar == (char)Keys.Back))
                {
                    e.Handled = true;
                }
            }
            //Apenas Números e BackSpace nos 6º e 7º Digitos
            if (txtPlaca.TextLength > 4)
            {
                if (!char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
                {
                    e.Handled = true;
                }
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
            btnEncerrar.Enabled = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (cmbStatus.SelectedIndex == 1)
            {
                btnEncerrar.Enabled = true;
            }
            else
            {
                btnEncerrar.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string DataEntrada = dtpEntrada.Value.ToString("dd/MM/yyyy");
            string DataSaida = dtpSaida.Value.ToString("dd/MM/yyyy");
            int idticket =0;
            if (txtIdTicket.Text != "")
            {
                idticket = Convert.ToInt32(txtIdTicket.Text);
            }

            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName="@Flag", SqlDbType = SqlDbType.Int, Value = 18},
                new SqlParameter(){ParameterName="@idTicket", SqlDbType = SqlDbType.Int, Value = idticket},
                new SqlParameter(){ParameterName="@Placa", SqlDbType = SqlDbType.VarChar, Value = txtPlaca.Text},
                new SqlParameter(){ParameterName="@DataEntrada", SqlDbType = SqlDbType.DateTime, Value = DataEntrada},
                new SqlParameter(){ParameterName="@DataSaida", SqlDbType = SqlDbType.DateTime, Value = DataSaida},
                new SqlParameter(){ParameterName = "@Status", SqlDbType = SqlDbType.Int, Value = cmbStatus.SelectedIndex}
            };
                dt = banco.InsertData("dbo.Funcoes_Pesquisa", sp);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erro!");
            }

        }
    }
}
