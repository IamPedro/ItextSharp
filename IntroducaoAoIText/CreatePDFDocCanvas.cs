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
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Annot;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Renderer;

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

            //float offset = 36;
            //float columnWidth = (ps.GetWidth() - offset * 2 + 10) / 3;
            //float columnHeigth = ps.GetHeight() - offset * 2;

            //Rectangle[] columns =
            //{
            //    new Rectangle(offset - 5, offset, columnWidth, columnHeigth),
            //    new Rectangle(offset + columnWidth, offset, columnWidth, columnHeigth),
            //    new Rectangle(offset + columnHeigth * 2 + 5, offset, columnWidth, columnHeigth)
            //};
            //document.RenderizadorDeColunas(3, 5);

            //document.SetRenderer(document.RenderizadorDeColunas(3, 5));
            //float alturaDaColuna = document.AlturaDaColuna();
            //float larguraDaColuna = document.LarguraDaColuna(3, 5);
            //string caminhoImagem = @"C:\Users\SAMIR-IBRAHIM-EM\Desktop\Estagio\BELIZE_HOLE.png";
            //Image imagem = new Image(ImageDataFactory.Create(caminhoImagem)).SetWidth(larguraDaColuna).SetHeight(alturaDaColuna);

            //string conteudo = @"Integer ac nulla et quam tristique viverra. 
            //                    Integer et hendrerit neque. Maecenas aliquet elementum massa non placerat. 
            //                    Etiam sed vestibulum velit, eu consequat neque. 
            //                    Vestibulum tempus mollis lectus at malesuada. 
            //                    Proin ac iaculis sem. Integer nec dapibus mauris, sed congue turpis. 
            //                    Pellentesque consectetur molestie ullamcorper. 
            //                    Nam scelerisque, quam ut vulputate sagittis, sapien sapien pharetra justo, tempor hendrerit nibh 
            //                    nisl sed massa. In eget condimentum velit. Nam congue magna a scelerisque iaculis. 
            //                    Nam id odio et enim convallis fermentum et non enim.";

            //string conteudo2 = @"Sagittis primis neque volutpat convallis sodales vulputate ac leo, 
            //                    lectus quis maecenas orci posuere netus ad etiam, dictumst varius porta 
            //                    senectus varius morbi eleifend. mi lectus vivamus sapien hac quisque vitae lacinia tempor, 
            //                    consectetur ullamcorper morbi curae est nisi fusce pharetra, purus sodales a cras cursus
            //                    convallis interdum. molestie pharetra luctus phasellus nullam convallis lacus tellus massa augue, 
            //                    aenean condimentum nec accumsan nec sit tristique a orci, a metus tortor mollis senectus tincidunt 
            //                    hendrerit venenatis. litora blandit nullam suspendisse ut potenti primis tempus tristique mi 
            //                    arcu sagittis rhoncus interdum, curabitur elit aliquam consequat sociosqu pellentesque himenaeos 
            //                    platea per neque rhoncus aliquet.";

            //document.Add(imagem);
            //document.Add(new AreaBreak());
            //document.Add(new Paragraph(conteudo));
            //document.Add(new AreaBreak());
            //document.Add(new Paragraph(conteudo2));

            document.Add(new Paragraph("Exemplo de anotação"));

            PdfAnnotation annotation = new PdfTextAnnotation(new Rectangle(20, 800, 0, 0))
                .SetOpen(true)
                .SetColor(ColorConstants.GREEN)
                .SetTitle(new PdfString("iText"))
                .SetContents("With iText, you can truly take your documentation needs to the next level.");
            pdf.GetFirstPage().AddAnnotation(annotation);
            //pdf.GetLastPage().AddAnnotation(annotation);

            PdfLinkAnnotation linkAnnotation = ((PdfLinkAnnotation)new PdfLinkAnnotation(new Rectangle(0, 0))
                .SetAction(PdfAction.CreateURI("https://itextpdf.com/")));

            Link link = new Link("aqui", linkAnnotation);
            Paragraph paragraph = new Paragraph("Exemplo de anotação com link. Clique ")
                .Add(link.SetUnderline())
                .Add(" para saber mais!");

            document.Add(paragraph);

            document.Close();


        }

        public static DocumentRenderer RenderizadorDeColunas(this Document documento, int quantidadeDeColunas, float espacoEntreColunas)
        {
            PageSize tamanhoDaPagina = documento.GetPdfDocument().GetDefaultPageSize();
            float larguraDaColuna = (tamanhoDaPagina.GetWidth() - documento.GetLeftMargin() - documento.GetRightMargin()
                - (espacoEntreColunas * (quantidadeDeColunas - 1))) / quantidadeDeColunas;
            float alturaDaColuna = tamanhoDaPagina.GetHeight() - documento.GetBottomMargin() - documento.GetTopMargin();
            Rectangle[] colunas = new Rectangle[quantidadeDeColunas];
            for (int i = 0; i < quantidadeDeColunas; i++)
            {
                colunas[i] = new Rectangle(documento.GetLeftMargin() + ((larguraDaColuna + espacoEntreColunas) * i),
                    documento.GetBottomMargin(), larguraDaColuna, alturaDaColuna);
            }
            return new ColumnDocumentRenderer(documento, false, colunas);
        }

        public static float AlturaDaColuna(this Document documento)
        {
            PageSize tamanhoDaPagina = documento.GetPdfDocument().GetDefaultPageSize();
            float alturaDaColuna = tamanhoDaPagina.GetHeight() - documento.GetBottomMargin() - documento.GetTopMargin();

            return alturaDaColuna;
        }

        public static float LarguraDaColuna(this Document documento, int quantidadeDeColunas, float espacoEntreColunas)
        {
            PageSize tamanhoDaPagina = documento.GetPdfDocument().GetDefaultPageSize();
            float larguraDaColuna = (tamanhoDaPagina.GetWidth() - documento.GetLeftMargin() - documento.GetRightMargin()
                                     - (espacoEntreColunas * (quantidadeDeColunas - 1))) / quantidadeDeColunas;

            return larguraDaColuna;
        }
    }
}
