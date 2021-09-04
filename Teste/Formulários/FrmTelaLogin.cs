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
        public FrmTelaLogin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string usuario = txtUsuario.Text, senha = txtSenha.Text;

            string str = @"Server=PCSEMIGAMER\SQLSERVER; Database=db_estacionamento; User Id=sa; Password=joaorotivGTA5";
            string query = "SELECT login,nivel,status FROM tb_usuario WHERE login='" + usuario + "' AND senha='" + senha + "'";
            
            SqlConnection conexao = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand(query,conexao);
            SqlDataReader reader;
            cmd.CommandType = CommandType.Text;
            
            try
            {
                conexao.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {                    
                    int status = Convert.ToInt32(reader[2]);
                    if (status ==1)
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Este usuário está inativo, contate o administrador do sistema!", "Falha no login!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Usuário e/ou senha incorretos!", "Falha no login!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("Codigo de erro:"+ ex, "Falha no login!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }
        
        }

        private void FrmTelaLogin_Load(object sender, EventArgs e)
        {

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
