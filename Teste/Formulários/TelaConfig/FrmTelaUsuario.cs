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
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName = "@Flag", SqlDbType = SqlDbType.Int, Value = 9},
            };
            try
            {
                dt = banco.InsertData("dbo.Funcoes_Pesquisa", sp);
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
        private void LimparCaixas()
        {
            txtId.Clear();
            txtLogin.Clear();
            txtSenha.Clear();
            txtConfirmSenha.Clear();
            numNivel.Value = 0;
            cmbStatus.SelectedIndex = -1;
            dataGridView1.ClearSelection();
            btnNovo.Enabled = true;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = false;
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
            List<SqlParameter> sp = new List<SqlParameter>()
                {

                    new SqlParameter(){ParameterName = "@Flag", SqlDbType = SqlDbType.Int, Value = 11},
                    new SqlParameter(){ParameterName = "@Login", SqlDbType = SqlDbType.NVarChar, Value = txtLogin.Text }
                };
            try
            {
                dt = banco.InsertData("dbo.Funcoes_Pesquisa", sp);
                if (dt.Rows.Count > 0)
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
            List<SqlParameter> sp = new List<SqlParameter>()
                {

                    new SqlParameter(){ParameterName = "@Flag", SqlDbType = SqlDbType.Int, Value = 2},
                    new SqlParameter(){ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = txtId.Text },
                    new SqlParameter(){ParameterName = "@Login", SqlDbType = SqlDbType.NVarChar, Value = txtLogin.Text },
                    new SqlParameter(){ParameterName = "@Senha", SqlDbType = SqlDbType.NVarChar, Value = txtConfirmSenha.Text },
                    new SqlParameter(){ParameterName = "@Nivel", SqlDbType = SqlDbType.Int, Value = numNivel.Value },
                    new SqlParameter(){ParameterName = "@Status", SqlDbType = SqlDbType.Int, Value = cmbStatus.SelectedIndex }
                };

            result = banco.EditData("dbo.Gerencia_Usuario", sp);
            if(result > 0)
            {
                MessageBox.Show("Usuario alterado com sucesso!", "Alteração Salva!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Globais.RegistrarLog(Globais.Login + " Alterou o Usuário ->" + txtLogin.Text);
                PreencherGrid();
            }
            else
            {
                MessageBox.Show("Falha ao Alterar Usuário!", "Falha ao Salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void SalvarUsuario()
        {
            int result;
            List<SqlParameter> sp = new List<SqlParameter>()
                {

                    new SqlParameter(){ParameterName = "@Flag", SqlDbType = SqlDbType.Int, Value = 1},
                    new SqlParameter(){ParameterName = "@Login", SqlDbType = SqlDbType.NVarChar, Value = txtLogin.Text },
                    new SqlParameter(){ParameterName = "@Senha", SqlDbType = SqlDbType.NVarChar, Value = txtConfirmSenha.Text },
                    new SqlParameter(){ParameterName = "@Nivel", SqlDbType = SqlDbType.Int, Value = numNivel.Value },
                    new SqlParameter(){ParameterName = "@Status", SqlDbType = SqlDbType.Int, Value = cmbStatus.SelectedIndex }
                };

            result = banco.EditData("dbo.Gerencia_Usuario", sp);
            if (result > 0)
            {
                MessageBox.Show("Usuario adicionado com sucesso!", "Novo usuário!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Globais.RegistrarLog(Globais.Login + " Adicionou o Usuário ->" + txtLogin.Text);
                PreencherGrid();
            }
            else
            {
                MessageBox.Show("Falha ao adicionar Usuário", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void FrmTelaUsuario_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            int linha = dgv.SelectedRows.Count;
            if(linha > 0)
            {
                string id = dgv.SelectedRows[0].Cells[0].Value.ToString();
                DataTable dt = new DataTable();
                List<SqlParameter> sp = new List<SqlParameter>()
                {

                    new SqlParameter(){ParameterName = "@Flag", SqlDbType = SqlDbType.Int, Value = 10},
                    new SqlParameter(){ParameterName = "@IdUsuario", SqlDbType = SqlDbType.Int, Value = id }
                };
                dt = banco.InsertData("dbo.Funcoes_Pesquisa",sp);
                txtId.Text = dt.Rows[0].ItemArray[0].ToString();
                txtLogin.Text = dt.Rows[0].ItemArray[1].ToString();
                cmbStatus.SelectedIndex = Convert.ToInt32(dt.Rows[0].ItemArray[3]);
                numNivel.Value = Convert.ToInt32(dt.Rows[0].ItemArray[2]);

            }
            btnExcluir.Enabled = true;
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView gridview;
            gridview = (DataGridView)sender;
            gridview.ClearSelection();
            LimparCaixas();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            int linha = dataGridView1.SelectedRows.Count;
            int result;
            if( linha > 0)
            {
                string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                List<SqlParameter> sp = new List<SqlParameter>()
                {

                    new SqlParameter(){ParameterName = "@Flag", SqlDbType = SqlDbType.Int, Value = 3},
                    new SqlParameter(){ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id }
                };
                result = banco.EditData("dbo.Gerencia_Usuario", sp);
                if(result > 0)
                {
                    MessageBox.Show("Usuário Deletado com Sucesso!", "Usuário Deletado!", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    Globais.RegistrarLog(Globais.Login + "Deletou o Usuario:" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                    PreencherGrid();
                    LimparCaixas();

                }
                else
                {
                    MessageBox.Show("Falha ao deletar Usuário!", "Falha!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numNivel_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtConfirmSenha_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
