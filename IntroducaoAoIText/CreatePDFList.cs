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
    public class CreatePDFList
    {
        public static void Cria()
        {
            string caminho = @"C:\WorkPedro\pdf\list.pdf";
            using FileStream fileStream = new FileStream(caminho, FileMode.Create);
            using PdfWriter writer = new PdfWriter(fileStream);
            using PdfDocument pdf = new PdfDocument(writer);
            using Document document = new Document(pdf);
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            document.Add(new Paragraph("Lista de compras a fazer:").SetFont(font));

            List list = new List()
                .SetSymbolIndent(12)
                .SetListSymbol("\u2022")
                .SetFont(font);

            list.Add(new ListItem("Arroz"))
                .Add(new ListItem("Feijão"))
                .Add(new ListItem("Carne"))
                .Add(new ListItem("Ovos"))
                .Add(new ListItem("Leite"))
                .Add(new ListItem("Maça"))
                .Add(new ListItem("Banana"));

            document.Add(list);
            document.Close();
        }
    }
}
