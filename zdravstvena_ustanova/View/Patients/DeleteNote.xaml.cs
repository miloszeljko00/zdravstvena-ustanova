using zdravstvena_ustanova.Model;
using System.Windows;

namespace zdravstvena_ustanova.View
{

    public partial class DeleteNote : Window
    {
        public Note Note { get; set; }
        public DeleteNote(Note n)
        {
            InitializeComponent();
            Note = n;
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;
            app.NoteController.Delete(Note.Id);
            this.Close();
        }

        private void goToNotes(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
