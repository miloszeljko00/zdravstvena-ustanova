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
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Model.Enums;

namespace zdravstvena_ustanova.View.Pages.SecretaryPages
{
    /// <summary>
    /// Interaction logic for OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public ICollectionView ItemsView { get; set; }

        public ObservableCollection<Item> Items { get; set; }

        public ObservableCollection<StoredItem> OrderedItems { get; set; }

        private HomePagePatients homePagePatients;

        public OrdersPage(HomePagePatients hpp)
        {
            InitializeComponent();
            DataContext = this;
            homePagePatients = hpp;
            var app = Application.Current as App;
            Items = new ObservableCollection<Item>(app.ItemController.GetAll());
            OrderedItems = new ObservableCollection<StoredItem>(app.OrderedItemController.GetAll());
            ItemsView = new CollectionViewSource { Source = Items }.View;
            ItemsView.Filter = i =>
           {
               Item item = i as Item;
               if (item.ItemType.Id == 2) //1-stalni, 2-potrosni
                   return true;
               else
                   return false;
           };
            itemsCB.ItemsSource = ItemsView;

        }

        private void add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var app = Application.Current as App;
            Item i = itemsCB.SelectedItem as Item;
            int quantity = Int32.Parse(quantityTB.Text); //vidi validacija ili onaj steper
            StoredItem orderedItem = new StoredItem(i, quantity, StorageType.VOID, new Warehouse(-1));
            orderedItem = app.OrderedItemController.Create(orderedItem);
            OrderedItems.Add(orderedItem);
        }

        private void Delivered_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var app = Application.Current as App;
            StoredItem orderedItem = dataGridOrderedItems.SelectedItem as StoredItem;
            
            StoredItem storedItem = app.StoredItemController.GetByWarehouseItemId(orderedItem.Item.Id);
            if (storedItem == null)
            {
                OrderedItems.Remove(orderedItem);

                if (orderedItem.Warehouse != null)
                    orderedItem.Warehouse.Id = 1;
                else
                    orderedItem.Warehouse = new Warehouse(1);

                orderedItem.StorageType = StorageType.WAREHOUSE;
                orderedItem = app.StoredItemController.Create(orderedItem);
                
            }
            else
            {
                storedItem.Quantity += orderedItem.Quantity;
                app.StoredItemController.Update(storedItem);
                OrderedItems.Remove(orderedItem);
            }
            app.OrderedItemController.Delete(orderedItem.Id);
            

        }

        private void Warehouse_Click(object sender, RoutedEventArgs e)
        {
            homePagePatients.SecretaryFrame.Content = new WarehouseItemsPage(homePagePatients);
        }
    }
}
