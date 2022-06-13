using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using zdravstvena_ustanova.Commands;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.View.Controls;
using zdravstvena_ustanova.View.ManagerMVVM.Model;
using zdravstvena_ustanova.View.Pages.ManagerPages;

namespace zdravstvena_ustanova.View.ManagerMVVM.ViewModel
{
    public class ManagerRoomsViewModel
    {
        #region fields
        public RoomsModel RoomsModel { get; set; }
        public NavigationService? NavigationService { get; set; }
        #endregion

        #region constructors
        public ManagerRoomsViewModel(NavigationService? navigationService)
        {
            NavigationService = navigationService;

            var app = Application.Current as App;

            RoomsModel = new RoomsModel(app.RoomController.GetAll());
            
            InventoryOverviewCommand =
                new RelayCommand(Execute_InventoryOverviewCommand, CanExecute_InventoryOverviewCommand);
            SearchBoxCommand = new RelayCommand(Execute_SearchBoxCommand, CanExecute_SearchBoxCommand);
            DeleteRoomCommand = new RelayCommand(Execute_DeleteRoomCommand, CanExecute_DeleteRoomCommand);
            EditRoomCommand = new RelayCommand(Execute_EditRoomCommand, CanExecute_EditRoomCommand);
            AddRoomCommand = new RelayCommand(Execute_AddRoomCommand, CanExecute_AddRoomCommand);
            RoomCalendarCommand = new RelayCommand(Execute_RoomCalendarCommand, CanExecute_RoomCalendarCommand);
        }
        #endregion

        #region commands
        public RelayCommand InventoryOverviewCommand { get; set; }
        private void Execute_InventoryOverviewCommand(object obj)
        {
            NavigationService.Navigate(new InventoryOverviewPage(RoomsModel.SelectedRoom));
        }
        private bool CanExecute_InventoryOverviewCommand(object obj)
        {
            return RoomsModel.SelectedRoom is not null;
        }

        public RelayCommand SearchBoxCommand { get; set; }
        private void Execute_SearchBoxCommand(object obj)
        {
            TextBox searchBox = obj as TextBox;
            
            if (searchBox.Text == "")
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
                searchBox.Background = textImageBrush;
            }
            else
            {

                searchBox.Background = null;
            }
        }
        private bool CanExecute_SearchBoxCommand(object obj)
        {
            return true;
        }

        public RelayCommand DeleteRoomCommand { get; set; }
        private void Execute_DeleteRoomCommand(object obj)
        {
            MainWindow.Modal.Content = new DeleteRoomControl(RoomsModel.Rooms, (DataGrid)obj);
            MainWindow.Modal.IsOpen = true;
        }
        private bool CanExecute_DeleteRoomCommand(object obj)
        {
            return RoomsModel.SelectedRoom is not null;
        }

        public RelayCommand EditRoomCommand { get; set; }
        private void Execute_EditRoomCommand(object obj)
        {
            MainWindow.Modal.Content = new EditRoomControl(RoomsModel.Rooms, (DataGrid)obj);
            MainWindow.Modal.IsOpen = true;
        }
        private bool CanExecute_EditRoomCommand(object obj)
        {
            return RoomsModel.SelectedRoom is not null;
        }

        public RelayCommand AddRoomCommand { get; set; }
        private void Execute_AddRoomCommand(object obj)
        {
            MainWindow.Modal.Content = new AddRoomControl(RoomsModel.Rooms);
            MainWindow.Modal.IsOpen = true;
        }
        private bool CanExecute_AddRoomCommand(object obj)
        {
            return true;
        }

        public RelayCommand RoomCalendarCommand { get; set; }
        private void Execute_RoomCalendarCommand(object obj)
        {
            var calendarIcon = (Image)obj;
            var dataContext = calendarIcon.DataContext;
            var dataSource = (Room)dataContext;
            long roomId = dataSource.Id;

            NavigationService.Navigate(new RoomCalendarOverview(roomId));
        }
        private bool CanExecute_RoomCalendarCommand(object obj)
        {
            return true;
        }
        #endregion
    }
}
