﻿using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using zdravstvena_ustanova.View.Controls;
using zdravstvena_ustanova.View.Controls.RoomsCalendar;

namespace zdravstvena_ustanova.View.Pages.ManagerPages
{
    /// <summary>
    /// Interaction logic for RoomCalendarOverview.xaml
    /// </summary>
    public partial class RoomCalendarOverview : Page, INotifyPropertyChanged
    {
        #region NotifyProperties
        private ObservableCollection<ScheduledAppointment> _scheduledAppointments;
        public ObservableCollection<ScheduledAppointment> ScheduledAppointments
        {
            get
            {
                return _scheduledAppointments;
            }
            set
            {
                if (Equals(_scheduledAppointments, value)) return;

                _scheduledAppointments = value;

                OnPropertyChanged("ScheduledAppointments");
                
            }
        }
        private RenovationAppointment _renovationAppointment;
        public RenovationAppointment RenovationAppointment
        {
            get
            {
                return _renovationAppointment;
            }
            set
            {
                if (Equals(_renovationAppointment, value)) return;

                _renovationAppointment = value;

                OnPropertyChanged("RenovationAppointment");

            }
        }
        #endregion

        public Room Room { get; set; }

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

        public RoomCalendarOverview(long roomId)
        {
            var app = Application.Current as App;
            Room = app.RoomController.GetById(roomId);
            InitializeComponent();
            DataContext = this;
            CalendarContainer.Children.Add(new RoomsCalendarControl(roomId, this));
            infoPanel.Children.Add(new SelectedScheduledAppointmentsListControl(this));
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}