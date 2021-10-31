using System.Windows.Forms;

namespace Teste
{
    public partial class FrmTelaMarca : Form
    {
        public FrmTelaMarca()
        {
            InitializeComponent();
        }

        private void FrmTelaMarca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {

            }
        }
    }
}
