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
using zdravstvena_ustanova.View.Controls.RoomsCalendar;

namespace zdravstvena_ustanova.View.Controls
{

    public partial class UnScheduleRenovation : UserControl, INotifyPropertyChanged
    {
        public RenovationAppointment SelectedRenovationAppointment { get; set; }
        public RoomsCalendarControl RoomsCalendarControl { get; set; }
        #region NotifyProperties
        private string _roomName;
        public string RoomName
        {
            get
            {
                return _roomName;
            }
            set
            {
                if (value != _roomName)
                {
                    _roomName = value;
                    OnPropertyChanged("RoomName");
                }
            }
        }
        private string _roomType;
        public string RoomType
        {
            get
            {
                return _roomType;
            }
            set
            {
                if (value != _roomType)
                {
                    _roomType = value;
                    OnPropertyChanged("RoomType");
                }
            }
        }
        private string _roomFloor;
        public string RoomFloor
        {
            get
            {
                return _roomFloor;
            }
            set
            {
                if (value != _roomFloor)
                {
                    _roomFloor = value;
                    OnPropertyChanged("RoomFloor");
                }
            }
        }
        private string _renovationStart;
        public string RenovationStart
        {
            get
            {
                return _renovationStart;
            }
            set
            {
                if (value != _renovationStart)
                {
                    _renovationStart = value;
                    OnPropertyChanged("RenovationStart");
                }
            }
        }
        private string _renovationEnd;
        public string RenovationEnd
        {
            get
            {
                return _renovationEnd;
            }
            set
            {
                if (value != _renovationEnd)
                {
                    _renovationEnd = value;
                    OnPropertyChanged("RenovationEnd");
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

        public UnScheduleRenovation(RenovationAppointment selectedRenovationAppointment, RoomsCalendarControl roomsCalendarControl)
        {
            InitializeComponent();
            DataContext = this;
            SelectedRenovationAppointment = selectedRenovationAppointment;
            RoomsCalendarControl = roomsCalendarControl;

            RoomName = SelectedRenovationAppointment.Room.Name;
            RoomType = SelectedRenovationAppointment.Room.RoomType.ToString();
            RoomFloor = SelectedRenovationAppointment.Room.Floor.ToString();
            RenovationStart = SelectedRenovationAppointment.StartDate.ToString("dd.MM.yyyy");
            RenovationEnd = SelectedRenovationAppointment.EndDate.ToString("dd.MM.yyyy");
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedRenovationAppointment != null)
            {
                var app = Application.Current as App;
                if(SelectedRenovationAppointment.RenovationType.Id == 1)
                {
                    var renovationAppointmentsWithSelectedRoomAsMergingRoom = (List<RenovationAppointment>)app.RenovationAppointmentController.
                                                                        GetRenovationAppointmentsByMergeRoomForMergeRenovation(SelectedRenovationAppointment.Room.Id); ;
                    if(renovationAppointmentsWithSelectedRoomAsMergingRoom != null && renovationAppointmentsWithSelectedRoomAsMergingRoom.Count > 0)
                    {
                        foreach(var renovation in renovationAppointmentsWithSelectedRoomAsMergingRoom)
                        {
                            app.RenovationAppointmentController.Delete(renovation.Id);
                        }
                    }
                }
                else if(SelectedRenovationAppointment.RenovationType.Id == 2)
                {
                    var room = SelectedRenovationAppointment.FirstRoom;
                    var renovationAppointmentOfMergedRoom = (List<RenovationAppointment>)app.RenovationAppointmentController.
                                                            GetIfContainsDateForRoom(SelectedRenovationAppointment.StartDate, room.Id);
                    
                    foreach(var renovationAppointment in renovationAppointmentOfMergedRoom)
                    {
                        app.RenovationAppointmentController.Delete(renovationAppointment.Id);
                    }

                }
                app.RenovationAppointmentController.Delete(SelectedRenovationAppointment.Id);
            }
            RoomsCalendarControl.DisplayCalendarForMonth(RoomsCalendarControl.DisplayedMonth);

            MainWindow.Modal.IsOpen = false;
            MainWindow.Modal.Content = null;
        }
    }
}
