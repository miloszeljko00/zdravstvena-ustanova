using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace zdravstvena_ustanova.View
{
    public partial class PasswordChange : Window
    {
        public SolidColorBrush Brush { get; set; }

        public PasswordChange()
        {
            InitializeComponent();

            Brush = (SolidColorBrush)tekucaLoz.BorderBrush;
        }

        private void goToAccount(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void validate(object sender, RoutedEventArgs e)
        {
            bool isValid = true;
            if (novaLoz.Password == "")
            {
                novaLoz.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (tekucaLoz.Password.Length < 8)
            {
                tekucaLoz.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (novaLoz.Password != potvrda.Password)
            {
                potvrda.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (novaLoz.Password.Length < 8)
            {
                novaLoz.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (isValid)
            {
                this.Close();
            }
        }
        private void lostFocus(object sender, RoutedEventArgs e)
        {
            if (novaLoz.Password.Length < 8)
            {
                novaLozLabel.Content += "   (MIN 8 KARAKTERA)";
                novaLoz.BorderBrush = Brushes.Red;
            }
            else
            {
                novaLozLabel.Content = "Unesite novu lozinku:";
                novaLoz.BorderBrush = Brush;
            }
        }

        private void focusPonovna(object sender, RoutedEventArgs e)
        {
            potvrda.BorderBrush = Brush;
        }

        private void focusTekuca(object sender, RoutedEventArgs e)
        {
            tekucaLoz.BorderBrush = Brush;
        }
    }
}
