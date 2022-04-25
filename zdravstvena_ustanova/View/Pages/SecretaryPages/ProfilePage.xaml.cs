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

namespace zdravstvena_ustanova.View.Pages.SecretaryPages
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        MainWindow mainWindow;
        public ProfilePage(MainWindow mw)
        {
            InitializeComponent();
            mainWindow = mw;
        }

        private void Logout_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.Height = 720;
            mainWindow.Width = 1280;
            mainWindow.Main.Content = new LoginPage(mainWindow);
        }
    }
}
