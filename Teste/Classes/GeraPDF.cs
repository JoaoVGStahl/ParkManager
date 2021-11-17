using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.IO;

namespace Teste
{
    public  partial class GeraPDF
    {
        public Document pdfDoc;
        public string folderPath = @"c:\ParkManager\ticket\";
        public string filename = "teste.pdf";

        public string TicketEntrada()
        {
            pdfDoc = new Document(PageSize.A4, 1.27f, 1.27f, 20, 80);
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                FileStream stream = new FileStream(folderPath + filename, FileMode.Create);
                stream.Dispose();
                ConstroiPdf();
            }
            catch (Exception)
            {
                throw;
            }
            return folderPath + filename;
        }
        public Font GetMyFont(string font, string path)
        {
            if (!FontFactory.IsRegistered(font))
            {
                var fontpah = @"c:\ParkManager\assets\" + path;
                FontFactory.Register(fontpah, font);
            }
            var fontbold = FontFactory.GetFont(font);
            fontbold.SetStyle(0);
            return fontbold;
        }
        private void InserirImg(string url, float x, float y)
        {
            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(url);
            //Resize image depend upon your need
            jpg.ScaleToFit(75f, 75f);
            //Give space before image
            jpg.SpacingBefore = 10f;
            //Give some space after the image
            jpg.SpacingAfter = 1f;
            //jpg.Alignment = Element.ALIGN_LEFT;
            jpg.SetAbsolutePosition(x, y);
            pdfDoc.Add(jpg);
        }
        private void ConstroiPdf()
        {
            try
            {
                using (FileStream stream = new FileStream(folderPath + filename, FileMode.Append))
                {
                    var w = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    InserirImg(@"c:\ParkManager\assets\CarIcon.png", 45, 765);
                    InserirImg(@"c:\ParkManager\assets\warning.png", 260, 70);

                    Header();

                    PdfContentByte cb = w.DirectContent;
                    BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    cb.SetFontAndSize(bf, 28f);
                    cb.SaveState();
                    cb.BeginText();

                    GeraLabels(cb);

                    cb.SetTextMatrix(145f, 650f);
                    cb.ShowText("João Vitor Girardi Stahl");

                    cb.SetTextMatrix(130f, 585f);
                    cb.ShowText("ABC-1234");


                    cb.SetTextMatrix(120f, 520f);
                    cb.ShowText("Carro");


                    cb.SetTextMatrix(135f, 455f);
                    cb.ShowText("Audi");


                    cb.SetTextMatrix(150f, 390f);
                    cb.ShowText("40");


                    cb.SetTextMatrix(185f, 325f);
                    cb.ShowText("Joao.Girardi");

                    cb.SetTextMatrix(150f, 230f);
                    cb.ShowText("13:40:00 16/11/2021");

                    cb.SetFontAndSize(GetMyFont("Calibri Bold", "calibrib.ttf").BaseFont, 40);

                    AdicionarLinha(cb);

                    cb.EndText();
                    cb.RestoreState();
                    pdfDoc.Close();
                    stream.Dispose();
                    System.Diagnostics.Process.Start(folderPath + filename);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void GeraLabels(PdfContentByte cb)
        {
            cb.SetTextMatrix(45f, 650f);
            cb.ShowText("Cliente:");

            cb.SetTextMatrix(45f, 585f);
            cb.ShowText("Placa:");

            cb.SetTextMatrix(45f, 520f);
            cb.ShowText("Tipo:");

            cb.SetTextMatrix(45f, 455f);
            cb.ShowText("Marca:");

            cb.SetTextMatrix(45f, 390f);
            cb.ShowText("#Ticket:");

            cb.SetTextMatrix(45f, 325f);
            cb.ShowText("Atendente:");

            cb.SetTextMatrix(150f, 45f);
            cb.ShowText("Não deixe seu ticket no carro!");
        }

        private void Header()
        {
            PdfPTable Title = new PdfPTable(1);//Here 1 is Used For Count of Column
            PdfPTable Cnpj = new PdfPTable(1);
            PdfPTable Localizacao = new PdfPTable(1);
            PdfPTable Linha = new PdfPTable(1);

            //pdfTable1.DefaultCell.Padding = 5;
            Title.WidthPercentage = 100;
            Title.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            Title.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            //pdfTable1.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170);
            Title.DefaultCell.BorderWidth = 0;


            //pdfTable1.DefaultCell.Padding = 5;
            Cnpj.WidthPercentage = 100;
            Cnpj.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            Cnpj.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            //pdfTab2e1.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170);
            Cnpj.DefaultCell.BorderWidth = 0;

            Localizacao.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            Localizacao.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            Localizacao.WidthPercentage = 100;
            Localizacao.DefaultCell.BorderWidth = 0;

            Linha.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            Linha.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            Linha.WidthPercentage = 100;
            Linha.DefaultCell.BorderWidth = 0;

            Chunk c1 = new Chunk("", FontFactory.GetFont("Calibri"));
            c1.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
            c1.Font.SetStyle(1);
            c1.Font.Size = 24;

            Phrase p1 = new Phrase();
            p1.Add(c1);
            Title.AddCell(p1);

            Chunk c2 = new Chunk("\nCNPJ: ", FontFactory.GetFont("Bahnschrift SemiCondensed"));
            c2.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);

            c2.Font.SetStyle(0);//0 For Normal Font
            c2.Font.Size = 14;
            Phrase p2 = new Phrase();
            p2.Add(c2);
            Cnpj.AddCell(p2);

            Chunk c3 = new Chunk("Teste", FontFactory.GetFont("Bahnschrift SemiCondensed"));
            c3.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
            c3.Font.SetStyle(0);
            c3.Font.Size = 14;
            Phrase p3 = new Phrase();
            p3.Add(c3);
            Localizacao.AddCell(p3);

            pdfDoc.Add(Title);
            pdfDoc.Add(Cnpj);
            pdfDoc.Add(Localizacao);
        }

        private void AdicionarLinha(PdfContentByte cb)
        {
            cb.SetTextMatrix(0f, 720f);
            cb.ShowText("-------------------------------------------------");

            cb.SetTextMatrix(0f, 142f);
            cb.ShowText("-------------------------------------------------");
        }
    }
}
