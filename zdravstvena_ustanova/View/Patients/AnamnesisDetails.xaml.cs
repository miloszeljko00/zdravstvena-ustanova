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
    public partial class AnamnesisDetails : Window
    {
        public AnamnesisDetails(Anamnesis a)

        {
            InitializeComponent();
            diagnosis.Text = a.Diagnosis;
            conclusion.Text = a.Conclusion;
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
