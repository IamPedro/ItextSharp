using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroducaoAoIText
{
    public class MyEventHandler : IEventHandler
    {
        public virtual void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdfDoc = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();
            int pageNumber = pdfDoc.GetPageNumber(page);
            Rectangle pageSize = page.GetPageSize();
            PdfCanvas pdfCanvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);

            Color limeColor = new DeviceCmyk(0.208f, 0, 0.584f, 0);
            Color blueColor = new DeviceCmyk(0.445f, 0.0546f, 0, 0.0667f);

            pdfCanvas.SaveState().SetFillColor(pageNumber % 2 == 1 ? limeColor : blueColor).Rectangle(pageSize.GetLeft(),
                pageSize.GetBottom(), pageSize.GetWidth(), pageSize.GetHeight()).Fill().RestoreState();

            pdfCanvas.BeginText().SetFontAndSize(PdfFontFactory.CreateFont(StandardFonts.HELVETICA), 9).MoveText(pageSize.GetWidth() / 2 - 60,
                pageSize.GetTop() - 20).ShowText("MILAGRES SAO REAIS SE VOCE ACREDITA NELES").MoveText(60, -pageSize.GetTop() + 30)
                .ShowText(pageNumber.ToString()).EndText();

            Canvas canvas = new Canvas(pdfCanvas, page.GetPageSize());
            canvas.SetFontColor(ColorConstants.WHITE);
            canvas.SetProperty(Property.FONT_SIZE, UnitValue.CreatePointValue(60));
            canvas.SetProperty(Property.FONT, PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
            canvas.ShowTextAligned(new Paragraph("FALA DELE"), 298, 421, pdfDoc.GetPageNumber(page), TextAlignment.CENTER, VerticalAlignment.MIDDLE, 45);
            pdfCanvas.Release();
        }
    }
}
