using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    /// <summary>
    /// Interaction logic for RoomsOccupancyPage.xaml
    /// </summary>
    public partial class RoomsOccupancyPage : Page
    {
        public ObservableCollection<Room> Rooms { get; set; }
        public RoomsOccupancyPage()
        {
            InitializeComponent();
            DataContext = this;
            var app = Application.Current as App;
            Rooms = new ObservableCollection<Room>();
            var rooms = app.RoomController.GetAll();
            foreach (var room in rooms)
            {
                Rooms.Add(room);
            }
        }

        private void GenerateReportButton_OnClick(object sender, RoutedEventArgs e)
        {
            DateTime startDate = (DateTime)StartDatePicker.SelectedDate;
            DateTime endDate = (DateTime)EndDatePicker.SelectedDate;

            NavigationService.Navigate(new RoomGeneratedReportPage((Room)RoomsDataGrid.SelectedItem, startDate, endDate));
        }
    }
}
