using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teste
{
    public partial class FrmTelaGeral : Form
    {
        Banco banco = new Banco();
        public FrmTelaGeral()
        {
            InitializeComponent();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtCaminho.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            btnSelecionar.Enabled = true;
            rbFotoAtiv.Enabled = true;
            rbFotoDes.Enabled = true;
            rbCancelaAtiv.Enabled = true;
            rbCancelaDes.Enabled = true;
            rbPdfAtiv.Enabled = true;
            rbPdfDes.Enabled = true;
            btnSalvar.Enabled = true;
        }

        private void rbFotoAtiv_CheckedChanged(object sender, EventArgs e)
        {
            
            
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            
            if(txtCaminho.Text != null)
            {
                try
                {
                    SalvarProps();
                    SalvarBanco();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Erro ao Salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleciona um Diretório Válido!", "Erro ao Salvar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SalvarProps()
        {
            if (rbFotoAtiv.Checked)
            {
                Properties.Settings.Default.Foto = true;
            }
            else
            {
                Properties.Settings.Default.Foto = false;
            }
            if (rbCancelaAtiv.Checked)
            {
                Properties.Settings.Default.Cancelas = true;
            }
            else
            {
                Properties.Settings.Default.Cancelas = false;
            }
            if (rbPdfAtiv.Checked)
            {
                Properties.Settings.Default.GerarPDF = true;
            }
            else
            {
                Properties.Settings.Default.GerarPDF = false;
            }
            Properties.Settings.Default.Save();
        }
        private void SalvarBanco()
        {
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName= "@IdEstacionamento", SqlDbType = SqlDbType.Int, Value = Properties.Settings.Default.IDEstacionamento},
                    new SqlParameter(){ParameterName="@Caminho_Foto", SqlDbType = SqlDbType.VarChar, Value = txtCaminho.Text}
                };
                int result = banco.ExecuteProcedureReturnInt("dbo.Salva_Caminho_Foto", sp);
                if(result > 0)
                {
                    btnSelecionar.Enabled = false;
                    rbFotoAtiv.Enabled = false;
                    rbFotoDes.Enabled = false;
                    rbCancelaAtiv.Enabled = false;
                    rbCancelaDes.Enabled = false;
                    rbPdfAtiv.Enabled = false;
                    rbPdfDes.Enabled = false;
                    btnEditar.Enabled = true;
                    btnSalvar.Enabled = false;
                    MessageBox.Show("Configurações Salvas com sucesso!", "Salvamento concluído!", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                }
                else
                {
                    MessageBox.Show("Erro ao salvar!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmTelaGeral_Load(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.StringBanco != null)
            {
                CarregarRB();
                CarregarBanco();
            }
            else
            {
                MessageBox.Show("Conecte-se a um banco de dados primeiro!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Dispose();
            }
        }
        private void CarregarRB()
        {
            if (Properties.Settings.Default.Foto)
            {
                rbFotoAtiv.Checked = true;
            }
            else
            {
                rbFotoDes.Checked = true;
            }
            if (Properties.Settings.Default.Cancelas)
            {
                rbCancelaAtiv.Checked = true;
            }
            else
            {
                rbCancelaDes.Checked = true;
            }
            if (Properties.Settings.Default.GerarPDF)
            {
                rbPdfAtiv.Checked = true;
            }
            else
            {
                rbPdfDes.Checked = true;
            }
        }
        private void CarregarBanco()
        {
            DataTable dt = new DataTable();
            try
            {
                
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName= "@IdEstacionamento", SqlDbType = SqlDbType.Int, Value = Properties.Settings.Default.IDEstacionamento}
                };
                dt = banco.ExecuteProcedureReturnDataTable("dbo.Geral", sp);
                if(dt.Rows.Count > 0)
                {
                    txtCaminho.Text = dt.Rows[0]["caminho_foto_padrao"].ToString();
                    lblAtivos.Text = dt.Rows[0]["Ativos"].ToString();
                    lblInativos.Text = dt.Rows[0]["Inativos"].ToString();
                    lblTotal.Text = dt.Rows[0]["Total"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmTelaLog Frm = new FrmTelaLog();
            Frm.Show();
        }
    }
}
