using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO.Ports;  // necessário para ter acesso as portas
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Teste
{
    public partial class FrmTelaDesenvolvedor : Form
    {
        Banco banco = new Banco();
        public FrmTelaDesenvolvedor()
        {
            InitializeComponent();
        }

        //Novo
        private void atualizaListaCOMs()
        {
            int i;
            bool quantDiferente;    //flag para sinalizar que a quantidade de portas mudou

            i = 0;
            quantDiferente = false;

            //se a quantidade de portas mudou
            if (cbPortaArduino.Items.Count == SerialPort.GetPortNames().Length)
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    if (cbPortaArduino.Items[i++].Equals(s) == false)
                    {
                        quantDiferente = true;
                    }
                }
            }
            else
            {
                quantDiferente = true;
            }

            //Se não foi detectado diferença
            if (quantDiferente == false)
            {
                return;                     //retorna
            }

            //limpa comboBox
            cbPortaArduino.Items.Clear();

            //adiciona todas as COM diponíveis na lista
            foreach (string s in SerialPort.GetPortNames())
            {
                cbPortaArduino.Items.Add(s);
            }
            //seleciona a primeira posição da lista
            //cbPortaArduino.SelectedIndex = 0;
        }

        //Novo
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                try
                {
                    serialPort1.PortName = cbPortaArduino.Items[cbPortaArduino.SelectedIndex].ToString();
                    serialPort1.Open();
                   // Arduino.PortaCOM = cbPortaArduino.Items[cbPortaArduino.SelectedIndex].ToString();

                }
                catch
                {
                    return;

                }
                if (serialPort1.IsOpen)
                {
                    btConectar.Text = "Desconectar";
                    cbPortaArduino.Enabled = false;

                }
            }
            else
            {

                try
                {
                    serialPort1.Close();
                    cbPortaArduino.Enabled = true;
                    btConectar.Text = "Conectar";
                }
                catch
                {
                    return;
                }

            }
        }

        /* Para Fechar Porta COM        
        if(serialPort1.IsOpen == true)
        {
         serialPort1.Close();
        }
        */



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
                    dt = banco.ExecuteProcedureReturnDataTable("dbo.Pesquisa_TelaDesenvolvedor");
                    if (dt.Rows.Count > 0)
                    {
                        txtID.Text = dt.Rows[0]["id"].ToString();
                        txtCaminho.Text = dt.Rows[0]["Caminho Log"].ToString();
                        cbPortaArduino.Text = dt.Rows[0]["Porta Arduino"].ToString();
                        string Connection = dt.Rows[0]["String Conexão"].ToString();
                        var split = Connection.Split(new string[] { "Server=", "Database=", "User Id=", "Password=", ";" }, StringSplitOptions.RemoveEmptyEntries);
                        txtServidor.Text = split[0];
                        txtNomeBanco.Text = split[1];
                        txtUsuario.Text = split[2];
                        txtSenha.Text = split[3];
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
            if (txtID.Text != "")
            {
                SalvarBanco(StrConn, "Edit");
            }
            else if (txtID.Text == "")
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
                    new SqlParameter(){ParameterName = "@Porta_Arduino", SqlDbType = SqlDbType.NVarChar, Value = cbPortaArduino.Text},
                    new SqlParameter(){ParameterName = "@String_Conn", SqlDbType = SqlDbType.NVarChar, Value = StrConn}
                };
                if (method == "Edit")
                {
                    sp.Add(new SqlParameter() { ParameterName = "@Id_Estacionamento", SqlDbType = SqlDbType.Int, Value = txtID.Text });
                }
                result = banco.ExecuteProcedureReturnInt("dbo.Parametros", sp);
                if (result > 0)
                {
                    txtCaminho.Enabled = false;
                    cbPortaArduino.Enabled = false;
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
            cbPortaArduino.Enabled = true;
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

        private void timerCOM_Tick(object sender, EventArgs e)
        {
            atualizaListaCOMs();
        }

        private void cbPortaArduino_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }

        private void txtPortaArduino_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Write("A");

            }
            else {
                MessageBox.Show("nao conectado");
            }
        }
    }
}
