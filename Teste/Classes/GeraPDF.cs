using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;

namespace Teste
{
    class GeraPDF
    {
        private string nome { get; set; }

        private DataTable dados { get; set; }

        public DataTable Dados
        {
            get { return dados; }
            set { dados = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        private void GerarPDF(string tipo)
        {
            Document pdf = new Document(PageSize.A4);
            pdf.SetMargins(40, 40, 40, 80);
            pdf.AddCreationDate();
            string caminho = @"C:\ParkManager\" + tipo + @"\" + nome + ".pdf";
            PdfWriter write = PdfWriter.GetInstance(pdf, new FileStream(caminho, FileMode.Create));
            Header(pdf);
        }
        private void Header(Document pdf)
        {
            pdf.Open();
            Paragraph p = new Paragraph("teste", new Font(Font.NORMAL, 14));
            p.Alignment = Element.ALIGN_CENTER;
            p.Add("Teste");
            pdf.Close();
            Main(pdf);
        }
        private void Main(Document pdf)
        {
            switch (nome)
            {
                case "Relatório":
                    //PdfRelatorio();
                    break;
                case "IniciaTicket":
                    //PdfIniciaTicket();
                    break;
                case "EncerraTicket":
                    //PdfEncerraTicket();
                    break;
                default:
                    break;
            }
        }
        private void Footer()
        {

        }
        
    }
}
