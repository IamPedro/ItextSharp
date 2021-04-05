using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ExemploRelatorioConsole
{
    public class MontadorPDF
    {
        private Document doc;
        private const string caminho = @"C:\Users\pedro\Desktop\PDF\study.pdf";
        private PdfWriter writer;
        public PdfPTable tabelaPrincipal;
        public float alturaMaximaDeUmaPagina;

        public MontadorPDF()
        {
            doc = new Document(PageSize.A4);
            doc.SetMargins(40, 40, 40, 80);
            writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));
            tabelaPrincipal = new PdfPTable(1)
            {
                WidthPercentage = 100,
                SplitLate = false,
                TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin,
                LockedWidth = true
            };
            alturaMaximaDeUmaPagina = doc.PageSize.Height - doc.TopMargin - doc.BottomMargin;
        }

        public float GetAlturaMaximaDeUmaPagina()
        {
            return alturaMaximaDeUmaPagina;
        }

        public bool EhParaPularParaProximaPagina()
        {
            var espacoRestanteDaUltimaPagina = writer.GetVerticalPosition(false) - doc.BottomMargin;
            var espacoJaOcupadoNaUltimaPagina = alturaMaximaDeUmaPagina - espacoRestanteDaUltimaPagina;

            if (espacoJaOcupadoNaUltimaPagina > (alturaMaximaDeUmaPagina * 0.80))
            {
                return true;
            }
            return false;
        }

        // Insere a tabela principal no documento,
        // Exclui todas as rows dessa tabela com um loop com o count rows e DeleteLastRow e
        // colocar um doc.NewPage()
        public void PulaParaProximaPagina()
        {
            doc.NewPage();
        }

        public void LimpaTabelaPrincipal()
        {
            while (tabelaPrincipal.Rows.Count > 0)
            {
                tabelaPrincipal.DeleteLastRow();
            }
        }

        public void PulaParaProximaPagina2()
        {
            var remainingPageSpace = writer.GetVerticalPosition(false) - doc.BottomMargin;
            PdfPCell cell = new PdfPCell(new Phrase(string.Empty))
            {
                Border = Rectangle.NO_BORDER,
                FixedHeight = remainingPageSpace,
                //BackgroundColor = BaseColor.BLUE
            };
            tabelaPrincipal.DeleteLastRow();

            tabelaPrincipal.AddCell(cell);

            InsereTabelaNoDocumento();

            var qtdRowsTP = tabelaPrincipal.Rows.Count;           
            tabelaPrincipal.DeleteLastRow();
            qtdRowsTP = tabelaPrincipal.Rows.Count;                        
        }

        public void AbreDocumento()
        {
            doc.Open();
        }

        public void InsereTabelaNoDocumento()
        {            
            doc.Add(tabelaPrincipal);            
        }

        public void FechaDocumento()
        {
            doc.Close();
        }

        public PdfPTable MontaTabelaSecundaria()
        {
            PdfPTable tabelaSecundaria = new PdfPTable(6)
            {
                TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin,
                LockedWidth = true
            };

            return tabelaSecundaria;
        }

        public PdfPTable MontaTabelaTerciaria()
        {
            PdfPTable tabelaTerciaria = new PdfPTable(6)
            {
                TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin,
                LockedWidth = true
            };

            return tabelaTerciaria;
        }

        public void AbrePDF()
        {
            System.Diagnostics.Process.Start(caminho);
        }

    }
}
