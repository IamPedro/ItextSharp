using System;
using System.IO;
using System.Linq;
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

        public bool EhParaPularParaProximaPagina(PdfPTable tabelaPrimaria, PdfPTable tabelaSecundaria)
        {
            var espacoRestanteDaUltimaPagina = writer.GetVerticalPosition(false) - doc.BottomMargin;
            var somaDaAlturaDaTabelaPrimariaComTituloMaisUmaLinhaDaTabelaSecundaria
                = tabelaPrimaria.TotalHeight + (tabelaSecundaria.GetRowHeight(0) + tabelaSecundaria.GetRowHeight(1));

            //var espacoJaOcupadoNaUltimaPagina = alturaMaximaDeUmaPagina - espacoRestanteDaUltimaPagina;

            if (somaDaAlturaDaTabelaPrimariaComTituloMaisUmaLinhaDaTabelaSecundaria
                > espacoRestanteDaUltimaPagina)
            {
                return true;
            }
            return false;
        }

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

        public PdfPTable MontaTabelaPrimaria()
        {
            PdfPTable tabelaPrimaria = new PdfPTable(3)
            {
                TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin,
                LockedWidth = true
            };

            return tabelaPrimaria;
        }

        public PdfPTable MontaTabelaSecundaria()
        {
            PdfPTable tabelaSecundaria = new PdfPTable(6)
            {
                TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin,
                LockedWidth = true
            };

            tabelaSecundaria.DefaultCell.Colspan = 6;
            tabelaSecundaria.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabelaSecundaria.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            tabelaSecundaria.AddCell(new Phrase($"Header Secundario", new Font(Font.FontFamily.COURIER, 15)));
            tabelaSecundaria.HeaderRows = 1;

            tabelaSecundaria.DefaultCell.Colspan = 0;
            tabelaSecundaria.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;

            for (int j = 0; j < 114; j++)
            {
                tabelaSecundaria.AddCell(new Phrase($"Linha {j + 1}, Coluna 1"));
                tabelaSecundaria.AddCell(new Phrase($"Linha {j + 1}, Coluna 2"));
                tabelaSecundaria.AddCell(new Phrase($"Linha {j + 1}, Coluna 3"));
                tabelaSecundaria.AddCell(new Phrase($"Linha {j + 1}, Coluna 4"));
                tabelaSecundaria.AddCell(new Phrase($"Linha {j + 1}, Coluna 5"));
                tabelaSecundaria.AddCell(new Phrase($"Linha {j + 1}, Coluna 6"));
            }

            return tabelaSecundaria;
        }

        public PdfPTable MontaTabelaTerciaria()
        {
            PdfPTable tabelaTerciaria = new PdfPTable(6)
            {
                TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin,
                LockedWidth = true
            };

            tabelaTerciaria.DefaultCell.Colspan = 6;
            tabelaTerciaria.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabelaTerciaria.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
            tabelaTerciaria.AddCell(new Phrase($"Header Terciario", new Font(Font.FontFamily.COURIER, 15)));
            tabelaTerciaria.HeaderRows = 1;

            tabelaTerciaria.DefaultCell.Colspan = 0;
            tabelaTerciaria.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;

            for (int j = 114; j < 219; j++)
            {
                tabelaTerciaria.AddCell(new Phrase($"Linha {j + 1}, Coluna 1"));
                tabelaTerciaria.AddCell(new Phrase($"Linha {j + 1}, Coluna 2"));
                tabelaTerciaria.AddCell(new Phrase($"Linha {j + 1}, Coluna 3"));
                tabelaTerciaria.AddCell(new Phrase($"Linha {j + 1}, Coluna 4"));
                tabelaTerciaria.AddCell(new Phrase($"Linha {j + 1}, Coluna 5"));
                tabelaTerciaria.AddCell(new Phrase($"Linha {j + 1}, Coluna 6"));
            }

            return tabelaTerciaria;
        }

        public void AbrePDF()
        {
            System.Diagnostics.Process.Start(caminho);
        }

        public PdfPTable CriaTabelaCustomizada(int qtdColunasDinamicas)
        {
            int qtdColunasFixas = 8;
            int qtdTotalColunas = qtdColunasFixas + qtdColunasDinamicas;

            return new PdfPTable(ObtemLarguras(qtdTotalColunas, qtdColunasDinamicas)) 
            {
                WidthPercentage = 100,
                TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin
            };
        }

        public float[] ObtemLarguras(int qtdTotalColunas, int qtdColunasDinamicas)
        {
            float[] largurasColunas = new float[qtdTotalColunas];

            DefineLarguraColunasFixas(largurasColunas, qtdTotalColunas);

            DefineLarguraColunasDinamicas(largurasColunas, qtdColunasDinamicas);

            DistribuiLarguraRestanteDaTabela(largurasColunas);

            return largurasColunas;
        }

        private void DefineLarguraColunasFixas(float[] largurasColunas, int qtdTotalColunas)
        {
            largurasColunas[0] = 3;
            largurasColunas[1] = 13;
            largurasColunas[2] = 8;
            largurasColunas[3] = 8;
            largurasColunas[4] = 7;
            largurasColunas[qtdTotalColunas - 3] = 5;
            largurasColunas[qtdTotalColunas - 2] = 8;
            largurasColunas[qtdTotalColunas - 1] = 5;
        }


        private void DefineLarguraColunasDinamicas(float[] largurasColunas, int qtdColunasDinamicas)
        {
            var larguraRestanteDaTabela = 100 - largurasColunas.Sum();
            var larguraColunaDinamica = larguraRestanteDaTabela / qtdColunasDinamicas;
            for (int i = 5; i < largurasColunas.Length - 3; i++)
            {
                largurasColunas[i] = larguraColunaDinamica > 5 ? 5 : larguraColunaDinamica;
            }
        }

        private void DistribuiLarguraRestanteDaTabela(float[] largurasColunas)
        {
            var larguraRestanteDaTabela = 100 - largurasColunas.Sum();
            for (int i = 0; larguraRestanteDaTabela > 0; i++)
            {
                if (larguraRestanteDaTabela < 5) 
                {
                    largurasColunas[1] = larguraRestanteDaTabela;
                }
                else
                {
                    if(i <= 1 || i > 3)
                    {
                        largurasColunas[1] += 5;
                    }
                    else if(i == 2)
                    {
                        largurasColunas[largurasColunas.Length - 3] += 4;
                    }
                }
                larguraRestanteDaTabela -= 5;
            }
        }
    }
}
