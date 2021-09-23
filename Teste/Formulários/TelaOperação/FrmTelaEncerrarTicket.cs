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

namespace Teste
{
    public partial class FrmTelaEncerrarTicket : Form
    {
        Banco banco = new Banco();
        DateTime DataFormatada;
        public FrmTelaEncerrarTicket()
        {
            InitializeComponent();
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
                dt = banco.ProcedureSemParametros(4);
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
                dt = banco.ProcedureCarregarTicket(3, Globais.IdTicket);
                if (dt.Rows.Count > 0)
                {

                    lblIdTicket.Text = "#" + Convert.ToString(dt.Rows[0].ItemArray[0]);//ID Ticket

                    DataEntrada = Convert.ToDateTime(dt.Rows[0].ItemArray[2]);
                    HoraEntrada = Convert.ToDateTime(dt.Rows[0].ItemArray[1]);
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
            lblPermanencia.Text = Convert.ToString(ts);
            double Valor = 0;
            int horas = ts.Hours;
            int minutos = ts.Minutes;
            if (horas > 0)
            {
                if (minutos < 16)
                {
                    Valor = horas * 6;
                }
                else
                {
                    horas += 1;
                    Valor = horas * 6;
                }
            }
            else if (minutos >= 16)
            {
                Valor = horas * 6;
            }
            else
            {
                Valor = 0;
            }
            txtTotal.Text = Convert.ToString(Valor);

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
            if(cmbFormaPagamento.Text == "Dinheiro")
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
            double troco;
            if(txtRecebido.TextLength >= 1)
            {
                double Total = Convert.ToDouble(txtTotal.Text), Recebido = Convert.ToDouble(txtRecebido.Text);
                if(Recebido > Total)
                {
                    troco = Recebido - Total;
                    txtTroco.Text = Convert.ToDouble(troco).ToString("N2");
                }
                else
                {
                    troco = 0;
                    txtTroco.Text = Convert.ToDouble(troco).ToString("N2");
                }
            }
            else
            {
                troco = 0;
                txtTroco.Text = Convert.ToDouble(troco).ToString("N2");
            }
            
        }

        private void txtRecebido_Leave(object sender, EventArgs e)
        {
            if(txtRecebido.TextLength >= 1)
            {
                txtRecebido.Text = Convert.ToDouble(txtRecebido.Text).ToString("N2");
            }
            
        }
    }
}
