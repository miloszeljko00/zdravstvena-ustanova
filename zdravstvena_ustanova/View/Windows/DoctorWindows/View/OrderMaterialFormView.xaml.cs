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
using zdravstvena_ustanova.View.Windows.DoctorWindows.ViewModel;

namespace zdravstvena_ustanova.View.Windows.DoctorWindows.View
{
    /// <summary>
    /// Interaction logic for OrderMaterialFormView.xaml
    /// </summary>
    public partial class OrderMaterialFormView : Window
    {
        public OrderMaterialFormView()
        {
            InitializeComponent();
            DataContext = new OrderMaterialFormViewModel(this);
        }
    }
}
