using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    /// <summary>
    /// Interaction logic for InventoryOverviewPage.xaml
    /// </summary>
    public partial class InventoryOverviewPage : Page, INotifyPropertyChanged
    {
        public ObservableCollection<ItemType> ItemTypes { get; set; }
        public ItemType AllItemTypes { get; set; }
        public ObservableCollection<StoredItem> StoredItems { get; set; }

        #region NotifyProperties
        private Room _room;
        public Room Room 
        { 
            get
            {
                return _room;
            } set 
            {
                if (value != _room || value.StoredItems.Count != _room.StoredItems.Count)
                {
                    _room = value;
                    OnPropertyChanged("Room");
                }
            }
        }
        private ItemType _selectedItemType;
        public ItemType SelectedItemType
        {
            get
            {
                return _selectedItemType;
            }
            set
            {
                if (value != _selectedItemType)
                {
                    _selectedItemType = value;
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

        public InventoryOverviewPage(Room room)
        {
            var app = Application.Current as App;
            Room = room;
            InitializeComponent();
            DataContext = this;
            ItemTypes = new ObservableCollection<ItemType>();
            AllItemTypes = new ItemType(-1, "Svi");
            ItemTypes.Add(AllItemTypes);

            var itemTypes = app.ItemTypeController.GetAll();
            foreach (var itemType in itemTypes)
            {
                ItemTypes.Add(itemType);
            }
            SelectedItemType = AllItemTypes;

            StoredItems = new ObservableCollection<StoredItem>();
            foreach (var storedItem in Room.StoredItems)
            {
                StoredItems.Add(storedItem);
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
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

                StoredItems.Clear();
                foreach(var storedItem in Room.StoredItems)
                {
                    StoredItems.Add(storedItem);
                }

                // Use the brush to paint the button's background.
                SearchTextBox.Background = textImageBrush;
            }
            else
            { 
                SearchTextBox.Background = null;
                var app = Application.Current as App;
                string searchText = SearchTextBox.Text;
                var searchedStoredItems = app.RoomController.FilterStoredItemsByName(Room.Id, searchText);
                StoredItems.Clear();
                foreach(var storedItem in searchedStoredItems)
                {
                    StoredItems.Add(storedItem);
                }
            }
        }

        private void AddItemToRoomIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Modal.Content = new AddItemToRoom(Room, roomItemsDataGrid);
            MainWindow.Modal.IsOpen = true;
        }

        private void RemoveItemFromRoomIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (roomItemsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Odaberi predmet!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MainWindow.Modal.Content = new RemoveItemFromRoomControl(Room, roomItemsDataGrid);
            MainWindow.Modal.IsOpen = true;
        }

        private void ScheduleItemTransferButton_Click(object sender, RoutedEventArgs e)
        {
            if (roomItemsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Odaberi predmet!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MainWindow.Modal.Content = new ScheduleItemTransferFromRoom(Room, roomItemsDataGrid);
            MainWindow.Modal.IsOpen = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(SelectedItemType.Id == AllItemTypes.Id)
            {
                StoredItems.Clear();
                foreach(var storedItem in Room.StoredItems)
                {
                    StoredItems.Add(storedItem);
                }
            }
            else
            {
                var app = Application.Current as App;

                var filteredStoredItems = app.RoomController.FilterStoredItemsByType(Room.Id, SelectedItemType);

                StoredItems.Clear();
                foreach(var storedItem in filteredStoredItems)
                {
                    StoredItems.Add(storedItem);
                }
            }
        }
    }
}
