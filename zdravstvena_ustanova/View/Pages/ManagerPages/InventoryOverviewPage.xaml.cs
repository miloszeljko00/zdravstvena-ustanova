using Model;
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

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    /// <summary>
    /// Interaction logic for InventoryOverviewPage.xaml
    /// </summary>
    public partial class InventoryOverviewPage : Page, INotifyPropertyChanged
    {
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
            Room = room;
            InitializeComponent();
            DataContext = this;
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

                // Use the brush to paint the button's background.
                SearchTextBox.Background = textImageBrush;
            }
            else
            {

                SearchTextBox.Background = null;
            }
        }

        private void AddItemToRoomIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Modal.Content = new AddItemToRoom(Room, roomItemsDataGrid);
            MainWindow.Modal.IsOpen = true;
        }

        private void RemoveItemToRoomIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (roomItemsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Odaberi predmet!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MainWindow.Modal.Content = new RemoveItemFromRoomControl(Room, roomItemsDataGrid);
            MainWindow.Modal.IsOpen = true;
        }
    }
}
