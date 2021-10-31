using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Teste
{
    public partial class FrmTelaPesquisaTicket : Form
    {
        Banco banco = new Banco();
        FrmTelaOperacao form;
        public FrmTelaPesquisaTicket(FrmTelaOperacao Form)
        {
            InitializeComponent();
            this.form = Form;
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
            if (dataGridView1.SelectedRows[0].Cells["Status"].Value.ToString() == "Ativo")
            {
                dataGridView1.SelectedRows[0].Cells["Status"].Value.ToString();
                Globais.IdTicket = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                FrmTelaEncerrarTicket Frm = new FrmTelaEncerrarTicket(form);
                Frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Este Ticket ja foi encerrado!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtIdTicket.Text.Length > 0)
            {
                txtPlaca.Enabled = false;
                txtPlaca.Clear();
                dtpEntrada.Enabled = false;
                dtpSaida.Enabled = false;
            }
            else
            {
                txtPlaca.Enabled = true;
                txtPlaca.Clear();
                dtpEntrada.Enabled = true;
                dtpSaida.Enabled = true;
                cmbStatus.Enabled = true;
            }
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
            if (cmbStatus.Text == "Ativo" || cmbStatus.Text == "Todos")
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
            DefinirParametros();
        }
        private void DefinirParametros()
        {
            if (txtIdTicket.Text == "")
            {
                PesquisarTicket(placa: txtPlaca.Text, DataEntrada: dtpEntrada.Value.ToString(), DataSaida: dtpSaida.Value.ToString(), status: cmbStatus.SelectedIndex.ToString());
            }
            else
            {
                PesquisarTicket(IdTicket: txtIdTicket.Text, status: cmbStatus.SelectedIndex.ToString());
            }
        }
        private void PesquisarTicket(string IdTicket = null, string placa = null, string DataEntrada = null, string DataSaida = null, string status = null)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@idTicket", SqlDbType = SqlDbType.Int, Value = IdTicket},
                    new SqlParameter(){ParameterName="@Placa", SqlDbType = SqlDbType.VarChar, Value = placa},
                    new SqlParameter(){ParameterName="@DataEntrada", SqlDbType = SqlDbType.DateTime, Value = DataEntrada},
                    new SqlParameter(){ParameterName="@DataSaida", SqlDbType = SqlDbType.DateTime, Value = DataSaida},
                    new SqlParameter(){ParameterName = "@Status", SqlDbType = SqlDbType.Int, Value = status}
                };
                dt = banco.ExecuteProcedureReturnDataTable("dbo.Select_Tela_Pesquisa", sp);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!");
            }
        }
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows[0].Cells["Status"].Value.ToString() == "Ativo")
            {
                form.PesquisaTicket(dataGridView1.SelectedRows[0].Cells["Placa"].Value.ToString());
                this.Dispose();
            }
        }
    }
}
