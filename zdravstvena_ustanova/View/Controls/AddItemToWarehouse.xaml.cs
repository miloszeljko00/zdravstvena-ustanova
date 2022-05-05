using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using zdravstvena_ustanova.View.Model;

namespace zdravstvena_ustanova.View.Controls
{
    public partial class AddItemToWarehouse : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<StoredItem> StoredItems { get; set; }
        public DataGrid WarehouseItemsDataGrid { get; set; }
        public Warehouse Warehouse { get; set; }
        #region NotifyProperties
        private int _itemsForTransfer;
        public int ItemsForTransfer
        {
            get
            {
                return _itemsForTransfer;
            }
            set
            {
                if (value != _itemsForTransfer)
                {
                    _itemsForTransfer = value;
                    OnPropertyChanged("ItemsForTransfer");
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

        public AddItemToWarehouse(DataGrid warehouseItemsDataGrid, ObservableCollection<StoredItem> storedItems, Warehouse warehouse)
        {
            Items = new ObservableCollection<Item>();
            InitializeComponent();
            DataContext = this;

            var app = App.Current as App;
            StoredItems = storedItems;
            Warehouse = warehouse;

            WarehouseItemsDataGrid = warehouseItemsDataGrid;

            Items = new ObservableCollection<Item>(app.ItemController.GetAll());
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
           
            if (ItemsSearchBox.SelectedItem == null || ItemsForTransfer <= 0)
            {
                MessageBox.Show("Popuni sva polja prvo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = Application.Current as App;

            Item item = (Item)ItemsSearchBox.SelectedItem;

            StoredItem storedItem = new StoredItem(item, ItemsForTransfer, StorageType.WAREHOUSE, Warehouse);
            storedItem = app.StoredItemController.Create(storedItem);
            foreach(StoredItem si in StoredItems)
            {
                if(si.Id == storedItem.Id)
                {
                    storedItem.Item = si.Item;
                    storedItem.Warehouse = si.Warehouse;
                    StoredItems.Remove(si);
                    break;
                }
            }
            StoredItems.Add(storedItem);


            CollectionViewSource.GetDefaultView(WarehouseItemsDataGrid.ItemsSource).Refresh();

            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }
        private void QuantityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
