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

            for (int i = 0; i < 5; i++)
            {
                PdfPTable tabelaPrimaria = montador.MontaTabelaPrimaria();
                tabelaPrimaria.DefaultCell.Colspan = 3;
                tabelaPrimaria.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tabelaPrimaria.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                tabelaPrimaria.AddCell(new Phrase($"Header Primario {i + 1}", new Font(Font.FontFamily.COURIER, 20)));

                tabelaPrimaria.DefaultCell.Colspan = 0;
                tabelaPrimaria.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                tabelaPrimaria.AddCell(new Phrase($"Header 1"));
                tabelaPrimaria.AddCell(new Phrase($"Header 2"));
                tabelaPrimaria.AddCell(new Phrase($"Header 3"));
                tabelaPrimaria.HeaderRows = 2;

                tabelaPrimaria.AddCell(new Phrase($"Responsavel 1"));
                tabelaPrimaria.AddCell(new Phrase($"Responsavel 2"));
                tabelaPrimaria.AddCell(new Phrase($"Responsavel 3"));

                PdfPTable tabelaSecundaria = montador.MontaTabelaSecundaria();

                PdfPTable tabelaTerciaria = montador.MontaTabelaTerciaria();

                if (montador.EhParaPularParaProximaPagina(tabelaPrimaria, tabelaSecundaria))
                {
                    montador.PulaParaProximaPagina();
                }

                montador.tabelaPrincipal.AddCell(new PdfPCell(tabelaPrimaria));
                montador.tabelaPrincipal.AddCell(new PdfPCell(tabelaSecundaria));
                montador.tabelaPrincipal.AddCell(new PdfPCell(tabelaTerciaria));

                montador.InsereTabelaNoDocumento();
                montador.LimpaTabelaPrincipal();
            }

            montador.FechaDocumento();

            montador.AbrePDF();

        }
    }
}
