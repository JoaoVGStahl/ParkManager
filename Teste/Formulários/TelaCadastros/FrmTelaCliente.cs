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
using System.Text.RegularExpressions;

namespace Teste
{
    public partial class FrmTelaCliente : Form
    {
        Banco banco = new Banco();
        public FrmTelaCliente()
        {
            InitializeComponent();
        }

        private void FrmTelaCliente_Load(object sender, EventArgs e)
        {
            cmbStatus.SelectedIndex = 1;
            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;
            CarregarGrid();

        }
        private void CarregarGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@Status", SqlDbType = SqlDbType.Int, Value = cmbStatus.SelectedIndex}
                };
                dt = banco.InsertData("dbo.Select_Cliente_Grid", sp);
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Width = 48;
                    dataGridView1.Columns[1].Width = 658;
                    dataGridView1.Columns[2].Width = 118;
                }
                else
                {
                    MessageBox.Show("Falha ao carregar a lista de cliente!", "Falha ao carregar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                btnNovo.Enabled = false;
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
                    dt = banco.InsertData("dbo.Select_Cliente", sp);
                    if (dt.Rows.Count > 0)
                    {
                        txtID.Text = dt.Rows[0]["ID"].ToString();
                        txtNome.Text = dt.Rows[0]["Nome"].ToString();
                        mskTelefone.Text = dt.Rows[0]["Telefone"].ToString();
                        cmbStatus.SelectedIndex = Convert.ToInt32(dt.Rows[0]["Status"]);
                        
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
        }
        private void LimparCaixas()
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnExcluir.Enabled = false;
            btnLimpar.Enabled = false;
            txtNome.Clear();
            txtNome.Enabled = false;
            mskTelefone.Clear();
            mskTelefone.Enabled = false;
            cmbStatus.Enabled = false;
            txtID.Clear();
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
                    int LinhasAfetadas = banco.EditData("dbo.Gerencia_Cliente", sp);
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
                    if (cmbStatus.SelectedIndex >= 0)
                    {
                        if (txtID.Text == "")
                        {
                            VerificarCliente();
                        }
                        else
                        {
                            DataTable dt = new DataTable();
                            dt = VerificarTicket();
                            if(dt.Rows.Count > 0)
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
                        MessageBox.Show("Selecione Status Válido!", "Falha!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dt = banco.InsertData("dbo.Select_Cliente_Nome_Telefone", sp);
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
                dt = banco.InsertData("dbo.Select_QtdTicket_Cliente", sp);
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
                        new SqlParameter(){ParameterName="@Status", SqlDbType = SqlDbType.Int, Value = cmbStatus.SelectedIndex}
                    };
                if (txtID.Text != "" && mode == "Edit")
                {
                    sp.Add(new SqlParameter() { ParameterName = "@idCliente", SqlDbType = SqlDbType.Int, Value = txtID.Text });
                }
                int LinhasAfetadas = banco.EditData("dbo.Gerencia_Cliente",sp);
                if(LinhasAfetadas > 0)
                {
                    if(mode == "Save")
                    {
                        MessageBox.Show("Cliente Adionado com sucesso!", "Cliente Adicionado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CarregarGrid();
                        Globais.RegistrarLog(Globais.Login + " Adicionou o Cliente:" + txtNome.Text + " Telefone:" + mskTelefone.Text);
                    }else if(mode == "Edit")
                    {
                        MessageBox.Show("Cliente Editado com sucesso!", "Cliente Salvo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Globais.RegistrarLog(Globais.Login + " Alterou o Cliente ID:" + txtID.Text + ".");
                        CarregarGrid();
                    }
                }
                else
                {
                    if(mode == "Save")
                    {
                        MessageBox.Show("Falha ao adicionar Cliente", "Cliente NÃO Salvo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }else if(mode == "Edit")
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
        private void btnNovo_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled = false;
            btnLimpar.Enabled = true;
            btnSalvar.Enabled = true;
            txtNome.Enabled = true;
            mskTelefone.Enabled = true;
            cmbStatus.Enabled = true;
        }

        private void FrmTelaCliente_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.KeyCode:
                    break;

                case Keys.F3:
                    btnNovo.PerformClick();
                    break;

                case Keys.F5:
                    btnSalvar.PerformClick();
                    break;

                default:
                    break;
            }
        }
    }
}
