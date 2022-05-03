using Model;
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
using zdravstvena_ustanova.View.Controls;

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    public partial class WarehouseInventoryOverviewPage : Page, INotifyPropertyChanged
    {
        public ObservableCollection<StoredItem> StoredItems { get; set; }
        #region NotifyProperties
        private Warehouse _warehouse;
        public Warehouse Warehouse
        {
            get
            {
                return _warehouse;
            }
            set
            {
                if (value != _warehouse)
                {
                    _warehouse = value;
                    OnPropertyChanged("Warehouse");
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

        public WarehouseInventoryOverviewPage()
        {
            var app = Application.Current as App;

            Warehouse = app.WarehouseController.GetAll().SingleOrDefault();
            StoredItems = new ObservableCollection<StoredItem>(Warehouse.StoredItems);

            InitializeComponent();
            DataContext = this;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTextBox.Text == "")
            {
                // Create an ImageBrush.
                ImageBrush textImageBrush = new();
                textImageBrush.ImageSource =
                    new BitmapImage(
                        new Uri(App.ProjectPath + "/Resources/img/search-name.png")
                    );
                textImageBrush.AlignmentX = AlignmentX.Left;
                textImageBrush.Stretch = Stretch.None;

                // Use the brush to paint the button's background.
                SearchTextBox.Background = textImageBrush;
            }
            else
            {

                SearchTextBox.Background = null;
            }
        }

        private void AddNewItemIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Modal.Content = new AddItemToWarehouse(WarehouseItemsDataGrid, StoredItems, Warehouse);
            MainWindow.Modal.IsOpen = true;
        }

        private void RemoveItemIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (WarehouseItemsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Odaberi predmet!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MainWindow.Modal.Content = new RemoveItemFromWarehouse(WarehouseItemsDataGrid, StoredItems, Warehouse);
            MainWindow.Modal.IsOpen = true;
        }

        private void ScheduleItemTransferButton_Click(object sender, RoutedEventArgs e)
        {
            if (WarehouseItemsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Odaberi predmet!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MainWindow.Modal.Content = new ScheduleItemTransferFromWarehouse(Warehouse, WarehouseItemsDataGrid);
            MainWindow.Modal.IsOpen = true;
        }
    }
}
