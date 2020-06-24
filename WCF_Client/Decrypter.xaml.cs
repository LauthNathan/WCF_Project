using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using iText;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace WCF_Client {
    /// <summary>
    /// Logique d'interaction pour Decrypter.xaml
    /// </summary>
    public partial class Decrypter : Page {
        public Decrypter() {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog fileDialog = new OpenFileDialog {
                Multiselect = true,
                DefaultExt = ".pdf",
                Filter = "PDF File (.pdf)|*.pdf"
            };
            if (fileDialog.ShowDialog() == true) {
                Stream a = fileDialog.OpenFile();
                StringBuilder txt = new StringBuilder();
                Parallel.ForEach(fileDialog.FileNames, file => {
                    string content = "";
                    PdfReader pdfReader = new PdfReader(file);
                    PdfDocument pdfDocument = new PdfDocument(pdfReader);
                    for (int p = 1; p <= pdfDocument.GetNumberOfPages(); p++) {
                        content += PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(p), new SimpleTextExtractionStrategy());
                    }
                    pdfDocument.Close();
                    pdfReader.Close();
                    File.WriteAllText("D:\\dev\\Projet\\pdf.txt", content);
                });
            }
        }
    }
}
