using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;

namespace zdravstvena_ustanova.View
{
    public partial class Registration : Window
    {
        public SolidColorBrush Brush { get; set; }
        public Registration()
        {
            InitializeComponent();
            Brush = (SolidColorBrush)ime.BorderBrush;
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            this.Close();
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
            if (jmbg.Text == "")
            {
                jmbg.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (!DateTime.TryParseExact(datum.Text, "d.M.yyyy.", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateValue))
            {
                datum.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (osiguranje.Text == "")
            {
                osiguranje.BorderBrush = Brushes.Red;
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
            if (lozinka.Password == "")
            {
                lozinka.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (ponovna.Password != lozinka.Password)
            {
                ponovna.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (isValid)
            {
                MessageBox.Show("USPESNA REGISTRACIJA", "Potvrda registracije", MessageBoxButton.OK);
                this.Close();
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

        private void focusJMBG(object sender, RoutedEventArgs e)
        {
            jmbg.BorderBrush = Brush;
        }

        private void focusOsiguranje(object sender, RoutedEventArgs e)
        {
            osiguranje.BorderBrush = Brush;
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

        private void lostFocus(object sender, RoutedEventArgs e)
        {
            if (lozinka.Password.Length < 8)
            {
                obavestenje.Content = "Lozinka prekratka(min 8 karaktera)";
                lozinka.BorderBrush = Brushes.Red;
            }
            else
            {
                obavestenje.Content = "";
                lozinka.BorderBrush = Brush;
            }
        }

        private void focusPonovna(object sender, RoutedEventArgs e)
        {
            ponovna.BorderBrush = Brush;
        }

        private void focusDatum(object sender, KeyboardFocusChangedEventArgs e)
        {
            datum.BorderBrush = Brush;
        }

        private void loadedDatum(object sender, RoutedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            if (datePicker != null)
            {
                System.Windows.Controls.Primitives.DatePickerTextBox datePickerTextBox = FindVisualChild<System.Windows.Controls.Primitives.DatePickerTextBox>(datePicker);
                if (datePickerTextBox != null)
                {
                    ContentControl watermark = datePickerTextBox.Template.FindName("PART_Watermark", datePickerTextBox) as ContentControl;
                    if (watermark != null)
                    {
                        watermark.Content = "Odaberi datum";
                    }
                }
            }
        }
        private T FindVisualChild<T>(DependencyObject depencencyObject) where T : DependencyObject
        {
            if (depencencyObject != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depencencyObject); ++i)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depencencyObject, i);
                    T result = (child as T) ?? FindVisualChild<T>(child);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }
    }
}
