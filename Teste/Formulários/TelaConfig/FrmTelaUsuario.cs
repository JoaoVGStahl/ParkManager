using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            try
            {
                dt = banco.ExecuteProcedureReturnDataTable("dbo.Pesquisa_Usuarios");
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 300;
                dataGridView1.Columns[2].Width = 83;
                dataGridView1.Columns[3].Width = 96;
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
            txtId.Enabled = false;
            txtLogin.Enabled = false;
            txtSenha.Enabled = false;
            txtConfirmSenha.Enabled = false;
            cmbStatus.Enabled = false;
            numNivel.Enabled = false;
            btnLimpar.Enabled = false;
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
            if (txtLogin.Text != "" && txtSenha.Text != "" && txtConfirmSenha.Text != "")
            {
                if (txtSenha.Text == txtConfirmSenha.Text)
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
                MessageBox.Show("Há campos vazios que precisam ser preenchidos!", "Falha ao salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void VerificarUsuario()
        {
            DataTable dt = new DataTable();
            List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@Login", SqlDbType = SqlDbType.NVarChar, Value = txtLogin.Text }
                };
            try
            {
                dt = banco.ExecuteProcedureReturnDataTable("dbo.Pesquisa_Usuario_Login", sp);
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
            string senha = Globais.EncodeToMd5(txtConfirmSenha.Text);
            int result;
            List<SqlParameter> sp = new List<SqlParameter>()
                {

                    new SqlParameter(){ParameterName = "@Flag", SqlDbType = SqlDbType.Int, Value = 2},
                    new SqlParameter(){ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = txtId.Text},
                    new SqlParameter(){ParameterName = "@Login", SqlDbType = SqlDbType.NVarChar, Value = txtLogin.Text },
                    new SqlParameter(){ParameterName = "@Senha", SqlDbType = SqlDbType.NVarChar, Value = senha },
                    new SqlParameter(){ParameterName = "@Nivel", SqlDbType = SqlDbType.Int, Value = numNivel.Value },
                    new SqlParameter(){ParameterName = "@Status", SqlDbType = SqlDbType.Int, Value = cmbStatus.SelectedIndex }
                };

            result = banco.ExecuteProcedureReturnInt(NameProcedure: "dbo.Gerencia_Usuario", sp: sp);
            if (result > 0)
            {
                Globais.RegistrarLog(Globais.Login + " Alterou o Usuário ->" + txtLogin.Text);
                PreencherGrid();
                MessageBox.Show("Usuario alterado com sucesso!", "Alteração Salva!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Falha ao Alterar Usuário!", "Falha ao Salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void SalvarUsuario()
        {
            string senha = Globais.EncodeToMd5(txtConfirmSenha.Text);
            int result;
            List<SqlParameter> sp = new List<SqlParameter>()
                {

                    new SqlParameter(){ParameterName = "@Flag", SqlDbType = SqlDbType.Int, Value = 1},
                    new SqlParameter(){ParameterName = "@Login", SqlDbType = SqlDbType.NVarChar, Value = txtLogin.Text },
                    new SqlParameter(){ParameterName = "@Senha", SqlDbType = SqlDbType.NVarChar, Value = senha },
                    new SqlParameter(){ParameterName = "@Nivel", SqlDbType = SqlDbType.Int, Value = numNivel.Value },
                    new SqlParameter(){ParameterName = "@Status", SqlDbType = SqlDbType.Int, Value = cmbStatus.SelectedIndex }
                };

            result = banco.ExecuteProcedureReturnInt("dbo.Gerencia_Usuario", sp);
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
            if (linha > 0)
            {
                string id = dgv.SelectedRows[0].Cells[0].Value.ToString();
                DataTable dt = new DataTable();
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@IdUsuario", SqlDbType = SqlDbType.Int, Value = id }
                };
                dt = banco.ExecuteProcedureReturnDataTable("dbo.Pesquisa_Usuario_Id", sp);
                if (dt.Rows.Count > 0)
                {
                    txtId.Text = dt.Rows[0]["ID"].ToString();
                    txtLogin.Text = dt.Rows[0]["Login"].ToString();
                    cmbStatus.SelectedIndex = Convert.ToInt32(dt.Rows[0]["Status"]);
                    numNivel.Value = Convert.ToInt32(dt.Rows[0]["Nivel"]);
                }
                else
                {
                    MessageBox.Show("Usuario selecionado não foi encontrado!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            txtLogin.Enabled = true;
            txtSenha.Enabled = true;
            txtConfirmSenha.Enabled = true;
            cmbStatus.Enabled = true;
            numNivel.Enabled = true;
            btnExcluir.Enabled = true;
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            btnLimpar.Enabled = true;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            LimparCaixas();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView gridview;
            gridview = (DataGridView)sender;
            gridview.ClearSelection();
            LimparCaixas();
        }

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            int linha = dataGridView1.SelectedRows.Count;
            int result;
            if (linha > 0)
            {
                string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                List<SqlParameter> sp = new List<SqlParameter>()
                {

                    new SqlParameter(){ParameterName = "@Flag", SqlDbType = SqlDbType.Int, Value = 3},
                    new SqlParameter(){ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id }
                };
                result = banco.ExecuteProcedureReturnInt("dbo.Gerencia_Usuario", sp);
                if (result > 0)
                {
                    MessageBox.Show("Usuário Deletado com Sucesso!", "Usuário Deletado!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
