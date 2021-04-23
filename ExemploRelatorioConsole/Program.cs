using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ExemploRelatorioConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MontadorPDF montador = new MontadorPDF();
            montador.AbreDocumento();

            //for (int i = 0; i < 5; i++)
            //{
            //    montador.MontaTabelaPrimaria(i);

            //    montador.MontaTabelaSecundaria();

            //    montador.MontaTabelaTerciaria();
            //}

            PdfPTable tabelaPrincipal = new PdfPTable(new float[] { 30f, 70f });
            

            for (int i = 0; i < 10; i++)
            {
                PdfPTable tabelaPrimaria = new PdfPTable(new float[] { 30f, 70f });
                PdfPTable tabelaSecundaria = new PdfPTable(new float[] { 35f, 35f, 30f });
                if(i == 2)
                {
                    //tabelaPrimaria.DefaultCell.Colspan = 2;
                    //tabelaPrimaria.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    //tabelaPrimaria.AddCell(new Phrase($"Header Primario {i * 2 + 2}", new Font(Font.FontFamily.COURIER, 20)));

                    //tabelaPrimaria.DefaultCell.Colspan = 0;
                    //tabelaPrimaria.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

                    //PdfPCell cell1 = new PdfPCell(new Phrase(string.Empty));
                    //cell1.DisableBorderSide(Rectangle.RIGHT_BORDER);

                    //PdfPCell cell2 = new PdfPCell(new Phrase($"Header Primario {i * 2 + 2}", new Font(Font.FontFamily.COURIER, 20)));
                    //cell2.DisableBorderSide(Rectangle.LEFT_BORDER);

                    //tabelaPrimaria.AddCell(cell1);
                    //tabelaPrimaria.AddCell(cell2);

                    tabelaPrimaria.AddCell(new PdfPCell(new Phrase(string.Empty)) 
                    { 
                        Border = ~Rectangle.RIGHT_BORDER 
                    });
                    tabelaPrimaria.AddCell(new PdfPCell(new Phrase($"Header Primario {i * 2 + 2}", new Font(Font.FontFamily.COURIER, 20)))
                    {
                        Border = ~Rectangle.LEFT_BORDER,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });

                    //tabelaPrimaria.AddCell(new PdfPCell(new Phrase(string.Empty)) { Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER });
                    //tabelaPrimaria.AddCell(new PdfPCell(new Phrase($"Header Primario {i * 2 + 2}", new Font(Font.FontFamily.COURIER, 20))) { Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER });
                }
                else
                {
                    tabelaPrimaria.AddCell(new PdfPCell(new Phrase($"Header Primario {i * 2 + 1}", new Font(Font.FontFamily.COURIER, 20))) 
                    { 
                        Rotation = 90,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_CENTER
                    });
                    tabelaPrimaria.AddCell(new PdfPCell(new Phrase($"Header Primario {i * 2 + 2}", new Font(Font.FontFamily.COURIER, 20)))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    });
                }

                for (int j = 0; j < 12; j++)
                {
                    tabelaSecundaria.AddCell(new Phrase($"Celula Secundaria {j + 1}", new Font(Font.FontFamily.COURIER, 20)));
                }

                tabelaPrincipal.AddCell(new PdfPCell(tabelaPrimaria));
                tabelaPrincipal.AddCell(new PdfPCell(tabelaSecundaria));                
            }

            montador.InsereTabelaNoDocumento(tabelaPrincipal);

            montador.FechaDocumento();

            montador.AbrePDF();



        }
    }
}
