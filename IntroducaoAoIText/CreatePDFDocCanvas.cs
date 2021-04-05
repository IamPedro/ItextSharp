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
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace IntroducaoAoIText
{
    public static class CreatePDFDocCanvas
    {
        public static void Cria()
        {
            string caminho = @"C:\WorkPedro\pdf\docCanvas.pdf";
            using FileStream fileStream = new FileStream(caminho, FileMode.Create);
            using PdfWriter writer = new PdfWriter(fileStream);
            using PdfDocument pdf = new PdfDocument(writer);
            PageSize ps = PageSize.A5;
            Document document = new Document(pdf, ps);

            float offset = 36;
            float columnWidth = (ps.GetWidth() - offset * 2 + 10) / 3;
            float columnHeigth = ps.GetHeight() - offset * 2;

            Rectangle[] columns =
            {
                new Rectangle(offset - 5, offset, columnWidth, columnHeigth),
                new Rectangle(offset + columnWidth, offset, columnWidth, columnHeigth),
                new Rectangle(offset + columnHeigth * 2 + 5, offset, columnWidth, columnHeigth)
            };

            document.SetRenderer(new ColumnDocumentRenderer(document, columns));

            string caminhoImagem = @"C:\Users\SAMIR-IBRAHIM-EM\Desktop\BELIZE_HOLE.png";
            Image imagem = new Image(ImageDataFactory.Create(caminhoImagem)).SetWidth(columnWidth);

            string conteudo = @"Integer ac nulla et quam tristique viverra. 
                                Integer et hendrerit neque. Maecenas aliquet elementum massa non placerat. 
                                Etiam sed vestibulum velit, eu consequat neque. 
                                Vestibulum tempus mollis lectus at malesuada. 
                                Proin ac iaculis sem. Integer nec dapibus mauris, sed congue turpis. 
                                Pellentesque consectetur molestie ullamcorper. 
                                Nam scelerisque, quam ut vulputate sagittis, sapien sapien pharetra justo, tempor hendrerit nibh nisl sed massa. 
                                In eget condimentum velit. Nam congue magna a scelerisque iaculis. 
                                Nam id odio et enim convallis fermentum et non enim.\n\n";

            document.Add(imagem);
            document.Add(new Paragraph(conteudo));

            document.Close();


        }
    }
}
