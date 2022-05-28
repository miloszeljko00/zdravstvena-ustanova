using zdravstvena_ustanova.Model;
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
using System.Windows.Shapes;

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
