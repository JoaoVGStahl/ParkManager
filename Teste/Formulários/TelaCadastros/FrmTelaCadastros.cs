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
    public partial class FrmTelaCadastros: Form
    {
        public FrmTelaCadastros()
        {
            InitializeComponent();
        }

        private void FrmTelaCadastros_Load(object sender, EventArgs e)
        {
            
        }

        private void FundoBotao(Button botao)
        {
            btnVeiculos.BackColor = Color.WhiteSmoke;
            btnCliente.BackColor = Color.WhiteSmoke;
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
    }
}
