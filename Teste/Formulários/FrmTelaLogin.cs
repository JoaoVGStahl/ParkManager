using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Teste
{
    public partial class FrmTelaLogin : Form
    {
        Banco banco = new Banco();
        FrmTelaOperacao Frm = new FrmTelaOperacao();
        public FrmTelaLogin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Destroi o form, registra log e encerra a aplicação
            Globais.RegistrarLog("Sistema foi encerrado");
            Frm.Dispose();
            this.Dispose();
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EfetuarLogin();
        }

        private void EfetuarLogin()
        {
            DataTable dt = new DataTable();
            string usuario = txtUsuario.Text, senha = txtSenha.Text;
            if (usuario != "" && senha != "")
            {
                if (usuario != Properties.Settings.Default["UserRoot"].ToString() && senha != Properties.Settings.Default["SenhaRoot"].ToString())
                {
                    try
                    {
                        List<SqlParameter> sp = new List<SqlParameter>()
                        {
                            new SqlParameter(){ParameterName="@Flag", SqlDbType = SqlDbType.Int, Value = 4},
                            new SqlParameter(){ParameterName="@Login", SqlDbType = SqlDbType.VarChar, Value = usuario},
                            new SqlParameter(){ParameterName="@Senha", SqlDbType = SqlDbType.VarChar, Value = senha}
                        };
                        dt = banco.InsertData("dbo.Gerencia_Usuario", sp);
                        if (dt.Rows.Count > 0)
                        {
                            int status = Convert.ToInt32(dt.Rows[0]["Status"].ToString());
                            if (status != 0)
                            {
                                PreencherGlobais(dt);
                                Globais.RegistrarLog(Globais.Login + " Efetuou Login.");
                                AbrirForm();
                            }
                            else
                            {
                                txtSenha.Clear();
                                txtUsuario.Select();
                                MessageBox.Show("Este usuário está inativo, contate o administrador do sistema!", "Falha no login!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            txtSenha.Clear();
                            txtUsuario.Select();
                            MessageBox.Show("Usuário e/ou senha incorretos ou não existem!", "Falha no login!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Falha no login!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (usuario == Properties.Settings.Default["UserRoot"].ToString() && senha == Properties.Settings.Default["SenhaRoot"].ToString())
                {
                    Globais.IdUsuario = 1;
                    Globais.Login = usuario;
                    Globais.Nivel = 3;
                    Globais.UserStatus = 1;
                    FrmTelaConfig Form = new FrmTelaConfig(this);
                    AbrirForm();
                }
            }
            else
            {
                txtUsuario.Focus();
                MessageBox.Show("Preencha todos os campos!", "Falha no login!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dt.Dispose();

        }

        private void AbrirForm()
        {
            this.Hide();
            Frm.ShowDialog();
        }

        private void PreencherGlobais(DataTable dt)
        {
            Globais.IdUsuario = Convert.ToInt32(dt.Rows[0]["ID"]);
            Globais.Login = dt.Rows[0]["Login"].ToString();
            Globais.Nivel = Convert.ToInt32(dt.Rows[0]["Nivel"]);
            Globais.UserStatus = Convert.ToInt32(dt.Rows[0]["Status"]);
            Frm.lblUsername.Text = Globais.Login;
        }

        private void FrmTelaLogin_Load(object sender, EventArgs e)
        {
            //Registra no Log que o sistema foi Inicializado
            Globais.RegistrarLog("Sistema foi Inicializado.");
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            string caracterespermitidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZ.";
            //Apenas Letras E BackSpace
            if (!(caracterespermitidos.Contains(e.KeyChar.ToString().ToUpper())) && !(e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void FrmTelaLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEntrar.PerformClick();
            }
        }
    }
}
