using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Teste
{
    public partial class FrmTelaEstacionamento : Form
    {
        Banco banco = new Banco();
        public FrmTelaEstacionamento()
        {
            InitializeComponent();
        }

        private void FrmTelaEstacionamento_Load(object sender, EventArgs e)
        {
            panel4.VerticalScroll.Value = 0;
            CarregarIdentificacao();
        }
        private void CarregarIdentificacao()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = @"IdEstacionamento", SqlDbType = SqlDbType.Int, Value = Properties.Settings.Default.IDEstacionamento }
                };
                dt = banco.ExecuteProcedureReturnDataTable("dbo.Info_Estacionamento", sp);
                if (dt.Rows.Count > 0)
                {
                    PreencherCampos(dt);
                }
                else
                {
                    AtivarCaixas();
                }
            }
            catch (Exception ex)
            {
                btnEditar.Enabled = false;
                MessageBox.Show(ex.Message, "Falha ao carregar a identificação!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }
        private void PreencherCampos(DataTable dt)
        {
            txtID.Text = dt.Rows[0]["ID"].ToString();
            txtRazaoSocial.Text = dt.Rows[0]["Razão Social"].ToString();
            mskCnpj.Text = dt.Rows[0]["CNPJ"].ToString();
            mskInscricao.Text = dt.Rows[0]["Inscrição Estadual"].ToString();
            mskTelefone.Text = dt.Rows[0]["Telefone"].ToString();
            mskCEP.Text = dt.Rows[0]["CEP"].ToString();
            txtNumero.Text = dt.Rows[0]["Número"].ToString();
            txtEndereco.Text = dt.Rows[0]["Endereço"].ToString();
            txtBairro.Text = dt.Rows[0]["Bairro"].ToString();
            txtCidade.Text = dt.Rows[0]["Cidade"].ToString();
            txtEstado.Text = dt.Rows[0]["Estado"].ToString();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            AtivarCaixas();
        }
        private void AtivarCaixas()
        {
            btnEditar.Enabled = false;
            btnSalvar.Enabled = true;
            txtRazaoSocial.Enabled = true;
            mskCnpj.Enabled = true;
            mskInscricao.Enabled = true;
            mskTelefone.Enabled = true;
            mskCEP.Enabled = true;
            txtNumero.Enabled = true;
            txtEndereco.Enabled = true;
            txtBairro.Enabled = true;
            txtCidade.Enabled = true;
            txtEstado.Enabled = true;
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Apenas Números
            if (!char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void txtEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Apenas Letras
            if (!char.IsLetter(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void txtCidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Apenas Letras
            if (!char.IsLetter(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            VerificarCaixas();
        }
        private void VerificarCaixas()
        {
            // If que verifica se todas as caixas estão preenchidas
            if (txtRazaoSocial.Text == "" || mskCnpj.Text == "" || mskInscricao.Text == "" || mskTelefone.Text == "" || mskCEP.Text == "" || txtNumero.Text == "" || txtEndereco.Text == "" || txtBairro.Text == "" || txtCidade.Text == "" || txtEstado.Text == "")
            {
                MessageBox.Show("Preencha todos os campos!", "Falha ao Salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (Regex.IsMatch(mskCnpj.Text, @"^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$"))
                {
                    if (Regex.IsMatch(mskInscricao.Text, @"^\d{3}\.\d{3}\.\d{3}\.\d{3}"))
                    {
                        if (Regex.IsMatch(mskTelefone.Text, @"^\([11-99]{2}\)[0|9]\d{4}\-\d{4}"))
                        {
                            if (Regex.IsMatch(mskCEP.Text, @"^\d{5}\-\d{3}"))
                            {
                                if (Regex.IsMatch(txtNumero.Text, @"^\d+"))
                                {
                                    if (Regex.IsMatch(txtCidade.Text, @"^\D+"))
                                    {
                                        if (txtID.Text != "")
                                        {
                                            SalvarIdentificacao("Edit");
                                        }
                                        else if (txtID.Text == "")
                                        {
                                            SalvarIdentificacao("Save");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Cidade Inválida!", "Falha ao Salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Número Inválido!", "Falha ao Salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("CEP Inválido!", "Falha ao Salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Telefone Inválido!", "Falha ao Salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Inscrição Estadual Inválida!", "Falha ao Salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("CNPJ Inválido!", "Falha ao Salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void SalvarIdentificacao(string method)
        {
            try
            {
                int LinhasAfetadas;
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@Flag", SqlDbType = SqlDbType.Int, Value = 2},
                    new SqlParameter(){ParameterName = "@Cnpj", SqlDbType = SqlDbType.VarChar, Value = mskCnpj.Text},
                    new SqlParameter(){ParameterName = "@Razao_Social", SqlDbType = SqlDbType.VarChar, Value = txtRazaoSocial.Text},
                    new SqlParameter(){ParameterName = "@Endereco", SqlDbType = SqlDbType.VarChar, Value = txtEndereco.Text},
                    new SqlParameter(){ParameterName = "@Bairro", SqlDbType = SqlDbType.VarChar, Value = txtBairro.Text},
                    new SqlParameter(){ParameterName = "@Numero", SqlDbType = SqlDbType.Int, Value = txtNumero.Text},
                    new SqlParameter(){ParameterName = "@Cidade", SqlDbType = SqlDbType.VarChar, Value = txtCidade.Text},
                    new SqlParameter(){ParameterName = "@Estado", SqlDbType = SqlDbType.Char, Value = txtEstado.Text},
                    new SqlParameter(){ParameterName = "@Cep", SqlDbType = SqlDbType.Char, Value = mskCEP.Text},
                    new SqlParameter(){ParameterName = "@Inscricao_Estadual", SqlDbType = SqlDbType.VarChar, Value = mskInscricao.Text },
                    new SqlParameter(){ParameterName = "@Telefone", SqlDbType = SqlDbType.VarChar, Value = mskTelefone.Text}
                };
                if (method == "Edit")
                {
                    sp.Add(new SqlParameter() { ParameterName = "@Id_Estacionamento", SqlDbType = SqlDbType.Int, Value = txtID.Text });
                }
                LinhasAfetadas = banco.ExecuteProcedureReturnInt("dbo.Parametros", sp);
                if (LinhasAfetadas > 0)
                {
                    DesativarCaixas();
                    btnEditar.Enabled = true;
                    Registar();
                    MessageBox.Show("Alterações Salvas com Sucesso!", "Salvamento Concluído!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Falha ao Salvar as alterações!", "Falha ao Salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Falha ao Salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Registar()
        {
            if(Estacionamento.razao_social != txtRazaoSocial.Text)
            {
                Globais.RegistrarLog(Globais.Login + " Alterou de Razão Social de: "+ Estacionamento.razao_social + " para: " + txtRazaoSocial.Text + ".");
                Estacionamento.razao_social = txtRazaoSocial.Text;
            }
            if(Estacionamento.cnpj != mskCnpj.Text)
            {
                Globais.RegistrarLog(Globais.Login + " Alterou o CNPJ de: " + Estacionamento.cnpj + " para: " + mskCnpj.Text + ".");
                Estacionamento.cnpj = mskCnpj.Text;
            }
            if(Estacionamento.telefone != mskTelefone.Text)
            {
                Globais.RegistrarLog(Globais.Login + " Alterou Telefone de: " + Estacionamento.telefone + " para: " + mskTelefone.Text + ".");
                Estacionamento.telefone = mskTelefone.Text;
            }
            if(Estacionamento.cep != mskCEP.Text)
            {
                Globais.RegistrarLog(Globais.Login + " Alterou CEP de: " + Estacionamento.cep + " para: " + mskCEP.Text + ".");
                Estacionamento.cep = mskCEP.Text;
            }
            if(Estacionamento.numero.ToString() != txtNumero.Text)
            {
                Globais.RegistrarLog(Globais.Login + " Alterou Número de: " + Estacionamento.numero.ToString() + " para: " + txtNumero.Text + ".");
                Estacionamento.numero = Convert.ToInt32(txtNumero.Text);
            }
            if(Estacionamento.endereco != txtEndereco.Text)
            {
                Globais.RegistrarLog(Globais.Login + " Alterou Endereço de:" + Estacionamento.endereco + " para: " + txtEndereco.Text + ".");
                Estacionamento.endereco = txtEndereco.Text;
            }
            if (Estacionamento.bairro != txtBairro.Text)
            {
                Globais.RegistrarLog(Globais.Login + " Alterou Bairro de:" + Estacionamento.bairro + " para: " + txtBairro.Text + ".");
                Estacionamento.bairro = txtBairro.Text;
            }
            if (Estacionamento.cidade != txtCidade.Text)
            {
                Globais.RegistrarLog(Globais.Login + " Alterou Cidade de:" + Estacionamento.cidade + " para: " + txtCidade.Text + ".");
                Estacionamento.cidade = txtCidade.Text;
            }
            if (Estacionamento.estado != txtEstado.Text)
            {
                Globais.RegistrarLog(Globais.Login + " Alterou Endereço de:" + Estacionamento.estado + " para: " + txtEstado.Text + ".");
                Estacionamento.estado = txtEstado.Text;
            }
        }
        private void DesativarCaixas()
        {
            btnSalvar.Enabled = false;
            txtRazaoSocial.Enabled = false;
            mskCnpj.Enabled = false;
            mskInscricao.Enabled = false;
            mskTelefone.Enabled = false;
            mskCEP.Enabled = false;
            txtNumero.Enabled = false;
            txtEndereco.Enabled = false;
            txtBairro.Enabled = false;
            txtCidade.Enabled = false;
            txtEstado.Enabled = false;
        }

        private void txtRazaoSocial_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEndereco_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
