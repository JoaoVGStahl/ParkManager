using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ParkManager
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
            txtId.Text = Properties.Settings.Default.IDEstacionamento.ToString();
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
                    new SqlParameter(){ParameterName="@IdEstacionamento", SqlDbType = SqlDbType.Int, Value = txtId.Text}
                };
                dt = banco.ExecuteProcedureReturnDataTable("dbo.CarregarValores", sp);
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
                btnEditar.Enabled = false;
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
            SalvarValores();
            DesativarCaixas();
        }
        private void SalvarValores()
        {
            try
            {
                int LinhasAfetadas;
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@Id_Estacionamento", SqlDbType = SqlDbType.Int, Value =  txtId.Text},
                    new SqlParameter(){ParameterName="@Tolerancia", SqlDbType = SqlDbType.Time, Value = "00:" + tbTolerancia.Value + ":00"}
                };
                if(txtPrecoHora.Text != "")
                {
                    sp.Add(new SqlParameter() { ParameterName = "@Valor_Hora", SqlDbType = SqlDbType.Decimal, Value = Convert.ToDecimal(txtPrecoHora.Text) });
                }
                if(txtCobrancaMinima.Text != "")
                {
                    sp.Add(new SqlParameter() { ParameterName = "@Valor_Minimo", SqlDbType = SqlDbType.Decimal, Value = Convert.ToDecimal(txtCobrancaMinima.Text) });
                }
                if(txtValorUnico.Text != "")
                {
                    sp.Add(new SqlParameter() { ParameterName = "@Valor_Unico", SqlDbType = SqlDbType.Decimal, Value = Convert.ToDecimal(txtValorUnico.Text) });
                }
                LinhasAfetadas = banco.ExecuteProcedureReturnInt("dbo.Salvar_Valor", sp);
                if (LinhasAfetadas > 0)
                {
                    try
                    {
                        Registrar();
                        AttModoUnico();
                        CarregarValores();
                        DesativarCaixas();
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
        private void Registrar()
        {
            try
            {
                if (txtPrecoHora.Text != "" && Estacionamento.valor_hr != Convert.ToDecimal(txtPrecoHora.Text))
                {
                    Globais.RegistrarLog(Globais.Login + " Alterou o Preço da Hora de: " + Estacionamento.valor_hr.ToString() + " para: " + txtPrecoHora.Text);
                    Estacionamento.valor_hr = Convert.ToDecimal(txtPrecoHora.Text);
                }
                if (txtCobrancaMinima.Text != "" && Estacionamento.valor_min != Convert.ToDecimal(txtCobrancaMinima.Text))
                {
                    Globais.RegistrarLog(Globais.Login + " Alterou Cobrança Mínima de: " + Estacionamento.valor_min.ToString() + " para: " + txtCobrancaMinima.Text);
                    Estacionamento.valor_min = Convert.ToDecimal(txtCobrancaMinima.Text);
                }
                if (txtValorUnico.Text!= "" && Estacionamento.valor_unico != Convert.ToDecimal(txtValorUnico.Text))
                {
                    Globais.RegistrarLog(Globais.Login + " Valor único de: " + Estacionamento.valor_unico.ToString() + " para: " + txtValorUnico.Text);
                    Estacionamento.valor_unico = Convert.ToDecimal(txtValorUnico.Text);
                }
                if (!(Properties.Settings.Default.ModoUnico.Equals(ckValorUnico.Checked)))
                {
                    if (ckValorUnico.Checked)
                    {
                        Globais.RegistrarLog(Globais.Login + " Ativou o Modo Unico");
                        Properties.Settings.Default.ModoUnico = true;
                        Globais.ModoUnico = true;
                    }
                    else
                    {
                        Globais.RegistrarLog(Globais.Login + " Desativou Modo Unico");
                        Properties.Settings.Default.ModoUnico = false;
                        Globais.ModoUnico = false;
                    }

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
