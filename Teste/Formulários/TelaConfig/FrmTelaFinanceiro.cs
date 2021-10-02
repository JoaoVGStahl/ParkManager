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
    public partial class FrmTelaFinanceiro : Form
    {
        Banco banco = new Banco();
        public FrmTelaFinanceiro()
        {
            InitializeComponent();
        }

        private void FrmTelaFinanceiro_Load(object sender, EventArgs e)
        {
            CarregarValores();
            if (Convert.ToBoolean(Properties.Settings.Default["ModoUnico"]))
            {
                AtivarModoUnico();
            }
            else
            {
                DesativarModoUnico();
            }
        }
        private void CarregarValores()
        {
            try
            {
                DataTable dt = new DataTable();
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@Flag", SqlDbType = SqlDbType.Int, Value = 14}
                };
                dt = banco.InsertData("dbo.Funcoes_Pesquisa", sp);
                if (dt.Rows.Count > 0)
                {
                    decimal PrecoHora, PrecoMin, PrecoUnico;
                    PrecoHora = Convert.ToDecimal(dt.Rows[0].ItemArray[0]);
                    PrecoMin = Convert.ToDecimal(dt.Rows[0].ItemArray[1]);
                    PrecoUnico = Convert.ToDecimal(dt.Rows[0].ItemArray[2]);
                    lblPrecoHora.Text = PrecoHora.ToString("C");
                    lblPrecoMin.Text = PrecoMin.ToString("C");
                    lblPrecoUnico.Text = PrecoUnico.ToString("C");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtPrecoHora_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrecoHora_KeyPress(object sender, KeyPressEventArgs e)
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
                    if (!txtPrecoHora.Text.Contains(','))
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

        private void txtCobrancaMinima_KeyPress(object sender, KeyPressEventArgs e)
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
                    if (!txtCobrancaMinima.Text.Contains(','))
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

        private void txtValorUnico_KeyPress(object sender, KeyPressEventArgs e)
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
                    if (!txtValorUnico.Text.Contains(','))
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

        private void txtPrecoHora_Leave(object sender, EventArgs e)
        {
            if(txtPrecoHora.Text.Length >= 1)
            {
                txtPrecoHora.Text = Convert.ToDecimal(txtPrecoHora.Text).ToString("N2");
            }
        }

        private void txtCobrancaMinima_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCobrancaMinima_Leave(object sender, EventArgs e)
        {
            if(txtCobrancaMinima.Text.Length >= 1)
            {
                txtCobrancaMinima.Text = Convert.ToDecimal(txtCobrancaMinima.Text).ToString("N2");
            }
        }

        private void txtValorUnico_Leave(object sender, EventArgs e)
        {
            if(txtValorUnico.Text.Length >= 1)
            {
                txtValorUnico.Text = Convert.ToDecimal(txtValorUnico.Text).ToString("N2");
            }
        }

        private void ckValorUnico_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void ckValorUnico_CheckStateChanged(object sender, EventArgs e)
        {
            if (ckValorUnico.Checked)
            {
                AtivarModoUnico();
            }
            else
            {
                DesativarModoUnico();
            }
            
        }
        private void AtivarModoUnico()
        {
            ckValorUnico.ForeColor = Color.White;
            ckValorUnico.FlatAppearance.MouseOverBackColor = Color.DarkBlue;
            ckValorUnico.BackColor = Color.DarkBlue;
            ckValorUnico.Text = "Ativado";
            Properties.Settings.Default["ModoUnico"] = true;
            Globais.ModoUnico = true;
            Properties.Settings.Default.Save();
        }
        private void DesativarModoUnico()
        {
            ckValorUnico.ForeColor = Color.Black;
            ckValorUnico.Text = "Desativado";
            ckValorUnico.FlatAppearance.MouseOverBackColor = Color.Silver;
            ckValorUnico.BackColor = Color.DarkGray;
            Properties.Settings.Default["ModoUnico"] = false;
            Globais.ModoUnico = false;
            Properties.Settings.Default.Save();
            
        }

        private void FrmTelaFinanceiro_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
 
        }
    }
}
