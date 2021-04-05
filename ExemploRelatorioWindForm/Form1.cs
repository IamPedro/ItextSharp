using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ExemploRelatorioWindForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Document doc = new Document(PageSize.A4);
            doc.SetMargins(40, 40, 40, 80);
            string caminho = @"C:\Users\pedro\Desktop\PDF\exemplo.pdf";

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));

            doc.Open();

            Paragraph titulo = new Paragraph();
            titulo.Font = new Font(iTextSharp.text.Font.FontFamily.COURIER, 40);
            titulo.Alignment = Element.ALIGN_CENTER;
            titulo.Add("Teste\n\n");
            doc.Add(titulo);

            Paragraph pagragrafo = new Paragraph();
            pagragrafo.Font = new Font(iTextSharp.text.Font.NORMAL, 12);
            string conteudo = @"Integer ac nulla et quam tristique viverra. 
                                Integer et hendrerit neque. Maecenas aliquet elementum massa non placerat. 
                                Etiam sed vestibulum velit, eu consequat neque. 
                                Vestibulum tempus mollis lectus at malesuada. 
                                Proin ac iaculis sem. Integer nec dapibus mauris, sed congue turpis. 
                                Pellentesque consectetur molestie ullamcorper. 
                                Nam scelerisque, quam ut vulputate sagittis, sapien sapien pharetra justo, tempor hendrerit nibh nisl sed massa. 
                                In eget condimentum velit. Nam congue magna a scelerisque iaculis. 
                                Nam id odio et enim convallis fermentum et non enim.\n\n";
            pagragrafo.Add(conteudo);
            doc.Add(pagragrafo);

            PdfPTable table = new PdfPTable(3);

            for (int i = 0; i < 3; i++)
            {
                table.AddCell($"Linha {i + 1}, Coluna 1");
                table.AddCell($"Linha {i + 1}, Coluna 2");
                table.AddCell($"Linha {i + 1}, Coluna 3");

            }
            doc.Add(table);

            doc.Close();

            System.Diagnostics.Process.Start(caminho);
        }
    }
}
