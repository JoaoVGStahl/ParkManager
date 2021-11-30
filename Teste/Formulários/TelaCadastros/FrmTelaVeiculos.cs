using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ParkManager
{
    public partial class FrmTelaVeiculos : Form
    {
        Banco banco = new Banco();
        FrmTelaOperacao FrmOp;
        FrmTelaCadastros FrmCad;
        public FrmTelaVeiculos()
        {
            InitializeComponent();
            this.FrmOp = (FrmTelaOperacao)Application.OpenForms["FrmTelaOperacao"];
            this.FrmCad = (FrmTelaCadastros)Application.OpenForms["FrmTelaCadastros"];
        }

        private void FrmTelaVeiculos_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;
            cmbTipo.SelectedIndexChanged -= cmbTipo_SelectedIndexChanged;
            PreencherGrid();
        }
        private void PreencherGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = banco.ExecuteProcedureReturnDataTable("dbo.Select_TelaCadastro_Veiculos");
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LimparCaixas();
        }
        private void CarregarComboTipo()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@Flag", SqlDbType = SqlDbType.Int, Value = 0}
                };
                dt = banco.ExecuteProcedureReturnDataTable("dbo.Funcoes_Pesquisa", sp);
                if (dt.Rows.Count > 0)
                {
                    cmbTipo.DataSource = null;
                    cmbTipo.DataSource = dt;
                    cmbTipo.ValueMember = "id_automovel";
                    cmbTipo.DisplayMember = "automovel";
                    cmbTipo.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Tipos de veiculos não encontrados!", "Falha ao Carregar Informações!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Falha ao Carregar Informações!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled = false;
            txtPlaca.Enabled = true;
            cmbTipo.Enabled = true;
            cmbStatus.Enabled = true;
            btnSalvar.Enabled = true;
            btnLimpar.Enabled = true;
            cmbStatus.SelectedIndex = 1;
            CarregarComboTipo();
            cmbTipo.SelectedIndexChanged += cmbTipo_SelectedIndexChanged;
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Caracteres permitidos
            string caracterespermitidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            //Apenas Letras E BackSpace nos 3 primeiros digitos
            if (txtPlaca.TextLength < 3)
            {
                if (!char.IsLetter(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
                {
                    e.Handled = true;
                }
            }
            //Apenas Números E BackSpace no 4º Digito
            if (txtPlaca.TextLength == 3)
            {
                if (!char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
                {
                    e.Handled = true;
                }
            }
            // Apenas letras de A-J e 0-9 e BackSpace no 5º Digito
            if (txtPlaca.TextLength == 4)
            {
                if (!(caracterespermitidos.Contains(e.KeyChar.ToString().ToUpper())) && !(e.KeyChar == (char)Keys.Back))
                {
                    e.Handled = true;
                }
            }
            //Apenas Números e BackSpace nos 6º e 7º Digitos
            if (txtPlaca.TextLength > 4)
            {
                if (!char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar) && !(e.KeyChar == (char)Keys.Back))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            VerificarCaixas();
        }
        private void VerificarCaixas()
        {
            DataTable dt = new DataTable();
            if (Regex.IsMatch(txtPlaca.Text, "^[A-Z]{3}[0-9]{1}[A-Z0-9]{1}[0-9]{2}"))
            {
                if (cmbTipo.SelectedIndex >= 0)
                {
                    if (cmbMarca.SelectedIndex >= 0)
                    {
                        if (cmbStatus.SelectedIndex >= 0)
                        {
                            if (txtId.Text == "")
                            {
                                dt = VerificarVeiculo();
                                if (dt.Rows.Count > 0)
                                {

                                    MessageBox.Show("Esta Placa já está cadastrada!", "Falha ao Salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtPlaca.Focus();
                                }
                                else
                                {
                                    SalvarVeiculo("Save");
                                }
                            }
                            else
                            {
                                dt = VerificarTicket();
                                if (dt.Rows.Count > 0)
                                {
                                    MessageBox.Show("Este veiculo possui um Ticket em aberto! \nEncerre-o e tente novamente!", "Falha!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    SalvarVeiculo("Edit");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Selecione Status", "Falha ao Verificar Dados!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbStatus.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selecione Uma Marca de Veiculo!", "Falha ao Verificar Dados!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbMarca.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Selecione Um Tipo de Veiculo!", "Falha ao Verificar Dados!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbTipo.Focus();
                }
            }
            dt.Dispose();
        }
        public DataTable VerificarVeiculo()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@Flag", SqlDbType = SqlDbType.Int, Value=13},
                    new SqlParameter(){ParameterName="@Placa", SqlDbType = SqlDbType.VarChar, Value = txtPlaca.Text}
                };
                dt = banco.ExecuteProcedureReturnDataTable("dbo.Funcoes_Pesquisa", sp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Falha!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }
        public DataTable VerificarTicket()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@idCarro", SqlDbType = SqlDbType.Int, Value = Convert.ToInt32(txtId.Text)}
                };
                dt = banco.ExecuteProcedureReturnDataTable("dbo.Select_TicketAberto_Veiculo", sp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Falha ao editar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }
        private void SalvarVeiculo(string method)
        {
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@Flag", SqlDbType = SqlDbType.Int, Value = 0},
                    new SqlParameter(){ParameterName = "@Placa", SqlDbType = SqlDbType.VarChar, Value = txtPlaca.Text },
                    new SqlParameter(){ParameterName ="@Tipo", SqlDbType = SqlDbType.VarChar, Value = cmbTipo.Text},
                    new SqlParameter(){ParameterName="@Marca", SqlDbType = SqlDbType.VarChar, Value = cmbMarca.Text},
                    new SqlParameter(){ParameterName="@Status", SqlDbType = SqlDbType.Int, Value = cmbStatus.SelectedIndex}
                };
                if (txtId.Text != "" && method == "Edit")
                {
                    sp.Add(new SqlParameter() { ParameterName = "@idCarro", SqlDbType = SqlDbType.Int, Value = txtId.Text });
                }
                int LinhasAfetadas = banco.ExecuteProcedureReturnInt("dbo.Gerencia_Veiculo", sp);
                if (LinhasAfetadas > 0)
                {
                    if (method == "Save")
                    {
                        Globais.RegistrarLog(Globais.Login + " Adicionou o Veiculo : " + txtPlaca.Text + ".");
                        MessageBox.Show("Veiculo Adicionado com Sucesso!", "Veiculo Salvo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PreencherGrid();
                    }
                    else if (method == "Edit")
                    {
                        Globais.RegistrarLog(Globais.Login + " Alterou o Veiculo : " + txtId.Text + ".");
                        MessageBox.Show("Veiculo Editado com sucesso!", "Veiculo Salvo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PreencherGrid();
                    }
                }
                else
                {
                    if (method == "Save")
                    {
                        MessageBox.Show("Falha ao Adiconar Veiculo!", "Veiculo não adicionado!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (method == "Edit")
                    {
                        MessageBox.Show("Falha ao Editar Veiculo!", "Veiculo não Salvo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Falha!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopularComboMarca();
        }
        private void PopularComboMarca()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@Flag", SqlDbType = SqlDbType.Int, Value = 1},
                    new SqlParameter(){ParameterName ="@Tipo", SqlDbType = SqlDbType.VarChar, Value = cmbTipo.Text}
                };
                dt = banco.ExecuteProcedureReturnDataTable("dbo.Funcoes_Pesquisa", sp);
                if (dt.Rows.Count > 0)
                {
                    cmbMarca.DataSource = null;
                    cmbMarca.DataSource = dt;
                    cmbMarca.ValueMember = "Tipo";
                    cmbMarca.DisplayMember = "Marca";
                    cmbMarca.SelectedIndex = -1;
                    cmbMarca.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Este tipo de Veiculo não tem marcas cadastradas!", "Falha ao carregas Marcas!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Falha ao carregas Marcas!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            SelecaoGrid();
        }
        private void SelecaoGrid()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                cmbTipo.SelectedIndexChanged -= cmbTipo_SelectedIndexChanged;
                txtPlaca.Enabled = true;
                btnSalvar.Enabled = true;
                btnLimpar.Enabled = true;
                btnNovo.Enabled = false;
                btnExcluir.Enabled = true;
                try
                {
                    string id = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
                    DataTable dt = new DataTable();
                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter(){ParameterName="@idCarro",SqlDbType = SqlDbType.Int, Value = id}
                    };
                    dt = banco.ExecuteProcedureReturnDataTable("dbo.Select_Veiculo_Especifico", sp);
                    if (dt.Rows.Count > 0)
                    {
                        txtId.Text = dt.Rows[0]["ID"].ToString();
                        txtPlaca.Text = dt.Rows[0]["Placa"].ToString();
                        CarregarComboTipo();
                        if (cmbTipo.Items.Count > 0)
                        {

                            cmbTipo.Text = dt.Rows[0]["Tipo"].ToString();
                            cmbTipo.Enabled = true;
                            if (cmbTipo.SelectedIndex != -1)
                            {
                                PopularComboMarca();
                                cmbMarca.Text = dt.Rows[0]["Marca"].ToString();
                                cmbMarca.Enabled = true;
                            }
                        }
                        cmbStatus.SelectedIndex = Convert.ToInt32(dt.Rows[0]["Status"]);
                        cmbTipo.SelectedIndexChanged += cmbTipo_SelectedIndexChanged;
                        dt.Clear();
                        dt = ContadorTicket();
                        if (dt.Rows.Count > 0)
                        {
                            lblTicket.Text = dt.Rows[0]["QTD"].ToString();
                            lblTicket.Visible = true;
                            lblCaptionTicket.Visible = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Falha ao carregar Veiculo!", "Falha!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Falha!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public DataTable ContadorTicket()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@Flag", SqlDbType = SqlDbType.Int, Value = 5},
                    new SqlParameter(){ParameterName ="@Placa", SqlDbType = SqlDbType.VarChar, Value = txtPlaca.Text}
                };
                dt = banco.ExecuteProcedureReturnDataTable("dbo.Funcoes_Pesquisa", sp);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Falha ao carregar Tickets!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView gridview;
            gridview = (DataGridView)sender;
            gridview.ClearSelection();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (txtId.Text == "")
            {
                dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
                SelecaoGrid();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                DataTable dt = new DataTable();
                string mensagem = "Tem Certeza que deseja excluir este veiculo?";
                string titulo = "Excluir Veiculo?";
                bool escolha = (MessageBox.Show(mensagem, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes);
                if (escolha)
                {
                    dt = VerificarTicket();
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Não é possivel excluir este veiculo pois há um Ticket em andamento, encerre-o e tente novamente!", "Falha ao excluir!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        ExcluirVeiculo();
                    }
                }
                dt.Dispose();

            }
            else
            {
                MessageBox.Show("Nenhum Veiculo Selecionado!", "Falha na Exclusão!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExcluirVeiculo()
        {
            try
            {

                string id = txtId.Text;
                List<SqlParameter> sp = new List<SqlParameter>()
                    {
                    new SqlParameter(){ParameterName="@Flag", SqlDbType = SqlDbType.Int, Value = 1},
                    new SqlParameter(){ParameterName="@idCarro",SqlDbType = SqlDbType.Int, Value = id}
                    };
                int LinhasAfetadas = banco.ExecuteProcedureReturnInt("dbo.Gerencia_Veiculo", sp);
                if (LinhasAfetadas > 0)
                {
                    Globais.RegistrarLog(Globais.Login + " Excluiu o Veiculo : " + txtPlaca.Text + ".");
                    MessageBox.Show("Veiculo Excluido com sucesso!", "Exclusão bem sucedida!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PreencherGrid();
                }
                else
                {
                    MessageBox.Show("Veiculo NÃO foi excluido!", "Exclusão mal sucedida!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exclusão mal sucedida!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCaixas();
        }
        private void LimparCaixas()
        {
            cmbTipo.SelectedIndexChanged -= cmbTipo_SelectedIndexChanged;
            txtPlaca.Clear();
            txtPlaca.Enabled = false;
            cmbTipo.SelectedIndex = -1;
            cmbTipo.Enabled = false;
            cmbMarca.SelectedIndex = -1;
            cmbMarca.Enabled = false;
            cmbStatus.SelectedIndex = -1;
            cmbStatus.Enabled = false;
            txtId.Clear();
            btnLimpar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = false;
            btnNovo.Enabled = true;
            lblCaptionTicket.Visible = false;
            lblTicket.Visible = false;
            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void FrmTelaVeiculos_KeyDown(object sender, KeyEventArgs e)
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

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmOp.Placa = dataGridView1.SelectedRows[0].Cells["Placa"].Value.ToString();
            FrmCad.Dispose();
            
        }
    }
}
