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
    public class CreatePDFImage
    {
        public static void Cria()
        {
            string caminho = @"C:\WorkPedro\pdf\image.pdf";
            using FileStream fileStream = new FileStream(caminho, FileMode.Create);
            using PdfWriter writer = new PdfWriter(fileStream);
            using PdfDocument pdf = new PdfDocument(writer);
            using Document document = new Document(pdf);

            string caminhoImagem = @"C:\Users\SAMIR-IBRAHIM-EM\Desktop\BELIZE_HOLE.png";
            Image imagem = new Image(ImageDataFactory.Create(caminhoImagem));

            document.Add(imagem);
            document.Close();
        }
    }
}
