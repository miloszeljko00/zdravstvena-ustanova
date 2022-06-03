using zdravstvena_ustanova.Model;
using System.Windows;

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
