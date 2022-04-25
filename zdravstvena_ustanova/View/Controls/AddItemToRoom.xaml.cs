using Model;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class AddItemToRoom : UserControl
    {
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<ItemViewModel> ItemViewModels { get; set; }
        public Room Room { get; set; }
        public Warehouse Warehouse { get; set; }
        DataGrid RoomItemsDataGrid { get; set; }

        public AddItemToRoom(Room room, DataGrid roomItemsDataGrid)
        {
            ItemViewModels = new ObservableCollection<ItemViewModel>();
            Items = new ObservableCollection<Item>();
            InitializeComponent();
            DataContext = this;

            var app = App.Current as App;

            Room = room;
            Warehouse = app.WarehouseController.GetAll().SingleOrDefault();
            RoomItemsDataGrid = roomItemsDataGrid;


            Items = new ObservableCollection<Item>(app.ItemController.GetAll());
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsSearchBox.SelectedItem == null || QuantityTextBox.Text == "")
            {
                MessageBox.Show("Popuni sva polja prvo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = Application.Current as App;

            Item item = (Item)ItemsSearchBox.SelectedItem;
            int quantity = int.Parse(QuantityTextBox.Text);

            if (NewInput.IsChecked == true)
            {
                StoredItem storedItem = new StoredItem(item, quantity, StorageType.ROOM, Room);
                app.StoredItemController.Create(storedItem);
                Room.StoredItems.Add(storedItem);
            }
            else
            {
                app.StoredItemController.MoveItemFromTo(Warehouse, Room, item, quantity);

            }
            RoomItemsDataGrid.ItemsSource = Room.StoredItems;
            CollectionViewSource.GetDefaultView(RoomItemsDataGrid.ItemsSource).Refresh();

            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }
        private void QuantityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void WarehouseInput_Checked(object sender, RoutedEventArgs e)
        {
            ItemViewModels.Clear();
            Items.Clear();
            foreach (var storedItem in Warehouse.StoredItems)
            {
                ItemViewModels.Add(new ItemViewModel(storedItem.Item, storedItem.Quantity));
                Items.Add(storedItem.Item);
            }
        }

        private void NewInput_Checked(object sender, RoutedEventArgs e)
        {
            var app = Application.Current as App;

            ItemViewModels.Clear();
            Items.Clear();
            foreach(var item in app.ItemController.GetAll())
            {
                Items.Add((Item)item);
            }
        }
    }
}
