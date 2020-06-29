using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using WCF_Client.proxy;

namespace WCF_Client {
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Page {
        public Login() {
            InitializeComponent();


        }

        private void button_Click(object sender, RoutedEventArgs e) {
            button.IsEnabled = false;
            button.Content = "Connexion...";
            string usr = username.Text;
            string pwd = password.Password;
            Task.Factory.StartNew(() => Auth(usr, pwd))
            .ContinueWith(t => {
                if (t.Result.statut_Op == true) {
                    MainWindow.tokenUser = t.Result.tokenUser;
                    this.NavigationService.Navigate(new Uri("Decrypter.xaml", UriKind.Relative));
                } else if (t.Result.info == "" || t.Result.info == null) {
                    MessageBox.Show("An error occured");
                } else {
                    MessageBox.Show(t.Result.info);
                }
                button.Content = "SE CONNECTER";
                button.IsEnabled = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        public MSG Auth(string username, string password) {
            var svc = new proxy.ComposantServiceClient();
            svc.ClientCredentials.Windows.ClientCredential.Domain = "WORKGROUP";
            svc.ClientCredentials.Windows.ClientCredential.UserName = "project";
            svc.ClientCredentials.Windows.ClientCredential.Password = "azerty";

            try {
                return svc.m_service(new MSG() {
                    appVersion = "1.0",
                    info = "[username, password]",
                    operationName = "Auth",
                    operationVersion = "1.0",
                    tokenApp = MainWindow.tokenApp,
                    statut_Op = true,
                    tokenUser = "not logged in",
                    data = new object[] { username, password }
                });
            } catch (Exception exc) {
                Console.WriteLine(exc.Message);
                return new MSG() {
                    appVersion = "1.0",
                    info = "Error contacting the server",
                    operationName = "Auth",
                    operationVersion = "1.0",
                    tokenApp = MainWindow.tokenApp,
                    statut_Op = false,
                    tokenUser = "not logged in",
                    data = new object[] { }
                };
            }
        }
    }
}
