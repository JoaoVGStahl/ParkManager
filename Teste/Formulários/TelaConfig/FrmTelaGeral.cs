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

namespace ParkManager
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
            if (!(Properties.Settings.Default.Foto.Equals(rbFotoAtiv.Checked)))
            {

                if (rbFotoAtiv.Checked)
                {
                    Globais.RegistrarLog(Globais.Login + " Ativou a captura de fotos.");
                    Properties.Settings.Default.Foto = true;
                }
                else
                {
                    Globais.RegistrarLog(Globais.Login + " Desativou a captura de fotos.");
                    Properties.Settings.Default.Foto = false;
                }
                
            }
            if (!(Properties.Settings.Default.Cancelas.Equals(rbCancelaAtiv.Checked)))
            {
                if (rbCancelaAtiv.Checked)
                {
                    Globais.RegistrarLog(Globais.Login + " Ativou o gerenciamento de cancelas.");
                    Properties.Settings.Default.Cancelas = true;
                }
                else
                {
                    Globais.RegistrarLog(Globais.Login + " Desativou o gerenciamento de cancelas.");
                    Properties.Settings.Default.Cancelas = false;
                }
                
            }
            if (!(Properties.Settings.Default.GerarPDF.Equals(rbPdfAtiv.Checked)))
            {
                if (rbPdfAtiv.Checked)
                {
                    Globais.RegistrarLog(Globais.Login + " Ativou a geração de Pdf");
                    Properties.Settings.Default.GerarPDF = true;
                }
                else
                {
                    Globais.RegistrarLog(Globais.Login + " Desativou a geração de Pdf");
                    Properties.Settings.Default.GerarPDF = false;
                }
                
            }
            if(Estacionamento.caminho_foto_padrao != txtCaminho.Text)
            {
                Globais.RegistrarLog(Globais.Login + " Alterou o caminho do diretório de Imagen de " + Estacionamento.caminho_foto_padrao + " para " + txtCaminho.Text + ".");
                Estacionamento.caminho_foto_padrao = txtCaminho.Text;
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
                btnEditar.Enabled = false;
                MessageBox.Show("Conecte-se a um banco de dados primeiro!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
