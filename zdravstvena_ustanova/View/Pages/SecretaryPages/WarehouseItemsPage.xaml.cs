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
    /// Interaction logic for WarehouseItemsPage.xaml
    /// </summary>
    public partial class WarehouseItemsPage : Page
    {
        public ICollectionView ItemsView { get; set; }

 

        public ObservableCollection<StoredItem> StoredItems { get; set; }

        private HomePagePatients homePagePatients;
        public WarehouseItemsPage(HomePagePatients hpp)
        {
            InitializeComponent();
            DataContext = this;
            homePagePatients = hpp;
            var app = Application.Current as App;
            StoredItems = new ObservableCollection<StoredItem>(app.StoredItemController.GetAll());
            ItemsView = new CollectionViewSource { Source = StoredItems }.View;
            ItemsView.Filter = i =>
            {
                StoredItem item = i as StoredItem;
                if (item.Item.ItemType.Id == 2 && item.StorageType == StorageType.WAREHOUSE) //1-stalni, 2-potrosni
                    return true;
                else
                    return false;
            };
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            homePagePatients.SecretaryFrame.Content = new OrdersPage(homePagePatients);
        }
    }
}
