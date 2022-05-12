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
using zdravstvena_ustanova.View.Model;

namespace zdravstvena_ustanova.View.Controls
{
    /// <summary>
    /// Interaction logic for EditItemControl.xaml
    /// </summary>
    public partial class EditItemControl : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<ItemViewModel> ItemViewModels;
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
        private ItemType _itemType;
        public ItemType ItemType
        {
            get
            {
                return _itemType;
            }
            set
            {
                if (value != _itemType)
                {
                    _itemType = value;
                    OnPropertyChanged("ItemType");
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

        public EditItemControl(ObservableCollection<ItemViewModel> itemViewModels, DataGrid itemsDataGrid)
        {
            InitializeComponent();
            DataContext = this;
            ItemViewModels = itemViewModels;
            ItemsDataGrid = itemsDataGrid;
            var app = Application.Current as App;
            var itemTypes = app.ItemTypeController.GetAll();
            ItemTypeComboBox.ItemsSource = itemTypes;
            foreach (var itemType in itemTypes)
            {
                if(itemType.Id == ((ItemViewModel)ItemsDataGrid.SelectedItem).Item.ItemType.Id)
                {
                    ItemTypeComboBox.SelectedItem = itemType;
                    ItemType = itemType;
                }
            }
            var selectedItem = ((ItemViewModel)ItemsDataGrid.SelectedItem).Item;
            ItemName = selectedItem.Name;
            ItemDescription = selectedItem.Description;
            ItemType = selectedItem.ItemType;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemNameTextBox.Text == "" || ItemDescriptionTextBox.Text == "")
            {
                MessageBox.Show("Popuni sva polja prvo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var itemViewModel = (ItemViewModel)ItemsDataGrid.SelectedItem;
            if (itemViewModel == null)
            {
                MessageBox.Show("Odaberi predmet!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var app = Application.Current as App;

            var selectedItem = itemViewModel.Item;

            selectedItem.Name = ItemNameTextBox.Text;
            selectedItem.Description = ItemDescriptionTextBox.Text;
            selectedItem.ItemType = (ItemType)ItemTypeComboBox.SelectedItem;

            app.ItemController.Update(selectedItem);

            CollectionViewSource.GetDefaultView(ItemsDataGrid.ItemsSource).Refresh();

            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }
    }
}
