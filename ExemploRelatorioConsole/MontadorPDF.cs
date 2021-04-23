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
        private const string caminho = @"C:\WorkPedro\pdf\study.pdf";
        private PdfWriter writer;
        public float alturaMaximaDeUmaPagina;
        private PdfPTable tabelaPrimaria;
        private PdfPTable tabelaSecundaria;
        private PdfPTable tabelaTerciaria;

        public MontadorPDF()
        {
            doc = new Document(PageSize.A4);
            doc.SetMargins(40, 40, 40, 80);
            writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));
            alturaMaximaDeUmaPagina = doc.PageSize.Height - doc.TopMargin - doc.BottomMargin;
        }

        public float GetAlturaMaximaDeUmaPagina()
        {
            return alturaMaximaDeUmaPagina;
        }

        public bool EhParaPularParaProximaPagina(PdfPTable tabelaPrimaria, PdfPTable tabelaSecundaria)
        {
            int qntdHeaderRows = tabelaSecundaria.HeaderRows;
            var alturaAtualDoCursor = writer.GetVerticalPosition(false);
            var espacoRestanteDaUltimaPagina = alturaAtualDoCursor - doc.BottomMargin;
            var somaDaAlturaDaTabelaPrimariaComHeadersMaisUmRegistroDaTabelaSecundaria
                = tabelaPrimaria.TotalHeight;

            //var espacoJaOcupadoNaUltimaPagina = alturaMaximaDeUmaPagina - espacoRestanteDaUltimaPagina;
            for (int i = 0; i < qntdHeaderRows + 1; i++)
            {
                somaDaAlturaDaTabelaPrimariaComHeadersMaisUmRegistroDaTabelaSecundaria += tabelaSecundaria.GetRowHeight(i);
            }

            return somaDaAlturaDaTabelaPrimariaComHeadersMaisUmRegistroDaTabelaSecundaria
                   > espacoRestanteDaUltimaPagina;
        }

        public bool EhParaPularParaProximaPagina(PdfPTable tabelaTerciaria)
        {
            int qntdHeaderRows = tabelaTerciaria.HeaderRows;
            var alturaAtualDoCursor = writer.GetVerticalPosition(false);
            var espacoRestanteDaUltimaPagina = alturaAtualDoCursor - doc.BottomMargin;
            var somaDaAlturaDosHeadersDaTabelaTerciariaMaisUmRegistro = 0.0;

            for (int i = 0; i < qntdHeaderRows + 1; i++)
            {
                somaDaAlturaDosHeadersDaTabelaTerciariaMaisUmRegistro += tabelaTerciaria.GetRowHeight(i);
            }
            return somaDaAlturaDosHeadersDaTabelaTerciariaMaisUmRegistro
                   > espacoRestanteDaUltimaPagina;
        }

        public void PulaParaProximaPagina()
        {
            doc.NewPage();
        }

        public void LimpaTabela(PdfPTable tabela)
        {
            while (tabela.Rows.Count > 0)
            {
                tabela.DeleteLastRow();
            }
        }

        public float AlturaDaTabela(PdfPTable tabela)
        {
            float altura = 0;

            for(int i = 0; i < tabela.Rows.Count; i++)
            {
                altura += tabela.GetRowHeight(i);
            }

            return altura;
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

        }

        public void AbreDocumento()
        {
            doc.Open();
        }

        public void InsereTabelaNoDocumento(PdfPTable tabela)
        {            
            doc.Add(tabela);            
        }

        public void FechaDocumento()
        {
            doc.Close();
        }

        public void MontaTabelaPrimaria(int i)
        {
            tabelaPrimaria = new PdfPTable(3)
            {
                TotalWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin,
                LockedWidth = true
            };

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
        }

        public void MontaTabelaSecundaria()
        {
            tabelaSecundaria = new PdfPTable(6)
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

            if (EhParaPularParaProximaPagina(tabelaPrimaria, tabelaSecundaria))
            {
                PulaParaProximaPagina();
            }

            InsereTabelaNoDocumento(tabelaPrimaria);
            InsereTabelaNoDocumento(tabelaSecundaria);
        }

        public void MontaTabelaTerciaria()
        {
            tabelaTerciaria = new PdfPTable(6)
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

            if (EhParaPularParaProximaPagina(tabelaTerciaria))
            {
                PulaParaProximaPagina();
            }

            InsereTabelaNoDocumento(tabelaTerciaria);
        }

        public float Soma(PdfPTable tabela)
        {
            float soma = 0;

            for(int i = 0; i < tabela.HeaderRows; i++)
            {
                soma += tabela.GetRowHeight(i);
            }

            return soma;
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
