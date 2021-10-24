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
    public partial class FrmTelaDesenvolvedor : Form
    {
        Banco banco = new Banco();
        public FrmTelaDesenvolvedor()
        {
            InitializeComponent();
        }

        private void CarregarInformacoes()
        {
            txtSenhaRoot.Text = Properties.Settings.Default["SenhaRoot"].ToString();
            txtConfirmSenhaRoot.Text = Properties.Settings.Default["SenhaRoot"].ToString();
            string StringBanco = Properties.Settings.Default["StringBanco"].ToString();
            if (StringBanco == "")
            {
                btnSalvar.Enabled = true;
                btnEditar.Enabled = false;
                AtivarCaixas();
            }
            else
            {
                DataTable dt = new DataTable();
                try
                {
                    List<SqlParameter> sp = new List<SqlParameter>()
                        {
                        new SqlParameter(){ParameterName = "@Flag", SqlDbType = SqlDbType.Int, Value = 12}
                        };
                    dt = banco.InsertData("dbo.Funcoes_Pesquisa", sp);
                    if (dt.Rows.Count > 0)
                    {
                        txtID.Text = dt.Rows[0].ItemArray[0].ToString();
                        txtCaminho.Text = dt.Rows[0].ItemArray[1].ToString();
                        txtPortaArduino.Text = dt.Rows[0].ItemArray[2].ToString();
                        string Connection = dt.Rows[0].ItemArray[3].ToString();
                        var array = Connection.Split(new string[] { "Server=", "Database=", "User Id=", "Password=", ";" }, StringSplitOptions.RemoveEmptyEntries);
                        txtServidor.Text = array[0];
                        txtNomeBanco.Text = array[1];
                        txtUsuario.Text = array[2];
                        txtSenha.Text = array[3];
                    }
                    else
                    {
                        btnSalvar.Enabled = true;
                        btnEditar.Enabled = false;
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "\nString Conexão:" + Properties.Settings.Default.StringBanco, "Falha ao se conectar com banco de dados!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void SalvarSettings()
        {
            string servidor = txtServidor.Text;
            string nomebanco = txtNomeBanco.Text;
            string usuario = txtUsuario.Text;
            string senha = txtSenha.Text;
            string StrConn;

            StrConn = "Server=" + servidor + ";Database=" + nomebanco + ";User Id=" + usuario + ";Password=" + senha + ";";
            Properties.Settings.Default["StringBanco"] = StrConn;
            Properties.Settings.Default["ArquivoAuditoria"] = txtCaminho.Text;
            Properties.Settings.Default["SenhaRoot"] = txtConfirmSenhaRoot.Text;
            Properties.Settings.Default.Save();
            if(txtID.Text != "")
            {
                SalvarBanco(StrConn, "Edit");
            }
            else if(txtID.Text == "")
            {
                SalvarBanco(StrConn, "Save");
            }
            

        }
        private void SalvarBanco(string StrConn, string method)
        {
            try
            {
                int result;
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@Flag", SqlDbType = SqlDbType.Int, Value =1},
                    new SqlParameter(){ParameterName = "@Caminho", SqlDbType = SqlDbType.NVarChar, Value = txtCaminho.Text},
                    new SqlParameter(){ParameterName = "@Porta_Arduino", SqlDbType = SqlDbType.NVarChar, Value = txtPortaArduino.Text},
                    new SqlParameter(){ParameterName = "@String_Conn", SqlDbType = SqlDbType.NVarChar, Value = StrConn}
                };
                if(method == "Edit")
                {
                    sp.Add(new SqlParameter() { ParameterName = "@Id_Estacionamento", SqlDbType = SqlDbType.Int, Value = txtID.Text });
                }
                result = banco.EditData("dbo.Parametros", sp);
                if (result > 0)
                {
                    txtCaminho.Enabled = false;
                    txtPortaArduino.Enabled = false;
                    txtServidor.Enabled = false;
                    txtNomeBanco.Enabled = false;
                    txtUsuario.Enabled = false;
                    txtSenha.Enabled = false;
                    txtSenhaRoot.Enabled = false;
                    txtConfirmSenhaRoot.Enabled = false;
                    btnSalvar.Enabled = false;
                    btnSelecionar.Enabled = false;
                    MessageBox.Show("Parâmetros Editado com Sucesso!", "Configurações Salvas!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Falha ao Salvar!", "Configurações NÃO Salvas!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Configurações NÃO Salvas!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            AtivarCaixas();
        }
        private void AtivarCaixas()
        {
            txtCaminho.Enabled = true;
            txtPortaArduino.Enabled = true;
            txtServidor.Enabled = true;
            txtNomeBanco.Enabled = true;
            txtUsuario.Enabled = true;
            txtSenha.Enabled = true;
            txtSenhaRoot.Enabled = true;
            txtConfirmSenhaRoot.Enabled = true;
            btnSalvar.Enabled = true;
            btnSelecionar.Enabled = true;
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            ValidarCaixas();
            btnEditar.Enabled = true;
            btnSalvar.Enabled = false;
        }
        private void ValidarCaixas()
        {
            if (Regex.IsMatch(txtPortaArduino.Text, "^[A-Z]{3}[0-9]{1}"))
            {
                if (Regex.IsMatch(txtServidor.Text, @"^[\S]+$"))
                {
                    if (Regex.IsMatch(txtNomeBanco.Text, @"^[\S]+$"))
                    {
                        if (Regex.IsMatch(txtUsuario.Text, @"^[\S]+$"))
                        {
                            if (txtSenhaRoot.Text == txtConfirmSenhaRoot.Text)
                            {
                                SalvarSettings();
                            }
                            else
                            {
                                MessageBox.Show("As senhas root são diferentes!", "Configurações NÃO Salvas!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nome de Usuário Inválido!", "Configurações NÃO Salvas!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Banco de Dados Inválido!", "Configurações NÃO Salvas!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Servidor Inválido!", "Configurações NÃO Salvas!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Porta Arduino Inválida!", "Configurações NÃO Salvas!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmTelaDesenvolvedor_Load_1(object sender, EventArgs e)
        {
            CarregarInformacoes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtCaminho.Text = folderBrowserDialog1.SelectedPath;

            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSenhaRoot_TextChanged(object sender, EventArgs e)
        {
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void txtServidor_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtCaminho_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNomeBanco_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
