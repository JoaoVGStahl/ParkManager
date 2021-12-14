using ParkManager.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ParkManager
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
                    DataTable dt = VerificarUsuario();
                    if (dt.Rows.Count > 0)
                    {
                        EditarUsuario(dt);
                    }
                    else
                    {
                        SalvarUsuario(Globais.EncodeToMd5(txtConfirmSenha.Text));
                    }
                }
                else
                {
                    MessageBox.Show("As senhas são diferentes! Verifique e tente novamente!", "Falha ao salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Há campos vazios que precisam ser preenchidos!", "Falha ao salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private DataTable VerificarUsuario()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@Login", SqlDbType = SqlDbType.NVarChar, Value = txtLogin.Text }
                };
                dt = banco.ExecuteProcedureReturnDataTable("dbo.Pesquisa_Usuario_Login", sp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Falha ao realizar consulta no banco de dados!");
            }
            return dt;
        }
        private void EditarUsuario(DataTable dt)
        {
            List<SqlParameter> sp = new List<SqlParameter>();

            Usuario usuarioAtual = new Usuario
            {
                Id = Convert.ToInt32(dt.Rows[0]["ID"]),
                Login = dt.Rows[0]["Login"].ToString(),
                Senha = dt.Rows[0]["Senha"].ToString(),
                Nivel = Convert.ToInt32(dt.Rows[0]["Nivel"]),
                Status = Convert.ToInt32(dt.Rows[0]["Status"])
            };

            string senha = Globais.EncodeToMd5(txtConfirmSenha.Text);

            if (!usuarioAtual.Login.Equals(txtLogin.Text))
            {
                sp.Add(new SqlParameter() { ParameterName = "@Login", SqlDbType = SqlDbType.NVarChar, Value = txtLogin.Text });
            }
            if (!usuarioAtual.Senha.Equals(senha))
            {
                sp.Add(new SqlParameter() { ParameterName = "@Senha", SqlDbType = SqlDbType.NVarChar, Value = senha });
            }
            if (usuarioAtual.Nivel != numNivel.Value)
            {
                sp.Add(new SqlParameter() { ParameterName = "@Nivel", SqlDbType = SqlDbType.Int, Value = numNivel.Value });
            }
            if (!usuarioAtual.Status.Equals(cmbStatus.SelectedIndex))
            {
                sp.Add(new SqlParameter() { ParameterName = "@Status", SqlDbType = SqlDbType.Int, Value = cmbStatus.SelectedIndex });
            }
            if(sp.Count > 0)
            {
                int result = banco.ExecuteProcedureReturnInt(NameProcedure: "dbo.Gerencia_Usuario", sp: sp);

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
            else
            {
                MessageBox.Show("Não há alterações a serem salvas!!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void SalvarUsuario(string senhaEncoded)
        {
            try
            {
                int result;
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@Login", SqlDbType = SqlDbType.NVarChar, Value = txtLogin.Text },
                    new SqlParameter(){ParameterName = "@Senha", SqlDbType = SqlDbType.NVarChar, Value = senhaEncoded },
                    new SqlParameter(){ParameterName = "@Nivel", SqlDbType = SqlDbType.Int, Value = numNivel.Value },
                    new SqlParameter(){ParameterName = "@Status", SqlDbType = SqlDbType.Int, Value = cmbStatus.SelectedIndex }
                };

                result = banco.ExecuteProcedureReturnInt("dbo.Novo_Usuario", sp);
                if (result > 0)
                {
                    MessageBox.Show("Usuario adicionado com sucesso!", "Novo usuário!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Globais.RegistrarLog(Globais.Login + " Adicionou o Usuário ->" + txtLogin.Text);
                    PreencherGrid();
                    LimparCaixas();
                }
                else
                {
                    MessageBox.Show("Falha ao adicionar Usuário", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());
                    AlterarStatusUsuario(id, 2);
                    PreencherGrid();
                    MessageBox.Show("Usuário Deletado com Sucesso!", "Usuário Deletado!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Globais.RegistrarLog(Globais.Login + "Deletou o Usuario:" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Nenhum usuário selecionado!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AlterarStatusUsuario(int id, int status)
        {
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id },
                    new SqlParameter(){ParameterName="@Status", SqlDbType = SqlDbType.Int, Value = status}
                };
                int result = banco.ExecuteProcedureReturnInt("dbo.Alterar_Status_Usuario", sp);
                if (result > 0)
                {
                    LimparCaixas();
                }
                else
                {
                    MessageBox.Show("Falha ao deletar Usuário!", "Falha!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
