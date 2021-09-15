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
            this.Dispose();
            Application.Exit();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text, senha = txtSenha.Text;
            //Login Usuário Root!
            if (usuario == Properties.Settings.Default.UserRoot && senha == Properties.Settings.Default.SenhaRoot)
            {
                Globais.IdUsuario = 1;
                Globais.Login = Properties.Settings.Default.UserRoot;
                Globais.Nivel = 3;
                Globais.UserStatus = 1;
                Frm.lblUsername.Text = Globais.Login;
                this.Hide();
                Frm.ShowDialog();
                return;
            }
            if (usuario == "" || senha == "")
            {
                CaixaAlerta("Preencha todos os campos!", "Falha no login!");
                txtUsuario.Focus();
                return;
            }
            else
            {
                //Query P/ Consulta Banco
                string query = @"
                SELECT 
                    id_usuario,login,nivel,status 
                FROM 
                    tb_usuario 
                WHERE 
                    login='" + usuario + "' AND senha='" + senha + "'";
                //Retorno do Banco em um DataTable!
                DataTable dt = new DataTable();
                dt = banco.QueryBancoSql(query);
                //Verifica se a consulta retornou algum valor!
                if (dt.Rows.Count > 0)
                {
                    //Verifica se o Usuário está Ativo!
                    int status = Convert.ToInt32(dt.Rows[0].ItemArray[3]);
                    if (status > 0)
                    {
                        //Atribui os valores do Usuario para Variaveis globais!
                        PreencherGlobais(dt);
                        Globais.RegistrarLog(Globais.Login + " Efetuou Login.");
                        this.Hide();
                        Frm.ShowDialog();

                    }
                    else
                    {
                        CaixaAlerta("Este usuário está inativo, contate o administrador do sistema!", "Falha no login!");
                        txtSenha.Clear();
                        txtUsuario.Select();
                    }
                }
                else
                {
                    CaixaAlerta("Usuário e/ou senha incorretos ou não existem!", "Falha no login!");
                    txtSenha.Clear();
                    txtUsuario.Select();
                }
            }

        }
        private void PreencherGlobais(DataTable dt)
        {
            Globais.IdUsuario = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
            Globais.Login = Convert.ToString(dt.Rows[0].ItemArray[1]);
            Globais.Nivel = Convert.ToInt32(dt.Rows[0].ItemArray[2]);
            Globais.UserStatus = Convert.ToInt32(dt.Rows[0].ItemArray[3]);
            Frm.lblUsername.Text = Globais.Login;
        }
        private void CaixaAlerta(string texto, string titulo)
        {
            MessageBox.Show(texto, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void FrmTelaLogin_Load(object sender, EventArgs e)
        {
            //Registra no Log que o sistema foi Inicializado
            Globais.RegistrarLog("Sistema foi Inicializado");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
