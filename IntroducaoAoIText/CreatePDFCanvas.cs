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
    public class CreatePDFCanvas
    {
        public static void Cria()
        {
            string caminho = @"C:\WorkPedro\pdf\canvas.pdf";
            using FileStream fileStream = new FileStream(caminho, FileMode.Create);
            using PdfWriter writer = new PdfWriter(fileStream);
            using PdfDocument pdf = new PdfDocument(writer);
            PageSize ps = PageSize.A4.Rotate();
            PdfPage page = pdf.AddNewPage(ps);
            PdfCanvas canvas = new PdfCanvas(page);

            canvas.ConcatMatrix(1, 0, 0, 1, ps.GetWidth() / 2, ps.GetHeight() / 2);

            Color grayColor = new DeviceCmyk(0f, 0f, 0f, 0.875f);
            canvas.SetLineWidth(3f)
                .SetStrokeColor(grayColor);

            canvas.MoveTo(-(ps.GetWidth() / 2 - 15), 0)
                .LineTo(ps.GetWidth() / 2 - 15, 0)
                .Stroke();

            canvas.SaveState()
                .SetLineJoinStyle(PdfCanvasConstants.LineJoinStyle.ROUND)
                .MoveTo(ps.GetWidth() / 2 - 25, -10)
                .LineTo(ps.GetWidth() / 2 - 15, 0)
                .LineTo(ps.GetWidth() / 2 - 25, 10)
                .Stroke()
                .RestoreState();

            canvas.MoveTo(0, -(ps.GetHeight() / 2 - 15))
                .LineTo(0, ps.GetHeight() / 2 - 15)
                .Stroke();

            canvas.SaveState()
                .SetLineJoinStyle(PdfCanvasConstants.LineJoinStyle.ROUND)
                .MoveTo(-10, ps.GetHeight() / 2 - 25)
                .LineTo(0, ps.GetHeight() / 2 - 15)
                .LineTo(10, ps.GetHeight() / 2 - 25)
                .Stroke()
                .RestoreState();

            for (int i = -((int) ps.GetWidth() / 2 - 61); i < ((int) ps.GetWidth() / 2 - 60); i += 40)
            {
                canvas.MoveTo(i, 5)
                    .LineTo(i, -5);
            }

            for (int j = -((int)ps.GetHeight() / 2 - 57); j < ((int)ps.GetHeight() / 2 - 56); j += 40)
            {
                canvas.MoveTo(5, j)
                    .LineTo(-5, j);
            }

            canvas.Stroke();

            Color blueColor = new DeviceCmyk(1f, 0.156f, 0f, 0.118f);
            canvas.SetLineWidth(0.5f)
                .SetStrokeColor(blueColor);

            for (int i = -((int) ps.GetHeight() / 2 - 57); i < ((int) ps.GetHeight() / 2 - 56); i += 40)
            {
                if(i == 0) continue;
                canvas.MoveTo(-(ps.GetWidth() / 2 - 15), i)
                    .LineTo(-5, i)
                    .MoveTo(5, i)
                    .LineTo(ps.GetWidth() / 2 - 15, i);
            }

            for (int j = -((int)ps.GetWidth() / 2 - 61); j < ((int)ps.GetWidth() / 2 - 60); j += 40)
            {
                if(j == 0) continue;
                canvas.MoveTo(j, -(ps.GetHeight() / 2 - 15))
                    .LineTo(j, -5)
                    .MoveTo(j, 5)
                    .LineTo(j, ps.GetHeight() / 2 - 15);
            }

            canvas.Stroke();

            Color greenColor = new DeviceCmyk(1f, 0f, 1f, 0.176f);
            canvas.SetLineWidth(2f)
                .SetStrokeColor(greenColor)
                .SetLineDash(10, 10, 8)
                .MoveTo(-(ps.GetWidth() / 2 - 15), -(ps.GetHeight() / 2 - 15))
                .LineTo(ps.GetWidth() / 2 - 15, ps.GetHeight() / 2 - 15)
                .Stroke();

            canvas.Stroke();

            PageSize newPageSize = PageSize.A4;
            PdfPage newPage = pdf.AddNewPage(newPageSize);

            float offset = 36;
            float columnWidth = (ps.GetWidth() - offset * 2 + 10) / 3;
            float columnHeigth = ps.GetWidth() - offset * 2;

            pdf.Close();

        }
    }
}
