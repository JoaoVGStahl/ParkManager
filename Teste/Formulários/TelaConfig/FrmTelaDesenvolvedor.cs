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
    public partial class FrmTelaDesenvolvedor : Form
    {
        Banco banco = new Banco();
        public FrmTelaDesenvolvedor()
        {
            InitializeComponent();
        }

        private void CarregarInformacoes()
        {
            string StringBanco = Properties.Settings.Default["StringBanco"].ToString();
            if ( StringBanco == "")
            {
                btnSalvar.Enabled = true;
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
                    if(dt.Rows.Count > 0)
                    {
                        txtCaminho.Text = dt.Rows[0].ItemArray[0].ToString();
                        txtPortaArduino.Text = dt.Rows[0].ItemArray[1].ToString();
                        string Connection = dt.Rows[0].ItemArray[2].ToString();
                        var array = Connection.Split(new string[] { "Server=", "Database=","User Id=", "Password=", ";" },StringSplitOptions.RemoveEmptyEntries);
                        txtServidor.Text = array[0];
                        txtNomeBanco.Text = array[1];
                        txtUsuario.Text = array[2];
                        txtSenha.Text = array[3];
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

            if (servidor != "" && nomebanco != "" && usuario != "" && senha != "")
            {
                StrConn = "Server=" + servidor + ";Database=" + nomebanco + ";User Id=" + usuario + ";Password=" + senha + ";";
                Properties.Settings.Default["StringBanco"] = StrConn;
                SalvarBanco(StrConn);

            }
            else
            {
                StrConn = "";
                Properties.Settings.Default["StringBanco"] = StrConn;
            }
            Properties.Settings.Default.Save();
            
        }
        private void SalvarBanco(string StrConn)
        {
            int result;
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName = "@Flag", SqlDbType = SqlDbType.Int, Value =1},
                new SqlParameter(){ParameterName = "@Caminho", SqlDbType = SqlDbType.NVarChar, Value = txtCaminho.Text},
                new SqlParameter(){ParameterName = "@Porta_Arduino", SqlDbType = SqlDbType.NVarChar, Value = txtPortaArduino.Text},
                new SqlParameter(){ParameterName = "@String_Conn", SqlDbType = SqlDbType.NVarChar, Value = StrConn}
            };
            try
            {
                result = banco.EditData("dbo.Parametros", sp);
                if (result > 0)
                {
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
            txtCaminho.Enabled = true;
            txtPortaArduino.Enabled = true;
            txtServidor.Enabled = true;
            txtNomeBanco.Enabled = true;
            txtUsuario.Enabled = true;
            txtSenha.Enabled = true;
            btnSalvar.Enabled = true;
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            btnEditar.Enabled = true;
            SalvarSettings();
        }

        private void FrmTelaDesenvolvedor_Load_1(object sender, EventArgs e)
        {
            CarregarInformacoes();
        }
    }
}
