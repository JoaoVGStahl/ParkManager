using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Teste
{
    public partial class FrmTelaEncerrarTicket : Form
    {
        Banco banco = new Banco();
        DateTime DataFormatada;
        FrmTelaOperacao Form;
        TimeSpan ts;
        public FrmTelaEncerrarTicket()
        {
            InitializeComponent();
            this.Form = (FrmTelaOperacao)Application.OpenForms["FrmTelaOperacao"];
        }

        private void FrmTelaEncerrarTicket_Load(object sender, EventArgs e)
        {
            cmbFormaPagamento.SelectedIndexChanged -= cmbFormaPagamento_SelectedIndexChanged;
            CarregarComboFormaPagamento();
            CarregarTicket();
            cmbFormaPagamento.SelectedIndexChanged += cmbFormaPagamento_SelectedIndexChanged;
        }
        private void CarregarComboFormaPagamento()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = banco.ExecuteProcedureReturnDataTable(NameProcedure: "dbo.Metodos_Pagamento");
                cmbFormaPagamento.DataSource = null;
                cmbFormaPagamento.DataSource = dt;
                cmbFormaPagamento.ValueMember = "id_pgt";
                cmbFormaPagamento.DisplayMember = "descricao";
                cmbFormaPagamento.SelectedItem = null;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Houve uma falha ao carregar os Métodos de pagamento!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CarregarTicket()
        {
            DateTime DataEntrada;
            DateTime HoraEntrada;

            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@IdTicket", SqlDbType = SqlDbType.Int, Value = Globais.IdTicket}
                };
                dt = banco.ExecuteProcedureReturnDataTable(NameProcedure: "dbo.Carregar_Tela_Encerrar", sp: sp);
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        lblIdTicket.Text = "#" + dt.Rows[0]["#Ticket"].ToString();//ID Ticket

                        DataEntrada = Convert.ToDateTime(dt.Rows[0]["Data Entrada"].ToString());
                        HoraEntrada = Convert.ToDateTime(dt.Rows[0]["Hora Entrada"].ToString());
                        lblHoraEntrada.Text = HoraEntrada.ToLongTimeString() + " " + DataEntrada.ToShortDateString();
                        DataFormatada = Convert.ToDateTime(DataEntrada.ToString("dd/MM/yyyy") + " " + HoraEntrada.ToString("HH:mm:ss"));

                        Ticket.idTicket = Convert.ToInt32(dt.Rows[0]["#Ticket"]);
                        Ticket.hora_entrada = DataFormatada.ToString();
                        Ticket.Usuario_entrada = dt.Rows[0]["UsuarioEntrada"].ToString();
                        Ticket.placa = dt.Rows[0]["Placa"].ToString();
                        Ticket.tipo = dt.Rows[0]["Tipo"].ToString();
                        Ticket.marca = dt.Rows[0]["Marca"].ToString();
                        Ticket.cliente = dt.Rows[0]["Nome Cliente"].ToString();
                        Ticket.telefone = dt.Rows[0]["Telefone"].ToString();
                        timer1.Enabled = true;
                        CalcularPreco(DataFormatada);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "Erro ao carregar ticket!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("Ticket já encerrado ou inexistente!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Houve uma falha ao carregar o ticket! \nRealize a pesquisa e tente novamente!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
            finally
            {
                dt.Dispose();
            }
        }
        private void CalcularPreco(DateTime DataEntrada)
        {
            DateTime DataSaida = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lblHoraSaida.Text = DataSaida.ToString("HH:mm:ss dd/MM/yyyy");
            if (DataSaida > DataEntrada)
            {
                ts = DataSaida - DataEntrada;
            }
            else
            {
                ts = DataEntrada - DataSaida;
            }

            decimal Total = 0, Valor = Estacionamento.valor_hr, ValorMinimo = Estacionamento.valor_min, ValorUnico = Estacionamento.valor_unico;
            int dias = ts.Days;
            int horas = ts.Hours;
            int minutos = ts.Minutes;
            int tolerancia = Estacionamento.tolerancia.Minutes;
            if (!Globais.ModoUnico)
            {
                if (dias > 0)
                {
                    horas += (dias * 24);
                }
                if (horas > 0)
                {
                    if (minutos > tolerancia)
                    {
                        horas += 1;
                        Total = horas * Valor;
                    }
                    else
                    {
                        Total = horas * Valor;
                    }
                }
                else if (minutos > tolerancia)
                {
                    horas += 1;
                    Total = horas * Valor;
                }
                else
                {
                    Total += ValorMinimo;
                }
            }
            else
            {
                Total += ValorUnico;
            }
            txtTotal.Text = Total.ToString("N2");
            PreencherLabelTempoPermanencia(ts);

        }
        private void PreencherLabelTempoPermanencia(TimeSpan ts)
        {
            lblPermanencia.Text = "";
            int dias = ts.Days;
            int horas = ts.Hours;
            int minutos = ts.Minutes;
            int segundos = ts.Seconds;
            if (dias > 0)
            {
                if (dias < 10)
                    lblPermanencia.Text += "0" + ts.Days.ToString() + ":";
                else
                    lblPermanencia.Text += ts.Days.ToString() + ":";
            }
            if (horas < 10)
                lblPermanencia.Text += "0" + ts.Hours.ToString() + ":";
            else
                lblPermanencia.Text += ts.Hours.ToString() + ":";

            if (minutos < 10)
                lblPermanencia.Text += "0" + ts.Minutes.ToString() + ":";
            else
                lblPermanencia.Text += ts.Minutes.ToString() + ":";

            if (segundos < 10)
                lblPermanencia.Text += "0" + ts.Seconds.ToString();
            else
                lblPermanencia.Text += ts.Seconds.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form.ContadorTicket();
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CalcularPreco(DataFormatada);
        }

        private void lblHoraSaida_Click(object sender, EventArgs e)
        {

        }

        private void cmbFormaPagamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal Valor = Convert.ToDecimal(txtTotal.Text);
            if (cmbFormaPagamento.Text == "Dinheiro")
            {
                txtRecebido.Focus();
                txtRecebido.ReadOnly = false;
            }
            else
            {
                txtRecebido.ReadOnly = true;
                txtRecebido.Text = Valor.ToString();
                txtTroco.Text = "0";
            }
        }

        private void txtRecebido_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((e.KeyChar < '0' || e.KeyChar > '9') &&
               (e.KeyChar != ',' && e.KeyChar != '.' &&
                e.KeyChar != (Char)13 && e.KeyChar != (Char)8))
            {
                e.KeyChar = (Char)0;
            }
            else
            {
                if (e.KeyChar == '.' || e.KeyChar == ',')
                {
                    if (!txtRecebido.Text.Contains(','))
                        e.KeyChar = ',';
                    else
                        e.KeyChar = (Char)0;
                }
            }
        }
        private void txtRecebido_TextChanged(object sender, EventArgs e)
        {
            decimal troco;
            if (txtRecebido.TextLength >= 1)
            {
                decimal Total = Convert.ToDecimal(txtTotal.Text), Recebido = Convert.ToDecimal(txtRecebido.Text);
                if (Recebido > Total)
                {
                    troco = Recebido - Total;
                    txtTroco.Text = Convert.ToDecimal(troco).ToString("N2");
                }
                else
                {
                    troco = Recebido - Total;
                    txtTroco.Text = Convert.ToDecimal(troco).ToString("N2");
                }
            }
            else
            {
                troco = 0;
                txtTroco.Text = Convert.ToDecimal(troco).ToString("N2");
            }

        }

        private void txtRecebido_Leave(object sender, EventArgs e)
        {
            if (txtRecebido.TextLength >= 1)
            {
                txtRecebido.Text = Convert.ToDecimal(txtRecebido.Text).ToString("N2");
            }

        }

        private void btnEncerrar_Click(object sender, EventArgs e)
        {
            decimal total = Convert.ToDecimal(txtTotal.Text), troco = Convert.ToDecimal(txtTroco.Text);
            if (troco < 0)
                MessageBox.Show("Não é possivel encerrar este ticket porque está faltando parte do pagamento!", "Falha ao encerrar ticket!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                EncerrarTicket(); 
        }
        private void EncerrarTicket()
        {
            string DataSaida = DateTime.Now.ToShortDateString(), HoraSaida = DateTime.Now.ToLongTimeString();
            
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@IdTicket", SqlDbType = SqlDbType.Int, Value = Ticket.idTicket},
                    new SqlParameter(){ParameterName = "@IdUsuario", SqlDbType = SqlDbType.Int, Value = Globais.IdUsuario},
                    new SqlParameter(){ParameterName = "@HoraSaida", SqlDbType = SqlDbType.Time, Value = HoraSaida},
                    new SqlParameter(){ParameterName = "@DataSaida", SqlDbType = SqlDbType.DateTime, Value = DataSaida},
                    new SqlParameter(){ParameterName = "@Forma_Pgt", SqlDbType = SqlDbType.VarChar, Value = cmbFormaPagamento.Text},
                    new SqlParameter(){ParameterName = "@Total", SqlDbType = SqlDbType.Decimal, Value = txtTotal.Text},
                    new SqlParameter(){ParameterName = "@Troco", SqlDbType = SqlDbType.Decimal, Value = txtTroco.Text},
                    new SqlParameter(){ParameterName = "@Permanencia", SqlDbType = SqlDbType.VarChar, Value = ts.ToString()}
                };
                int result = banco.ExecuteProcedureWithReturnValue("dbo.Encerrar_Ticket", sp);
                if(result > 0)
                {
                    Ticket.usuario_saida = Globais.Login;
                    Ticket.forma_pgt = cmbFormaPagamento.Text;
                    Ticket.total = Convert.ToDecimal(txtTotal.Text);
                    Ticket.troco = Convert.ToDecimal(txtTroco.Text);
                    Ticket.permanencia = ts.ToString();
                    Ticket.hr_saida = HoraSaida + " " + DataSaida;
                    MessageBox.Show("Ticket #" + result + " encerrado com sucesso!", "Ticket encerrado!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if(Properties.Settings.Default.GerarPDF)
                    {
                        Imprimir();
                    }
                    if (Properties.Settings.Default.Cancelas)
                    {
                        Arduino.WriteCom("S");
                    }
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Ticket não encerrado!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Imprimir()
        {
            GeraPDF gerador = new GeraPDF();
            gerador.filename = "Ticket_Saida_" + Ticket.idTicket.ToString() + "_" + Ticket.placa + ".pdf";
            string caminho = gerador.TicketSaida();
            if(caminho != null)
            {
                System.Diagnostics.Process.Start(caminho);
            }
            

        }
    }
}
