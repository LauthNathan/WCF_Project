using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Collections.ObjectModel;


namespace WCF_Client {
    /// <summary>
    /// Class used to display in the view.
    /// </summary>
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

        /// <summary>
        /// Upload button trigger.
        /// </summary>
        private void button_Click(object sender, RoutedEventArgs e) {
            // Displays the file dialog.
            OpenFileDialog ofd = new OpenFileDialog {
                Multiselect = true,
                DefaultExt = ".txt",
                Filter = "TXT File (.txt)|*.txt"
            };

            if (ofd.ShowDialog() == true) {
                Task.Factory.StartNew(() => {
                    List<object> data = addFiles(ofd);
                    return Decrypt(data);
                })
                .ContinueWith(t => {
                    if (t.Result.statut_Op == true) {
                        for (int i = 0; i < items.Count; i++) { items[i].status = t.Result.info; }
                        listView.Items.Refresh();
                    } else if (t.Result.info == "" || t.Result.info == null) {
                        MessageBox.Show("An error occured");
                    } else {
                        MessageBox.Show(t.Result.info);
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private proxy.MSG Decrypt(List<object> data) {
            // Creates the service.
            var svc = new proxy.ComposantServiceClient();
            svc.ClientCredentials.Windows.ClientCredential.Domain = "WORKGROUP";
            svc.ClientCredentials.Windows.ClientCredential.UserName = "project";
            svc.ClientCredentials.Windows.ClientCredential.Password = "azerty";

             // Creates the msg to send to the service.
            proxy.MSG msg = new proxy.MSG() {
                appVersion = "1.0",
                statut_Op = true,
                operationVersion = "1.0",
                tokenApp = MainWindow.tokenApp,
                tokenUser = MainWindow.tokenUser,
                operationName = "Decrypt",
                info = "[[fileName, fileContent]]",
                data = data.ToArray()
            };

            try {
                return svc.m_service(msg);
            } catch (Exception exc) {
                Console.WriteLine(exc.Message);
                msg.statut_Op = false;
                msg.info = "Error contacting the server";
                return msg;
            }
        }

        private List<object> addFiles(OpenFileDialog ofd) {
            List<object> data = new List<object>();

            Parallel.For(0, ofd.FileNames.Length, i => {
                string file = ofd.FileNames[i];
                string content = File.ReadAllText(file);
                data.Add(file);
                data.Add(content);
                // Updates the view according to the uploaded files.
                Dispatcher.Invoke(new Action(() => {
                    items.Add(new UploadedFile() { name = file, status = "Traitement" });
                }));
            });

            return data;
        }
    }
}
