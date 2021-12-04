using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ParkManager
{
    public partial class FrmTelaCliente : Form
    {
        Banco banco = new Banco();
        FrmTelaOperacao FormOp;
        FrmTelaCadastros FormCad;
        public FrmTelaCliente()
        {
            InitializeComponent();
            this.FormOp = (FrmTelaOperacao)Application.OpenForms["FrmTelaOperacao"];
            this.FormCad = (FrmTelaCadastros)Application.OpenForms["FrmTelaCadastros"];
        }

        private void FrmTelaCliente_Load(object sender, EventArgs e)
        {

            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;
        }
        private void CarregarGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = banco.ExecuteProcedureReturnDataTable("dbo.Select_Cliente_Grid");
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Width = 48;
                    dataGridView1.Columns[1].Width = 658;
                    dataGridView1.Columns[2].Width = 118;
                }
                else
                {
                    lblNada.Visible = true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Falha ao carregar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SelecaoGrid()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                txtNome.Enabled = true;
                mskTelefone.Enabled = true;
                btnSalvar.Enabled = true;
                btnLimpar.Enabled = true;
                btnExcluir.Enabled = true;
                try
                {
                    string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    DataTable dt = new DataTable();
                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter(){ParameterName="@idCliente", SqlDbType = SqlDbType.Int, Value = id}
                    };
                    dt = banco.ExecuteProcedureReturnDataTable("dbo.Select_Cliente", sp);
                    if (dt.Rows.Count > 0)
                    {
                        txtID.Text = dt.Rows[0]["ID"].ToString();
                        txtNome.Text = dt.Rows[0]["Nome"].ToString();
                        mskTelefone.Text = dt.Rows[0]["Telefone"].ToString();

                    }
                    else
                    {
                        MessageBox.Show("Falha ao encontrar Cliente!", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Falha", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            SelecaoGrid();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
            LimparCaixas();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            if (dataGridView1.Rows.Count > 0)
            {
                lblNada.Visible = false;
            }
        }
        private void LimparCaixas()
        {
            txtID.Clear();
            txtNome.Clear();
            mskTelefone.Clear();
            txtID.Clear();
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = false;
            btnLimpar.Enabled = false;
            dataGridView1.ClearSelection();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCaixas();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                DataTable dt = new DataTable();
                dt = VerificarTicket();
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["QTD"]) == 0)
                    {
                        ExcluirCliente();
                    }
                    else
                    {
                        MessageBox.Show("Não é possivel Excluir este cliente, pois ele possui um Ticket em Aberto!", "Cliente NÃO Salvo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void ExcluirCliente()
        {
            string mensagem = "Tem Certeza que deseja excluir este Cliente?";
            string titulo = "Excluir Cliente?";
            bool escolha = (MessageBox.Show(mensagem, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes);
            if (escolha)
            {
                try
                {
                    string id = txtID.Text;
                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter(){ParameterName="@Flag", SqlDbType = SqlDbType.Int, Value=1},
                        new SqlParameter(){ParameterName="@idCliente", SqlDbType = SqlDbType.Int, Value = id }
                    };
                    int LinhasAfetadas = banco.ExecuteProcedureReturnInt("dbo.Gerencia_Cliente", sp);
                    if (LinhasAfetadas > 0)
                    {
                        MessageBox.Show("Cadastro Excluido com Sucesso!", "Exclusão bem sucedida!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarGrid();
                    }
                    else
                    {
                        MessageBox.Show("Falha ao excluir Cliente ou ele não Existe!", "Falha!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Falha!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            VerificarCaixas();
        }
        private void VerificarCaixas()
        {
            if (Regex.IsMatch(txtNome.Text, @"^[A-Za-záàâãéèêíïóôõöúçÁÀÂÃÉÈÍÏÓÔÕÖÚÇ ]+$"))
            {
                if (Regex.IsMatch(mskTelefone.Text, @"^[(]{1}[11-99]{2}[)]{1}[0|9]{1}[0-9]{4}-[0-9]{4}"))
                {

                    if (txtID.Text == "")
                    {
                        VerificarCliente();
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt = VerificarTicket();
                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dt.Rows[0]["QTD"]) == 0)
                            {
                                SalvarCliente("Edit");
                            }
                            else
                            {
                                MessageBox.Show("Não é possivel Editar este cliente, pois ele possui um Ticket em Aberto!", "Cliente NÃO Salvo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Telefone Inválido!", "Falha!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Nome Inválido!", "Falha!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void VerificarCliente()
        {
            DataTable dt = new DataTable();
            try
            {

                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@Nome", SqlDbType = SqlDbType.VarChar, Value = txtNome.Text },
                    new SqlParameter(){ParameterName="@Telefone", SqlDbType = SqlDbType.VarChar, Value = mskTelefone.Text }
                };
                dt = banco.ExecuteProcedureReturnDataTable("dbo.Select_Cliente_Nome_Telefone", sp);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Este Cliente e Telefone já está cadastrado! \n ID:" + dt.Rows[0]["ID"].ToString(), "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNome.Focus();
                }
                else
                {
                    SalvarCliente("Save");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dt.Dispose();
            }
        }
        public DataTable VerificarTicket()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter(){ParameterName="@idCliente", SqlDbType = SqlDbType.Int, Value = txtID.Text},
                    };
                dt = banco.ExecuteProcedureReturnDataTable("dbo.Select_QtdTicket_Cliente", sp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cliente NÃO Salvo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }
        private void SalvarCliente(string mode)
        {
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter(){ParameterName="@Flag", SqlDbType = SqlDbType.Int, Value= 0},
                        new SqlParameter(){ParameterName="@Nome", SqlDbType = SqlDbType.VarChar, Value = txtNome.Text},
                        new SqlParameter(){ParameterName="@Telefone", SqlDbType = SqlDbType.VarChar, Value = mskTelefone.Text},
                    };
                if (txtID.Text != "" && mode == "Edit")
                {
                    sp.Add(new SqlParameter() { ParameterName = "@idCliente", SqlDbType = SqlDbType.Int, Value = txtID.Text });
                }
                int LinhasAfetadas = banco.ExecuteProcedureReturnInt("dbo.Gerencia_Cliente", sp);
                if (LinhasAfetadas > 0)
                {
                    if (mode == "Save")
                    {
                        MessageBox.Show("Cliente Adionado com sucesso!", "Cliente Adicionado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarGrid();
                        Globais.RegistrarLog(Globais.Login + " Adicionou o Cliente:" + txtNome.Text + " Telefone:" + mskTelefone.Text);
                    }
                    else if (mode == "Edit")
                    {
                        MessageBox.Show("Cliente Editado com sucesso!", "Cliente Salvo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Globais.RegistrarLog(Globais.Login + " Alterou o Cliente ID:" + txtID.Text + ".");
                        CarregarGrid();
                    }
                }
                else
                {
                    if (mode == "Save")
                    {
                        MessageBox.Show("Falha ao adicionar Cliente", "Cliente NÃO Salvo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (mode == "Edit")
                    {
                        MessageBox.Show("Falha ao Editar Cliente", "Cliente NÃO Salvo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Cliente NÃO Salvo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FrmTelaCliente_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.KeyCode:
                    break;

                case Keys.F3:
                    btnPesquisa.PerformClick();
                    break;

                case Keys.F5:
                    btnSalvar.PerformClick();
                    break;

                default:
                    break;
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            FormOp.NomeCli = dataGridView1.SelectedRows[0].Cells["Nome"].Value.ToString();
            FormOp.TelCli = dataGridView1.SelectedRows[0].Cells["Telefone"].Value.ToString();
            FormCad.Dispose();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            try
            {
                string tel = mskTelefone.Text != "(  )     -" ? mskTelefone.Text : null;
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@Nome", SqlDbType =  SqlDbType.VarChar, Value = txtNome.Text},
                    new SqlParameter(){ParameterName="@Telefone", SqlDbType = SqlDbType.VarChar, Value = tel}
                };
                dt = banco.ExecuteProcedureReturnDataTable("dbo.Select_Cliente_Grid", sp);
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Width = 48;
                    dataGridView1.Columns[1].Width = 658;
                    dataGridView1.Columns[2].Width = 118;
                }
                else
                {
                    dataGridView1.DataSource = null;
                    lblNada.Visible = true;
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Falha ao realizar pesquisa!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void FrmTelaCliente_KeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void FrmTelaCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtNome.Text != "" && mskTelefone.MaskCompleted)
            {
                btnSalvar.Enabled = true;
            }
            else
            {
                btnSalvar.Enabled = false;
            }
        }


        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            if (txtNome.Text != "" && mskTelefone.MaskCompleted)
            {
                btnSalvar.Enabled = true;
            }
            else
            {
                btnSalvar.Enabled = false;
            }
        }

        private void mskTelefone_TextChanged(object sender, EventArgs e)
        {
            if (txtNome.Text != "" && mskTelefone.MaskCompleted)
            {
                btnSalvar.Enabled = true;
            }
            else
            {
                btnSalvar.Enabled = false;
            }
        }
    }
}
