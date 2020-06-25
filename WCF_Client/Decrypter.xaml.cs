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
using System.Data;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;


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
                DefaultExt = ".txt",
                Filter = "TXT File (.txt)|*.txt"
            };
            if (fileDialog.ShowDialog() == true) {
                Task.Factory.StartNew(() => {
                    var svc = new proxy.ComposantServiceClient();
                    Stream a = fileDialog.OpenFile();
                    StringBuilder txt = new StringBuilder();
                    Parallel.ForEach(fileDialog.FileNames, file => {
                        string content = File.ReadAllText(file);
                        proxy.InputData data = new proxy.InputData() { fileName = file, fileContent = content };
                        proxy.InputData[] datas = new proxy.InputData[1];
                        datas[0] = data;
                        svc.m_service(new proxy.MSG() { operationName = "Decrypt", info = content, data = datas });
                    });
                }).ContinueWith(_ => {
                    button.Content = "🚀";
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
}
