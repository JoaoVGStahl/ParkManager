using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teste
{
    public partial class FrmTelaUsuario : Form
    {
        Banco banco = new Banco();

        public FrmTelaUsuario()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmTelaUsuario_Load(object sender, EventArgs e)
        {
            PreencherGrid();
        }
        private void PreencherGrid()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT id_usuario[ID], login[Login],nivel[Nivel],status[Status] FROM tb_usuario";
            try
            {
                dt = banco.QueryBancoSql(query);

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Falha ao carregar as informações!");
            }
            finally
            {
                dt.Dispose();
            }

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            string caracterespermitidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZ.";
            //Apenas Letras E BackSpace
            if (!(caracterespermitidos.Contains(e.KeyChar.ToString().ToUpper())) && !(e.KeyChar == (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AtivarFuncoes();
        }
        private void AtivarFuncoes()
        {
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            txtLogin.Enabled = true;
            txtSenha.Enabled = true;
            txtConfirmSenha.Enabled = true;
            cmbStatus.Enabled = true;
            cmbStatus.SelectedIndex = -1;
            numNivel.Enabled = true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            VerificarCaixas();
        }
        private void VerificarCaixas()
        {
            if(txtLogin.Text != "" && txtSenha.Text != "" && txtConfirmSenha.Text != "")
            {
                if(txtSenha.Text == txtConfirmSenha.Text)
                {
                    VerificarUsuario();
                }
                else
                {
                    MessageBox.Show("As senhas são diferentes! Verifiquei e tente novamente!", "Falha ao salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Há campos vazios que precisam ser preenchidos!","Falha ao salvar!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void VerificarUsuario()
        {
            DataTable dt = new DataTable();
            string login = txtLogin.Text;
            string query = @"SELECT * FROM tb_usuario WHERE login = '" + login + "'";

            try
            {
                dt = banco.QueryBancoSql(query);
                if(dt.Rows.Count > 0)
                {
                    EditarUsuario();
                }
                else
                {
                    SalvarUsuario();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Falha ao realizar consulta no banco de dados!");
            }
            finally
            {
                dt.Dispose();
            }
            

        }
        private void EditarUsuario()
        {
            int result;
            string query = @"
            UPDATE 
                tb_usuario 
            SET 
                login='" + txtLogin.Text + "', senha='" + txtSenha.Text + "', nivel=" + numNivel.Value + ",status=" + cmbStatus.SelectedIndex + "WHERE login='" + txtLogin.Text + "'";
            result = banco.DmlBancoSql(query);
            if(result == 1)
            {
                MessageBox.Show("Usuario alterado com sucesso!", "Alteração Salva!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Globais.RegistrarLog(Globais.Login + " Alterou o Usuário ->" + txtLogin.Text);
                PreencherGrid();
            }

        }
        private void SalvarUsuario()
        {
            int result;
            string query = @"
            INSERT INTO 
                tb_usuario(login,senha,nivel,status) 
            VALUES
                ('" + txtLogin.Text + "','" + txtConfirmSenha.Text + "'," + numNivel.Value + "," + cmbStatus.SelectedIndex + ")";
            result = banco.DmlBancoSql(query);
            if (result == 1)
            {
                MessageBox.Show("Usuario adicionado com sucesso!", "Novo usuário!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Globais.RegistrarLog(Globais.Login + " Adicionou o Usuário ->" + txtLogin.Text);
                PreencherGrid();
            }

        }

        private void FrmTelaUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }
    }
}
