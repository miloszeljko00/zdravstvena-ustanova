using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using zdravstvena_ustanova.View.ManagerMVVM.Model;
using zdravstvena_ustanova.View.ManagerMVVM.ViewModel;
using zdravstvena_ustanova.View.Model;

namespace zdravstvena_ustanova.View.ManagerMVVM.View
{
    /// <summary>
    /// Interaction logic for AddItemView.xaml
    /// </summary>
    public partial class AddItemView : UserControl
    {
        public AddItemView(ObservableCollection<ItemModel> itemViewModels)
        {
            InitializeComponent();
            DataContext = new AddItemViewModel(itemViewModels);
        }
    }
}
