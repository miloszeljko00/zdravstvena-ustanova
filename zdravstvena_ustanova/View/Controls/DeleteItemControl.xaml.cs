using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using zdravstvena_ustanova.View.Model;

namespace zdravstvena_ustanova.View.Controls
{
    /// <summary>
    /// Interaction logic for DeleteItemControl.xaml
    /// </summary>
    public partial class DeleteItemControl : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<ItemModel> ItemViewModels;
        public DataGrid ItemsDataGrid { get; set; }

        #region NotifyProperties
        private string _itemName;
        public string ItemName
        {
            get
            {
                return _itemName;
            }
            set
            {
                if (value != _itemName)
                {
                    _itemName = value;
                    OnPropertyChanged("ItemName");
                }
            }
        }
        private string _itemDescription;
        public string ItemDescription
        {
            get
            {
                return _itemDescription;
            }
            set
            {
                if (value != _itemDescription)
                {
                    _itemDescription = value;
                    OnPropertyChanged("ItemDescription");
                }
            }
        }
        private string _totalCount;
        public string TotalCount
        {
            get
            {
                return _totalCount;
            }
            set
            {
                if (value != _totalCount)
                {
                    _totalCount = value;
                    OnPropertyChanged("TotalCount");
                }
            }
        }
        #endregion
        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public DeleteItemControl(ObservableCollection<ItemModel> itemViewModels, DataGrid itemsDataGrid)
        {
            InitializeComponent();
            DataContext = this;
            ItemViewModels = itemViewModels;
            ItemsDataGrid = itemsDataGrid;
            

            var selectedItemViewModel = ((ItemModel)ItemsDataGrid.SelectedItem);

            ItemName = selectedItemViewModel.Item.Name;
            ItemDescription = selectedItemViewModel.Item.Description;
            TotalCount = selectedItemViewModel.TotalCount.ToString();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var itemViewModel = (ItemModel)ItemsDataGrid.SelectedItem;
            if (itemViewModel == null)
            {
                MessageBox.Show("Odaberi predmet!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = Application.Current as App;

            app.ItemController.Delete(itemViewModel.Item.Id);
            ItemViewModels.Remove(itemViewModel);

            CollectionViewSource.GetDefaultView(ItemsDataGrid.ItemsSource).Refresh();

            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }
    }
}
