using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teste
{
    public partial class FrmTelaLog : Form
    {
        public FrmTelaLog()
        {
            InitializeComponent();
            ImprimirLogs();
        }
        private void ImprimirLogs()
        {
            DirectoryInfo di = new DirectoryInfo(Properties.Settings.Default["ArquivoAuditoria"].ToString());

            foreach (FileInfo file in di.GetFiles())
            {
                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    string line;
                    string decode;
                    while ((line = sr.ReadLine()) != null)
                    {
                        decode = Globais.DecodeToString(line);

                        txtLog.Text += decode + Environment.NewLine;
                    }
                   
                }
            }
        }
    }
}
