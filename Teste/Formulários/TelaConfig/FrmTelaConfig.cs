using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teste
{
    public partial class FrmTelaConfig : Form
    {
        public FrmTelaConfig()
        {
            InitializeComponent();
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
            this.Dispose();
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
            AbreFormParent(3, Frm);

        }

        private void FrmTelaConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Globais.Login == Properties.Settings.Default.UserRoot)
            {

                FrmTelaLogin Frm = new FrmTelaLogin();
                Frm.ShowDialog();
                this.Dispose();
            }
            else
            {
                this.Dispose();
            }

            
        }
        
    }
}
