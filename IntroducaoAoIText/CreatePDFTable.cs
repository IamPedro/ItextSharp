using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iText.Kernel.Pdf;
using iText;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.IO.Util;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace IntroducaoAoIText
{
    public class CreatePDFTable
    {
        public static void Cria()
        {
            string caminho = @"C:\WorkPedro\pdf\table.pdf";
            using FileStream fileStream = new FileStream(caminho, FileMode.Create);
            using PdfWriter writer = new PdfWriter(fileStream);
            using PdfDocument pdf = new PdfDocument(writer);
            using Document document = new Document(pdf, PageSize.A4.Rotate());
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            document.SetMargins(20, 20, 20, 20);

            string DATA = @"C:\WorkPedro\united_states.csv";
            Table table = new Table(9)
                .UseAllAvailableWidth();
            using StreamReader sr = File.OpenText(DATA);
            string line = sr.ReadLine();
            PopulaTabela(table, line, bold, true);
            while ((line = sr.ReadLine()) != null)
            {
                PopulaTabela(table, line, font, false);
            }

            document.Add(table);

            PageSize newPageSize = PageSize.A4;
            PdfPage newPage = pdf.AddNewPage(newPageSize);

            document.Add(new AreaBreak());
            document.Add(new Paragraph("Teste do tamanho da pagina"));

            document.Close();
        }

        private static void PopulaTabela(Table table, string line, PdfFont font, bool isHeader)
        {
            StringTokenizer tokenizer = new StringTokenizer(line, ";");
            while (tokenizer.HasMoreTokens())
            {
                if (isHeader)
                {
                    table.AddHeaderCell(new Cell().Add(new Paragraph(tokenizer.NextToken()).SetFont(font)));
                }
                else
                {
                    table.AddCell(new Cell().Add(new Paragraph(tokenizer.NextToken()).SetFont(font)));
                }
            }
        }
    }
}
