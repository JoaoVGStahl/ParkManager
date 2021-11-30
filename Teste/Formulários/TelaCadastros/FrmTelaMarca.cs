using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ParkManager
{
    public partial class FrmTelaMarca : Form
    {
        Banco banco = new Banco();
        FrmTelaOperacao Frm;
        public FrmTelaMarca()
        {
            InitializeComponent();
            this.Frm = (FrmTelaOperacao)Application.OpenForms["FrmTelaOperacao"];
        }   

        private void FrmTelaMarca_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    btnSalvar.PerformClick();
                    break;
                case Keys.F3:
                    btnNovo.PerformClick();
                    break;
                default:
                    break;
            }
        }
        private void FrmTelaMarca_Load(object sender, System.EventArgs e)
        {
            dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;
            CarregarComboTipo();
            CarregarComboFiltro();
            LimparCaixas();
        }
        private void CarregarComboTipo()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = banco.ExecuteProcedureReturnDataTable(NameProcedure: "dbo.ComboBox_Tipo");
                cmbTipo.DataSource = dt;
                cmbTipo.ValueMember = "id_automovel";
                cmbTipo.DisplayMember = "automovel";
                cmbTipo.SelectedIndex = -1;
                cmbFiltro.DataSource = dt;
                cmbFiltro.ValueMember = "id_automovel";
                cmbFiltro.DisplayMember = "automovel";
                cmbFiltro.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Falha ao carregar Tipo de veiculos!");
            }
        }
        private void CarregarComboFiltro()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = banco.ExecuteProcedureReturnDataTable(NameProcedure: "dbo.ComboBox_Tipo");
                cmbFiltro.DataSource = dt;
                cmbFiltro.ValueMember = "id_automovel";
                cmbFiltro.DisplayMember = "automovel";
                cmbFiltro.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Falha ao carregar Filtro de veiculos!");
            }
        }

        private void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarGrid();
            
        }
        private void AtualizarGrid()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@Tipo", SqlDbType = SqlDbType.VarChar, Value = cmbFiltro.Text}
                };
                dt = banco.ExecuteProcedureReturnDataTable(NameProcedure: "dbo.Filtrar_Marcas_Veiculo", sp: sp);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Falha ao Carregar as marcas dos veiculos!");
            }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            AtivarCaixas();
        }
        private void AtivarCaixas()
        {
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            btnLimpar.Enabled = true;
            txtMarca.Enabled = true;
            cmbTipo.Enabled = true;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCaixas();
        }
        private void LimparCaixas()
        {
            txtId.Clear();
            cmbTipo.SelectedIndex = -1;
            txtMarca.Clear();
            txtMarca.Enabled = false;
            cmbTipo.Enabled = false;
            btnSalvar.Enabled = false;
            btnLimpar.Enabled = false;
            btnExcluir.Enabled = false;
            btnNovo.Enabled = true;
        }

        private void txtMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Apenas Letras, Espaço e Backspace
            if (!char.IsLetter(e.KeyChar) && !(e.KeyChar == (char)Keys.Back) && !(e.KeyChar == (char)Keys.Space))
            {
                e.Handled = true;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            ValidarCaixas();
        }
        private void ValidarCaixas()
        {
            if (cmbTipo.SelectedIndex >= 0)
            {
                if (Regex.IsMatch(txtMarca.Text, @"^[A-Za-záàâãéèêíïóôõöúçÁÀÂÃÉÈÍÏÓÔÕÖÚÇ ]+$"))
                {
                    if (txtId.Text == "")
                    {
                        VerificarMarca();
                    }
                    else
                    {
                        SalvarMarca(method: "Edit");
                    }
                }
                else
                {
                    MessageBox.Show("A marca digitada é inválida!", "Falha ao Salvar marca!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleciona um tipo de Veiculo!", "Falha ao salvar marca!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void VerificarMarca()
        {
            DataTable dt = new DataTable();
            try
            {
                
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@Marca", SqlDbType = SqlDbType.VarChar, Value = txtMarca.Text},
                    new SqlParameter(){ParameterName="@Tipo", SqlDbType = SqlDbType.VarChar, Value = cmbTipo.Text}
                };
                dt = banco.ExecuteProcedureReturnDataTable(NameProcedure: "dbo.Pesquisa_Marca_Esp", sp: sp);
                if (dt.Rows.Count > 0)
                {
                    txtMarca.Focus();
                    MessageBox.Show("Marca já cadastrada!", "Falha ao salvar marca!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SalvarMarca();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Falha ao salvar marca!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SalvarMarca(string method = null)
        {
            try
            {
                string procedure;
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@Marca", SqlDbType = SqlDbType.VarChar, Value = txtMarca.Text},
                    new SqlParameter(){ParameterName="@Tipo", SqlDbType = SqlDbType.VarChar, Value = cmbTipo.Text}
                };
                if (method == "Edit")
                {
                    sp.Add(new SqlParameter() { ParameterName = "@idMarca", SqlDbType = SqlDbType.Int, Value = txtId.Text });
                    procedure = "dbo.Alterar_Marca";
                }
                else
                {
                    procedure = "dbo.Adicionar_Marcas";
                }
                int LinhasAfetada = banco.ExecuteProcedureReturnInt(NameProcedure: procedure, sp: sp);
                if (LinhasAfetada > 0)
                {
                    Globais.RegistrarLog(Globais.Login + " Salvou a Marca" + txtMarca.Text+ "com Sucesso.");
                    AtualizarGrid();
                    LimparCaixas();
                    MessageBox.Show("Marca Salva com Sucesso!", "Marca Salva!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Marca NÃO salva!", "Falha ao salvar marca!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Falha ao salvar marca!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            SelecaoGrid();
        }
        private void SelecaoGrid()
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                txtMarca.Enabled = true;
                cmbTipo.Enabled = true;
                btnSalvar.Enabled = true;
                btnLimpar.Enabled = true;
                btnExcluir.Enabled = true;
                btnNovo.Enabled = false;
                try
                {
                    string id = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
                    DataTable dt = new DataTable();
                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter(){ParameterName="@idMarca", SqlDbType = SqlDbType.Int, Value = id}
                    };
                    dt = banco.ExecuteProcedureReturnDataTable(NameProcedure: "Select_Marca_Id", sp: sp);
                    if(dt.Rows.Count > 0)
                    {
                        txtId.Text = dt.Rows[0]["ID"].ToString();
                        cmbTipo.Text = dt.Rows[0]["Tipo"].ToString();
                        txtMarca.Text = dt.Rows[0]["Marca"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Marca NÃO encontrada", "Falha ao encontrar marca!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Falha ao encontrar marca!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            LimparCaixas();
            DataGridView gridview;
            gridview = (DataGridView)sender;
            gridview.ClearSelection();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if(txtId.Text != "")
            {
                string mensagem = "Tem certeza que deseja deletar a marca "+ txtMarca.Text + "?";
                string titulo = "Deletar marca?";
                bool escolha = (MessageBox.Show(mensagem, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes);
                if (escolha)
                {
                    ExcluirMarca();
                } 
            }
            else
            {
                MessageBox.Show("Seleciona uma marca primeiro!", "Falha na exclusão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExcluirMarca()
        {
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@idMarca", SqlDbType = SqlDbType.Int, Value = txtId.Text}
                };
                int Linhas = banco.ExecuteProcedureReturnInt(NameProcedure: "dbo.Deletar_Marca", sp: sp);
                if(Linhas > 0)
                {
                    LimparCaixas();
                    AtualizarGrid();
                    Frm.PopularComboMarca();
                    MessageBox.Show("Marca Deletada com sucesso!", "Marca Deletada!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Marca NÃO deletada!", "Falha!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Falha!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
