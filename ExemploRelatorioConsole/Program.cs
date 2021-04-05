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

            PdfPTable tabelaSecundaria = montador.MontaTabelaSecundaria();

            int i = 0;
            for (; i < 121; i++)
            {
                tabelaSecundaria.AddCell(new Phrase($"Linha {i + 1}, Coluna 1"));
                tabelaSecundaria.AddCell(new Phrase($"Linha {i + 1}, Coluna 2"));
                tabelaSecundaria.AddCell(new Phrase($"Linha {i + 1}, Coluna 3"));
                tabelaSecundaria.AddCell(new Phrase($"Linha {i + 1}, Coluna 4"));
                tabelaSecundaria.AddCell(new Phrase($"Linha {i + 1}, Coluna 5"));
                tabelaSecundaria.AddCell(new Phrase($"Linha {i + 1}, Coluna 6"));
            }

            montador.tabelaPrincipal.AddCell(new PdfPCell(tabelaSecundaria));
            montador.InsereTabelaNoDocumento();
            montador.LimpaTabelaPrincipal();

            if (montador.EhParaPularParaProximaPagina()) 
            { 
                montador.PulaParaProximaPagina(); 
            } 

            PdfPTable tabelaTerciaria = montador.MontaTabelaTerciaria();

            for (; i < 210; i++)
            {
                tabelaTerciaria.AddCell(new Phrase($"Linha {i + 1}, Coluna 1"));
                tabelaTerciaria.AddCell(new Phrase($"Linha {i + 1}, Coluna 2"));
                tabelaTerciaria.AddCell(new Phrase($"Linha {i + 1}, Coluna 3"));
                tabelaTerciaria.AddCell(new Phrase($"Linha {i + 1}, Coluna 4"));
                tabelaTerciaria.AddCell(new Phrase($"Linha {i + 1}, Coluna 5"));
                tabelaTerciaria.AddCell(new Phrase($"Linha {i + 1}, Coluna 6"));
            }

            montador.tabelaPrincipal.AddCell(new PdfPCell(tabelaTerciaria));

            montador.InsereTabelaNoDocumento();
            montador.LimpaTabelaPrincipal();
            montador.FechaDocumento();

            montador.AbrePDF();

        }
    }
}
