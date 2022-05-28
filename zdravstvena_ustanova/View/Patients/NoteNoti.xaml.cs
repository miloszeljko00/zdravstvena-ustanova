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
using System.Globalization;

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
