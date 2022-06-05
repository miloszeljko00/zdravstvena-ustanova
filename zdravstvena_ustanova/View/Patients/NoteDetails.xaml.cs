using zdravstvena_ustanova.Model;
using System;
using System.Windows;
using System.Globalization;

namespace zdravstvena_ustanova.View
{
    public partial class NoteDetails : Window
    {
        public Note Note { get; set; }
        public NoteDetails(Note n)

        {
            InitializeComponent();
            Note = n;
            name.Text = n.Name;
            content.Text = n.Content;
            if (n.Time.Equals("Onemoguceno"))
            {
                check.IsChecked = false;
                time.IsEnabled = false;
                time.Text = "";
            }
            else
            {
                check.IsChecked = true;
                time.IsEnabled = true;
                time.Text = n.Time;
            }
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

        private void update(object sender, RoutedEventArgs e)
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
                    note = new Note(Note.Id, Note.Patient.Id, name.Text, content.Text, time.Text);
                }
                else
                {
                    note = new Note(Note.Id, Note.Patient.Id, name.Text, content.Text, "Onemoguceno");
                }
                app.NoteController.Update(note);
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
