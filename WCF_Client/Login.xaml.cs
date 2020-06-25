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
            Task<bool>.Factory.StartNew(() => Auth(usr, pwd))
            .ContinueWith(t => {
                if (t.Result == true) this.NavigationService.Navigate(new Uri("Decrypter.xaml", UriKind.Relative));
                button.Content = "SE CONNECTER";
                button.IsEnabled = true;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        public bool Auth(string username, string password) {
            bool isAuth = false;

            var svc = new proxy.ComposantServiceClient();
            svc.ClientCredentials.Windows.ClientCredential.Domain = "WORKGROUP";
            svc.ClientCredentials.Windows.ClientCredential.UserName = username;
            svc.ClientCredentials.Windows.ClientCredential.Password = password;
            try {
                svc.m_service(new proxy.MSG() { appVersion = "Coucou toi" });
                isAuth = true;
            } catch (Exception exc) {
                Console.WriteLine(exc.Message);
                MessageBox.Show("Wrong login or password !");
            }
            return isAuth;
        }
    }
}
