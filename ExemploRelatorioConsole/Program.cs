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
                montador.MontaTabelaPrimaria(i);

                montador.MontaTabelaSecundaria();

                montador.MontaTabelaTerciaria();
            }

            montador.FechaDocumento();

            montador.AbrePDF();

        }
    }
}
