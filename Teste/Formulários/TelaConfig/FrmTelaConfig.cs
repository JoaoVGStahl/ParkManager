using System;
using System.Drawing;
using System.Windows.Forms;

namespace Teste
{
    public partial class FrmTelaConfig : Form
    {
        Form frm;
        public FrmTelaConfig(Form Frm)
        {
            InitializeComponent();
            this.frm = Frm;
        }
        private void FundoBotao(Button botao)
        {
            btnGeral.BackColor = Color.WhiteSmoke;
            btnGeral.ForeColor = Color.Black;
            btnEstacionamento.BackColor = Color.WhiteSmoke;
            btnEstacionamento.ForeColor = Color.Black;
            btnPrecos.BackColor = Color.WhiteSmoke;
            btnPrecos.ForeColor = Color.Black;
            btnUsuarios.BackColor = Color.WhiteSmoke;
            btnUsuarios.ForeColor = Color.Black;
            btnDev.BackColor = Color.WhiteSmoke;
            btnDev.ForeColor = Color.Black;
            botao.BackColor = Color.DarkBlue;
            botao.ForeColor = Color.White;
        }
        private void AbreFormParent(int nivel, Form Frm)
        {
            if (Properties.Settings.Default["StringBanco"].ToString() != null)
            {
                if (Globais.Nivel >= nivel)
                {
                    Frm.MdiParent = this;
                    Frm.Dock = DockStyle.Fill;
                    Frm.Show();
                }
                else
                {
                    MessageBox.Show("Seu usuário não tem permissão para acessar está area!", "Acesso Negado!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Conecte-se a um banco de dados primeiro!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FecharFormulariosFilhos()
        {
            // percorre todos os formulários abertos
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                // se o formulário for filho
                if (Application.OpenForms[i].IsMdiChild)
                {
                    // fecha o formulário
                    Application.OpenForms[i].Dispose();
                }
            }
        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrmTelaConfig_Load(object sender, EventArgs e)
        {
            if (Globais.Login == Properties.Settings.Default.UserRoot)
            {
                btnDev.Visible = true;
                btnDev.PerformClick();
            }
            lblUsuario.Text = Globais.Login;
        }
        private void btnGeral_Click(object sender, EventArgs e)
        {
            FundoBotao(btnGeral);

            FecharFormulariosFilhos();
            FrmTelaGeral Frm = new FrmTelaGeral();
            AbreFormParent(2, Frm);
        }
        private void btnEstacionamento_Click(object sender, EventArgs e)
        {
            FundoBotao(btnEstacionamento);

            FecharFormulariosFilhos();
            FrmTelaEstacionamento Frm = new FrmTelaEstacionamento();
            AbreFormParent(2, Frm);
        }
        private void btnPrecos_Click(object sender, EventArgs e)
        {
            FundoBotao(btnPrecos);

            FecharFormulariosFilhos();
            FrmTelaFinanceiro Frm = new FrmTelaFinanceiro();
            AbreFormParent(2, Frm);
        }
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            FundoBotao(btnUsuarios);

            FecharFormulariosFilhos();
            FrmTelaUsuario Frm = new FrmTelaUsuario();
            AbreFormParent(2, Frm);
        }
        private void btnDev_Click(object sender, EventArgs e)
        {
            FundoBotao(btnDev);
            FecharFormulariosFilhos();
            FrmTelaDesenvolvedor Frm = new FrmTelaDesenvolvedor();
            Frm.MdiParent = this;
            Frm.Dock = DockStyle.Fill;
            Frm.Show();
        }
        private void FrmTelaConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            frm.Show();
        }
    }
}
