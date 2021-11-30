using System;
using System.Drawing;
using System.Windows.Forms;

namespace ParkManager
{
    public partial class FrmTelaCadastros : Form
    {
        public FrmTelaCadastros()
        {
            InitializeComponent();
        }

        private void FrmTelaCadastros_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = Globais.Login;
        }

        private void FundoBotao(Button botao)
        {
            btnVeiculos.BackColor = Color.WhiteSmoke;
            btnVeiculos.ForeColor = Color.Black;
            btnCliente.BackColor = Color.WhiteSmoke;
            btnCliente.ForeColor = Color.Black;
            btnMarca.BackColor = Color.WhiteSmoke;
            btnMarca.ForeColor = Color.Black;
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
                    Application.OpenForms[i].Close();
                }
            }
        }
        private void btnVeiculos_Click(object sender, EventArgs e)
        {
            FundoBotao(btnVeiculos);
            FecharFormulariosFilhos();
            FrmTelaVeiculos Frm = new FrmTelaVeiculos();
            AbreFormParent(2, Frm);
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            FundoBotao(btnCliente);
            FecharFormulariosFilhos();
            FrmTelaCliente Frm = new FrmTelaCliente();
            AbreFormParent(2, Frm);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnMarca_Click(object sender, EventArgs e)
        {
            FundoBotao(btnMarca);
            FecharFormulariosFilhos();
            FrmTelaMarca Frm = new FrmTelaMarca();
            AbreFormParent(2, Frm);
        }
    }
}
