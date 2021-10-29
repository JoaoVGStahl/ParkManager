using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;

namespace Teste
{
    public partial class FrmTelaEncerrarTicket : Form
    {
        Banco banco = new Banco();
        DateTime DataFormatada;
        FrmTelaOperacao form;
        public FrmTelaEncerrarTicket(FrmTelaOperacao form)
        {
            InitializeComponent();
            this.form = form;
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
                dt = banco.InsertData(NameProcedure: "dbo.Metodos_Pagamento");
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
                dt = banco.InsertData(NameProcedure: "dbo.Carregar_Tela_Encerrar", sp: sp);
                if (dt.Rows.Count > 0)
                {
                    lblIdTicket.Text = "#" + dt.Rows[0]["#Ticket"].ToString();//ID Ticket

                    DataEntrada = Convert.ToDateTime(dt.Rows[0]["Data Entrada"].ToString());
                    HoraEntrada = Convert.ToDateTime(dt.Rows[0]["Hora Entrada"].ToString());
                    lblHoraEntrada.Text = HoraEntrada.ToLongTimeString() + " " + DataEntrada.ToShortDateString();
                    DataFormatada = Convert.ToDateTime(DataEntrada.ToString("dd/MM/yyyy") + " " + HoraEntrada.ToString("HH:mm:ss"));
                    timer1.Enabled = true;
                    CalcularPreco(DataFormatada);
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
            TimeSpan ts;
            if (DataSaida > DataEntrada)
            {
                ts = DataSaida - DataEntrada;
            }
            else
            {
                ts = DataEntrada - DataSaida;
            }

            decimal Total = 0, Valor = Globais.ValorHora, ValorMinimo = Globais.ValorMinimo, ValorUnico = Globais.ValorUnico;
            int dias = ts.Days;
            int horas = ts.Hours;
            int minutos = ts.Minutes;
            int tolerancia = Globais.Tolerencia.Minutes;
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
                {
                    lblPermanencia.Text += "0" + ts.Days.ToString() + ":";
                }
                else
                {
                    lblPermanencia.Text += ts.Days.ToString() + ":";
                }

            }
            if (horas < 10)
            {
                lblPermanencia.Text += "0" + ts.Hours.ToString() + ":";
            }
            else
            {
                lblPermanencia.Text += ts.Hours.ToString() + ":";
            }
            if(minutos < 10)
            {
                lblPermanencia.Text += "0" + ts.Minutes.ToString() + ":";
            }
            else
            {
                lblPermanencia.Text += ts.Minutes.ToString() + ":";
            }
            if(segundos < 10)
            {
                lblPermanencia.Text += "0" + ts.Seconds.ToString();
            }
            else
            {
                lblPermanencia.Text += ts.Seconds.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form.ContadorTicket();
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
                    {
                        e.KeyChar = ',';
                    }
                    else
                    {
                        e.KeyChar = (Char)0;
                    }
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
            {
                MessageBox.Show("Não é possivel encerrar este ticket porque está faltando parte do pagamento!", "Falha ao encerrar ticket!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Ticket Encerrado com sucesso!", "Ticket Encerrardo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
