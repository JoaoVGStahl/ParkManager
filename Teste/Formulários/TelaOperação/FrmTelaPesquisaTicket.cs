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
        FrmTelaOperacao Form;
        GeraPDF gerador = new GeraPDF();
        public FrmTelaPesquisaTicket()
        {
            InitializeComponent();
            this.Form = (FrmTelaOperacao)Application.OpenForms["FrmTelaOperacao"];
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
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            if (!Properties.Settings.Default.GerarPDF)
            {
                btnImprimir.Enabled = false;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows[0].Cells["Status"].Value.ToString() == "Ativo")
            {
                dataGridView1.SelectedRows[0].Cells["Status"].Value.ToString();
                Globais.IdTicket = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                FrmTelaEncerrarTicket Frm = new FrmTelaEncerrarTicket();
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
            btnPesquisa.PerformClick();
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
            if (dataGridView1.SelectedRows.Count > 0)
            {
                btnImprimir.Enabled = true;
                if (cmbStatus.Text == "Ativo" || cmbStatus.Text == "Todos")
                {
                    btnEncerrar.Enabled = true;
                }
                else
                {
                    btnEncerrar.Enabled = false;
                }
            }
            else
            {
                btnImprimir.Enabled = false;
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
                Form.PesquisaTicket(dataGridView1.SelectedRows[0].Cells["Placa"].Value.ToString());
                this.Dispose();
            }
            else
            {
                Ticket.idTicket = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["#Ticket"].Value.ToString());
                Ticket.status = 0;
                Form.PesquisaTicketID();
                this.Dispose();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                VerificarStatus();
            }
            else
            {
                MessageBox.Show("Selecione um Ticket Primeiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void VerificarStatus()
        {
            string status = dataGridView1.SelectedRows[0].Cells["Status"].Value.ToString();
            int type = 0;
            if (status == "Ativo")
            {
                ImprimirTicketAberto();
            }
            if (status == "Encerrado")
            {
                type = 0;
                ImprimirTicketEncerrado(type);
            }
        }
        private void ImprimirTicketEncerrado(int status)
        {
            try
            {
                DataTable dt = new DataTable();
                var id = dataGridView1.SelectedRows[0].Cells["#Ticket"].Value.ToString();
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@idTicket", SqlDbType = SqlDbType.Int, Value= id},
                    new SqlParameter(){ParameterName="@Status", SqlDbType = SqlDbType.Int, Value = status}
                };
                dt = banco.ExecuteProcedureReturnDataTable("dbo.Select_Tela_Pesquisa", sp);
                if (dt.Rows.Count > 0)
                {
                    Ticket.idTicket = Convert.ToInt32(dt.Rows[0]["#Ticket"]);
                    Ticket.cliente = dt.Rows[0]["Nome Cliente"].ToString();
                    Ticket.placa = dt.Rows[0]["Placa"].ToString();
                    Ticket.marca = dt.Rows[0]["Marca"].ToString();
                    Ticket.tipo = dt.Rows[0]["Tipo"].ToString();
                    Ticket.usuario_saida = dt.Rows[0]["Usuario Saida"].ToString();
                    Ticket.forma_pgt = dt.Rows[0]["Forma Pagamento"].ToString();
                    Ticket.total = Convert.ToDecimal(dt.Rows[0]["Total"]);
                    Ticket.troco = Convert.ToDecimal(dt.Rows[0]["Troco"]);
                    Ticket.permanencia = dt.Rows[0]["Permanência"].ToString();
                    Ticket.hr_saida = dt.Rows[0]["Hora Saida"].ToString() + " " + dt.Rows[0]["Data Saida"].ToString();// Hora + Data

                    gerador.filename = "Ticket_Saida_" + Ticket.idTicket.ToString() + "_" + Ticket.placa + "_2_Via" + ".pdf";
                    string caminho = gerador.TicketSaida(true);
                    if (caminho != null)
                    {
                        System.Diagnostics.Process.Start(caminho);
                    }
                    Globais.RegistrarLog(Globais.Login + " Gerou 2ª Via de saida do #Ticket " + Ticket.idTicket);

                }
                else
                {
                    MessageBox.Show("Falha ao tentar carregar Ticket!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ImprimirTicketAberto()
        {
            try
            {
                DataTable dt = new DataTable();
                var id = dataGridView1.SelectedRows[0].Cells["#Ticket"].Value.ToString();
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@IdTicket", SqlDbType = SqlDbType.Int, Value = id}
                };
                dt = banco.ExecuteProcedureReturnDataTable(NameProcedure: "dbo.Carregar_Tela_Encerrar", sp: sp);
                if (dt.Rows.Count > 0)
                {
                    Ticket.idTicket = Convert.ToInt32(dt.Rows[0]["#Ticket"]);
                    Ticket.hora_entrada = dt.Rows[0]["Hora Entrada"].ToString() + " " + dt.Rows[0]["Data Entrada"].ToString();// Hora + Data
                    Ticket.Usuario_entrada = dt.Rows[0]["UsuarioEntrada"].ToString();
                    Ticket.placa = dt.Rows[0]["Placa"].ToString();
                    Ticket.tipo = dt.Rows[0]["Tipo"].ToString();
                    Ticket.marca = dt.Rows[0]["Marca"].ToString();
                    Ticket.cliente = dt.Rows[0]["Nome Cliente"].ToString();
                    gerador.filename = "Ticket_Entrada_" + Ticket.idTicket.ToString() + "_" + Ticket.placa + "_2_Via " + ".pdf";
                    string caminho = gerador.TicketEntrada(true);
                    if (caminho != null)
                    {
                        System.Diagnostics.Process.Start(caminho);
                    }
                    Globais.RegistrarLog(Globais.Login + " Gerou 2ª Via de entrada do #Ticket " + Ticket.idTicket);
                }
                else
                {
                    MessageBox.Show("Erro ao carregar Ticket!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

    }
}
