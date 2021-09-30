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
    public partial class FrmTelaVeiculos : Form
    {
        Banco banco = new Banco();
        public FrmTelaVeiculos()
        {
            InitializeComponent();
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
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@Flag", SqlDbType = SqlDbType.Int, Value = 15}
                };
                dt = banco.InsertData("dbo.Funcoes_Pesquisa", sp);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;
            }
            catch (Exception)
            {

                throw;
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
                dt = banco.InsertData("dbo.Funcoes_Pesquisa", sp);
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
            string caracterespermitidos = "ABCDEFGHIJ0123456789";
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
            if (txtId.Text == "")
            {
                SalvarVeiculo();
            }
            else
            {
                VerificarVeiculo(); 
            }
            LimparCaixas();
        }
        private void VerificarVeiculo()
        {
            try
            {
                DataTable dt = new DataTable();
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@Flag",SqlDbType = SqlDbType.Int, Value = 17},
                    new SqlParameter(){ParameterName="@idCarro", SqlDbType = SqlDbType.Int, Value = Convert.ToInt32(txtId.Text)}
                };
                dt = banco.InsertData("dbo.Funcoes_Pesquisa", sp);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Este veiculo possui um Ticket em aberto! \nEncerre-o e tente novamente!", "Falha ao editar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    EditarVeiculo();
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Falha ao editar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SalvarVeiculo()
        {
            int LinhasAfetadas;
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
                LinhasAfetadas = banco.EditData("dbo.Gerencia_Veiculo", sp);
                if (LinhasAfetadas > 0)
                {
                    MessageBox.Show("Veiculo Adicionado com Sucesso!", "Veiculo Salvo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PreencherGrid();
                    
                }
                else
                {
                    MessageBox.Show("Falha ao Adiconar Veiculo!", "Veiculo não adicionado!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Falha ao Adiconar Veiculo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EditarVeiculo()
        {
            int LinhaAfetadas;
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName = "@Flag", SqlDbType = SqlDbType.Int, Value = 1},
                    new SqlParameter(){ParameterName = "@idCarro", SqlDbType = SqlDbType.Int, Value = txtId.Text},
                    new SqlParameter(){ParameterName = "@Placa", SqlDbType = SqlDbType.VarChar, Value = txtPlaca.Text },
                    new SqlParameter(){ParameterName ="@Tipo", SqlDbType = SqlDbType.VarChar, Value = cmbTipo.Text},
                    new SqlParameter(){ParameterName="@Marca", SqlDbType = SqlDbType.VarChar, Value = cmbMarca.Text},
                    new SqlParameter(){ParameterName="@Status", SqlDbType = SqlDbType.Int, Value = cmbStatus.SelectedIndex}
                };
                LinhaAfetadas = banco.EditData("dbo.Gerencia_Veiculo", sp);
                if (LinhaAfetadas > 0)
                {

                    MessageBox.Show("Veiculo Alterado com sucesso!", "Alteração concluida!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PreencherGrid();
                    
                }
                else
                {
                    MessageBox.Show("Houve um problema e não foi realizado a alteração!", "Falha ao realizar alteração!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Falha ao realizar alteração!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dt = banco.InsertData("dbo.Funcoes_Pesquisa", sp);
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
                try
                {

                    string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    DataTable dt = new DataTable();
                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                    new SqlParameter(){ParameterName="@Flag", SqlDbType = SqlDbType.Int, Value = 16},
                    new SqlParameter(){ParameterName="@idCarro",SqlDbType = SqlDbType.Int, Value = id}
                    };
                    dt = banco.InsertData("dbo.Funcoes_Pesquisa", sp);
                    if (dt.Rows.Count > 0)
                    {

                        txtId.Text = dt.Rows[0].ItemArray[0].ToString();
                        txtPlaca.Text = dt.Rows[0].ItemArray[1].ToString();
                        CarregarComboTipo();
                        if(cmbTipo.Items.Count > 0)
                        {

                            cmbTipo.Text = dt.Rows[0].ItemArray[2].ToString();
                            cmbTipo.Enabled = true;
                            if(cmbTipo.SelectedIndex != -1)
                            {
                                PopularComboMarca();
                                cmbMarca.Text = dt.Rows[0].ItemArray[3].ToString();
                                cmbMarca.Enabled = true;
                                cmbTipo.SelectedIndexChanged += cmbTipo_SelectedIndexChanged;
                            }
                        }
                        cmbStatus.SelectedIndex = Convert.ToInt32(dt.Rows[0].ItemArray[4].ToString());
                        cmbStatus.Enabled = true;
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
            btnNovo.Enabled = false;
            btnLimpar.Enabled = true;
            btnSalvar.Enabled = false;
            if(txtId.Text != "")
            {
                
            }
            else
            {
                MessageBox.Show("Selecione um veiculo ao lado para Excluir!","Falha na Exclusão!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCaixas();
        }
        private void LimparCaixas(){
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
            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;

        }
    }
}
