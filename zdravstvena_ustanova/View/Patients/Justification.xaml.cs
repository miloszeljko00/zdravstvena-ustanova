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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace zdravstvena_ustanova.View
{
    public partial class Justification : UserControl
    {
        public SolidColorBrush Brush { get; set; }
        public Justification()
        {
            InitializeComponent();
            Brush = (SolidColorBrush)odeljenje.BorderBrush;
        }

        private void validate(object sender, RoutedEventArgs e)
        {
            bool isValid = true;
            DateTime dateValue;
            if (odeljenje.Text == "")
            {
                odeljenje.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (skola.Text == "")
            {
                skola.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (!DateTime.TryParseExact(datumOd.Text, "d.M.yyyy.", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateValue))
            {
                datumOd.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (!DateTime.TryParseExact(datumDo.Text, "d.M.yyyy.", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateValue))
            {
                datumDo.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (razlog.Text == "")
            {
                razlog.BorderBrush = Brushes.Red;
                isValid = false;
            }
            if (isValid)
            {
                MessageBox.Show("PODNESEN ZAHTEV", "Potvrda podnosenja zahteva", MessageBoxButton.OK);
            }
        }

        private void focusOdeljenje(object sender, RoutedEventArgs e)
        {
            odeljenje.BorderBrush = Brush;
        }

        private void focusSkola(object sender, RoutedEventArgs e)
        {
            skola.BorderBrush = Brush;
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

        private void focusDatumOd(object sender, KeyboardFocusChangedEventArgs e)
        {
            datumOd.BorderBrush = Brush;
        }
        private void focusDatumDo(object sender, KeyboardFocusChangedEventArgs e)
        {
            datumDo.BorderBrush = Brush;
        }

        private void focusRazlog(object sender, RoutedEventArgs e)
        {
            razlog.BorderBrush = Brush;
        }
    }
}
