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
            timer1.Enabled = true;
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
            DateTime HoraSaida = DateTime.Now;
            lblHoraSaida.Text = HoraSaida.ToLongTimeString() + " " + HoraSaida.ToShortDateString();
            TimeSpan Permanencia;
            DateTime HoraEntrada;
            DataTable dt = new DataTable();
            try
            {
                dt = banco.ProcedureCarregarTicket(3,Globais.IdTicket);
                if (dt.Rows.Count > 0)
                {

                    lblIdTicket.Text = "#" + Convert.ToString(dt.Rows[0].ItemArray[0]);//ID Ticket
                    lblHoraEntrada.Text = Convert.ToString(dt.Rows[0].ItemArray[1]) + " " + Convert.ToString(dt.Rows[0].ItemArray[2]);//Hora - Data Entrada

                    HoraEntrada = Convert.ToDateTime(dt.Rows[0].ItemArray[2]);
                    HoraEntrada = Convert.ToDateTime(dt.Rows[0].ItemArray[1]);

                    Permanencia = HoraSaida - HoraEntrada;

                    lblPermanencia.Text = Permanencia.ToString(@"hh\:mm\:ss");
                    CalcularPreco(Permanencia);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Houve uma falha ao carregar o ticket! \nRealize a pesquisa e tente novamente!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
            
        }
        private void CalcularPreco(TimeSpan Tempo)
        {
            decimal Valor =0;
            int horas, minutos;

            horas = Convert.ToInt32(Tempo.TotalHours);
            minutos = Convert.ToInt32(Tempo.Minutes);
            if(horas > 0)
            {
                if(minutos > 0 && minutos < 16)
                {
                    Valor = horas * 6;
                }
                else
                {
                    horas += 1;
                    Valor = horas * 6;
                }
                
            }else if (minutos < 15)
            {
                Valor = 0;
            }
            else
            {
                Valor = 6;
            }
            txtTotal.Text = Valor.ToString();
           
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
            CarregarTicket();
        }

        private void lblHoraSaida_Click(object sender, EventArgs e)
        {

        }
    }
}
