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
using System.Text.RegularExpressions;

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
                decimal PrecoHora, PrecoMin, PrecoUnico;
                DataTable dt = new DataTable();
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@Flag", SqlDbType = SqlDbType.Int, Value = 14}
                };
                dt = banco.InsertData("dbo.Funcoes_Pesquisa", sp);
                if (dt.Rows.Count > 0)
                {
                    txtId.Text = dt.Rows[0]["ID"].ToString();

                    if ((dt.Rows[0]["Valor Hora"].ToString() == null) && (dt.Rows[0]["Valor Minimo"].ToString() == null) && (dt.Rows[0]["Valor Unico"].ToString() == null))
                    {
                        AtivarCaixas();
                    }
                    else
                    {
                        if (dt.Rows[0]["Valor Hora"].ToString() != null)
                        {
                            PrecoHora = Convert.ToDecimal(dt.Rows[0]["Valor Hora"].ToString());
                            lblPrecoHora.Text = PrecoHora.ToString("C");
                        }
                        if (dt.Rows[0]["Valor Minimo"].ToString() != null)
                        {
                            PrecoMin = Convert.ToDecimal(dt.Rows[0]["Valor Minimo"].ToString());
                            lblPrecoMin.Text = PrecoMin.ToString("C");
                        }
                        if (dt.Rows[0]["Valor Unico"].ToString() != null)
                        {
                            PrecoUnico = Convert.ToDecimal(dt.Rows[0]["Valor Unico"].ToString());
                            lblPrecoUnico.Text = PrecoUnico.ToString("C");
                        }
                        if (dt.Rows[0]["Tolerancia"].ToString() != null)
                        {
                            TimeSpan ts = Convert.ToDateTime(dt.Rows[0]["Tolerancia"].ToString()) - Convert.ToDateTime("00:00:00");
                            lblTolerancia.Text = ts.Minutes + " Minutos";
                        }
                    }
                }
                else
                {
                    btnEditar.Enabled = false;
                    dt.Dispose();
                    MessageBox.Show("Cadastre seu estacionamento primeiro!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Falha carregar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            if (txtPrecoHora.Text.Length >= 1)
            {
                txtPrecoHora.Text = Convert.ToDecimal(txtPrecoHora.Text).ToString("N2");
            }
        }

        private void txtCobrancaMinima_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCobrancaMinima_Leave(object sender, EventArgs e)
        {
            if (txtCobrancaMinima.Text.Length >= 1)
            {
                txtCobrancaMinima.Text = Convert.ToDecimal(txtCobrancaMinima.Text).ToString("N2");
            }
        }

        private void txtValorUnico_Leave(object sender, EventArgs e)
        {
            if (txtValorUnico.Text.Length >= 1)
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
        }
        private void DesativarModoUnico()
        {
            ckValorUnico.ForeColor = Color.Black;
            ckValorUnico.Text = "Desativado";
            ckValorUnico.FlatAppearance.MouseOverBackColor = Color.Silver;
            ckValorUnico.BackColor = Color.DarkGray;
        }

        private void FrmTelaFinanceiro_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            AtivarCaixas();
            tbTolerancia.Value = Convert.ToInt32(lblTolerancia.Text.Replace(" Minutos", ""));
            lblCont.Text = tbTolerancia.Value.ToString();
        }
        private void AtivarCaixas()
        {
            txtPrecoHora.Enabled = true;
            txtCobrancaMinima.Enabled = true;
            txtValorUnico.Enabled = true;
            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
            ckValorUnico.Enabled = true;
            tbTolerancia.Enabled = true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            VerificarCaixas();
        }
        private void VerificarCaixas()
        {
            decimal PHora = 0, CMinima = 0, VUnico = 0;
            if (txtPrecoHora.Text != "")
            {
                if (Regex.IsMatch(txtPrecoHora.Text, @"^\d+,\d{2}"))
                {
                    PHora = Convert.ToDecimal(txtPrecoHora.Text);
                }
                else
                {
                    MessageBox.Show("Preço Hora Inválido!", "Falha carregar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                PHora = -1;
            }
            if (txtCobrancaMinima.Text != "")
            {
                if (Regex.IsMatch(txtCobrancaMinima.Text, @"^\d+,\d{2}"))
                {
                    CMinima = Convert.ToDecimal(txtCobrancaMinima.Text);
                }
                else
                {
                    MessageBox.Show("Cobrança Minima Inválido!", "Falha carregar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                CMinima = -1;
            }
            if (txtValorUnico.Text != "")
            {
                if (Regex.IsMatch(txtValorUnico.Text, @"^\d+,\d{2}"))
                {
                    VUnico = Convert.ToDecimal(txtValorUnico.Text);
                }
                else
                {
                    MessageBox.Show("Valor Unico Inválido!", "Falha carregar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                VUnico = -1;
            }
            if (PHora != 0 || CMinima != 0 || VUnico != 0 || tbTolerancia.Value != Convert.ToInt32(lblTolerancia.Text.Replace(" Minutos", "")))
            {
                SalvarValores(PHora, CMinima, VUnico);
            }
            else if (ckValorUnico.Checked != Convert.ToBoolean(Properties.Settings.Default["ModoUnico"]))
            {
                AttModoUnico();
                MessageBox.Show("Modo Unico" + ckValorUnico.Text + "!", "Alteração!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Não Há alterações a serem salvas!", "Falha carregar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DesativarCaixas();
        }
        private void SalvarValores(decimal PHora, decimal CMinimo, decimal VUnico)
        {
            try
            {
                int LinhasAfetadas;
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@Id_Estacionamento", SqlDbType = SqlDbType.Int, Value =  txtId.Text},
                    new SqlParameter(){ParameterName="@Valor_Hora", SqlDbType = SqlDbType.Decimal, Value = PHora},
                    new SqlParameter(){ParameterName="@Valor_Minimo", SqlDbType = SqlDbType.Decimal, Value = CMinimo},
                    new SqlParameter(){ParameterName="@Valor_Unico", SqlDbType = SqlDbType.Decimal, Value = VUnico},
                    new SqlParameter(){ParameterName="@Tolerancia", SqlDbType = SqlDbType.Time, Value = "00:" + tbTolerancia.Value + ":00"}
                };
                LinhasAfetadas = banco.EditData("dbo.Salvar_Valor", sp);
                if (LinhasAfetadas > 0)
                {
                    try
                    {
                        AttModoUnico();
                        CarregarValores();
                        DesativarCaixas();
                        Globais.RegistrarLog(Globais.Login + " Alterou os Valores.");
                        MessageBox.Show("Alterações Salvas com sucesso!", "Salvamento concluido!", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Falha ao Salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Alterações não Salvas!", "Falha ao Salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Falha ao Salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AttModoUnico()
        {
            if (ckValorUnico.Checked)
            {
                Properties.Settings.Default["ModoUnico"] = true;
                Globais.ModoUnico = true;
                Properties.Settings.Default.Save();
            }
            else if (!ckValorUnico.Checked)
            {
                Properties.Settings.Default["ModoUnico"] = false;
                Globais.ModoUnico = false;
                Properties.Settings.Default.Save();
            }
            Globais.RegistrarLog(Globais.Login + " Atualizou Modo Unico para " + ckValorUnico.Text + ".");
        }
        private void DesativarCaixas()
        {
            txtPrecoHora.Enabled = false;
            txtCobrancaMinima.Enabled = false;
            txtValorUnico.Enabled = false;
            ckValorUnico.Enabled = false;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = true;
            tbTolerancia.Value = 1;
            lblCont.Text = tbTolerancia.Value.ToString();
        }

        private void splitter3_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            lblCont.Text = tbTolerancia.Value.ToString();

        }
    }
}
