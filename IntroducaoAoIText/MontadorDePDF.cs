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
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace IntroducaoAoIText
{
    public class MontadorDePDF
    {
        public static void Main(string[] args)
        {
            string[] pathPdfStrings =
            {
                @"C:\WorkPedro\pdf\list.pdf",
                @"C:\WorkPedro\pdf\image.pdf",
                @"C:\WorkPedro\pdf\table.pdf",
                @"C:\WorkPedro\pdf\canvas.pdf",
                @"C:\WorkPedro\pdf\docCanvas.pdf"
            };
            string caminho = @"C:\WorkPedro\pdf\pdfsCombinados.pdf";

            CreatePDFList.Cria();
            CreatePDFImage.Cria();
            CreatePDFTable.Cria();
            CreatePDFCanvas.Cria();
            CreatePDFDocCanvas.Cria();

            CombineMultiplesPDFs(pathPdfStrings, caminho);

            System.Diagnostics.Process.Start(caminho);
        }

        private static void CombineMultiplesPDFs(string[] pathPdfStrings, string caminho)
        {
            using FileStream fileStream = new FileStream(caminho, FileMode.Create);
            using PdfWriter writer = new PdfWriter(fileStream);
            using PdfDocument pdf = new PdfDocument(writer);
            PdfMerger pdfMerger = new PdfMerger(pdf);

            foreach (var fileNamePdfString in pathPdfStrings)
            {
                using PdfDocument newPdfDocument = new PdfDocument(new PdfReader(fileNamePdfString));
                pdfMerger.Merge(newPdfDocument, 1, newPdfDocument.GetNumberOfPages());
                newPdfDocument.Close();
            }

            pdfMerger.Close();
        }
    }
}
