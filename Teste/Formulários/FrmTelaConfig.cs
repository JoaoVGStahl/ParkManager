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

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            btnDev.Visible = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            
        }

        private void FrmTelaConfig_Load(object sender, EventArgs e)
        {
            if(Globais.Login == Properties.Settings.Default.UserRoot)
            {
                btnDev.Visible = true;
            }
            lblUsuario.Text = Globais.Login;
        }
        private void FundoBotao()
        {
            btnGeral.BackColor = Color.WhiteSmoke;
            btnEstacionamento.BackColor = Color.WhiteSmoke;
            btnPrecos.BackColor = Color.WhiteSmoke;
            btnUsuarios.BackColor = Color.WhiteSmoke;
            btnDev.BackColor = Color.WhiteSmoke;
        }

        private void btnGeral_Click(object sender, EventArgs e)
        {
            FundoBotao();
            btnGeral.BackColor = Color.DarkBlue;
        }

        private void btnEstacionamento_Click(object sender, EventArgs e)
        {
            FundoBotao();
            btnEstacionamento.BackColor = Color.DarkBlue;
        }

        private void btnPrecos_Click(object sender, EventArgs e)
        {
            FundoBotao();
            btnPrecos.BackColor = Color.DarkBlue;
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            FundoBotao();
            btnUsuarios.BackColor = Color.DarkBlue;
        }

        private void btnDev_Click(object sender, EventArgs e)
        {
            FundoBotao();
            btnDev.BackColor = Color.DarkBlue;
        }
    }
}
