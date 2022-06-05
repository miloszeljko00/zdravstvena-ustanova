using System.Collections.Generic;
using System.Windows;
using ModalControl;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.View.Pages;
using zdravstvena_ustanova.View.Pages.ManagerPages;

namespace zdravstvena_ustanova.View
{
    public partial class MainWindow : Window
    {
        public static Modal Modal { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            CenterWindowOnScreen();
            Modal = modal;
            Main.Content = new LoginPage(this);
        }
        private void CenterWindowOnScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }
    }
}
