using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace zdravstvena_ustanova.View
{
    public partial class Notes : UserControl
    {
        public ObservableCollection<Note> allNotes { get; set; }
        public ObservableCollection<Note> filtered { get; set; }
        public Notes()
        {
            InitializeComponent();
            this.refresh();
        }

        private void entered(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space && notesList.SelectedIndex != -1)
            {
                NoteDetails nd = new NoteDetails((Note)notesList.SelectedItem);
                nd.ShowDialog();
                this.refresh();
            }
        }

        private void goToCreateNote(object sender, RoutedEventArgs e)
        {
            CreateNote cn = new CreateNote();
            cn.ShowDialog();
            this.refresh();
        }
        private void refresh()
        {
            trazi.Text = "";
            allNotes = new ObservableCollection<Note>();
            var app = Application.Current as App;
            List<Note> ns = new List<Note>(app.NoteController.GetAll());
            foreach (Note no in ns)
            {
                if (app.LoggedInUser.Id == no.Patient.Id)
                {
                    allNotes.Add(no);
                }
            }
            notesList.ItemsSource = allNotes;
            delete.IsEnabled = false;
        }

        private void selected(object sender, SelectionChangedEventArgs e)
        {
            delete.IsEnabled = true;
        }

        private void goToDeleteNote(object sender, RoutedEventArgs e)
        {
            DeleteNote dn = new DeleteNote((Note)notesList.SelectedItem);
            dn.ShowDialog();
            this.refresh();
        }
        private void pretraga(object sender, RoutedEventArgs e)
        {
            filtered = new ObservableCollection<Note>();
            if (trazi.Text == "")
                notesList.ItemsSource = allNotes;
            else
            {
                foreach (Note n in allNotes)
                {
                    if (n.Name.ToLower().Contains(trazi.Text.ToLower()) || n.Time.ToLower().StartsWith(trazi.Text.ToLower()))
                        filtered.Add(n);
                }
                notesList.ItemsSource = filtered;
            }

        }
    }
}
