using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WCF_Client.proxy;

namespace WCF_Client {
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Page {
        public Login() {
            InitializeComponent();
        }

        /// <summary>
        /// Login button trigger.
        /// </summary>
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

        /// <summary>
        /// Authenticates to the service.
        /// <param name="username">the username.</param>
        /// <param name="password">the password.</param>
        /// </summary>
        private MSG Auth(string username, string password) {
            var svc = new proxy.ComposantServiceClient();
            svc.ClientCredentials.Windows.ClientCredential.Domain = "WORKGROUP";
            svc.ClientCredentials.Windows.ClientCredential.UserName = "project";
            svc.ClientCredentials.Windows.ClientCredential.Password = "azerty";

            proxy.MSG msg = new proxy.MSG() {
                appVersion = "1.0",
                info = "[username, password]",
                operationName = "Auth",
                operationVersion = "1.0",
                tokenApp = MainWindow.tokenApp,
                statut_Op = true,
                tokenUser = "not logged in",
                data = new object[] { username, password }
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
    }
}
