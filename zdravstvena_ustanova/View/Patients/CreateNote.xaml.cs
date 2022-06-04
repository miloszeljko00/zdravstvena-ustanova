using zdravstvena_ustanova.Model;
using System;
using System.Windows;
using System.Globalization;

namespace zdravstvena_ustanova.View
{
    public partial class CreateNote : Window
    {
        public CreateNote()

        {
            InitializeComponent();
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void enabled(object sender, RoutedEventArgs e)
        {
            time.IsEnabled = true;
        }

        private void disabled(object sender, RoutedEventArgs e)
        {
            time.IsEnabled = false;
        }

        private void create(object sender, RoutedEventArgs e)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime dt;
            var app = Application.Current as App;
            Note note;
            try
            {
                if (check.IsChecked == true)
                {
                    dt = DateTime.ParseExact(time.Text, "HH:mm", provider);
                    if (dt != null)
                    {
                        warning.Content = "";
                    }
                    note = new Note(0, app.LoggedInUser.Id, name.Text, content.Text, time.Text);
                }
                else
                {
                    note = new Note(0, app.LoggedInUser.Id, name.Text, content.Text, "Onemoguceno");
                }
                note = app.NoteController.Create(note);
                this.Close();
            }
            catch
            {
                warning.Content = "Pogresan vremenski format (HH:mm)";
            }
        }

        private void focused(object sender, RoutedEventArgs e)
        {
            warning.Content = "";
        }
    }
}
