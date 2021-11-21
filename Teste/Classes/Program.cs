using System;
using System.IO;
using System.Windows.Forms;

namespace Teste
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            VerificaDiretorio();
            Application.Run(new FrmTelaLogin());
        }
        static void VerificaDiretorio()
        {
            if (!Directory.Exists(Properties.Settings.Default.ArquivoAuditoria))
            {
                Directory.CreateDirectory(Properties.Settings.Default.ArquivoAuditoria);
            }
            if (!Directory.Exists(@"C:\ParkManager\ticket"))
            {
                Directory.CreateDirectory(@"C:\ParkManager\ticket");
            }
            if (!Directory.Exists(@"C:\ParkManager\assets"))
            {
                Directory.CreateDirectory(@"C:\ParkManager\assets");
            }
            if (!Directory.Exists(@"C:\ParkManager\fotos"))
            {
                Directory.CreateDirectory(@"C:\ParkManager\fotos");
            }
        }
    }
}
