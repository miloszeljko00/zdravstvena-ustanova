using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace zdravstvena_ustanova.View
{
    public partial class PatientAccount : UserControl
    {
        public SolidColorBrush Brush { get; set; }
        public PatientAccount()
        {
            InitializeComponent();
            Brush = (SolidColorBrush)ime.BorderBrush;
        }

        private void validate(object sender, RoutedEventArgs e)
        {
            bool isValid = true;
            DateTime dateValue;
            if (ime.Text == "")
            {
                ime.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (prezime.Text == "")
            {
                prezime.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (telefon.Text == "")
            {
                telefon.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (email.Text == "")
            {
                email.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (adresa.Text == "")
            {
                adresa.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (grad.Text == "")
            {
                grad.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (posta.Text == "")
            {
                posta.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (korisnik.Text == "")
            {
                korisnik.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (isValid)
            {
                MessageBox.Show("IZMENA NALOGA USPESNA", "Potvrda izmene naloga", MessageBoxButton.OK);
            }
        }
        private void focusIme(object sender, RoutedEventArgs e)
        {
            ime.BorderBrush = Brush;
        }

        private void focusPrezime(object sender, RoutedEventArgs e)
        {
            prezime.BorderBrush = Brush;
        }

        private void focusTelefon(object sender, RoutedEventArgs e)
        {
            telefon.BorderBrush = Brush;
        }

        private void focusEmail(object sender, RoutedEventArgs e)
        {
            email.BorderBrush = Brush;
        }

        private void focusAdresa(object sender, RoutedEventArgs e)
        {
            adresa.BorderBrush = Brush;
        }

        private void focusGrad(object sender, RoutedEventArgs e)
        {
            grad.BorderBrush = Brush;
        }

        private void focusPosta(object sender, RoutedEventArgs e)
        {
            posta.BorderBrush = Brush;
        }

        private void focusKorisnik(object sender, RoutedEventArgs e)
        {
            korisnik.BorderBrush = Brush;
        }

        private void goToChangePassword(object sender, RoutedEventArgs e)
        {
            PasswordChange pc = new PasswordChange();
            pc.ShowDialog();
        }
    }
}
