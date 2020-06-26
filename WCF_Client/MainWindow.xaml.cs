using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ServiceModel;

namespace WCF_Client {
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public static string tokenApp = "#A`ut8kNX7t.%L%#Ierr3sBYi}`S=bXRK5.iWo[Reu>^|Km9fW+K!C%{Q}O&xU,";
        public static string tokenUser;
        public MainWindow() {
            InitializeComponent();
            MainFrame.Navigate(new Uri("Login.xaml", UriKind.Relative));
        }
    }
}
