using zdravstvena_ustanova.Model;
using System.Windows;

namespace zdravstvena_ustanova.View
{
    public partial class NoteNoti : Window
    {
        public Note Note { get; set; }
        public NoteNoti(Note n)

        {
            InitializeComponent();
            Note = n;
            name.Text = n.Name;
            content.Text = n.Content;
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
