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
        DateTime DataFormatada;
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
                dt = banco.ProcedureCarregarTicket(3,Globais.IdTicket);
                if (dt.Rows.Count > 0)
                {

                    lblIdTicket.Text = "#" + Convert.ToString(dt.Rows[0].ItemArray[0]);//ID Ticket

                    DataEntrada = Convert.ToDateTime(dt.Rows[0].ItemArray[2]);
                    HoraEntrada = Convert.ToDateTime(dt.Rows[0].ItemArray[1]);
                    lblHoraEntrada.Text = HoraEntrada.ToLongTimeString() + " " + DataEntrada.ToShortDateString();
                    DataFormatada = Convert.ToDateTime(DataEntrada.ToString("dd/MM/yyyy") +" "+ HoraEntrada.ToString("HH:mm:ss"));
                    timer1.Enabled = true;
                    CalcularPreco(DataFormatada);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Houve uma falha ao carregar o ticket! \nRealize a pesquisa e tente novamente!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            else if(minutos >= 16)
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
    }
}
