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
    public class UploadedFile {
        public string name { get; set; }
        public string status { get; set; }
    }

    /// <summary>
    /// Logique d'interaction pour Decrypter.xaml
    /// </summary>
    public partial class Decrypter : Page {
        public ObservableCollection<UploadedFile> items = new ObservableCollection<UploadedFile>();
        public Decrypter() {
            InitializeComponent();
            listView.ItemsSource = items;
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog fileDialog = new OpenFileDialog {
                Multiselect = true,
                DefaultExt = ".txt",
                Filter = "TXT File (.txt)|*.txt"
            };
            if (fileDialog.ShowDialog() == true) {
                Task.Factory.StartNew(() => {
                    List<object> data = new List<object>();
                    var svc = new proxy.ComposantServiceClient();
                    Stream a = fileDialog.OpenFile();
                    StringBuilder txt = new StringBuilder();
                    Parallel.For(0, fileDialog.FileNames.Length, i => {
                        string file = fileDialog.FileNames[i];
                        Console.WriteLine(file);
                        Dispatcher.Invoke(new Action(() => {
                            items.Add(new UploadedFile() { name = file, status = "Traitement" });
                        }));
                        string content = File.ReadAllText(file);
                        data.Add(file);
                        data.Add(content);
                    });
                    return svc.m_service(new proxy.MSG() {
                        appVersion = "1.0",
                        statut_Op = true,
                        operationVersion = "1.0",
                        tokenApp = MainWindow.tokenApp,
                        tokenUser = MainWindow.tokenUser,
                        operationName = "Decrypt",
                        info = "[[fileName, fileContent]]",
                        data = data.ToArray()
                    });
                }).ContinueWith(t => {
                    Console.WriteLine(t.Result.info);
                    for (int i = 0; i < items.Count; i++) {
                        items[i].status = t.Result.info;
                    }
                    listView.Items.Refresh();
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
}
