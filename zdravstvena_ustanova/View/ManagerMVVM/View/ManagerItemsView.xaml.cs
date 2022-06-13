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
using System.Windows.Navigation;
using System.Windows.Shapes;
using zdravstvena_ustanova.View.ManagerMVVM.ViewModel;

namespace zdravstvena_ustanova.View.ManagerMVVM.View
{
    /// <summary>
    /// Interaction logic for ManagerItemsView.xaml
    /// </summary>
    public partial class ManagerItemsView : Page
    {
        public ManagerItemsView(Frame frame)
        {
            InitializeComponent();
            DataContext = new ManagerItemsViewModel(frame.NavigationService);
        }
    }
}
